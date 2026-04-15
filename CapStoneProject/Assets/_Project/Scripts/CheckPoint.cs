using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<RespawnPlayer>(out var player))
        {
            player.SetSpawnPoint(player.transform.position);
        }
    }
}
