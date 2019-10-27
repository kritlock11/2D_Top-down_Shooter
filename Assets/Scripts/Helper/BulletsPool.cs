using System.Collections.Generic;
using UnityEngine;

namespace Shooter_2D_test
{
    public class BulletsPool
    {
        private readonly int _poolSize;
        public List<BaseBullet> bullets;

        public BulletsPool(BaseBullet baseBullet, int poolSize)
        {
            _poolSize = poolSize;
            bullets = new List<BaseBullet>();

            for (var i = 0; i < _poolSize; i++)
            {
                var bullet = Object.Instantiate(baseBullet);
                bullet.gameObject.SetActive(false);
                bullets.Add(bullet);
            }
        }
    }
}
