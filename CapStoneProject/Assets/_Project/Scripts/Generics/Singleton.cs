using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool isApplicationQuit = false;
    protected virtual bool ShouldBeDestoyOnLoad() => false;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>(FindObjectsInactive.Include);
                if (instance == null && !isApplicationQuit)
                {
                    GameObject singleton = new GameObject(typeof(T).ToString());
                    instance = singleton.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
            if (!ShouldBeDestoyOnLoad())
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    protected virtual void OnApplicationQuit()
    {
        isApplicationQuit = true;
    }
}
