using UnityEngine;

namespace Shooter_2D_test
{
    public class UiManager
    {
        private WeaponUiText _weaponUiText;
        public WeaponUiText WeaponUiText
        {
            get
            {
                if (!_weaponUiText)
                    _weaponUiText = Object.FindObjectOfType<WeaponUiText>();
                return _weaponUiText;
            }
        }

        private SelectedWeaponUiImg _selectedWeaponUiImg;
        public SelectedWeaponUiImg SelectedWeaponUiImg
        {
            get
            {
                if (!_selectedWeaponUiImg)
                    _selectedWeaponUiImg = Object.FindObjectOfType<SelectedWeaponUiImg>();
                return _selectedWeaponUiImg;
            }
        }

        private SelectedWeaponUiText _selectedWeaponUiText;
        public SelectedWeaponUiText SelectedWeaponUiText
        {
            get
            {
                if (!_selectedWeaponUiText)
                    _selectedWeaponUiText = Object.FindObjectOfType<SelectedWeaponUiText>();
                return _selectedWeaponUiText;
            }
        }

        private TargetsLeftUi _targetsLeftUi;
        public TargetsLeftUi TargetsLeftUi
        {
            get
            {
                if (!_targetsLeftUi)
                    _targetsLeftUi = Object.FindObjectOfType<TargetsLeftUi>();
                return _targetsLeftUi;
            }
        }

        private ScoreUi _scoreUiUi;
        public ScoreUi ScoreUiUi
        {
            get
            {
                if (!_scoreUiUi)
                    _scoreUiUi = Object.FindObjectOfType<ScoreUi>();
                return _scoreUiUi;
            }
        }

        private ComboUi _comboUi;
        public ComboUi ComboUi
        {
            get
            {
                if (!_comboUi)
                    _comboUi = Object.FindObjectOfType<ComboUi>();
                return _comboUi;
            }
        }
    }
}
