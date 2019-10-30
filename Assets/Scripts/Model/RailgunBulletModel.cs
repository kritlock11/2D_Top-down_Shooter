using UnityEngine;

namespace Shooter_2D_test
{
    public class RailgunBulletModel : BaseBullet
    {

        public BulletType Type = BulletType.GunBullet;


        private void OnEnable()
        {
            DestroyBullet(0.4f);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.transform.GetComponent<BaseBullet>() || coll.transform.CompareTag("Player"))
            {
                return;
            }

            if (coll.transform.CompareTag("Enemy"))
            {
                var target = coll.gameObject.GetComponent<EnemyModel>();

                Main.EnemyController.SetDamage(target, new CollisionInfo(_baseDamage));
            }

            DestroyBullet(0.4f);
        }
    }
}
