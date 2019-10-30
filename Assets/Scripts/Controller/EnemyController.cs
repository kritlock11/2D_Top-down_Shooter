using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shooter_2D_test
{
    public class EnemyController : BaseController, IOnUpdate, IOnStart, ISetDamage
    {
        private Main Main;
        private float _score;
        private float _timer;
        private float _startTimer = 2;
        public event System.Action OnEnemySpawn;
        public event System.Action OnEnemyDeath;

        public HashSet<EnemyModel> GetBotList { get; } = new HashSet<EnemyModel>();
        public float Score { get => _score; private set => _score = value; }

        public void OnStart()
        {
            Main = ServiceLocator.GetService<Main>();
            Physics2D.queriesStartInColliders = false;

            _timer = _startTimer;
        }

        public void OnUpdate()
        {
            BotActions();
        }

        public void BotActions()
        {
            if (GetBotList.Count == 0) return;

            for (var i = 0; i < GetBotList.Count; i++)
            {
                var bot = GetBotList.ElementAt(i);
                bot.AmbushTimer -= Time.deltaTime;
                Tick(bot);
                bot.DistanseToPlayer = (bot.Transform.position - Main.PlayerModel.Transform.position).sqrMagnitude;
            }
        }

        public void Tick(EnemyModel bot)
        {
            var td = Time.deltaTime;

            if (bot.BotState == BotState.Died) return;

            if (bot.BotState != BotState.Detected)
            {
                if (bot.BotState != BotState.Ambushed)
                {
                    if ((bot.Point - (Vector2)bot.Transform.position).sqrMagnitude > 0.1f) //TODO

                    {
                        if (bot.BotState != BotState.Inspection)
                        {
                            if (bot.BotState != BotState.Patrol)
                            {
                                bot.BotState = BotState.Patrol;
                                bot.Point = GetRandVector();

                            }
                            else
                            {
                                MovePoint(bot, bot.Point);

                                var col = Physics2D.OverlapCircleAll(bot.Transform.position, 1);

                                if (col.Length == 0)
                                {
                                    return;
                                }
                                else if (bot.AmbushTimer <= 0)
                                {
                                    for (int i = 0; i < col.Length; i++)
                                    {
                                        if (col[i].CompareTag("Player"))
                                        {
                                            bot.BotState = BotState.Ambushed;
                                            bot.BotStateSwitch(BotState.Patrol, 3);
                                            bot.AmbushTimer = bot.StartAmbushTimer;
                                        }
                                    }
                                }

                                if ((bot.Point - (Vector2)bot.Transform.position).sqrMagnitude <= 1) //TODO
                                {
                                    bot.BotState = BotState.Inspection;
                                    bot.BotStateSwitch(BotState.Non, 1);
                                }
                            }
                        }
                    }
                }

                if (bot.BotVision.VisionM(bot.Transform, Main.PlayerModel.Transform) &&
                    bot.BotState != BotState.Ambushed)
                {
                    bot.BotState = BotState.Detected;
                }
            }
            else
            {
                var distance = (bot.Transform.position - Main.PlayerModel.Transform.position).sqrMagnitude;

                if (bot.BotVision.VisionM(bot.Transform, Main.PlayerModel.Transform))
                {
                    bot.Point = Main.PlayerModel.Transform.position;
                    MovePoint(bot, bot.Point);
                }
                else
                {
                    if (distance <= 3 * 3)
                    {
                        bot.Point = Main.PlayerModel.Transform.position;
                        MovePoint(bot, bot.Point);
                        _timer = _startTimer;
                    }
                    else
                    {
                        bot.Point = Main.PlayerModel.Transform.position;
                        MovePoint(bot, bot.Point);
                        _timer -= td;
                        if (_timer <= 0)
                        {
                            bot.BotState = BotState.Non;
                            _timer = _startTimer;
                        }
                    }
                }

                //if (bot.BotVision.VisionM(bot.Transform, Main.PlayerModel.Transform))
                //{
                //    //TODO остановиться 
                //    BaseWeapon.Fire();
                //}
            }
        }

        public void AddBotToList(EnemyModel bot)
        {
            if (!GetBotList.Contains(bot))
            {
                GetBotList.Add(bot);
                OnEnemySpawn?.Invoke();
            }
        }

        public void RemoveBotFromList(EnemyModel bot)
        {
            if (GetBotList.Contains(bot))
            {
                _score++;

                GetBotList.Remove(bot);
                OnEnemySpawn?.Invoke();
                OnEnemyDeath?.Invoke();
            }
        }

        private void MovePoint(EnemyModel bot, Vector2 point)
        {
            bot.Transform.position = Vector2.MoveTowards(bot.Transform.position, point, bot.Speed * Time.deltaTime);
        }

        public void SetDamage(EnemyModel bot, CollisionInfo info)
        {
            if (bot.BotState == BotState.Died) return;

            else if (bot.Hp > 0)
            {
                bot.Hp -= info.Damage;
            }

            if (bot.Hp <= 0)
            {
                bot.BotState = BotState.Died;
                RemoveBotFromList(bot);

                Object.Instantiate(Main.EffectPrefab, bot.transform.position, Quaternion.identity);

                Main.CamShakeController.CamScreenShake();

                Object.Destroy(bot.gameObject);
            }
        }

        private Vector2 GetRandVector()
        {
            return Random.insideUnitCircle * Random.Range(5, 10);
        }
    }
}