using UnityEngine;

public class Tree : MonoBehaviour, IChop
{
    private int scrapYield = 1;

    public void Chop()
    {
        GameManager.Instance.GainScrap(scrapYield);
        Destroy(gameObject);
    }

    public void SetScrapYield (int newYield)
    {
        scrapYield = newYield;
    }
}
