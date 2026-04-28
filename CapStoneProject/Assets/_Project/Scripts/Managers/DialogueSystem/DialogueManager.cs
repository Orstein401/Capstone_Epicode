using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class DialogueManager : Singleton<DialogueManager>, IDataPersistence
{
    [Header("UI Dialogue")]
    [SerializeField] private GameObject uiDialogue;
    [SerializeField] private TextMeshProUGUI textDialogue;

    [Header("Ui Choice")]
    [SerializeField] private DialogueChoice prefabChoice;
    private List<DialogueChoice> choicesList;

    [Header("Componet")]
    private Story currentStory;
    private static DialogueVariables variables = new DialogueVariables();
    // [SerializeField] private TextAsset gloablInk;

    [Header("State Dialogue")]
    [SerializeField] private StateDialogue dialogueState;
    private enum StateDialogue { Hidden, Entering, Playing, SelectionChoice, ApplyngChoice, Exiting }
    [Header("Action on Complete")]
    private Action onDialogueComplete;
    private UnityEvent onDialogueCompleteEvent;
    private void Start()
    {
        uiDialogue.SetActive(false);
        dialogueState = StateDialogue.Hidden;
        choicesList = new List<DialogueChoice>();
        choicesList.Add(prefabChoice);
    }
    private void Update()
    {
        switch (dialogueState)
        {
            case StateDialogue.Entering:
            case StateDialogue.ApplyngChoice:
                dialogueState = StateDialogue.Playing;
                return;
            case StateDialogue.Playing:
                if (Input.GetButtonDown("Submit"))
                {
                    ContinueStory();
                }
                break;
            case StateDialogue.SelectionChoice:
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(choicesList[0].gameObject);
                }
                break;
            case StateDialogue.Exiting:
            case StateDialogue.Hidden:
                return;
        }
    }
    protected override bool ShouldBeDestoyOnLoad() => true;
    public bool IsDialoguePlaying()
    {
        switch (dialogueState)
        {
            default:
            case StateDialogue.Playing:
            case StateDialogue.Entering:
            case StateDialogue.Exiting:
            case StateDialogue.SelectionChoice:
            case StateDialogue.ApplyngChoice:
                return true;
            case StateDialogue.Hidden:
                return false;
        }
    }

    public void EnterDialogueMode(TextAsset inkJson, string targetKnot = null) => EnterDialogueMode(inkJson, targetKnot, null, null);

    public void EnterDialogueMode(TextAsset inkJson, Action onDialogueCompleteAction) => EnterDialogueMode(inkJson, null, onDialogueCompleteAction);
    public void EnterDialogueMode(TextAsset inkJson, string targetKnot, Action onDialogueCompleteAction) => EnterDialogueMode(inkJson, targetKnot, onDialogueCompleteAction);

    public void EnterDialogueMode(TextAsset inkJson, UnityEvent onDialogueCompleteEvent) => EnterDialogueMode(inkJson, null, null, onDialogueCompleteEvent);
    public void EnterDialogueMode(TextAsset inkJson, string targetKnot, UnityEvent onDialogueCompleteEvent) => EnterDialogueMode(inkJson, targetKnot, null, onDialogueCompleteEvent);


    public void EnterDialogueMode(TextAsset inkJson, string targetKnot, Action onDialogueComplete = null, UnityEvent onDialogueCompleteEvent = null)
    {
        Manager_Ui.Instance.ReadLetterUi(false);
        this.onDialogueComplete = onDialogueComplete;
        this.onDialogueCompleteEvent = onDialogueCompleteEvent;
        currentStory = new Story(inkJson.text);

        if (!string.IsNullOrEmpty(targetKnot))
        {
            currentStory.ChoosePathString(targetKnot);
        }
        uiDialogue.SetActive(true);
        dialogueState = StateDialogue.Entering;

        variables.AddNewGlobalVariablesFromStory(currentStory);
        variables.StartListening(currentStory);
        ContinueStory();
    }
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string text = currentStory.Continue();
            DisplayOrHideChoices();
            if (HasChoice())
            {
                dialogueState = StateDialogue.SelectionChoice;
                if (!string.IsNullOrEmpty(text))
                {
                    textDialogue.text = text;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(text))
                {
                    textDialogue.text = text;
                    if (dialogueState != StateDialogue.Entering && dialogueState != StateDialogue.ApplyngChoice)
                    {
                        dialogueState = StateDialogue.Playing;
                    }
                }
                else
                {
                    ContinueStory();
                }
            }
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private IEnumerator ExitDialogueMode()
    {
        dialogueState = StateDialogue.Exiting;
        yield return new WaitForSeconds(0.1f);

        variables.StopListening(currentStory);
        uiDialogue.SetActive(false);
        textDialogue.text = "";
        if (variables.GetBool("hasLetter"))
        {
            Manager_Ui.Instance.ReadLetterUi(true);
        }

        if (onDialogueComplete != null)
        {
            onDialogueComplete.Invoke();
        }
        if (onDialogueCompleteEvent != null)
        {
            onDialogueCompleteEvent.Invoke();
        }
        dialogueState = StateDialogue.Hidden;
    }
    private bool HasChoice()
    {
        return currentStory.currentChoices != null && currentStory.currentChoices.Count > 0;
    }

    private void DisplayOrHideChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;
        for (; index < currentChoices.Count && index < choicesList.Count; index++)
        {
            choicesList[index].gameObject.SetActive(true);
            choicesList[index].choicetext.text = currentChoices[index].text;
        }
        for (; index < currentChoices.Count; index++)
        {
            DialogueChoice choice = Instantiate(prefabChoice, prefabChoice.transform.parent);
            choice.choiceIndex = index;

            choicesList.Add(choice);

            choice.gameObject.SetActive(true);
            choice.choicetext.text = currentChoices[index].text;

        }
        for (; index < choicesList.Count; index++)
        {
            choicesList[index].gameObject.SetActive(false);
        }
        if (currentChoices.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(choicesList[0].gameObject);
        }
    }
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        dialogueState = StateDialogue.ApplyngChoice;
        ContinueStory();
    }

    public static bool GetBool(string nameValue)
    {
        return variables.GetBool(nameValue);
    }
    public static void SetBool(string nameValue, bool value)
    {
        variables.SetBool(nameValue, value);
    }
    public static int GetInt(string nameValue)
    {
        return variables.GetInt(nameValue);
    }
    public static void SetInt(string nameValue, int value)
    {
        variables.SetInt(nameValue, value);
    }

    void IDataPersistence.SaveData(GameSave data)
    {
        data.KeysInt = new List<string>();
        data.KeysBool = new List<string>();
        data.ValuesBool = new List<bool>();
        data.ValuesInt = new List<int>();
        variables.FillListsWithVariables(data.KeysInt, data.ValuesInt, data.KeysBool, data.ValuesBool);
    }
    void IDataPersistence.LoadData(GameSave data)
    {
        variables.SetBoolsFromList(data.KeysBool, data.ValuesBool);
        variables.SetIntegersFromList(data.KeysInt, data.ValuesInt);
    }
}
