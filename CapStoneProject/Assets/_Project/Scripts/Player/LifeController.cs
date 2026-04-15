using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LifeController : MonoBehaviour
{
    private PlayerController controller;
    private RespawnPlayer respawn;
    private Coroutine coroutine;
    [SerializeField] private float timerRespawn = 3f;

    private void Awake()
    {
        controller=GetComponent<PlayerController>();
        respawn=GetComponent<RespawnPlayer>();
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
        controller.enabled = false;
        //animazione;
        yield return new WaitForSeconds(timerRespawn);
        respawn.Respawn();
        controller.enabled = true;
        coroutine = null;
    }
    
}
