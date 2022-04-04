using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
   public CharacterController  characterController;

   public PlayerController  playerController;

   public  Transform playerTransform;

   public  Transform rootTransform;

   public Transform weaponsHolder;
 
   public Animator playerAnimator;
  
   [SerializeField] AudioSource playerFootStepAudio;
   public AudioSource PlayerFootStepAudio { get { return playerFootStepAudio; } }
 
  
  void Start()
  {
     playerController.InitialSetUp();

     playerController.SelectInitialWeapon(weaponsHolder); 
  }   

  void Update()
  {
     playerController.PlayerMovement();
     
     playerController.MouseLookAround(); 

     playerController.PlayFootStepAudio();

     playerController.SelectWeapon();
  }

 


   
}
