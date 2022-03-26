using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Axis 
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
}




[System.Serializable]
public class PlayerControls
{
    public KeyCode KeyForJump;
    public KeyCode KeyForCrouch;
    public KeyCode KeyForLockCursor;
}