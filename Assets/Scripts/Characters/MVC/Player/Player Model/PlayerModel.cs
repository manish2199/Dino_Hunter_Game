using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
   public PlayerModel(PlayerScriptableObject playerScriptableObject)
  {
        Speed = playerScriptableObject.speed;
        Gravity = playerScriptableObject.gravity ;   
        JumpForce = playerScriptableObject.jumpForce;   
        IsMouseInverted  = playerScriptableObject.isMouseRotationInverted;
        MouseSensitivity  =  playerScriptableObject.mouseSensitivity;
        PlayerControls = playerScriptableObject.playerControls;
        defaultLookLimits = playerScriptableObject.defaultLookLimits;

  }
  
  public float Speed {  get; } 
  
  public float Gravity { get; }

  public float JumpForce { get; }

  public Vector3 MoveDirection { get; set; } 

  public float VerticalVelocity { get; set; }

  public bool IsMouseInverted { get; } 

  public Vector2 defaultLookLimits;

  public float MouseSensitivity  { get; }    // 5
 
  public PlayerControls PlayerControls { get; } 

  public bool MouseUnlock { get; set; }

  public Vector2 MouseLookAngles;

  public Vector2 CurrentMouseLook;

         
 // int smooth_steps = 10;
 // float smooth_weight = 0.4f;
 // vector2 default_look_limits;
 // vector2 curr _mouse_look;
 // vector2 smooth_move
 // int last_look_frame;   

}
