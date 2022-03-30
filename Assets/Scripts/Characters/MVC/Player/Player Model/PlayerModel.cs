using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
      public PlayerModel(PlayerScriptableObject playerScriptableObject)
     {     
        #region  Movement
        MoveSpeed = playerScriptableObject.moveSpeed;
        SprintSpeed = playerScriptableObject.sprintSpeed;
        CrouchSpeed = playerScriptableObject.crouchSpeed;
        Gravity = playerScriptableObject.gravity ;   
        JumpForce = playerScriptableObject.jumpForce;   
        CrouchHeight = playerScriptableObject.crouchedHeight;
        StandingHeight = playerScriptableObject.standingHeight;
        #endregion
       
        # region MouseLook
        IsMouseInverted  = playerScriptableObject.isMouseRotationInverted;
        MouseSensitivity  =  playerScriptableObject.mouseSensitivity;
        defaultLookLimits = playerScriptableObject.defaultLookLimits;
        #endregion

        # region FootstepsAudio   
        FootStepClips = playerScriptableObject.FootStepClips;
        MinWalkVolume  = playerScriptableObject.minWalkVolume;
        MaxWalkVolume = playerScriptableObject.maxWalkVolume;
        SprintVolume = playerScriptableObject.sprintVolume;
        CrouhVolume = playerScriptableObject.crouhVolume;
        WalkStepDistance = playerScriptableObject.walkStepDistance;
        SprintStepDistance = playerScriptableObject.sprintStepDistance;
        CrouchStepDistance = playerScriptableObject.crouchStepDistance;
        # endregion

        PlayerControls = playerScriptableObject.playerControls;
     }

  # region MovementProperties
  public float Speed { get; set; }
  public float MoveSpeed {  get; } 
  public float CrouchSpeed {  get; } 
  public float SprintSpeed {  get; } 
  public float CrouchHeight {  get; } 
  public float StandingHeight {  get; } 
  public float Gravity { get; }
  public float JumpForce { get; }
  public Vector3 MoveDirection { get; set; } 
  public float VerticalVelocity { get; set; }
  public bool IsCrouched { get; set; }
  #endregion

  # region MouseLookProperties
  public bool IsMouseInverted { get; } 
  public Vector2 defaultLookLimits;
  public float MouseSensitivity  { get; }  
  public bool MouseUnlock { get; set; }
  public Vector2 MouseLookAngles;
  public Vector2 CurrentMouseLook;
  #endregion
 
  # region FootStepAudioProperty
  public float MinFootStepVolume { get; set; }
  public float MaxFootStepVolume { get; set; }
  public float StepDistance { get; set; }
  public float AccumulatedStepDistance { get; set; }
  public AudioClip[] FootStepClips { get;}
  public float MinWalkVolume { get; } 
  public float MaxWalkVolume { get;}
  public float SprintVolume { get; }
  public float CrouhVolume { get; }
  public float WalkStepDistance { get; }
  public float SprintStepDistance { get; }
  public float CrouchStepDistance { get; }
  # endregion

  public PlayerControls PlayerControls { get; }
}


// int smooth_steps = 10;
 // float smooth_weight = 0.4f;
 // vector2 default_look_limits;
 // vector2 curr _mouse_look;
 // vector2 smooth_move
 // int last_look_frame;   
