using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shooter_2D_test
{
    public class EnemyController : BaseController, IOnUpdate, IOnStart, ISetDamage
    {
        private Main Main;
        private float _timer;
        private float _startTimer = 2;

        #region UI
        private float _score;
        private string _allDead = $" ALL DEADDDD! ";
        private System.Action OnEnemySpawn;
        private System.Action OnEnemyDeath;
        #endregion

        public HashSet<EnemyModel> GetBotList { get; } = new HashSet<EnemyModel>();

        public void OnStart()
        {
            UiManager.TargetsLeftUi.Text = "";

            Main = ServiceLocator.GetService<Main>();
            Physics2D.queriesStartInColliders = false;

            _timer = _startTimer;
            OnEnemySpawn += RefreshBotCount;
            OnEnemyDeath += RefreshScore;
            //OnEnemyDeath -= RefreshScore;                                                  TODO
            //OnEnemySpawn -= RefreshBotCount;                                               TODO

        }

        public void OnUpdate()
        {
            DistanseToPlayer();
        }

        void RefreshBotCount()
        {
            if (GetBotList.Count > 0)
            {
                if (GetBotList.Count <= 10)
                {
                    UiManager.TargetsLeftUi.Color = Color.green;
                }
                if (GetBotList.Count > 10 && GetBotList.Count<= 20)
                {
                    UiManager.TargetsLeftUi.Color = Color.yellow;
                }
                if (GetBotList.Count > 20 && GetBotList.Count <= 40)
                {
                    UiManager.TargetsLeftUi.Color = Color.red;
                }
                UiManager.TargetsLeftUi.Text = $" Enemies on map : {GetBotList.Count}";
            }
            else
            {
                UiManager.TargetsLeftUi.Text = _allDead;
            }
        }

        void RefreshScore()
        {
            _score++;
            if (_score <= 10)
            {
                UiManager.ScoreUiUi.Color = Color.green;
            }
            if (_score > 30 && _score <= 50)
            {
                UiManager.ScoreUiUi.Color = Color.yellow;
            }
            if (_score > 50 && _score <= 100)
            {
                UiManager.ScoreUiUi.Color = Color.red;
            }
            UiManager.ScoreUiUi.Text = $" Score : {_score}";
        }

        public void DistanseToPlayer()
        {
            if (GetBotList.Count == 0) return;

            for (var i = 0; i < GetBotList.Count; i++)
            {
                var bot = GetBotList.ElementAt(i);
                bot.AmbushTimer -= Time.deltaTime;
                Tick(bot);
                bot.DistanseToPlayer = (bot.Transform.position - Main.PlayerModel.Transform.position).magnitude;
            }
        }

        private Vector2 GetRandVector()
        {
            return Random.insideUnitCircle * Random.Range(5, 10);
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
                GetBotList.Remove(bot);
                OnEnemySpawn?.Invoke();
                OnEnemyDeath?.Invoke();
            }
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

        public void Tick(EnemyModel bot)
        {
            var td = Time.deltaTime;

            if (bot.BotState == BotState.Died) return;

            if (bot.BotState != BotState.Detected)
            {
                if (bot.BotState != BotState.Ambushed)
                {
                    if (Vector2.Distance(bot.Point, bot.Transform.position) > 0.1f)
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

                                if (Vector2.Distance(bot.Point, bot.Transform.position) <= 1)
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
                var distance = Vector2.Distance(bot.Transform.position, Main.PlayerModel.Transform.position);

                if (bot.BotVision.VisionM(bot.Transform, Main.PlayerModel.Transform))
                {
                    bot.Point = Main.PlayerModel.Transform.position;
                    MovePoint(bot, bot.Point);
                }
                else
                {
                    if (distance <= 3)
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

        private void MovePoint(EnemyModel bot, Vector2 point)
        {
            bot.Transform.position = Vector2.MoveTowards(bot.Transform.position, point, bot.Speed * Time.deltaTime);
        }
        public void Follow()
        {
            var deltaTime = Time.deltaTime;

            for (var i = 0; i < GetBotList.Count; i++)
            {
                var bot = GetBotList.ElementAt(i);

                bot.Transform.position = Vector2.MoveTowards
                    (
                    bot.Transform.position,
                    Main.PlayerModel.Transform.position,
                    bot.Speed * deltaTime
                    );
            }
        }
    }
}