namespace Shooter_2D_test
{
    public class WeaponController: BaseController
    {
        private BaseWeapon SelectedWeapon;
        public void On(BaseObjectScene weapon)
        {
            SelectedWeapon = (BaseWeapon)weapon;
            SelectedWeapon.IsVisible = true;

            UiManager.WeaponUiText.SetActive(true);
            UiManager.SelectedWeaponUiText.SetActive(true);
            UiManager.WeaponUiText.ShowData(SelectedWeapon.Clip.BulletsCount, SelectedWeapon.CountClip);
            AimColor();
            UiManager.SelectedWeaponUiText.Text = SelectedWeapon.gameObject.name;
        }

        public void Off()
        {
            if (SelectedWeapon == null) return;

            SelectedWeapon.IsVisible = false;
            SelectedWeapon = null;

            UiManager.WeaponUiText.SetActive(false);
            UiManager.SelectedWeaponUiText.SetActive(false);
        }

        public void ReloadClip()
        {
            if (SelectedWeapon == null) return;

            SelectedWeapon.ReloadClip();

            UiManager.WeaponUiText.ShowData(SelectedWeapon.Clip.BulletsCount, SelectedWeapon.CountClip);
            AimColor();
        }

        public void Fire()
        {
            if (SelectedWeapon == null) return;

            SelectedWeapon.Fire();

            UiManager.WeaponUiText.ShowData(SelectedWeapon.Clip.BulletsCount, SelectedWeapon.CountClip);
            AimColor();
        }

        private void AimColor()
        {
            if (SelectedWeapon.Clip.BulletsCount <= 5)
            {
                UiManager.WeaponUiText.Color = UnityEngine.Color.red;
            }
            if (SelectedWeapon.Clip.BulletsCount > 5 && SelectedWeapon.Clip.BulletsCount <= 10)
            {
                UiManager.WeaponUiText.Color = UnityEngine.Color.yellow;
            }
            if (SelectedWeapon.Clip.BulletsCount > 10 && SelectedWeapon.Clip.BulletsCount <= 40)
            {
                UiManager.WeaponUiText.Color = UnityEngine.Color.green;
            }
        }
    }
}
