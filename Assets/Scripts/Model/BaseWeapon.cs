using System.Collections.Generic;
using UnityEngine;

namespace Shooter_2D_test
{
    public abstract class BaseWeapon : BaseObjectScene
    {
        public Clip Clip;
        private int _countClip = 5;
        [SerializeField] protected BaseBullet Bullet;
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 999;
        [SerializeField] protected float _rechergeTime = 0.2f;
        [SerializeField] protected WeaponType _weaponType;
        
        private int _maxBulletsCount = 40;
        private int _minBulletsCount = 20;
        public int CountClip => _clips.Count;
        protected bool _isReady = true;

        private Queue<Clip> _clips = new Queue<Clip>();
        protected BulletsPool bulletsPool;

        protected virtual void Start()
        {
            switch (_weaponType)
            {
                case WeaponType.Gun:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_gun");
                    break;

                case WeaponType.ShortGun:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_gun");
                    break;

                case WeaponType.GranadeGun:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_granade");
                    break;

                case WeaponType.MineGun:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_mine");
                    break;

                case WeaponType.BolaGun:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_bola");
                    break;

                case WeaponType.TurrtGun:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_turret");
                    break;

                case WeaponType.GunOnTurret:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_gun");
                    break;

                case WeaponType.Railgun:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_railgun");
                    break;

                default:
                    Bullet = Resources.Load<BaseBullet>("AmmoPrefabs/Bullet_gun");
                    break;
            }

            bulletsPool = new BulletsPool(Bullet, 10);

            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { BulletsCount = Random.Range(_minBulletsCount, _maxBulletsCount) });
            }

            ReloadClip();
        }

        public virtual void Fire() { Fire(Vector2.zero); }
        public virtual void Fire(Vector2 v2) { }
        protected void ReadyShoot() => _isReady = true;
        protected void AddClip(Clip clip) => _clips.Enqueue(clip);

        public void ReloadClip()
        {
            if (CountClip <= 0) return;
            Clip = _clips.Dequeue();
        }
    }
}
