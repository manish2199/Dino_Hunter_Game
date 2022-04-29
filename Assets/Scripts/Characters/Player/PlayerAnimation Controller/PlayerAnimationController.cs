using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public void CamZoom(bool canZoom)
    {
      if(canZoom)
      {
        // play zoom in animation
        Player.Instance.PlayerAnimator.SetBool("Zoom",true);
      }
      else
       {
       // play zoom out animation
       Player.Instance.PlayerAnimator.SetBool("Zoom",false);
       }
  }
  
}

