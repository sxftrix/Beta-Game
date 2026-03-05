using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ResourceManager : SerializedMonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

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
    [Header("Resource Stock Dictionary (For Reference Only")]
    [SerializeField] private Dictionary<string, int> _resourceList = new Dictionary<string, int>();
    
    public static event Action<string, int> OnResourceChanged;
    public static event Action<string> OnResourceUnlocked;

    public void AddNewResource(string resourceName)
    {
        _resourceList.Add(resourceName, 0);
        OnResourceUnlocked?.Invoke(resourceName);
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

    public List<string> GetCurrentResourceTypes()
    {
        List<string> types = new List<string>();
        foreach (var resource in _resourceList)
        {
            types.Add(resource.Key);
        }
        return types;
    }
}
