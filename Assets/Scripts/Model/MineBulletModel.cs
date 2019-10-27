using UnityEngine;

namespace Shooter_2D_test
{
    public class MineBulletModel : BaseBullet
    {
        public BulletType Type = BulletType.Mine;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")
                || collision.GetComponent<BaseBullet>())
            {
                return;
            }
            var col = Physics2D.OverlapCircleAll(Transform.position, 1);
            if (col.Length == 0) return;

            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].CompareTag("Enemy"))
                {
                    var target = col[i].gameObject.GetComponent<EnemyModel>();
                    Main.EnemyController.SetDamage(target, new CollisionInfo(_baseDamage));
                }
                DestroyBullet();
            }
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}
