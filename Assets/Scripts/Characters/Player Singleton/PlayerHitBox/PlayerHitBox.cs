using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour ,  IDamagable
{
   public void TakeDamage(int damage)
   {
      print("Taken Damage");
      Player.Instance.TakeDamage(damage);     
   } 
    
}
