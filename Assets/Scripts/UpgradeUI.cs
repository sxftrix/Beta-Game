using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

    void Start()
    {
        SettlementBuilding.OnBuildingEnable += ActivateUI;
    }
    
    void OnEnable() 
    {
        SettlementBuilding.OnCostChanged += UpdateDisplay;
    }

    void OnDisable() 
    {
        SettlementBuilding.OnCostChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(Dictionary<string, int> upgradeCosts, string buildingName)
    {
        if (buildingName == targetBuilding)
        {
            textElement.text = "";
            foreach (var costs in upgradeCosts)
            {
                textElement.text = costs.Key.ToString() + ": " + costs.Value.ToString() + "\n";
            }
        }
    }

    private void ActivateUI(string buildingName)
    {
        if (buildingName == targetBuilding)
        {
            this.gameObject.SetActive(true);
        }
    }
}
