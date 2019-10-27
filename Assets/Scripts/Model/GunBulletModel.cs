using UnityEngine;

namespace Shooter_2D_test
{
    public class GunBulletModel : BaseBullet
    {
        public BulletType Type = BulletType.GunBullet;

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.transform.GetComponent<BaseBullet>())
            {
                return;
            }
            if (coll.transform.CompareTag("Enemy"))
            {
                var target = coll.gameObject.GetComponent<EnemyModel>();

                Main.EnemyController.SetDamage(target, new CollisionInfo(_baseDamage));
            }

            DestroyBullet();
        }
    }
}
