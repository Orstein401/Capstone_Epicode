using UnityEngine;
public class InteractFile : MonoBehaviour, IInteractable
{
    [SerializeField] public SO_Document myDocument;
    public void InteractWithObject()
    {
        Manager_Ui.Instance.SetUpDocument(myDocument);
    }
}
