using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.LCM._01.Scripts.UI
{
    public class BuildingIconUI : MonoBehaviour
    {
        [SerializeField] private Image buildingIcon;
        [SerializeField] private TextMeshProUGUI buildingNameText;
        
        public void Initialize(Sprite icon, string name)
        {
            buildingIcon.sprite = icon;
            buildingNameText.SetText(name);
        }
    }
}