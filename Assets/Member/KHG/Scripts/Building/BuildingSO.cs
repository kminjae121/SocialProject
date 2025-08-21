using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "SO/Building")]
public class BuildingSO : ScriptableObject
{
    public float BuildTime;
    public int BuildCost;
    public int DestroyCost;
    public int Population;
}
