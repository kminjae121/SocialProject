using System;
using UnityEngine;

public class SunFactory : Factory
{
    private void Awake()
    {
        _minusEvent += HandleMinus;
        _fixEvent += HandleFix;
        _brokeEvent += HandleBroke;
    }


    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }
    private void HandleBroke()
    {
        gameObject.SetActive(false);
    }

    private void HandleFix()
    {
        PlusIncreaseEnergy();
    }

    private void HandleMinus()
    {
        MinusIncreaseEnergy();
    }
}
