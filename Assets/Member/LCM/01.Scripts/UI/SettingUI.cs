using DG.Tweening;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class SettingUI : MonoBehaviour
    {
        [SerializeField] private float closeTime = 0.5f;
        public void ClickQuitButton()
        {
            Application.Quit();
        }

        public void ClickCloseButton()
        {
            transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}