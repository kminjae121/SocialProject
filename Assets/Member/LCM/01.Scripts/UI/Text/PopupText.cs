using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI.Text
{
    public class PopupText : MonoBehaviour
    {
        [Header("Popup Setting")] 
        
        [SerializeField] private TextMeshProUGUI popupText;
        [SerializeField] private float popupDuration;

        public void Initialize(Transform startTransform, Transform endTransform, string text, Color color)
        {
            popupText.SetText(text);
            popupText.color = color;
            transform.position = startTransform.position;
            
            transform.DOMoveY(endTransform.position.y, popupDuration)
                .OnComplete(() => {
                    Destroy(gameObject);
                });
        }
    }
}
