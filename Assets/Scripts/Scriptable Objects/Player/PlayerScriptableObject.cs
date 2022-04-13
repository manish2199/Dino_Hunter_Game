using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewPlayerScriptableObject")]
public class PlayerScriptableObject : Character
{   

   #region Movement 
    [Header("Player Movement")]
    public float gravity;
    public float jumpForce;
    public float crouchSpeed;
    public float sprintSpeed; 
    public float crouchedHeight;
    public float standingHeight;
    #endregion 

   #region MouseLook 
    [Header("Player Mouse Look")]
    public bool isMouseRotationInverted;
    public float mouseSensitivity;
    public Vector2 defaultLookLimits;
    #endregion

   #region Audio     
    [Header("PlayerAudio Settings")]
    public AudioClip[] FootStepClips;
    public float minWalkVolume,maxWalkVolume;
    public float sprintVolume;
    public float crouhVolume;
    public float walkStepDistance;
    public float sprintStepDistance;
    public float crouchStepDistance;
    #endregion


   [Header("Player Controls")]
   public PlayerControls playerControls;


   [Header("Player Health")]
   public int PlayerHealth;
} 
