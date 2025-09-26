using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Member.LCM._01.Scripts.UI
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private Image loadingImage;
        
        public void SetTimeAndStartLoading(float time)
        {
            loadingImage.DOFillAmount(1f, time);
        }
    }
}