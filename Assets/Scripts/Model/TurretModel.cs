using UnityEngine;

namespace Shooter_2D_test
{
    public class TurretModel : BaseWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            _weaponType = WeaponType.GunOnTurret;
        }

        public override void Fire(Vector2 v2)
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
                    bulletsPool.bullets[i].Rigidbody.AddForce(v2* _force);
                    break;
                }
            }

            Clip.BulletsCount--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechergeTime);
        }
    }
}
