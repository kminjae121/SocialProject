using UnityEngine;

public class NuclrearObject : Factory
{
    private void Awake()
    {
        _minusEvent += HandleMinus;
    }
    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }

    private void HandleMinus()
    {

    }
}
