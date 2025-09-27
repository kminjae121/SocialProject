using DG.Tweening;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject settingUI;
        [SerializeField] private float showTime = 0.5f;

        public void ClickButton()
        {
            settingUI.SetActive(true);
            settingUI.transform.localScale = Vector3.zero;
            settingUI.transform.DOScale(Vector3.one, showTime).SetEase(Ease.InOutQuad);
        }
    }
}