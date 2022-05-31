using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMouseLookController : MonoBehaviour
{
   // job is to control mouse look
   
   private Vector2 DefaultLookLimits;
    
   private Vector2 MouseLookAngles;
    
   private Vector2 CurrentMouseLook;
    
   PlayerScriptableObject playerScriptableObject;
   
   Transform playerTransform;    
   Transform rootTransform;

   public void InitializeMouseLook()
   {
      playerScriptableObject = Player.Instance.PlayerScriptableObject;
      playerTransform =  Player.Instance.playerTransform;
      rootTransform =  Player.Instance.rootTransform;
      LockMouseCurserToCenter();
   }

   public void LockMouseCurserToCenter() 
   {
      Cursor.lockState = CursorLockMode.Locked;
   }

   public void LockAndUnlockCursor()
   {
      if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForLockCursor) )
     {
       if(isCursorLocked())
        {
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true ;
         }
       else
        {
           Cursor.visible = false;
           Cursor.lockState = CursorLockMode.Locked;
        } 
     }
   }

   public bool isCursorLocked()
   {
     if(Cursor.lockState == CursorLockMode.Locked)
     {
       return true;
     }
     return false;
   }


   public void LookAround()
   {
      CurrentMouseLook = new Vector2 (Input.GetAxis(Axis.MOUSEY),Input.GetAxis(Axis.MOUSEX));

      MouseLookAngles.x += CurrentMouseLook.x * playerScriptableObject.mouseSensitivity * (playerScriptableObject.isMouseRotationInverted ? 1f : -1f ) ;  
      MouseLookAngles.y += CurrentMouseLook.y * playerScriptableObject.mouseSensitivity ;  

      MouseLookAngles.x = Mathf.Clamp(MouseLookAngles.x,playerScriptableObject.defaultLookLimits.x,playerScriptableObject.defaultLookLimits.y);

      rootTransform.localRotation = Quaternion.Euler(MouseLookAngles.x, 0f, 0f );
      playerTransform.localRotation = Quaternion.Euler( 0f,MouseLookAngles.y, 0f );
   }
}

public class Axis 
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string MOUSEX = "Mouse X";
    public const string MOUSEY = "Mouse Y";
}


