using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility.ObjectPool.Runtime;

namespace Member.LCM._01.Scripts.UI.Text
{
    public class PopupText : MonoBehaviour, IPoolable
    {
        [Header("Pool")]
        [field: SerializeField] public PoolingItemSO PoolingType { get; private set; }
        public GameObject GameObject => gameObject;
        
        private Pool _myPool;

        [Header("Popup Setting")] 
        
        [SerializeField] private TextMeshProUGUI popupText;
        [SerializeField] private float popupDuration;

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void Initialize(Transform startTransform, Transform endTransform, string text, Color color)
        {
            popupText.SetText(text);
            popupText.color = color;
            transform.position = startTransform.position;
            
            transform.DOMoveY(endTransform.position.y, popupDuration)
                .OnComplete(() => {
                    _myPool.Push(this);
                });
        }

        public void ResetItem()
        {
            transform.position = Vector3.zero;
        }
    }
}
