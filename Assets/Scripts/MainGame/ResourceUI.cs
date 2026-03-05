using System;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private string targetResource;
    
    private TextMeshProUGUI _textElement;
    
    void OnEnable() 
    {
        _textElement =  GetComponent<TextMeshProUGUI>();
        _textElement.text = targetResource + ": ";
        ResourceManager.OnResourceChanged += UpdateDisplay;
    }

    void OnDisable() 
    {
        ResourceManager.OnResourceChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(string resourceName, int amount)
    {
        if (resourceName == targetResource)
        {
            _textElement.text = targetResource + ": " + amount.ToString();
        }
    }
}
