using UnityEngine;

public class MainSettlement : MonoBehaviour
{
   public static MainSettlement Instance { get; private set; }

   private int mainLevel;
   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(this);
         return;
      }
      Instance = this;
   }

   public void updateMainLevel(int newLevel)
   { 
      mainLevel = newLevel;
   }
   
   public int GetMainLevel => mainLevel;
}
