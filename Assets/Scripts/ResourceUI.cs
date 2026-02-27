using System;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private string targetResource;
    [SerializeField] private TextMeshProUGUI nameElement;
    [SerializeField] private TextMeshProUGUI textElement;
    
    //Displays Resource Name
    void Start()
    {
        nameElement.text = targetResource + ":";
    }
    
    //Adds and Removes EventListener when needed
    void OnEnable() 
    {
        Resources.OnResourceChanged += UpdateDisplay;
    }   
    void OnDisable() 
    {
        Resources.OnResourceChanged -= UpdateDisplay;
    }
    
    //When tracked resource changes value, updates UI
    private void UpdateDisplay(string resourceName, int amount)
    {
        if (resourceName == targetResource)
        {
            textElement.text = amount.ToString();
        }
    }
}
