using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources Instance { get; private set; }

    [Header("Player Resources")]
    public int resources;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SpendResources(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            return true;
        }
        return false;
    }

    public void AddResources(int amount)
    {
        resources += amount;
    }
}
