using System;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private string targetResource;
    [SerializeField] private TextMeshProUGUI nameElement;
    [SerializeField] private TextMeshProUGUI textElement;

    void Start()
    {
        nameElement.text = targetResource + ":";
    }
    
    void OnEnable() 
    {
        Resources.OnResourceChanged += UpdateDisplay;
    }

    void OnDisable() 
    {
        Resources.OnResourceChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(string resourceName, int amount)
    {
        if (resourceName == targetResource)
        {
            textElement.text = amount.ToString();
        }
    }
}
