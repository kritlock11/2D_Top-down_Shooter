using UnityEngine;
using TMPro;

namespace Shooter_2D_test
{
    public class TargetsLeftUi : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
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

        public int Size
        {
            set => _text.fontSize = value;
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }
    }
}
