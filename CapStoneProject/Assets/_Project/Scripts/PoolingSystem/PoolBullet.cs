using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBullet : Singleton<PoolBullet>
{
    [SerializeField] private Bullet prefab;
    [SerializeField] private int sizePool = 50;
    [SerializeField] private int numEmergercy = 20;
    private Queue<Bullet> pool = new Queue<Bullet>();
    protected override void Awake()
    {
        base.Awake();
        CreatePool(sizePool);
    }
    public void CreatePool(int numPrefab)
    {
        for (int i = 0; i < numPrefab; ++i)
        {
            var obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public Bullet GetPrefab()
    {
        if (pool.Count == 0)
        {
            CreatePool(numEmergercy);
        }
        var obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }
    public void ReturnToPool(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
