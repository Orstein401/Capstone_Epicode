using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public SoundID SoundID;
    public AudioClip[] Clips;
}
[System.Serializable]
public class SaveData
{
    public Vector3 SpawnPoint;
    public string inkState;
    public SO_LetterItem letter;

}