using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Parametres")]
    private float speed;
    [Header("Direction")]
    private Vector3 directionBullet;
    [Header("Timer Despawn")]
    [SerializeField] private float lifeTime;
    private float currentTime;
    private void OnEnable()
    {
        currentTime = lifeTime;
    }
    private void Update()
    {
        transform.position += directionBullet * (speed * Time.deltaTime);
        DespawnTimer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LifeController>(out var player))
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
        transform.rotation = rotation;
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
