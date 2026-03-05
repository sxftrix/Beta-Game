using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GlobalMinigameHandler : SerializedMonoBehaviour
{
    public static GlobalMinigameHandler Instance;
    
    [Header("REQUIRED: Minigame List")]
    [SerializeField] private List<string> minigameList = new List<string>(); //Add Scene name to Build Profiles and in this list through Inspector
    
    public static event Action OnMinigameStart;
    public static event Action OnMinigameEnd;
    
    private string currentMinigame;
    private bool minigamePlaying = false;
    
    private void Awake()
    {
        BuildingMinigame.OnStartMinigame += LaunchMinigame;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LaunchMinigame(string chosenGame)
    {
        foreach (string minigame in minigameList)
        {
            if (minigame == chosenGame)
            {
                SceneManager.LoadScene(chosenGame);
            }
        }
    }
}
