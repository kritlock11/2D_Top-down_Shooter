using UnityEngine;
using UnityEngine.UI;

namespace Shooter_2D_test
{
    public class SelectedWeaponUiImg : MonoBehaviour
    {
        private Image _img;

        public Sprite Img { get => _img.sprite; set => _img.sprite = value; }

        private void Start()
        {
            _img = GetComponent<Image>();
        }

        public void SetActive(bool value)
        {
            _img.gameObject.SetActive(value);
        }
    }
}
