using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string path;
    [SerializeField] private RespawnPlayer player;
    private void Start()
    {
        path = Application.persistentDataPath + "/SaveGame.json";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadGame();
        }
    }
    public void SaveGame()
    {
        SaveData currentSave = new SaveData();
        currentSave.SpawnPoint = player.SpawnPoint;
        currentSave.inkState = DialogueManager.Instance.GetStoryJson();
        currentSave.letter = InventoryManager.Instance.LetterItem;

        string json = JsonUtility.ToJson(currentSave, true);
        File.WriteAllText(path, json);
    }
    public void LoadGame()
    {
        if (!File.Exists(path)) return;
        SaveData loadSave = new SaveData();
        string json = File.ReadAllText(path);
        loadSave = JsonUtility.FromJson<SaveData>(json);

        player.SetSpawnPoint(loadSave.SpawnPoint);
        player.Respawn();
        if (loadSave.letter != null)
        {
            InventoryManager.Instance.AddLetter(loadSave.letter);
        }
        if (loadSave.inkState!=null)
        {
            //DialogueManager.Instance.LoadStory(loadSave.inkState);
        }

    }
}
