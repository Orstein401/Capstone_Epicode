using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotsMenu : MonoBehaviour
{
    SaveSlot[] saveSlots;
    private bool isLoadingGame=false;
    private void Awake()
    {
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }
    public void ActiveMenu(bool isLoading)
    {
        isLoadingGame = isLoading;
        Dictionary<string, GameSave> profils = DataManager.Instance.GetAllProfiles();
        foreach (SaveSlot slot in saveSlots)
        {
            GameSave profileData = null;
            profils.TryGetValue(slot.GetProfileID(), out profileData);
            slot.SetSlot(profileData);
            if (profileData==null&&isLoadingGame)
            {
                slot.SetInteractable(false);
            }
            else
            {
                slot.SetInteractable(true);
            }
        }
    }
    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        Disablebutton();
        DataManager.Instance.ChangeProfileId(saveSlot.GetProfileID());
        if (!isLoadingGame)
        {
            DataManager.Instance.NewGame();
        }
        DataManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(1);
    }
    public void Disablebutton()
    {
        foreach (SaveSlot slot in saveSlots)
        {
            slot.SetInteractable(false);
        }
    }
}
