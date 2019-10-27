using UnityEngine;

namespace Shooter_2D_test
{
    public class CamShakeController : IOnStart
    {
        public Animator camAnim;

        public readonly int Shake = Animator.StringToHash("Shake");
        public readonly int Shake1 = Animator.StringToHash("Shake1");
        public readonly int Shake2 = Animator.StringToHash("Shake2");

        private int[] _shakes = new int[3];

        public void CamScreenShake()
        {
            var r = _shakes[Random.Range(0, _shakes.Length)];
            camAnim.SetTrigger(r);
        }

        public void OnStart()
        {
            camAnim = Camera.main.GetComponent<Animator>();

            _shakes[0] = Shake;
            _shakes[1] = Shake1;
            _shakes[2] = Shake2;

        }
    }
}

