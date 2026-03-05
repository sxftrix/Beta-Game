using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Resource[] Resources;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Resources = Utils.GetAllInstances<Resource>();
        Resources = Resources.OrderBy(p => p.ResourceID).ToArray();
        foreach (Resource resource in Resources)
        {
            Debug.Log(resource.name);
        }
    }
}
