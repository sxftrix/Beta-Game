using System;
using TMPro;
using UnityEngine;

public class ResourceTally : MonoBehaviour
{
    [Header("REQUIRED: Parameters")]
    [SerializeField] private Resource targetResource; 
    
    private TextMeshProUGUI tallyText;
    private void Start()
    {
        if (targetResource == null)
        {
            Debug.LogError("ResourceTally in " + name + ": targetResource == null");
        }
        tallyText = GetComponent<TextMeshProUGUI>();
        if (tallyText == null)
        {
            Debug.LogError("ResourceTally in " + name + ": targetResource == null");
        }
    }

    private void OnEnable()
    {
        Resource.OnTotalChanged += UpdateTally;
    }
    
    private void OnDisable()
    {
        Resource.OnTotalChanged -= UpdateTally;
    }

    private void UpdateTally(Resource res)
    {
        if (res == targetResource)
        {
            tallyText.text = targetResource.GetName() + ": " + targetResource.GetTotal().ToString();
        }
    }
}
