using DG.Tweening;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class BuildingUI : MonoBehaviour
    {
        [SerializeField] private RectTransform buttonTrm;
        [SerializeField] private RectTransform buildingPanelTrm;

        [SerializeField] private float upDonwTime = 0.6f;
        
        private bool _isShow;
        
        public void ClickShowButton()
        {
            if (_isShow)
            {
                buttonTrm.DORotate(Vector3.zero, upDonwTime, RotateMode.Fast);
                
                buttonTrm.DOAnchorPosY(-490f, upDonwTime);
                buildingPanelTrm.DOAnchorPosY(-650f, upDonwTime);
            }
            else
            {
                buttonTrm.DORotate(new Vector3(0,0,-180f), upDonwTime, RotateMode.Fast);
                
                buttonTrm.DOAnchorPosY(-270f, upDonwTime);
                buildingPanelTrm.DOAnchorPosY(-430f, upDonwTime);
            }
            _isShow = !_isShow;
        }
    }
}