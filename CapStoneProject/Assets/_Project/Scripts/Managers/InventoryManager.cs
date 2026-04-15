using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private SO_LetterItem letterItem;
    [SerializeField] private string nameHasReadLetter="hasReadLetter";
    [SerializeField] private string nameHasLetter="hasLetter";
    private bool hasLetter;

    public SO_LetterItem LetterItem => letterItem;
    public void AddLetter(SO_LetterItem item)
    {
        letterItem = item;
        hasLetter = true;
        DialogueManager.SetBool(nameHasLetter,hasLetter);
        Manager_Ui.Instance.ReadLetterUi(hasLetter);
    }
    public void ReadLetter()
    {
        if (hasLetter)
        {
            letterItem.UseItem();
            return;
        }
        Debug.Log("non hai nessuna lettera");
    }
    public void RemoveLetter()
    {
        hasLetter = false;
        letterItem = null;
        DialogueManager.SetBool(nameHasLetter, hasLetter);
        DialogueManager.SetBool(nameHasReadLetter, false);
        Manager_Ui.Instance.ReadLetterUi(hasLetter);
    }
}
