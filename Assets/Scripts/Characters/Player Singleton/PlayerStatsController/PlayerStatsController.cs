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
      
       print(PlayerHealth);
       float damageDealed = (Player.Instance.PlayerScriptableObject.PlayerHealth - PlayerHealth );

       GameplayUIManager.Instance.UpdateDamageIndicator(damageDealed);

       if(PlayerHealth <= 0)
       {
           Debug.Log("Player Is Dead ");
       }

    }
}
