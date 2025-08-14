using UnityEngine;

public abstract class ConstructionObject : MonoBehaviour, IConstruction
{
    public virtual void StartConstruction()
    {
    }

    public virtual void StopContruction()
    {
    }

    public void DestroyThisObject()
    {
        gameObject.SetActive(false);
    }
}
