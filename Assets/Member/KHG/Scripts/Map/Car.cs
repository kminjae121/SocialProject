using UnityEngine;
using Utility.ObjectPool.Runtime;

public class Car : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolingItemSO poolingItem;
    [SerializeField] private Material[] materials;
    private Pool _carPool;
    private float _moveSpeed;
    private MeshRenderer _mRenderer;
    public PoolingItemSO PoolingType => poolingItem;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        _mRenderer = GetComponent<MeshRenderer>();
    }

    public void ResetItem()
    {
        _moveSpeed = 0;
    }

    public void SetUpPool(Pool pool)
    {
        _carPool = pool;
    }

    public void Push() => _carPool.Push(this);

    public void Action(float speed)
    {
        _moveSpeed = speed;
        _mRenderer.material = materials[Random.Range(0,materials.Length)];
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _moveSpeed;
    }
};
