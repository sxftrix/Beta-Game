using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    /// NOTE:
    /// THIS IS TO BE REPLACED IN FULL VERSION WITH THE PROPER RESOURCE MANAGER SCRIPT FROM MAIN SCENE
    /// THIS IS PURELY FOR TESTING PURPOSES

    public static ResourceManager Instance;
    public int totalResources = 10;
    public TextMeshProUGUI resourceText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public bool CanAfford(int amount)
    {
        return totalResources >= amount;
    }

    public void SpendResources(int amount)
    {
        totalResources -= amount;
        UpdateUI();
        Debug.Log($"Spent {amount}. Current Total: {totalResources}");
    }

    public void AddResources(int amount)
    {
        totalResources += amount;
        UpdateUI();
        Debug.Log($"Earned {amount}. Current Total: {totalResources}");
    }

    void UpdateUI()
    {
        if (resourceText != null)
            resourceText.text = "Resources: " + totalResources;
    }
}
