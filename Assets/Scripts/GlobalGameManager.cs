using System;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    public static GlobalGameManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private int MainLevel = 0;

    public static event Action<int> OnMainLevelUp;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnMainLevelUp?.Invoke(MainLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLeveL(int newLevel)
    {
        MainLevel = newLevel;
        OnMainLevelUp?.Invoke(MainLevel);
    }
}
