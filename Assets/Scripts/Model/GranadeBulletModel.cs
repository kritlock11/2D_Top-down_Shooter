using UnityEngine;

namespace Shooter_2D_test
{
    public class GranadeBulletModel : BaseBullet
    {
        public BulletType Type = BulletType.Granade;
        private float _boomTimer = 2;

        private void OnEnable()
        {
            BoomInv();
        }

        private void BoomInv()
        {
            Invoke(nameof(Boom), _boomTimer);
        }

        private void Boom()
        {
            var col = Physics2D.OverlapCircleAll(Transform.position, 2);
            if (col.Length == 0) return;

            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].CompareTag("Enemy"))
                {
                    var target = col[i].gameObject.GetComponent<EnemyModel>();
                    Main.EnemyController.SetDamage(target, new CollisionInfo(_baseDamage));
                    target.BotState = BotState.Died;
                }
                DestroyBullet();
            }
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 2);
        }
    }
}
