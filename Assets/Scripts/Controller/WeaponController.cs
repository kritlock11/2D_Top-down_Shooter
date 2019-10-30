using System;
namespace Shooter_2D_test
{
    public class WeaponController: BaseController
    {
        private BaseWeapon _selectedWeapon;

        public BaseWeapon SelectedWeapon { get => _selectedWeapon; set => _selectedWeapon = value; }

        public event Action OnOn;
        public event Action OnOff;
        public event Action OnFire;
        public event Action OnReload;

        public void On(BaseObjectScene weapon)
        {
            _selectedWeapon = (BaseWeapon)weapon;
            _selectedWeapon.IsVisible = true;

            OnOn?.Invoke();
        }

        public void Off()
        {
            if (_selectedWeapon == null) return;

            _selectedWeapon.IsVisible = false;
            _selectedWeapon = null;

            OnOff?.Invoke();
        }

        public void ReloadClip()
        {
            if (_selectedWeapon == null) return;

            _selectedWeapon.ReloadClip();

            OnReload?.Invoke();
        }

        public void Fire()
        {
            if (_selectedWeapon == null) return;

            _selectedWeapon.Fire();

            OnFire?.Invoke();
        }
    }
}
