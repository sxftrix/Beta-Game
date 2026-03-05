using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GlobalMinigameHandler : SerializedMonoBehaviour
{
    public static GlobalMinigameHandler Instance;
    private string currentMinigameName;
    private bool minigamePlaying = false;
    
    
    [Header("Resource Stock Dictionary (For Reference Only")]
    [SerializeField] private Dictionary<string, int> _earnedResources = new Dictionary<string, int>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void InitializeMinigameEarnables (List<string> resources)
    {
        foreach (string resourceName in resources)
        {
            _earnedResources.Add(resourceName, 0);
        }
    }

    private void OnMinigameEnd()
    {
        foreach (var resource in _earnedResources)
        {
            ResourceManager.Instance.GainResource(resource.Key, resource.Value);
        }
    }
}
