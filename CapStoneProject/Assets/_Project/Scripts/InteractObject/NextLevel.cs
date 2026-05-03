using UnityEngine;

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
