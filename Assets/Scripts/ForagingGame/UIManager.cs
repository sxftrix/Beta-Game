using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    public TextMeshProUGUI scrapText;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScrapText(int totalScrap)
    {
        scrapText.text = totalScrap.ToString();
    }

    public void UpdateGoldText(int totalGold)
    {
        goldText.text = totalGold.ToString();
    }
}
