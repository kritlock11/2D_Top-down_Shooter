using UnityEngine;

namespace Shooter_2D_test
{
    public class InputController : IOnUpdate
    {
        Main Main = ServiceLocator.GetService<Main>();

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Main.WeaponController.Fire();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Main.Inventory.GetWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Main.Inventory.GetWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Main.Inventory.GetWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Main.Inventory.GetWeapon(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Main.Inventory.GetWeapon(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Main.Inventory.GetWeapon(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Main.Inventory.GetWeapon(6);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Main.PlayerController.BlackDeath();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Main.PlayerController.Kick();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Main.WeaponController.ReloadClip();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Main.PlayerController.Dash();
            }
            if (Input.GetKey(KeyCode.Tab))
            {
                Main.ControlsPanelSwitch(true);
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                Main.ControlsPanelSwitch(false);
            }
        }
    }
}
