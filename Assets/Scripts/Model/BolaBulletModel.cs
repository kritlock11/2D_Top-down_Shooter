using UnityEngine;

namespace Shooter_2D_test
{
    public class BolaBulletModel : BaseBullet
    {
        public BulletType Type = BulletType.Bola;
        private float _boomTimer = 2;

        private void OnEnable()
        {
            BoomInv();
        }

        private void BoomInv()
        {
            Invoke(nameof(Boom), _boomTimer);
        }

        private void Boom()
        {
            var col = Physics2D.OverlapCircleAll(Transform.position, 3);
            if (col.Length == 0) return;

            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].CompareTag("Enemy"))
                {
                    var vector = (transform.position - col[i].transform.position).normalized;
                    col[i].gameObject.GetComponent<Rigidbody2D>().AddForce(vector * 30, ForceMode2D.Impulse);
                }

                DestroyBullet();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 3);
        }
    }
}
