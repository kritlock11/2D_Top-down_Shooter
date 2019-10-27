using UnityEngine;

namespace Shooter_2D_test
{
    public class BaseBullet : BaseObjectScene
    {
        [SerializeField] protected float _baseDamage = 10;

        protected Main Main;

        protected override void Awake()
        {
            base.Awake();
            Main = ServiceLocator.GetService<Main>();
        }

        protected void DestroyBullet(float time = 0)
        {
            Invoke(nameof(SetActiveFalse), time);
        }

        protected void SetActiveFalse()
        {
            gameObject.SetActive(false);
        }
    }
}
