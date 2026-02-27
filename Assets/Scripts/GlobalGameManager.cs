using UnityEngine;
using UnityEngine.Serialization;

public class GlobalGameManager : MonoBehaviour
{
    public static GlobalGameManager Instance;

    private void Awake()
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
    
    [Header("Required GameObjects")]
    [SerializeField] private GameObject mainSettlement;
    [SerializeField] private GameObject lumberMill;
    [SerializeField] private GameObject fishingPort;
    [SerializeField] private GameObject gardens;
    
    
    [Header("Main Settlement Level")]
    [SerializeField] private int _mainLevel;

    private void OnEnable()
    {
        SettlementBuilding.OnMainLevelUp += MainLevelUp;
    }

    private void Start()
    {
        UnlockBuilding(mainSettlement);
    }

    private void UnlockBuilding(GameObject building)
    {
        building.SetActive(true);
    }

    private void MainLevelUp(int newLevel)
    {
        _mainLevel = newLevel;
        Debug.Log("Main Settlement Level now: " + _mainLevel);
        CheckForNewUnlocks();
    }

    private void CheckForNewUnlocks()
    {
        switch (_mainLevel)
        {
            case 5:
            {
                UnlockBuilding(lumberMill);
                break;
            }
            case 10:
            {
                UnlockBuilding(fishingPort);
                break;
            }
            case 15:
            {
                UnlockBuilding(gardens);
                break;
            }
        }
        
    }
    
}
