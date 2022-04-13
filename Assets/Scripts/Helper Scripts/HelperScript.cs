using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Axis 
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string MOUSEX = "Mouse X";
    public const string MOUSEY = "Mouse Y";
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
    public KeyCode WeaponAttackKey;
    public KeyCode KeyToAimWeapon; 
    public KeyCode KeyToOpenInventory;
}


public class WeaponAnimatorParameters
{
    public const string AimBooleanText = "AIM";
    public const string ShootTriggerText = "Shoot";
}