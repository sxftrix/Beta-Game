using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private string targetBuilding;
    
    private TextMeshProUGUI textElement;

    void Awake()
    {
        textElement = this.GetComponent<TextMeshProUGUI>();
        if (textElement == null)
        {
            Debug.LogWarning("UpgradeUI: No TextMeshProUGUI attached to UpgradeUI");
        }
    }
    
    void OnEnable() 
    {
        Building.OnCostChanged += UpdateDisplay;
    }

    void OnDisable() 
    {
        Building.OnCostChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(string buildingName, int newCost)
    {
        if (buildingName == targetBuilding)
        {
            textElement.text = "Cost: " + newCost;
        }
    }
}
