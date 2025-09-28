using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDataSO", menuName = "Scriptable Objects/ObjectDataSO")]
public class ObjectDataSO : ScriptableObject
{
    public List<ObjectData> objectData = new List<ObjectData>();
}
[Serializable]
public class ObjectData
{
    [field: SerializeField] public Sprite thisImage { get; set; }

    [field: SerializeField] 
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int size { get; private set; } = Vector2Int.zero;
    [field: SerializeField]
    public GameObject prefab { get; private set; }
    [field: SerializeField]
    public GameObject visualPrefab { get; private set; }
    
    [field: SerializeField] public Transform trm { get; private set; }
    
    [field: SerializeField] public int price { get; private set; }
    
    [field: SerializeField] public Vector3 DetectedRangeVec { get; private set; }
    
    [field: SerializeField] public float DetectedRangeFloat { get; private set; }
    
    [field: SerializeField] public LayerMask DetectedLayer { get; private set; }
}
