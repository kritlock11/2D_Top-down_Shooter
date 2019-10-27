using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter_2D_test
{
    public class SpawnerController : BaseController, IOnUpdate, IOnStart
    {
        private Main Main;
        private float _timer;
        private float _reTimer = 2;
        private Vector2[] _baraksSpawner = new Vector2[3];
        public Vector2[] BaraksSpawner { get => _baraksSpawner; set => _baraksSpawner = value; }

        public HashSet<SpawnerModel> GetBarakList { get; } = new HashSet<SpawnerModel>();

        public void OnStart()
        {
            _timer = _reTimer;
            Main = ServiceLocator.GetService<Main>();

            BaraksSpawner[0] = new Vector2(-7f, 3.4f);
            BaraksSpawner[1] = new Vector2(-7f, -3f);
            BaraksSpawner[2] = new Vector2(7f, -3f);

            BarakInst();
        }

        public void OnUpdate()
        {
            BotInst();
        }

        private void BarakInst()
        {
            for (var i = 0; i < _baraksSpawner.Length; i++)
            {
                var barak = Object.Instantiate
                    (
                    Main.BarakPrefab,
                    BaraksSpawner[i],
                    Quaternion.identity
                    );
                barak.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -9;
                AddBarakToList(barak);
            }
        }

        private void AddBarakToList(SpawnerModel barak)
        {
            if (!GetBarakList.Contains(barak))
            {
                GetBarakList.Add(barak);
            }
        }

        public void RemoveBarakFromList(SpawnerModel barak)
        {
            if (GetBarakList.Contains(barak))
            {
                GetBarakList.Remove(barak);
            }
        }

        private void BotInst()
        {
            if (_timer <= 0)
            {
                var randPos = Random.Range(0, BaraksSpawner.Length);
                var bot = Object.Instantiate(Main.EnemyModelPrefab, BaraksSpawner[randPos], Quaternion.identity);
                Main.EnemyController.AddBotToList(bot);
                _timer = _reTimer;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }
}
