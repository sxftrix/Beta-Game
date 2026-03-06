using UnityEngine;

public class UISetAdder : MonoBehaviour
{
    public static UISetAdder Instance;

    [Header("REQUIRED: UI Set Prefab")] 
    [SerializeField] private GameObject UISetPrefab;
    [SerializeField] private Canvas MainGameCanvas;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    private void OnEnable()
    {
        MainGameManager.OnBuildingConstructed += UISetup;
    }
    
    private void OnDisable()
    {
        MainGameManager.OnBuildingConstructed -= UISetup;
    }
    
    private void UISetup(GameObject building)
    {
        var uiLoc = building.transform;
        var uiSet = Instantiate(UISetPrefab, uiLoc.position, uiLoc.rotation);
        uiSet.transform.SetParent(MainGameCanvas.transform, false);
        uiSet.transform.position = uiLoc.position;
        uiSet.GetComponent<BuildingUISet>().InitializeUI(building.GetComponent<Building>());
        
    }
}
