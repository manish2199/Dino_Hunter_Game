using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLookController : MonoBehaviour
{
    // job is to control mouse look
     
    private PlayerScriptableObject playerScriptableObject;
   
    private Vector2 DefaultLookLimits;
    
    private Vector2 MouseLookAngles;
    
    private Vector2 CurrentMouseLook;
   
   void Start()
   {
       playerScriptableObject  = Player.Instance.PlayerScriptableObject;
       LockMouseCurserToCenter();
   }

   public void LockMouseCurserToCenter() 
   {
      Cursor.lockState = CursorLockMode.Locked;
   }

   private void LockAndUnlockCursor()
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


   private void LookAround()
   {
      CurrentMouseLook = new Vector2 (Input.GetAxis(Axis.MOUSEY),Input.GetAxis(Axis.MOUSEX));

      MouseLookAngles.x += CurrentMouseLook.x * playerScriptableObject.mouseSensitivity * (playerScriptableObject.isMouseRotationInverted ? 1f : -1f ) ;  
      MouseLookAngles.y += CurrentMouseLook.y * playerScriptableObject.mouseSensitivity ;  

      MouseLookAngles.x = Mathf.Clamp(MouseLookAngles.x,playerScriptableObject.defaultLookLimits.x,playerScriptableObject.defaultLookLimits.y);

      Player.Instance.rootTransform.localRotation = Quaternion.Euler(MouseLookAngles.x, 0f, 0f );
      Player.Instance.playerTransform.localRotation = Quaternion.Euler( 0f,MouseLookAngles.y, 0f );
   }

   public void MouseLookAround()
   {
    LockAndUnlockCursor();
 
    if(isCursorLocked())
     {
       LookAround();
     }
  }
}