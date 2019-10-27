using UnityEngine;

namespace Shooter_2D_test
{
    public class UpdateController : MonoBehaviour
    {
        [HideInInspector] public Main Main;

        private void Awake()
        {
            Main = ServiceLocator.GetService<Main>();
        }

        private void Update()
        {
            for (var i = 0; i < Main.Updates.Count; i++)
            {
                Main.Updates[i].OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < Main.FixedUpdates.Count; i++)
            {
                Main.FixedUpdates[i].OnFixedUpdate();
            }
        }
    }
}
