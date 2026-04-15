using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Parametres")]
    private float speed;

    [Header("Componets")]
    private Rigidbody rb;

    [Header("Direction")]
    private Vector3 directionBullet;

    [Header("Timer Despawn")]
    [SerializeField] private float lifeTime;
    private float currentTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        currentTime = lifeTime;
    }
    private void Update()
    {
        DespawnTimer();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + directionBullet * (speed * Time.fixedDeltaTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<LifeController>(out var player))
        {
            player.DeathPlayer();
        }
        PoolBullet.Instance.ReturnToPool(this);
    }
    public void SetDirectionRotationAndSpeed(Vector3 direction, float speedBullet)
    {
        if (direction.magnitude > 1) direction.Normalize();
        directionBullet = direction;

        Quaternion rotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(rotation);

        speed = speedBullet;
    }
    public void DespawnTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            PoolBullet.Instance.ReturnToPool(this);
        }
    }
}
