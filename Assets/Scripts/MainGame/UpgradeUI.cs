using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.LookDev;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private string targetBuilding;
    
    private TextMeshProUGUI textElement;
    
    private void OnEnable() 
    {
        textElement = this.GetComponent<TextMeshProUGUI>();
        if (textElement == null)
        {
            Debug.LogWarning("UpgradeUI: No TextMeshProUGUI attached to UpgradeUI");
        }
        SettlementBuilding.OnCostChanged += UpdateDisplay;
    }

    void OnDisable() 
    {
        SettlementBuilding.OnCostChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(string buildingName, Dictionary<string, int> newCosts)
    {
        if (buildingName == targetBuilding)
        {
            textElement.text = "";
            foreach (var key in newCosts.Keys)
            {
                textElement.text += newCosts[key] + " " + key + " ";
            }
        }
    }
}
