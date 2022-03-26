using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewPlayerScriptableObject")]
public class PlayerScriptableObject : Character
{
    
    [Header("Player Movement")]
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;


    [Header("Mouse Rotation")]
    [SerializeField] bool isMouseRotationInverted;
    [SerializeField] float mouseSensitivity;


    [Header("Player Controls")]
    [SerializeField] PlayerControls playerControls;
     


} 
