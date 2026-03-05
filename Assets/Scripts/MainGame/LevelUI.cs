using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
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
        SettlementBuilding.OnBuildingUpgraded += UpdateDisplay;
    }

    void OnDisable() 
    {
        SettlementBuilding.OnBuildingUpgraded -= UpdateDisplay;
    }

    private void UpdateDisplay(string buildingName, int newLevel)
    {
        if (buildingName == targetBuilding)
        {
            textElement.text = newLevel.ToString();
        }
    }
}
