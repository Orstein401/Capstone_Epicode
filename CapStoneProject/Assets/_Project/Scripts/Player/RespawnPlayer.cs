using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private Vector3 spawnPoint;
    private CharacterController player;

    public Vector3 SpawnPoint=> spawnPoint;
    private void Awake()
    {
        player = GetComponent<CharacterController>();
        spawnPoint= transform.position;
    }
    public void Respawn()
    { 
        if (player != null)
        {
            player.enabled = false;
        }
        player.transform.position = spawnPoint;
        if (player != null)
        {
            player.enabled = true;
        }
    }
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
