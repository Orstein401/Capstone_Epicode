using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Transform of reference")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;

    [Header("Parametres turret")]
    [SerializeField] private float range;
    [SerializeField] private float speedRotation;
    [SerializeField] private float fireRate;
    private float lastTimeShoot;

    [Header("Projectile data")]
    private Vector3 direction;

    [Header("Parametres Projectile")]
    [SerializeField] private float speedProjectile;

    private AudioSource sourceAudio;
    private void Awake()
    {
        sourceAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (player != null)
        {
            direction = player.position - transform.position;
            if (direction.magnitude <= range)
            {
                RotateTurret();
                if (Time.time - lastTimeShoot > fireRate)
                {
                    Shoot(firePoint.forward);
                    lastTimeShoot = Time.time;
                }
            }
        }
    }
    private void Shoot(Vector3 direction)
    {
        Bullet bullet = PoolBullet.Instance.GetPrefab();
        bullet.transform.position = firePoint.position;
        bullet.SetDirectionRotationAndSpeed(direction, speedProjectile);

        AudioManager.Instance.PlaySound(sourceAudio, SoundID.Shoot);
    }
    private void RotateTurret()
    {
        direction.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);

        transform.rotation = rotation;
    }
}
