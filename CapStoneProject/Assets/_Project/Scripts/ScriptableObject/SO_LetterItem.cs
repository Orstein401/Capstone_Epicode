using UnityEngine;

[CreateAssetMenu(menuName = "Letter")]
public class SO_LetterItem : SO_Document
{
    public void UseItem()
    {
        Manager_Ui.Instance.SetUpDocument(this);
        DialogueManager.SetBool("hasReadLetter",true);
    }
}
