using UnityEngine;
using UnityEngine.UI;

namespace Shooter_2D_test
{
    public class PanelMoveBehaviour : MonoBehaviour
    {
        [SerializeField] private Button hidePanel;

        private void OnEnable()
        {
            hidePanel.onClick.AddListener(HidePanel_OnClick);
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            hidePanel.onClick.RemoveListener(HidePanel_OnClick);
        }

        private void HidePanel_OnClick()
        {
            transform.gameObject.SetActive(false);
        }
    }
}

