using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{   
   
    // only one job is to handle player movements

    public bool IsCrouched { get; set; }    

    [HideInInspector] public float Speed;

    [HideInInspector] public Vector3 MoveDirection { get; set; }

    [HideInInspector] public float VerticalVelocity { get; set; }

    public static event Action OnSprinting;
    public static event Action OnWalking;
    public static event Action OnCrouched;

    PlayerScriptableObject playerScriptableObject;

    CharacterController characterController;
    Transform playerTransform;    
    Transform rootTransform;

    public void SetInitialMovementSetup()
    {
        playerScriptableObject = Player.Instance.PlayerScriptableObject;
        characterController =  Player.Instance.characterController;
        playerTransform =  Player.Instance.playerTransform;
        rootTransform =  Player.Instance.rootTransform;
        IsCrouched = false;
        Speed = playerScriptableObject.moveSpeed; 
    }

    public void MovePlayer()
   { 
      HandleSprint();
   
      MoveDirection = new Vector3( Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL) );
  
      MoveDirection = playerTransform.TransformDirection(MoveDirection);
      MoveDirection *= Speed * Time.deltaTime;
    
      ApplyGravity();
     
      Player.Instance.characterController.Move(MoveDirection);
   }




   public void HandleSprint()
   { 
      if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForSprint) &&  !IsCrouched )
      {
        Speed = playerScriptableObject.sprintSpeed; 
        OnSprinting?.Invoke(); 
      }
      else if( Input.GetKeyUp(playerScriptableObject.playerControls.KeyForSprint) && !IsCrouched )
      {
        Speed = playerScriptableObject.moveSpeed; 
        OnWalking?.Invoke();
      }  
   }


   public void HandleCrouch()
   { 
        if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForCrouch) )
       {
           if(IsCrouched)
           {
               Speed = playerScriptableObject.moveSpeed;
               OnWalking?.Invoke();

               IsCrouched = false;

               Vector3 temp = rootTransform.localPosition;
               temp.y = playerScriptableObject.standingHeight;
               rootTransform.localPosition = temp;
           }
           else
           {
               Speed = playerScriptableObject.crouchSpeed;
               OnCrouched?.Invoke();
              
               IsCrouched = true;
              
               Vector3 temp = rootTransform.localPosition;
               temp.y = playerScriptableObject.crouchedHeight;
               rootTransform.localPosition = temp;
           }
       }
   }
   
   public void ApplyGravity()
  {
    VerticalVelocity  -= playerScriptableObject.gravity * Time.deltaTime;
   
    CheckForJump() ;
 
    Vector3 temp = MoveDirection;
    temp.y =  VerticalVelocity  * Time.deltaTime;
    MoveDirection = temp; 
  }
  
  public void CheckForJump()
  {
      if( characterController.isGrounded  && Input.GetKeyDown(playerScriptableObject.playerControls.KeyForJump) ) 
      {
         VerticalVelocity = playerScriptableObject.jumpForce;
      }
  }

}
