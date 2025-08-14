using System;
using System.Collections;
using System.Runtime.Serialization.Json;
using UnityEngine;

public abstract class Factory : ConstructionObject
{
    [Header("FactorySO")]
    [SerializeField] private FactorySO _factorySO;

    [Header("ModifierValue")]
    [SerializeField] private float _modifierValue;

    #region Property Field
    protected float _reduceTime => _factorySO.ReduceTime;
   
    protected float _reduceValue => _factorySO.ReduceValue;

    protected float _increaseEnergy
    {
        get => _factorySO.IncreasingValue;

        set
        {
            _increaseEnergy = value;
        }
    }

    protected LayerMask _whatIsCanConstuction => _factorySO._whatIsCollect;

    #endregion 

    protected event Action _brokeEvent;
    protected event Action _minusEvent;
    protected event Action _fixEvent;

    protected float _currentEfficiency = 10;

    protected Coroutine _reduceCoroutine;
    protected Coroutine _makingCoroutine;


    private void OnEnable()
    {
        AutoMakingEnergies();
        AutoReduceEfficiency();
    }
    private void OnDisable()
    {
        StopCoroutine(_makingCoroutine);
        if (_reduceCoroutine == null)
            return;
        StopCoroutine(_reduceCoroutine);
    }

    public override void StartConstruction()
    {

    }

    public override void StopContruction()
    {

    }

    public void PlusIncreaseEnergy()
    {
        if (_increaseEnergy == _factorySO.IncreasingValue) return;

        _increaseEnergy += _modifierValue;

        if (_increaseEnergy >= _factorySO.IncreasingValue)
        {
            _increaseEnergy = _factorySO.IncreasingValue;
            return;
        }
    }

    public void MinusIncreaseEnergy()
    {
        _increaseEnergy -= _modifierValue;
    }

    protected virtual void MakingEnergy()
    {
        
    }

    public void FixFactory(float addValue)
    {
        if (_currentEfficiency > 0 && _reduceCoroutine != null)
        {
            _currentEfficiency += addValue;
            _fixEvent?.Invoke();
            return;
        }
        else
        {
            _currentEfficiency += addValue;
            _fixEvent?.Invoke();
            _reduceCoroutine = StartCoroutine(AutoReduce());
        }
    }

    private void AutoMakingEnergies()
    {
        _makingCoroutine = StartCoroutine(AutoMakingEnergy());
    }

    private void AutoReduceEfficiency()
    {
        _reduceCoroutine = StartCoroutine(AutoReduce());
    }

    private IEnumerator AutoReduce()
    {
        while(_currentEfficiency > 0)
        {
            yield return new WaitForSeconds(_reduceTime);

            _currentEfficiency -= _reduceValue;
            _minusEvent?.Invoke();
        }
        _currentEfficiency = 0;
        _reduceCoroutine = null;
        _brokeEvent?.Invoke();
    }

    private IEnumerator AutoMakingEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            MakingEnergy();
        }
    }
}
