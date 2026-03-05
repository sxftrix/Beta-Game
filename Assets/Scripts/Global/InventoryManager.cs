using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("CHEAT MODE: Turn on to gain resources")]
    public bool CheatMode;
    
    [Header("REFERENCE ONLY DON'T EDIT")]
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
        Resources = Resources.OrderBy(p => p.GetID()).ToArray();
        foreach (Resource resource in Resources)
        {
            resource.Reset();
            Debug.Log(resource.name);
            if (CheatMode)
            {
                resource.Gain(100000);
            }
        }
    } 
    
    
}
