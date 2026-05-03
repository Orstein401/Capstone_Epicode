using UnityEngine;

public class Mine : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LifeController>(out var player))
        {
            AudioManager.Instance.PlaySound(audioSource, SoundID.Exsplosion);
            player.DeathPlayer();
        }
    }
}
