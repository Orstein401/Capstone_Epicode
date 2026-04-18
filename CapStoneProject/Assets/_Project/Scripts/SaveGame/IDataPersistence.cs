using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    public void SaveData(GameSave data);
    public void LoadData(GameSave data);
}
