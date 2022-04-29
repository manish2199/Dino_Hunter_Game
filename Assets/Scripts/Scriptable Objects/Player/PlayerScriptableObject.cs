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
    public AudioClip BattleAudioClip;
    public AudioClip GamePlayAudioClip;
    public AudioClip ItemCollectedClip;
    public AudioClip AchievementCompleteClip;

    #endregion


   [Header("Player Controls")]
   public PlayerControls playerControls;


   [Header("Player Health")]
   public int PlayerHealth;
} 

[System.Serializable]
public class PlayerControls
{
    public KeyCode KeyForJump;
    public KeyCode KeyForCrouch;
    public KeyCode KeyForLockCursor;
    public KeyCode KeyForSprint;
    public KeyCode KeyToSelectWeapon1;
    public KeyCode KeyToSelectWeapon2;
    public KeyCode KeyToSelectWeapon3;
    public KeyCode KeyToSelectWeapon4;
    public KeyCode KeyToSelectWeapon5;
    public KeyCode KeyToSelectWeapon6;
    public KeyCode KeyToUseHealthKit;
    public KeyCode WeaponAttackKey;
    public KeyCode KeyToAimWeapon; 
    public KeyCode KeyToOpenInventory;
    public KeyCode KeyToInteractWithObjects;
}


