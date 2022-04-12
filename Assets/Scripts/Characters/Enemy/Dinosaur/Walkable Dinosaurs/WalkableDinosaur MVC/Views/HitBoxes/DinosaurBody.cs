using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurBody : MonoBehaviour , IDamagable
{
   [SerializeField] DinosaurBodyPartType dinosaurBodyPart;

    public void TakeDamage(int damage)
   {
      if(dinosaurBodyPart == DinosaurBodyPartType.Head)
      {
          // recieve double damage
      }
      else if( dinosaurBodyPart == DinosaurBodyPartType.MainBody)
      {
         // recieve less damage than head
      }
   }
}
