using System.Linq;
using UnityEngine;

namespace Shooter_2D_test
{
    public class PlayerController : BaseController, IOnStart, IOnUpdate, IOnFixedUpdate
    {
        private Main Main;

        public void OnStart()
        {
            Main = ServiceLocator.GetService<Main>();
            Physics2D.queriesStartInColliders = false;
        }

        public void OnUpdate()
        {
            Main.PlayerModel.X = Input.GetAxis("Horizontal");
            Main.PlayerModel.Y = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(Main.PlayerModel.X, Main.PlayerModel.Y);
            Main.PlayerModel.MousePos = Main.MainCamera.ScreenToWorldPoint(Input.mousePosition);

            Moving(dir);
        }

        public void OnFixedUpdate()
        {
            var lookDir = Main.PlayerModel.MousePos - Main.PlayerModel.Rigidbody.position;
            var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            Main.PlayerModel.Rigidbody.rotation = angle;
        }

        private void Moving(Vector2 dir)
        {
            Main.PlayerModel.Rigidbody.velocity = new Vector2(dir.x * Main.PlayerModel.Speed, dir.y * Main.PlayerModel.Speed);
        }

        public void BlackDeath()
        {
            var col = Physics2D.OverlapCircleAll(Main.PlayerModel.Transform.position, 1)
                .Where(w => w.CompareTag("Enemy"))
                .Where(b => b.GetComponent<EnemyModel>().BotState == BotState.Ambushed)
                .OrderBy(o => o.GetComponent<EnemyModel>().DistanseToPlayer)
                .ToArray();

            for (int i = 0; i < col.Length; i++)
            {
                var target = col[i].GetComponent<EnemyModel>();

                if (target.Hp > 0)
                {
                    Main.EnemyController.SetDamage(target, new CollisionInfo(target.MaxHp));
                }

                Main.PlayerModel.Transform.position = col[i].transform.position;

                if (i == col.Length - 1)
                {
                    Main.InvokeSometh($"BlackDeath", 0.2f);
                }
            }
        }

        public void Kick()
        {
            var col = Physics2D.OverlapCircleAll(Main.PlayerModel.Transform.position, 1)
                .Where(w => w.CompareTag("Enemy"))
                .OrderBy(o => o.GetComponent<EnemyModel>().DistanseToPlayer)
                .Take(1)
                .ToArray();

            for (int i = 0; i < col.Length; i++)
            {
                var target = col[i].GetComponent<EnemyModel>();
                var vector = (col[i].transform.position - Main.PlayerModel.Transform.position).normalized;
                col[i].gameObject.GetComponent<Rigidbody2D>().AddForce(vector * 100, ForceMode2D.Impulse);
                target.Kicked = true;
            }
        }

        public void Dash()
        {
            Main.PlayerModel.Transform.position = Vector2.MoveTowards(Main.PlayerModel.Transform.position, Main.PlayerModel.MousePos, 2);
        }
    }
}
