using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "FactorySO")]
public class FactorySO : ScriptableObject
{
    public string FactoryName;

    [Header("IncreasingValue")]
    [Range(0, 20)]
    public float IncreasingValue;

    [Header("ReduceTime")]
    [Range(0, 20)]
    public float ReduceTime = 3f;

    [Header("ReduceValue")]
    [Range(0, 20)]
    public float ReduceValue = 0.1f;

    [Space(5)]
    [Header("CollectGroundLayerMask")]
    public LayerMask _whatIsCollect;

    private void OnValidate()
    {
        if (FactoryName == string.Empty)
            return;

        name = FactoryName;
    }
}
