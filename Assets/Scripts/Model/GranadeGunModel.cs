using UnityEngine;

namespace Shooter_2D_test
{
    public class GranadeGunModel : BaseWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            _weaponType = WeaponType.GranadeGun;
        }

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.BulletsCount <= 0) return;
            if (!Bullet) return;

            for (int i = 0; i < bulletsPool.bullets.Count; i++)
            {
                if (!bulletsPool.bullets[i].gameObject.activeInHierarchy)
                {
                    bulletsPool.bullets[i].transform.position = _barrel.transform.position;
                    bulletsPool.bullets[i].transform.rotation = _barrel.transform.rotation;
                    bulletsPool.bullets[i].gameObject.SetActive(true);
                    var shootVector = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                    bulletsPool.bullets[i].Rigidbody.AddForce(shootVector * _force);
                    break;
                }
            }

            Clip.BulletsCount--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechergeTime);
        }
    }
}
