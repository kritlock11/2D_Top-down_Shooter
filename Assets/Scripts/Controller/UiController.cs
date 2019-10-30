using System.Text;
using UnityEngine;

namespace Shooter_2D_test
{
    public class UiController : BaseController, IOnStart
    {
        private Main Main;

        private StringBuilder _targetsSb;
        private StringBuilder _scoreSB;
        private StringBuilder _weaponSB;
        private StringBuilder _ammoSB;

        private string _enemiesStr = "Enemies on map : ";
        private string _scoreStr = "Score : ";
        private string _allDead = $" ALL DEADDDD! ";

        public void OnStart()
        {
            Main = ServiceLocator.GetService<Main>();

            _targetsSb = new StringBuilder();
            _targetsSb.Append($"{_enemiesStr}");

            _scoreSB = new StringBuilder();
            _scoreSB.Append($"{_scoreStr}");

            _ammoSB = new StringBuilder();
            _weaponSB = new StringBuilder();

            UiManager.TargetsLeftUi.Text = _targetsSb.ToString();
            UiManager.ScoreUi.Text = _scoreSB.ToString();

            Main.EnemyController.OnEnemySpawn += RefreshBotCount;
            Main.EnemyController.OnEnemyDeath += RefreshScore;

            Main.WeaponController.OnOn += AimColor;
            Main.WeaponController.OnOn += WeaponUiTextOn;
            Main.WeaponController.OnOn += RefreshSelectedWeaponText;
            Main.WeaponController.OnOn += RefreshAmmoClip;

            Main.WeaponController.OnOff += WeaponUiTextOff;

            Main.WeaponController.OnFire += AimColor;
            Main.WeaponController.OnFire += RefreshAmmoClip;

            Main.WeaponController.OnReload += AimColor;
            Main.WeaponController.OnReload += RefreshAmmoClip;
        }

        void WeaponUiTextOn()
        {
            UiManager.WeaponUiText.SetActive(true);
            UiManager.SelectedWeaponUiText.SetActive(true);
        }

        void WeaponUiTextOff()
        {
            UiManager.WeaponUiText.SetActive(false);
            UiManager.SelectedWeaponUiText.SetActive(false);
        }

        void RefreshAmmoClip()
        {
            UiManager.WeaponUiText.Text = _ammoSB.TextClearBuilder(Main.WeaponController.SelectedWeapon.Clip.BulletsCount, Main.WeaponController.SelectedWeapon.CountClip);
        }

        void RefreshSelectedWeaponText()
        {
            UiManager.SelectedWeaponUiText.Text = _weaponSB.TextStringBuilder(Main.WeaponController.SelectedWeapon.gameObject.name);
        }

        private void AimColor()
        {
            var bulletsCount = Main.WeaponController.SelectedWeapon.Clip.BulletsCount;

            if (bulletsCount <= 5)
            {
                UiManager.WeaponUiText.Color = Color.red;
            }
            if (bulletsCount > 5 && bulletsCount <= 10)
            {
                UiManager.WeaponUiText.Color = Color.yellow;
            }
            if (bulletsCount > 10 && bulletsCount <= 40)
            {
                UiManager.WeaponUiText.Color = Color.green;
            }
        }

        public void RefreshBotCount()
        {
            var botCount = Main.EnemyController.GetBotList.Count;

            if (botCount > 0)
            {
                if (botCount <= 10)
                {
                    UiManager.TargetsLeftUi.Color = Color.green;
                }
                if (botCount > 10 && botCount <= 20)
                {
                    UiManager.TargetsLeftUi.Color = Color.yellow;
                }
                if (botCount > 20 && botCount <= 40)
                {
                    UiManager.TargetsLeftUi.Color = Color.red;
                }

                UiManager.TargetsLeftUi.Text = _targetsSb.TextReBuilder(_enemiesStr.Length, botCount);
            }
            else
            {
                UiManager.TargetsLeftUi.Text = _allDead;
            }
        }

        public void RefreshScore()
        {
            var score = Main.EnemyController.Score;

            if (score <= 10)
            {
                UiManager.ScoreUi.Color = Color.green;
            }
            if (score > 30 && score <= 50)
            {
                UiManager.ScoreUi.Color = Color.yellow;
            }
            if (score > 50 && score <= 100)
            {
                UiManager.ScoreUi.Color = Color.red;
            }

            UiManager.ScoreUi.Text = _scoreSB.TextReBuilder(_scoreStr.Length, score);
        }
    }
}
