using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Utility.ObjectPool.Runtime;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private PoolingItemSO carItem;
    [SerializeField] private PoolManagerMono poolManager;

    [SerializeField] private Transform[] spawners;

    [Header("Settings")]
    [SerializeField] private float spawnSpeed = 1f;
    [SerializeField] private float randomSpawnBetween = 0.5f;
    [SerializeField] private float carMoveSpeed;

    private bool _canSpawn;

    private void Start()
    {
        SetSpawn(true);
    }

    public void SetSpawn(bool value)
    {
        _canSpawn = value;
        if (value) StartCoroutine(CarSpawn());
    }

    private IEnumerator CarSpawn()
    {
        Car car = poolManager.Pop<Car>(carItem);
        Transform spawnPoint = spawners[Random.Range(0, spawners.Length)];
        car.transform.position = spawnPoint.position;
        car.transform.rotation = spawnPoint.rotation;

        car.Action(carMoveSpeed);

        yield return new WaitForSeconds(spawnSpeed + Random.Range(-randomSpawnBetween, randomSpawnBetween));

        if(_canSpawn) StartCoroutine(CarSpawn());
    }
}
