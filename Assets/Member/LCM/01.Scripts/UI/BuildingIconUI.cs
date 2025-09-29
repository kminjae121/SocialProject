using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.LCM._01.Scripts.UI
{
    public class BuildingIconUI : MonoBehaviour
    {
        [SerializeField] private Image buildingIcon;
        [SerializeField] private TextMeshProUGUI buildingNameText;
        [SerializeField] private ConstructionSystem constructionSystem;

        private Button _iconButton;
        

        private void Awake()
        {
            _iconButton = GetComponent<Button>();
        }

        public void Initialize(Sprite icon, string name, int id)
        {
            buildingIcon.sprite = icon;
            buildingNameText.SetText(name);
            _iconButton.onClick.AddListener(() => constructionSystem.StartPlacement(id));
        }
    }
}