using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    public TextMeshProUGUI choicetext;
    public int choiceIndex;

    public void OnChoiceClicked()
    {
        DialogueManager.Instance.MakeChoice(choiceIndex);
    }
}
