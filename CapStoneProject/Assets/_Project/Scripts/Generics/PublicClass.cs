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
    public string InkState;
    public SO_LetterItem Letter;

}