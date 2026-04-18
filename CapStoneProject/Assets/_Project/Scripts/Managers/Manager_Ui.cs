using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager_Ui : Singleton<Manager_Ui>
{
    [Header("DocumentUi")]
    [SerializeField] private GameObject uiDocument;
    [SerializeField] private Image backGroundDocu;
    [SerializeField] private TextMeshProUGUI titleDocu;
    [SerializeField] private TextMeshProUGUI textDocu;

    [Header("Interface Player")]
    [SerializeField] private GameObject uiInterfacePlayer;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI readLetter;
    public bool IsDocumentOpen { get; private set; }
    [Header("MenuUi")]
    [SerializeField] private GameObject uiMenu;
    public bool IsMenuOpen=false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!IsDocumentOpen)
        {
            OpenOrCloseMenu();
        }
    }
    protected override bool ShouldBeDestoyOnLoad() => true;
    public void OpenOrCloseMenu()
    {
        IsMenuOpen = !IsMenuOpen;
        uiMenu.SetActive(IsMenuOpen);
        if (IsMenuOpen)
        {
            Time.timeScale = 0f;
            return;
        }
        Time.timeScale = 1f;
    }
    public void SetUpDocument(SO_Document file)
    {
        if (file == null) return;
        Time.timeScale = 0f;
        IsDocumentOpen = true;
        uiInterfacePlayer.SetActive(false);
        interactText.enabled = false;
        uiDocument.SetActive(true);

        backGroundDocu.sprite = file.BackgroundFile;
        titleDocu.SetText(file.TitleFile);
        textDocu.SetText(file.TextFile);
    }
    public void CloseDocument()
    {
        IsDocumentOpen = false;
        uiInterfacePlayer.SetActive(true);
        uiDocument.SetActive(false);
        Time.timeScale = 1f;
    }
    public void InteractionUi(bool active)
    {
        if (interactText.enabled != active) interactText.enabled = active;
    }
    public void ReadLetterUi(bool active)
    {
        if (readLetter.enabled != active) readLetter.enabled = active;
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
    public void SaveButton()
    {
        DataManager.Instance.SaveGame();
    }
    public void LoadButton()
    {
        DataManager.Instance.LoadGame();
    }
}
