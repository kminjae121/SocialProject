using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDataSO", menuName = "Scriptable Objects/ObjectDataSO")]
public class ObjectDataSO : ScriptableObject
{
    public List<ObjectData> objectData;
}
[Serializable]
public class ObjectData
{
    [field: SerializeField] 
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int size { get; private set; } = Vector2Int.zero;
    [field: SerializeField]
    public GameObject prefab { get; private set; }
    
    [field: SerializeField] public Transform trm { get; private set; }
    
    
    
}
