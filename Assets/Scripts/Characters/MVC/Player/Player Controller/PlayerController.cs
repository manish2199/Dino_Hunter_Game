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
      PlayerModel.MouseUnlock = true;    
      PlayerView = GameObject.Instantiate<PlayerView>(playerView,positionToInstantiate.position,Quaternion.identity);
      PlayerView.playerController = this;
  }


   #region Attack
   public void SelectWeapon()
   {
      if(Input.GetKeyDown(PlayerModel.PlayerControls.KeyToSelectWeapon1))
      {
        // WeaponService.Instance.ActivateWeapon(PlayerView.rootTransform,0);
         PlayerService.Instance.SelectWeapons(PlayerView.rootTransform,0);
      }
      if(Input.GetKeyDown(PlayerModel.PlayerControls.KeyToSelectWeapon2))
      {
        // WeaponService.Instance.ActivateWeapon(PlayerView.rootTransform,1);
         PlayerService.Instance.SelectWeapons(PlayerView.rootTransform,1);
      }
      // if(Input.GetKeyDown(PlayerModel.playerControls.KeyToSelectWeapon3))
      // {
      //    WeaponService.ActivateWeapon(PlayerView.rootTransform);
      // }
      // if(Input.GetKeyDown(PlayerModel.playerControls.KeyToSelectWeapon4))
      // {
      //    WeaponService.ActivateWeapon(PlayerView.rootTransform);
      // }
      // if(Input.GetKeyDown(PlayerModel.playerControls.KeyToSelectWeapon5))
      // {
      //    WeaponService.ActivateWeapon(PlayerView.rootTransform);
      // }
      // if(Input.GetKeyDown(PlayerModel.playerControls.KeyToSelectWeapon6))
      // {
      //    WeaponService.ActivateWeapon(PlayerView.rootTransform);
      // }
   }

    public void SelectInitialWeapon(Transform fps)
    {
      if(fps != null)
      {
        PlayerService.Instance.SelectInitialWeapon(fps);
      }
    }    
   #endregion
     
    
   
   # region MovementController
   public void MovePlayer()
   { 
     HandleSprint();
   
     PlayerModel.MoveDirection = new Vector3( Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL) );
    

     PlayerModel.MoveDirection = PlayerView.playerTransform.TransformDirection(PlayerModel.MoveDirection);
     PlayerModel.MoveDirection *= PlayerModel.Speed * Time.deltaTime;
    
      ApplyGravity();
     
    //  Debug.Log(PlayerModel.MoveDirection);
     PlayerView.characterController.Move(PlayerModel.MoveDirection);
     
   }

   public void HandleSprint()
   { 
      if( Input.GetKeyDown(PlayerModel.PlayerControls.KeyForSprint) && !PlayerModel.IsCrouched )
      {
        PlayerModel.Speed = PlayerModel.SprintSpeed; 

        PlayerModel.MinFootStepVolume = PlayerModel.SprintVolume;
        PlayerModel.MaxFootStepVolume = PlayerModel.SprintVolume;
        PlayerModel.StepDistance =PlayerModel.SprintStepDistance;
      }
      else if( Input.GetKeyUp(PlayerModel.PlayerControls.KeyForSprint) && !PlayerModel.IsCrouched )
      {
        PlayerModel.Speed = PlayerModel.MoveSpeed; 

        PlayerModel.MinFootStepVolume = PlayerModel.MinWalkVolume;
        PlayerModel.MaxFootStepVolume = PlayerModel.MaxWalkVolume;
        PlayerModel.StepDistance =PlayerModel.WalkStepDistance;

      }
   }

   public void HandleCrouch()
   { 
       if( Input.GetKeyDown(PlayerModel.PlayerControls.KeyForCrouch) )
       {
           if(PlayerModel.IsCrouched)
           {
               PlayerModel.Speed = PlayerModel.MoveSpeed;
               PlayerModel.IsCrouched = false;
               Vector3 temp = PlayerView.rootTransform.localPosition;
               temp.y = PlayerModel.StandingHeight;
               PlayerView.rootTransform.localPosition = temp;

               PlayerModel.MinFootStepVolume = PlayerModel.MinWalkVolume;
               PlayerModel.MaxFootStepVolume = PlayerModel.MaxWalkVolume;
               PlayerModel.StepDistance =PlayerModel.WalkStepDistance;
           }
           else
           {
               PlayerModel.Speed = PlayerModel.CrouchSpeed;
               PlayerModel.IsCrouched = true;
               Vector3 temp = PlayerView.rootTransform.localPosition;
               temp.y = PlayerModel.CrouchHeight;
               PlayerView.rootTransform.localPosition = temp;

               PlayerModel.MinFootStepVolume = PlayerModel.CrouhVolume;
               PlayerModel.MaxFootStepVolume = PlayerModel.CrouhVolume;
               PlayerModel.StepDistance =PlayerModel.CrouchStepDistance;
           }
       }
   }
   
   public void ApplyGravity()
  {
    PlayerModel.VerticalVelocity  -= PlayerModel.Gravity * Time.deltaTime;
   
    CheckForJump() ;
 
    Vector3 temp = PlayerModel.MoveDirection;
    temp.y =  PlayerModel.VerticalVelocity  * Time.deltaTime;
    PlayerModel.MoveDirection = temp; 
  }
  
   public void CheckForJump()
   {
     if( PlayerView.characterController.isGrounded  && Input.GetKeyDown(PlayerModel.PlayerControls.KeyForJump) ) 
     {
       PlayerModel.VerticalVelocity = PlayerModel.JumpForce;
      }
   }

   public void PlayerMovement()
   {
     MovePlayer();

     HandleCrouch();
   }
   #endregion


   # region MouseLookController
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
   #endregion

  #region  FootStepAudioController
  public void PlayFootStepAudio()
  {
    if(!PlayerView.characterController.isGrounded)
     { 
       return;
     }

     if(PlayerView.characterController.velocity.sqrMagnitude > 0 )
     {
        PlayerModel.AccumulatedStepDistance += Time.deltaTime;

        if(PlayerModel.AccumulatedStepDistance > PlayerModel.StepDistance)
        {
           PlayerView.PlayerFootStepAudio.volume = Random.Range(PlayerModel.MinFootStepVolume,PlayerModel.MaxFootStepVolume);
           PlayerView.PlayerFootStepAudio.clip = PlayerModel.FootStepClips[Random.Range(0 , PlayerModel.FootStepClips.Length)];
           PlayerView.PlayerFootStepAudio.Play();

           PlayerModel.AccumulatedStepDistance = 0f;
         }
      }
     else
     {
        PlayerModel.AccumulatedStepDistance = 0f;
      }
   }

  public void SetInitialFootStepAudio()
  {
    PlayerModel.StepDistance= PlayerModel.WalkStepDistance;
    PlayerModel.MinFootStepVolume = PlayerModel.MinWalkVolume;
    PlayerModel.MaxFootStepVolume = PlayerModel.MaxWalkVolume;
   }
   #endregion


  public void InitialSetUp()
  {
     PlayerModel.Speed = PlayerModel.MoveSpeed; 

     LockMouseCurserToCenter();

     SetInitialFootStepAudio();
  }

}
