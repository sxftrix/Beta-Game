using UnityEngine;

public class Tree : MonoBehaviour, IChop
{
    private int scrapYield = 1;
    private int goldYield = 1;

    public void Chop()
    {
        if (GameManager.Instance.reachedMaxScrap())
        {
            GameManager.Instance.GainGold(goldYield);
        }
        else
        {
            GameManager.Instance.GainScrap(scrapYield);
        }
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        return;
    }

    public void SetScrapYield (int newYield)
    {
        scrapYield = newYield;

    }

    public void SetGoldYield(int newYield)
    {
        goldYield = newYield;

    }
}
