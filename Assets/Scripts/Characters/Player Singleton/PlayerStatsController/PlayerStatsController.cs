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
      OpenInventorySystem();

      CollectFromSupplies();
   }

   public void ResetPlayerHealth()
   {
        PlayerHealth = Player.Instance.PlayerScriptableObject.PlayerHealth;
        GameplayUIManager.Instance.UpdateDamageIndicator(PlayerHealth);
   }

   public void ResetScore()
   {
       PlayerScore = 0;
   }
 
   private void UseMedikit()
   {
        if(Input.GetKeyDown(Player.Instance.PlayerScriptableObject.playerControls.KeyToUseHealthKit))
        {
            // demand healthkit from inventory 
            if(InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit) == 0 )
            {
                return;
            }

           if(PlayerHealth < 100 )
           {   
              PlayerHealth += InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit);


              if(PlayerHealth > 100)
              {
                PlayerHealth = 100;
              }

              float damageLeft = (Player.Instance.PlayerScriptableObject.PlayerHealth - PlayerHealth );

              GameplayUIManager.Instance.UpdateDamageIndicator(damageLeft);
           }
        }
   }


   public void CollectFromSupplies()
   {
       RaycastHit hit;

       if(Physics.Raycast(Player.Instance.PlayerCollectableTransform.position,Player.Instance.PlayerCollectableTransform.forward, out hit,3f))
       {
           ICollectable collectable = hit.transform.gameObject.GetComponent<ICollectable>();

           if(collectable != null && Input.GetKeyDown(Player.Instance.PlayerScriptableObject.playerControls.KeyToInteractWithObjects))
           { 
              collectable.Collect();
           }
       }
   
   }

   public void IncreaseScore(WalkingDinosaurType walkableDinosaurType)
   {
       if(walkableDinosaurType == WalkingDinosaurType.Raptors)
       {
           PlayerScore += 20;
       }
       if(walkableDinosaurType == WalkingDinosaurType.TRex)
       {
           PlayerScore += 40;
       }

       GameplayUIManager.Instance.UpdatePlayerScore(PlayerScore);

       if(GameData.GetEasyDifficulty() == 1)
       {
          if( PlayerScore > GameData.GetEasyDifficultyHighScore())
          {
            GameData.SetEasyDifficultyHighScore(PlayerScore);
          }
       }
       if(GameData.GetMediumDifficulty() == 1)
       {
          if( PlayerScore > GameData.GetMediumDifficultyHighScore())
          {
            GameData.SetMediumDifficultyHighScore(PlayerScore);
          }
       }
       if(GameData.GetHardDifficulty() == 1)
       {
          if( PlayerScore > GameData.GetHardDifficultyHighScore())
          {
            GameData.SetHardDifficultyHighScore(PlayerScore);
          }
       }
  
   }




   public void OpenInventorySystem()
   {  
       if(Input.GetKeyDown(Player.Instance.PlayerScriptableObject.playerControls.KeyToOpenInventory))
      {
          GameplayUIManager.Instance.ActivateInventory();
      }
   }



    public void TakeDamage(int damage)
    {
       PlayerHealth -= damage;
      
       float damageDealed = (Player.Instance.PlayerScriptableObject.PlayerHealth - PlayerHealth );

       GameplayUIManager.Instance.UpdateDamageIndicator(damageDealed);

       if(PlayerHealth <= 0)
       {
        //    Debug.Log("Player Is Dead ");
            // handle Player Death 
            GamePlayManager.Instance.PlayerDied();
       }

    }
}
