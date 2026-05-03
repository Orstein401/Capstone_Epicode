using UnityEngine;

public class Consequences : MonoBehaviour, IInteractable
{
    [SerializeField] SO_Document[] finals;
    public void InteractWithObject()
    {
        int final = DialogueManager.GetInt("Final");
        Manager_Ui.Instance.SetUpDocument(finals[final]);
    }

}
