namespace Shooter_2D_test
{
    public class Inventory : IOnStart
    {
        private Main Main;
        private BaseWeapon[] _weapons = new BaseWeapon[5];
        public BaseWeapon[] Weapons { get => _weapons; set => _weapons = value; }

        public void OnStart()
        {
            Main = ServiceLocator.GetService<Main>();
            _weapons = Main.PlayerModel.Transform.GetComponentsInChildren<BaseWeapon>();

            foreach (var weapon in Weapons)
            {
                weapon.IsVisible = false;
            }
        }

        public void GetWeapon(int i)
        {
            Main.WeaponController.Off();

            if (i <= _weapons.Length - 1)
            {
                Main.WeaponController.On(_weapons[i]);
            }
        }
    }
}
