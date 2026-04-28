using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataManager : Singleton<DataManager>
{
    private GameSave dataGame;
    private List<IDataPersistence> dataPersistence;
    private FileDataHandler fileDataHandler;
    [SerializeField] private string path = "SaveGame.json";
    private string profileId = "";
    protected override void Awake()
    {
        base.Awake();
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, path);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void NewGame()
    {
        dataGame = new GameSave();
    }
    public void LoadGame()
    {
        dataGame = fileDataHandler.Load(profileId);
        if (dataGame == null) { Debug.Log("non ci sono salvataggi"); return; }
        foreach (IDataPersistence data in dataPersistence)
        {
            data.LoadData(dataGame);
            //Debug.Log("ha caricato un dato");
        }
    }
    public void SaveGame()
    {
        if (dataGame == null) { return; }
        foreach (IDataPersistence data in dataPersistence)
        {
            data.SaveData(dataGame);
            //Debug.Log("ha salvato");
        }
       fileDataHandler.Save(dataGame,profileId);
    }
    public void ChangeProfileId(string profileId)
    {
        this.profileId = profileId;
        LoadGame();
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistence = FindAllDataPersistentObject();
        LoadGame();
    }
    private List<IDataPersistence> FindAllDataPersistentObject()
    {
        IEnumerable<IDataPersistence> dataObject = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataObject);
    }
    public Dictionary<string, GameSave> GetAllProfiles()
    {
        return fileDataHandler.LoadAllProfiles();
    }
}
