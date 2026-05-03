using Cinemachine;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour, IDataPersistence
{
    private Vector3 spawnPoint;
    private CharacterController player;
    [SerializeField] private Transform targetCam;
    [SerializeField] CinemachineFreeLook vCam;

    public Vector3 SpawnPoint => spawnPoint;
    private void Awake()
    {
        player = GetComponent<CharacterController>();
        spawnPoint = transform.position;
    }
    private void Teleport(Vector3 destination)
    {
        if (Manager_Ui.Instance.IsMenuOpen)
        {
            Manager_Ui.Instance.OpenOrCloseMenu();
        }
        if (player != null)
        {
            player.enabled = false;
        }
        Vector3 oldPostion = targetCam.position;
        player.transform.position = destination;
        WarpCam(targetCam.position, oldPostion);
        if (player != null)
        {
            player.enabled = true;
        }
    }
    public void Respawn()
    {
        Teleport(spawnPoint);
    }
    private void WarpCam(Vector3 newPostion, Vector3 oldPosition)
    {
        Vector3 delta = newPostion - oldPosition;
        vCam.OnTargetObjectWarped(targetCam, delta);

    }
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    void IDataPersistence.SaveData(GameSave data)
    {
        data.SpawnPoint = spawnPoint;
        data.LastPosition = player.transform.position;
    }

    void IDataPersistence.LoadData(GameSave data)
    {
        spawnPoint = data.SpawnPoint;
        Teleport(data.LastPosition);

    }
}
