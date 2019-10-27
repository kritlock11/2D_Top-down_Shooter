using UnityEngine;

namespace Shooter_2D_test
{
    public class ShortGunModel : BaseWeapon
    {
        private float spredBulletsCount = 5;

        protected override void Awake()
        {
            base.Awake();
            _weaponType = WeaponType.ShortGun;
        }

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.BulletsCount <= 0) return;
            if (!Bullet) return;

            for (int i = 0; i < spredBulletsCount; i++)
            {
                var shootVector = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

                float angle = Mathf.Atan2(shootVector.y, shootVector.x) * Mathf.Rad2Deg;
                float spread = Random.Range(-10, 10);
                Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));
                var bullet = Instantiate(Bullet, _barrel.position, bulletRotation);
                bullet.Layer = 10;
                bullet.Rigidbody.AddForce(bullet.transform.right  * _force);
            }

            Clip.BulletsCount--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechergeTime);
        }
    }
}
