using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
   public CharacterController  characterController;

   public PlayerController  playerController;

   public  Transform playerTransform;

   public  Transform rootTransform;
 
  
  void Start()
  {
     playerController.LockMouseCurserToCenter();
  }   

  void Update()
  {
     playerController.PlayerMovement();
     
     playerController.MouseLookAround(); 
  }
   
}
