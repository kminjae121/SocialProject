using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.LCM._01.Scripts.UI
{
    public class BuildingIconUI : MonoBehaviour
    {
        [SerializeField] private Image buildingIcon;
        [SerializeField] private TextMeshProUGUI buildingNameText;
        private ConstructionSystem _constructionSystem;

        private int _id;

        private void Awake()
        {
            _constructionSystem = FindAnyObjectByType<ConstructionSystem>();
        }

        public void Initialize(Sprite icon, string name, int id)
        {
            buildingIcon.sprite = icon;
            buildingNameText.SetText(name);
            _id = id;
        }

        public void OnClickIcon()
        {
            _constructionSystem.StartPlacement(_id);
        }
    }
}