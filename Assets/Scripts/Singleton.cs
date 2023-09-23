using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // Check if an instance already exists in the scene
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    // If not found, create a new GameObject with the script attached
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();

                    // Ensure the GameObject isn't destroyed when loading new scenes
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }

    // Additional initialization code for your Singleton can be placed in Awake
    // protected virtual void Awake()
    // {
    //       Debug.Log("Singleton Awake: " + typeof(T).Name);
    // }

    // Optionally, you can include other methods or properties specific to your Singleton
}
