using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUi;
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    [SerializeField] private Button exitGame;
    [SerializeField] private SaveSlotsMenu saveSlots;
    public void NewGame()
    {
        DeactiveMenu();
        saveSlots.ActiveMenu(false);
    }
    public void LoadGame()
    {
        DeactiveMenu();
        saveSlots.ActiveMenu(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ActiveMenu()
    {
        saveSlots.gameObject.SetActive(false);
        mainMenuUi.SetActive(true);
    }
    public void DeactiveMenu()
    {
        mainMenuUi.SetActive(false);
        saveSlots.gameObject.SetActive(true);
    }
}
