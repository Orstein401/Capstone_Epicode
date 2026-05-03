using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] private string walk = "IsWalk";
    [SerializeField] private string run = "IsRun";
    [SerializeField] private string jump = "IsJump";
    [SerializeField] private string ground = "IsGround";
    [SerializeField] private string death = "IsDeath";
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    public void UpdateStates(Vector3 direction, bool isRun, bool isGrounded)
    {
        bool isWalking = direction != Vector3.zero;
        bool isRunning = isWalking && isRun;

        playerAnimator.SetBool(ground, isGrounded);
        playerAnimator.SetBool(walk, isWalking);
        playerAnimator.SetBool(run, isRunning);
    }
    public void TriggerJump()
    {
        playerAnimator.SetTrigger(jump);
    }
    public void TriggerDeath()
    {
        playerAnimator.SetTrigger(death);
    }
}
