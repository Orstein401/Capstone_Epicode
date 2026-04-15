using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private void Update()
    {
        if (!DialogueManager.Instance.IsDialoguePlaying())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
