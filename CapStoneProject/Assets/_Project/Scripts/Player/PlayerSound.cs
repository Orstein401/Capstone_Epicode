using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }
    public void FootStepSound()
    {
        AudioManager.Instance.PlaySound(audioSource, SoundID.FootStep);
    }
    public void RunFootStepSound()
    {
        AudioManager.Instance.PlaySound(audioSource, SoundID.RunFootStep);
    }
}
