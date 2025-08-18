using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public bool Active { get; private set; }

    public abstract void SetActive(bool light);
}
