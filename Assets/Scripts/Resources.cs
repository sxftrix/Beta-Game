using UnityEngine;
using System.Collections.Generic;
using System;

public class Resources : MonoBehaviour
{
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
    
    private Dictionary<string, int> _resourceList = new Dictionary<string, int>();
    private int mainLevel;
    
    public static event Action<string, int> OnResourceChanged;

    public void AddNewResource(string resourceName)
    {
        _resourceList.Add(resourceName, 0);
    }

    public int GetResource(string resourceName)
    {
        return _resourceList[resourceName];
    }

    public void GainResource(string resourceName, int gainAmount)
    {
        _resourceList[resourceName] += gainAmount;
        OnResourceChanged?.Invoke(resourceName, _resourceList[resourceName]);
    }

    public void LoseResource(string resourceName, int loseAmount)
    {
        _resourceList[resourceName] -= loseAmount;
        OnResourceChanged?.Invoke(resourceName, _resourceList[resourceName]);
    }
}
