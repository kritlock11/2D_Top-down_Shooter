using System.Linq;
using UnityEngine;

namespace Shooter_2D_test
{
    public class TurretBulletModel : BaseBullet
    {
        public BulletType Type = BulletType.GunBullet;

        private float _timer;
        private float _startTimer = 0.5f;
        private bool _turretOn;

        public float Timer { get => _timer; set => _timer = value; }
        public float StartTimer { get => _startTimer; set => _startTimer = value; }
        public bool TurretOn { get => _turretOn; set => _turretOn = value; }

        private void OnEnable()
        {
            Main.TurretBulletController.AddBotToList(this);
            _timer = _startTimer;
            StopInv();
        }

        private void StopInv()
        {
            Invoke(nameof(Stop), 0.5f);
        }


        void Stop()
        {
            Rigidbody.velocity = Vector2.zero;
            TurretOn = true;
        }

    }
}
