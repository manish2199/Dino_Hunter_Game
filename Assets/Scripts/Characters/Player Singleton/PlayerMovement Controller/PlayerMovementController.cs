using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{   
   
    // only one job is to handle player movements
   
    private PlayerScriptableObject playerScriptableObject ;

    [HideInInspector] public float Speed;

    [HideInInspector] public Vector3 MoveDirection { get; set; }

    [HideInInspector] public float VerticalVelocity { get; set; }


    void Start()
    {   
        Player.Instance.IsCrouched = false;
        playerScriptableObject  = Player.Instance.PlayerScriptableObject;
        Speed = playerScriptableObject.moveSpeed; 
    }

    public void MovePlayer()
   { 
      HandleSprint();
   
      MoveDirection = new Vector3( Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL) );
  
     MoveDirection = transform.TransformDirection(MoveDirection);
     MoveDirection *= Speed * Time.deltaTime;
    
      ApplyGravity();
     
      Player.Instance.characterController.Move(MoveDirection);
   }

   public void HandleSprint()
   { 
      if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForSprint) && !Player.Instance.IsCrouched )
      {
        Speed = playerScriptableObject.sprintSpeed; 
        Player.Instance.SetSprintFootStepClips();
      }
      else if( Input.GetKeyUp(playerScriptableObject.playerControls.KeyForSprint) && !Player.Instance.IsCrouched )
      {
        Speed = playerScriptableObject.moveSpeed; 
        Player.Instance.SetWalkingFootStepClips();
      }  
   }


   public void HandleCrouch()
   { 
        if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForCrouch) )
       {
           if(Player.Instance.IsCrouched)
           {
               Speed = playerScriptableObject.moveSpeed;
               Player.Instance.SetWalkingFootStepClips();
                
               Player.Instance.IsCrouched = false;

               Vector3 temp = Player.Instance.rootTransform.localPosition;
               temp.y = playerScriptableObject.standingHeight;
               Player.Instance.rootTransform.localPosition = temp;
           }
           else
           {
               Speed = playerScriptableObject.crouchSpeed;
               Player.Instance.SetCrouchedFootStepClips();
             
               Player.Instance.IsCrouched = true;
              
               Vector3 temp = Player.Instance.rootTransform.localPosition;
               temp.y = playerScriptableObject.crouchedHeight;
               Player.Instance.rootTransform.localPosition = temp;
           
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
      if( Player.Instance.characterController.isGrounded  && Input.GetKeyDown(playerScriptableObject.playerControls.KeyForJump) ) 
      {
       VerticalVelocity = playerScriptableObject.jumpForce;
      }
   }

   public void PlayerMovement()
   {
     MovePlayer();

     HandleCrouch();
   }

}
