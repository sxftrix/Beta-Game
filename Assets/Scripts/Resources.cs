using UnityEngine;
using System.Collections.Generic;
using System;

public class Resources : MonoBehaviour
{
    //Singleton: Player Resources are universally accessible, only one can exist
    public static Resources Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    //Dictionary: stores resource types and resource amounts
    private Dictionary<string, int> _resourceList = new Dictionary<string, int>();
    
    //Events: made so other objects can detect if a type of resource changes value and if a new resource is added.
    public static event Action<string, int> OnResourceChanged;
    public static event Action<string> OnUnlockResource;

    //private method so only this script can change the value of the dictionary
    private void AddResource(string resourceName)
    {
        foreach (var resource in _resourceList)
        {
            if (resource.Key == resourceName)
            {
                Debug.Log("Resource: "  + resource.Key + " already exists.");
                return;
            }
        }
        _resourceList.Add(resourceName, 0);
        Debug.Log("Resource: " + resourceName + " added.");
        OnUnlockResource?.Invoke(resourceName);
    }
    
    //public method to add new resource to resource list
    public void AddNewResource(string resourceName)
    {
        AddResource(resourceName);
    }
    
    //public method to get value of a specific resource
    public int GetResource(string resourceName)
    {
        return _resourceList[resourceName];
    }
    
    //public method to add to a specific resource
    public void GainResource(string resourceName, int gainAmount)
    {
        _resourceList[resourceName] += gainAmount;
        OnResourceChanged?.Invoke(resourceName, _resourceList[resourceName]);
    }
    
    //public method to subtract from to a specific resource
    public void LoseResource(string resourceName, int loseAmount)
    {
        _resourceList[resourceName] -= loseAmount;
        OnResourceChanged?.Invoke(resourceName, _resourceList[resourceName]);
    }

    public List<string> GetResourceNames()
    {
        var resourceNames = new List<string>();
        foreach (var resource in _resourceList) {resourceNames.Add(resource.Key);}
        return resourceNames;
    }
}
