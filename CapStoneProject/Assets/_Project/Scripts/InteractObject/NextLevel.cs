using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, IInteractable
{
    [SerializeField] RespawnPlayer player;
    [SerializeField] Vector3 newPosition;
    public void InteractWithObject()
    {
        player.SetSpawnPoint(newPosition);
        player.Respawn();
    }
}
