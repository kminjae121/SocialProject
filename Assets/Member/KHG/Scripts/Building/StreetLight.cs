using System.Collections.Generic;
using UnityEngine;

public class StreetLight : Structure
{
    [SerializeField] private List<GameObject> lights;
    public override void SetActive(bool value)
    {
        foreach (var light in lights)
        {
            light.SetActive(value);
        }
    }
}
