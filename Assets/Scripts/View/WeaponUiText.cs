using UnityEngine;
using TMPro;

namespace Shooter_2D_test
{
    public class WeaponUiText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public string Text
        {
            set => _text.text = $"{value}";
        }

        public Color Color
        {
            set => _text.color = value;
        }

        public void ShowData(int bulletCount, int clipCount)
        {
            _text.text = $"{bulletCount}/{clipCount}";
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }
    }
}
