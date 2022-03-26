using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController 
{
  public PlayerModel   PlayerModel   {   get;  protected set;  }

  public PlayerView   PlayerView       {   get;  protected set;  }

  public PlayerController( PlayerModel playerModel , PlayerView playerView , Transform positionToInstantiate ) 
  {
      PlayerModel = playerModel;
      PlayerView = GameObject.Instantiate<PlayerView>(playerView,positionToInstantiate.position,Quaternion.identity);
      PlayerView.playerController = this;
      PlayerModel.MouseUnlock = true;    
  }


 public void MovePlayer()
{
    PlayerModel.MoveDirection = new Vector3( Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL) );

   PlayerModel.MoveDirection = PlayerView.playerTransform.TransformDirection(PlayerModel.MoveDirection);
   PlayerModel.MoveDirection *= PlayerModel.Speed * Time.deltaTime;

   ApplyGravity();
    
   PlayerView.characterController.Move(PlayerModel.MoveDirection);
}


public void ApplyGravity()
{
    PlayerModel.VerticalVelocity  -= PlayerModel.Gravity * Time.deltaTime;
   
    Jump() ;
 
    Vector3 temp = PlayerModel.MoveDirection;
    temp.y =  PlayerModel.VerticalVelocity  * Time.deltaTime;
    PlayerModel.MoveDirection = temp; 
}


public void Jump()
{
   if( PlayerView.characterController.isGrounded  && Input.GetKeyDown(PlayerModel.PlayerControls.KeyForJump) ) 
   {
         PlayerModel.VerticalVelocity = PlayerModel.JumpForce;
   }
}


public void LockMouseCurserToCenter()
{
   Cursor.lockState = CursorLockMode.Locked;
}


public void LockAndUnlockCursor()
{
   if( Input.GetKeyDown(PlayerModel.PlayerControls.KeyForLockCursor) )
    {
       if(isCursorLocked())
       {
          Cursor.lockState = CursorLockMode.None;
       }
       else
       {
           Cursor.visible = false;
           Cursor.lockState = CursorLockMode.Locked;
       } 
    }
}



private bool isCursorLocked()
{
  if(Cursor.lockState == CursorLockMode.Locked)
    {
      return true;
    }
  return false;
}


public void LookAround()
{
   PlayerModel.CurrentMouseLook = new Vector2 (Input.GetAxis(Axis.MOUSEY),Input.GetAxis(Axis.MOUSEX));

   PlayerModel.MouseLookAngles.x += PlayerModel.CurrentMouseLook.x * PlayerModel.MouseSensitivity * (PlayerModel.IsMouseInverted ? 1f : -1f ) ;  
   PlayerModel.MouseLookAngles.y += PlayerModel.CurrentMouseLook.y * PlayerModel.MouseSensitivity ;  

   PlayerModel.MouseLookAngles.x = Mathf.Clamp(PlayerModel.MouseLookAngles.x,PlayerModel.defaultLookLimits.x,PlayerModel.defaultLookLimits.y);

   
   PlayerView.rootTransform .localRotation = Quaternion.Euler(PlayerModel.MouseLookAngles.x, 0f, 0f );
   PlayerView.playerTransform .localRotation = Quaternion.Euler( 0f, PlayerModel.MouseLookAngles.y, 0f );

}


public void MouseLookAround()
{
  LockAndUnlockCursor();
 

  if(isCursorLocked())
  {
     LookAround();
  }
}



public void PlayerMovement()
{
   MovePlayer();

//    ApplyGravity();
}


}
