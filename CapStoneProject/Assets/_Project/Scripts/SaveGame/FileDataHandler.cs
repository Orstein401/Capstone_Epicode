using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public void Save(GameSave gameData, string profileId)
    {
        if (profileId == null) { return; }
        string path = Path.Combine(dataDirPath, profileId, dataFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(path));

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, json);
    }
    public GameSave Load(string profileId)
    {
        string path = Path.Combine(dataDirPath, profileId, dataFileName);
        if (!File.Exists(path)) return null;

        GameSave loadSave = new GameSave();
        string json = File.ReadAllText(path);
        loadSave = JsonUtility.FromJson<GameSave>(json);
        return loadSave;
    }
    public Dictionary<string, GameSave> LoadAllProfiles()
    {
        Dictionary<string, GameSave> profileDic = new Dictionary<string, GameSave>();
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + profileId);
                continue;
            }
            GameSave profileData = Load(profileId);
            if (profileData != null)
            {
                profileDic.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }
        return profileDic;
    }
}

