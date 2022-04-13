using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
   private int PlayerScore;

   private int PlayerHealth;

   void Start()
   {
       PlayerHealth = Player.Instance.PlayerScriptableObject.PlayerHealth;
   }


   void Update()
   {
      if(Input.GetKeyDown(Player.Instance.PlayerScriptableObject.playerControls.KeyToOpenInventory))
      {
          OpenInventorySystem();
      }
   }

   public void OpenInventorySystem()
   {
       GameplayUIManager.Instance.ActivateInventory();
   }



    public void TakeDamage(int damage)
    {
       PlayerHealth -= damage;

       print(PlayerHealth);      
     
       if(PlayerHealth <= 0)
       {
           Debug.Log("Player Is Dead ");
       }

    }
}
