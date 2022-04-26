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

    public static event Action<PlayerScriptableObject> OnSprinting;
    public static event Action<PlayerScriptableObject> OnWalking;
    public static event Action<PlayerScriptableObject> OnCrouched;


    public void SetInitialMovementSetup(PlayerScriptableObject playerScriptableObject)
    {
        IsCrouched = false;
        Speed = playerScriptableObject.moveSpeed; 
    }

    public void MovePlayer(PlayerScriptableObject playerScriptableObject,CharacterController characterController,Transform playerTransform)
   { 
      HandleSprint(playerScriptableObject);
   
      MoveDirection = new Vector3( Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL) );
  
      MoveDirection = playerTransform.TransformDirection(MoveDirection);
      MoveDirection *= Speed * Time.deltaTime;
    
      ApplyGravity(playerScriptableObject ,characterController);
     
      Player.Instance.characterController.Move(MoveDirection);
   }




   public void HandleSprint(PlayerScriptableObject playerScriptableObject)
   { 
      if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForSprint) &&  !IsCrouched )
      {
        Speed = playerScriptableObject.sprintSpeed; 
        OnSprinting?.Invoke(playerScriptableObject);
        // Player.Instance.SetSprintFootStepClips(); 

      }
      else if( Input.GetKeyUp(playerScriptableObject.playerControls.KeyForSprint) && !IsCrouched )
      {
        Speed = playerScriptableObject.moveSpeed; 
        OnWalking?.Invoke(playerScriptableObject);
        // Player.Instance.SetWalkingFootStepClips();
      }  
   }


   public void HandleCrouch(PlayerScriptableObject playerScriptableObject,Transform rootTransform)
   { 
        if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForCrouch) )
       {
           if(IsCrouched)
           {
               Speed = playerScriptableObject.moveSpeed;
              //  Player.Instance.SetWalkingFootStepClips();
               OnWalking?.Invoke(playerScriptableObject);

               IsCrouched = false;

               Vector3 temp = rootTransform.localPosition;
               temp.y = playerScriptableObject.standingHeight;
               rootTransform.localPosition = temp;
           }
           else
           {
               Speed = playerScriptableObject.crouchSpeed;
              //  Player.Instance.SetCrouchedFootStepClips();
               OnCrouched?.Invoke(playerScriptableObject);
              
               IsCrouched = true;
              
               Vector3 temp = rootTransform.localPosition;
               temp.y = playerScriptableObject.crouchedHeight;
               rootTransform.localPosition = temp;
           }
       }
   }
   
   public void ApplyGravity(PlayerScriptableObject playerScriptableObject , CharacterController characterController)
  {
    VerticalVelocity  -= playerScriptableObject.gravity * Time.deltaTime;
   
    CheckForJump(characterController,playerScriptableObject) ;
 
    Vector3 temp = MoveDirection;
    temp.y =  VerticalVelocity  * Time.deltaTime;
    MoveDirection = temp; 
  }
  
   public void CheckForJump(CharacterController characterController,PlayerScriptableObject playerScriptableObject)
   {
      if( characterController.isGrounded  && Input.GetKeyDown(playerScriptableObject.playerControls.KeyForJump) ) 
      {
       VerticalVelocity = playerScriptableObject.jumpForce;
      }
   }

}
