using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMouseLookController : MonoBehaviour
{
    // job is to control mouse look
   
    private Vector2 DefaultLookLimits;
    
    private Vector2 MouseLookAngles;
    
    private Vector2 CurrentMouseLook;
   

   public void InitializeMouseLook()
   {
      LockMouseCurserToCenter();
   }

   public void LockMouseCurserToCenter() 
   {
      Cursor.lockState = CursorLockMode.Locked;
   }

   public void LockAndUnlockCursor(PlayerScriptableObject playerScriptableObject)
   {
      if( Input.GetKeyDown(playerScriptableObject.playerControls.KeyForLockCursor) )
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

   public bool isCursorLocked()
   {
     if(Cursor.lockState == CursorLockMode.Locked)
     {
       return true;
     }
     return false;
   }


   public void LookAround(PlayerScriptableObject playerScriptableObject,Transform rootTransform,Transform playerTransform)
   {
      CurrentMouseLook = new Vector2 (Input.GetAxis(Axis.MOUSEY),Input.GetAxis(Axis.MOUSEX));

      MouseLookAngles.x += CurrentMouseLook.x * playerScriptableObject.mouseSensitivity * (playerScriptableObject.isMouseRotationInverted ? 1f : -1f ) ;  
      MouseLookAngles.y += CurrentMouseLook.y * playerScriptableObject.mouseSensitivity ;  

      MouseLookAngles.x = Mathf.Clamp(MouseLookAngles.x,playerScriptableObject.defaultLookLimits.x,playerScriptableObject.defaultLookLimits.y);

      rootTransform.localRotation = Quaternion.Euler(MouseLookAngles.x, 0f, 0f );
      playerTransform.localRotation = Quaternion.Euler( 0f,MouseLookAngles.y, 0f );
   }
}