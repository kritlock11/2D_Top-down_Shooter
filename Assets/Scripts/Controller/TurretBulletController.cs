using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shooter_2D_test
{
    public class TurretBulletController : BaseController, IOnStart, IOnUpdate
    {
        private Main Main;


        #region UI
        private float _score;
        private string _allDead = $" ALL DEADDDD! ";
        private System.Action OnEnemySpawn;
        private System.Action OnEnemyDeath;
        #endregion

        public HashSet<TurretBulletModel> GetTurretList { get; } = new HashSet<TurretBulletModel>();

        public void OnStart()
        {
            Main = ServiceLocator.GetService<Main>();
            Physics2D.queriesStartInColliders = false;
        }

        public void OnUpdate()
        {
            if (GetTurretList.Count == 0) return;

            for (var i = 0; i < GetTurretList.Count; i++)
            {
                var turret = GetTurretList.ElementAt(i);
                if (turret.TurretOn)
                {
                    TurretUpdate(turret);
                }

                turret.Timer -= Time.deltaTime;
            }
        }

        public void TurretUpdate(TurretBulletModel turret)
        {
            if (turret.Timer <= 0)
            {
                var col = Physics2D.OverlapCircleAll(turret.Transform.position, 3)
                    .Where(w => w.CompareTag("Enemy"))
                    .OrderBy(o => o.GetComponent<EnemyModel>().DistanseToPlayer)
                    .Take(1)
                    .ToArray();

                if (col.Length == 0) return;
                var turretTarget = col[0];

                var vector = FindVectorRotation(turret.transform, turretTarget.transform);
                if (vector == Vector2.zero) return;

                var weapon = turret.transform.GetComponentInChildren<BaseWeapon>();

                Debug.DrawLine(turret.transform.position, turretTarget.transform.position, Color.yellow);

                weapon.Fire(vector);
                turret.Timer = turret.StartTimer;
            }
        }

        public Vector2 FindVectorRotation(Transform from, Transform to)
        {
            var hit = Physics2D.Linecast(from.position, to.position);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    var vector = (to.transform.position - from.transform.position).normalized;
                    return vector;
                }
            }

            return Vector2.zero;
        }

        public void AddBotToList(TurretBulletModel turret)
        {
            if (!GetTurretList.Contains(turret))
            {
                GetTurretList.Add(turret);
            }
        }

        public void RemoveBotFromList(TurretBulletModel turret)
        {
            if (GetTurretList.Contains(turret))
            {
                GetTurretList.Remove(turret);
            }
        }
    }
}