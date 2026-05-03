using UnityEngine;
using UnityEngine.Events;

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private TextAsset dialogue;
    [SerializeField] private UnityEvent actionNpc;
    public void InteractWithObject()
    {
        DialogueManager.Instance.EnterDialogueMode(dialogue, actionNpc);
    }
}
