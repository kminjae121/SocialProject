using System.Collections.Generic;
using UnityEngine;

namespace KHG.Scripts.Building
{ 
    public class Building : Structure
    {
        [SerializeField] private List<MeshRenderer> windows;
        public override void SetActive(bool value)
        {
            foreach (var win in windows)
                win.enabled = value;
        }
    }
}
