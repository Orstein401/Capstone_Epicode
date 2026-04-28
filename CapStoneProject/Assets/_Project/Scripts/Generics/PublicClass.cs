using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public SoundID SoundID;
    public AudioClip[] Clips;
}
[System.Serializable]
public class GameSave
{
    public Vector3 LastPosition;
    public Vector3 SpawnPoint;
    public SO_LetterItem Letter;

    public List<string> ObjectsId = new List<string>();
    public List<bool> ObjectStates = new List<bool>();

    public List<string> KeysInt;
    public List<string> KeysBool;
    public List<int> ValuesInt;
    public List<bool> ValuesBool;
}