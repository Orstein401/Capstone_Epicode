using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LifeController : MonoBehaviour
{
    private PlayerController controller;
    private RespawnPlayer respawn;
    private PlayerAnimation anim;
    private Coroutine coroutine;
    [SerializeField] private float timerRespawn = 3f;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        respawn = GetComponent<RespawnPlayer>();
        anim = GetComponentInChildren<PlayerAnimation>();
    }
    public void DeathPlayer()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(DeathRoutine());
        }
        Debug.Log("è morto");
    }
    private IEnumerator DeathRoutine()
    {
        controller.ActiveOrDisactiveInput();
        anim.TriggerDeath();
        yield return new WaitForSeconds(timerRespawn);
        respawn.Respawn();
        controller.ActiveOrDisactiveInput();
        coroutine = null;
    }

}
