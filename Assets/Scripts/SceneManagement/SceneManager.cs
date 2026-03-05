using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManagement : MonoBehaviour
{ 
    public static GlobalSceneManagement Instance;

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

    private Scene currentScene;
    
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public Scene GetCurrentScene()
    {
        return  currentScene;
    }
    
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void AddScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
    }
    
    
}
