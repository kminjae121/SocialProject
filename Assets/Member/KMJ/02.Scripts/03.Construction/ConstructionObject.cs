using System;
using System.Collections;
using KHG.Scripts.Buildings;
using UnityEngine;

public enum BuildingType
{
    Factory,
    Building
}
public class ConstructionObject : MonoBehaviour
{
    [SerializeField] private BuildingSO thisBuildingSO;

    [SerializeField] private MeshRenderer objMeshRenderer;

    [SerializeField] private BuildingType buildType;

    [SerializeField] private Building buildingCompo;

    private void Start()
    {
        objMeshRenderer.enabled = false;
    }

    public void StartConstructionObject()
    {
        StartCoroutine(BuildWait(thisBuildingSO.BuildTime));
    }

    public void EndConstructionObject()
    {
        if (buildType == BuildingType.Building)
        {
            buildingCompo.AddBuildingPoplulation();    
        }
    }

    private IEnumerator BuildWait(float waitingTimte)
    {
        yield return new WaitForSeconds(waitingTimte);
        objMeshRenderer.enabled = true;
        EndConstructionObject();
    }
}
