using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;

    public void CamZoom(bool canZoom)
    {
      if(canZoom)
      {
        // play zoom in animation
        playerAnimator.SetBool("Zoom",true);
      }
      else
       {
       // play zoom out animation
       playerAnimator.SetBool("Zoom",false);
       }
  }
  
}
