using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuest : MonoBehaviour, IInteractable, IDataPersistence
{
    [SerializeField] private string objectID;
    [SerializeField] private string nameItemQuest;
    private bool isActive = true;
    public void InteractWithObject()
    {
        int currentNumItem = DialogueManager.GetInt(nameItemQuest);
        DialogueManager.SetInt(nameItemQuest, currentNumItem + 1);
        isActive = false;
        gameObject.SetActive(isActive);
    }

    public void LoadData(GameSave data)
    {
        int index = data.ObjectsId.IndexOf(objectID);

        if (index != -1)
        {
            isActive = data.ObjectStates[index];
            gameObject.SetActive(isActive);
        }
    }

    public void SaveData(GameSave data)
    {
        int index = data.ObjectsId.IndexOf(objectID);

        if (index == -1)
        {
            data.ObjectsId.Add(objectID);
            data.ObjectStates.Add(isActive);
        }
        else
        {
            data.ObjectStates[index] = isActive;
        }
    }
}
