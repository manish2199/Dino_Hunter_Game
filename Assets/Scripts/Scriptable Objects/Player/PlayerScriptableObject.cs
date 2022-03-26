using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewPlayerScriptableObject")]
public class PlayerScriptableObject : Character
{
    
    [Header("Player Movement")]
    public float gravity;
    public float jumpForce;


    [Header("Mouse Rotation")]
    public bool isMouseRotationInverted;
    public float mouseSensitivity;
    public Vector2 defaultLookLimits;


    [Header("Player Controls")]
    public PlayerControls playerControls;
     


} 
