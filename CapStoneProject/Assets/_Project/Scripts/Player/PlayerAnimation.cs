using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnim;
    [SerializeField] private string walk = "IsWalk";
    [SerializeField] private string run = "IsRun";
    [SerializeField] private string jump = "IsJump";
    [SerializeField] private string ground = "IsGround";
    [SerializeField] private string death = "IsDeath";
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }
    public void UpdateStates(Vector3 direction, bool isRun, bool isGrounded)
    {
        bool isWalking = direction != Vector3.zero;
        bool isRunning = isWalking && isRun;

        playerAnim.SetBool(ground, isGrounded);
        playerAnim.SetBool(walk, isWalking);
        playerAnim.SetBool(run, isRunning);
    }
    public void TriggerJump()
    {
        playerAnim.SetTrigger(jump);
    }
    public void TriggerDeath()
    {
        playerAnim.SetTrigger(death);
    }
}
