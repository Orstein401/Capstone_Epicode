using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distanceView = 10f;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (Manager_Ui.Instance.IsDocumentOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Manager_Ui.Instance.CloseDocument();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && !DialogueManager.Instance.IsDialoguePlaying())
        {
            InventoryManager.Instance.ReadLetter();

        }
        Interact();
    }
    private void Interact()
    {
        Ray directionPoint = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(directionPoint, out RaycastHit hitinfo, distanceView))
        {
            if (hitinfo.collider.TryGetComponent<IInteractable>(out var interactable) && !DialogueManager.Instance.IsDialoguePlaying())
            {
                Manager_Ui.Instance.InteractionUi(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.InteractWithObject();
                }
                return;
            }
        }
        Manager_Ui.Instance.InteractionUi(false);
    }
}


// Debug.DrawRay(viewCam.position, directionPoint.direction * distanceView, Color.red, 1f);