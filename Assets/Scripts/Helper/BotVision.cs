using UnityEngine;

namespace Shooter_2D_test
{
    [System.Serializable]
    public class BotVision
    {
        public float ActiveDis = 3f;
        public float ActiveAng = 60;

        public bool VisionM(Transform bot, Transform target)
        {
            return Dist(bot, target) && Angle(bot, target) && CheckBloked(bot, target);
        }

        private bool CheckBloked(Transform bot, Transform target)
        {
            var hit = Physics2D.Linecast(bot.position, target.position, target.gameObject.layer);

            if (hit.collider == null)
            {
                Debug.DrawLine(bot.position, target.position, Color.yellow);
                return true;
            }
            return false;
        }

        private bool Angle(Transform bot, Transform target)
        {
            var angle = Vector2.Angle(bot.GetComponent<EnemyModel>().Point, target.position - bot.position);
            return angle <= ActiveAng;
        }

        private bool Dist(Transform bot, Transform target)
        {
            var dist = Vector2.Distance(bot.position, target.position);
            return dist <= ActiveDis;
        }
    }
}
