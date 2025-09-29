using System;
using System.Collections;
using Core.Events;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    [Header("FactorySO")]
    [SerializeField] private FactorySO _factorySO;

    [Header("ModifierValue")]
    [SerializeField] private float _modifierValue;

    [SerializeField] protected GameEventChannelSO _weatherEventChannel;
    
    [SerializeField] protected GameEventChannelSO _electricityEventChannel;
    
    #region Property Field
    protected float _reduceTime
    {
        get => _factorySO.ReduceTime;
        set => _factorySO.ReduceTime = value;
    }

    protected float _reduceValue
    {
        get => _factorySO.ReduceValue;
        set => _factorySO.ReduceValue = value;
    }

    protected float _increaseEnergy;

    protected LayerMask _whatIsCanConstuction
    {
        get => _factorySO._whatIsCollect;
        set => _factorySO._whatIsCollect = value;
    }

    #endregion 

    protected event Action _minusEvent;

    protected event Action _fixEvent;

    protected float _currentEfficiency = 10;

    protected Coroutine _reduceCoroutine;
    protected Coroutine _makingCoroutine;

    

    private void OnEnable()
    {
        AutoMakingEnergies();
        _weatherEventChannel.AddListener<WeatherChangeEvent>(WeatherCondition);
        _increaseEnergy = _factorySO.IncreasingValue;
    }

    private void Start()
    {
        AutoReduceEfficiency();
    }

    protected virtual void WeatherCondition(WeatherChangeEvent evt)
    {
        
    }
    

    private void OnDisable()
    {
        StopCoroutine(_makingCoroutine);
        if (_reduceCoroutine == null)
            return;
        StopCoroutine(_reduceCoroutine);
    }
    
    public void PlusIncreaseEnergy()
    {
      //  if (_increaseEnergy == _factorySO.IncreasingValue) return;

        _increaseEnergy += _modifierValue;

        if (_increaseEnergy >= _factorySO.IncreasingValue)
        {
            _increaseEnergy = _factorySO.IncreasingValue;
            return;
        }
    }

    public void MinusIncreaseEnergy()
    {
        if (_increaseEnergy <= 0)
        {
            _increaseEnergy = 0;
            return;
        }
        
        _increaseEnergy -= _modifierValue;
    }

    protected virtual void MakingEnergy()
    {
        
    }

    public void FixFactory(float addValue)
    {
        if (_reduceCoroutine != null)
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
        while(_increaseEnergy > 0)
        {
            yield return new WaitForSeconds(_reduceTime);
            
            print("됨");
            _currentEfficiency -= _reduceValue;
            _minusEvent?.Invoke();
        }
        
        _currentEfficiency = 0;
        _reduceCoroutine = null;
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
