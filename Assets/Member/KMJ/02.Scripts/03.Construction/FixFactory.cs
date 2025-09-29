using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Member.KMJ._02.Scripts._03.Construction
{
    public class FixFactory : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsFactory;
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit,int.MaxValue, whatIsFactory))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.transform.GetComponent<Factory>().FixFactory(10);
                }
            }
        }
    }
}