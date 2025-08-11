using System.Collections;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    [Header("ReduceTime")]
    [SerializeField] protected float _reduceTime = 3f;

    [Space(10)]
    [Header("ReduceValue")]
    [SerializeField] protected float _reduceValue = 0.1f;

    protected float currentEfficiency = 100;

    protected Coroutine _reduceCoroutine;

    

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
