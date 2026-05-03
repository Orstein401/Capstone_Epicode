using UnityEngine;

public class DeactiveObjects : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
