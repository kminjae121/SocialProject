using DG.Tweening;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class BuildingUI : MonoBehaviour
    {
        [SerializeField] private RectTransform buttonTrm;
        [SerializeField] private RectTransform buildingPanelTrm;
        
        [SerializeField] private float upDownTime = 0.6f;
        
        [SerializeField] private BuildingIconUI buildingIconUI;
        [SerializeField] private Transform contentTrm;
        
        [SerializeField] private ObjectDataSO objectData;
        
        private bool _isShow;

        private void Start()
        {
            foreach (var building in objectData.objectData)
            {
                Instantiate(buildingIconUI, contentTrm).Initialize(building.thisImage, building.Name);
            }
        }

        public void ClickShowButton()
        {
            if (_isShow)
            {
                buttonTrm.DORotate(Vector3.zero, upDownTime, RotateMode.Fast);
                
                buttonTrm.DOAnchorPosY(-490f, upDownTime);
                buildingPanelTrm.DOAnchorPosY(-650f, upDownTime);
            }
            else
            {
                buttonTrm.DORotate(new Vector3(0,0,-180f), upDownTime, RotateMode.Fast);
                
                buttonTrm.DOAnchorPosY(-270f, upDownTime);
                buildingPanelTrm.DOAnchorPosY(-430f, upDownTime);
            }
            _isShow = !_isShow;
        }
    }
}