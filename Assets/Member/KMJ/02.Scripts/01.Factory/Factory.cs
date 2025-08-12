using System.Collections;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    [Header("FactorySO")]
    [SerializeField] private FactorySO _factorySO;
    protected float _reduceTime => _factorySO.ReduceTime;
   
    protected float _reduceValue => _factorySO.ReduceValue;

    protected float _increaseEnergy => _factorySO.IncreasingValue;

    protected float currentEfficiency = 100;

    protected Coroutine _reduceCoroutine;


    private void OnEnable()
    {
        AutoReduceEfficiency();
    }
    private void OnDisable()
    {
        StopCoroutine(_reduceCoroutine);
    }

    protected virtual void MakingEnergy()
    {

    }
    
    public void FixFactory(float addValue)
    {
        currentEfficiency += addValue;
    }

    private void AutoReduceEfficiency()
    {
        _reduceCoroutine = StartCoroutine(AutoReduce());
    }

    private IEnumerator AutoReduce()
    {
        while(true)
        {
            yield return new WaitForSeconds(_reduceTime);

            currentEfficiency -= _reduceValue;
        }
    }
}
