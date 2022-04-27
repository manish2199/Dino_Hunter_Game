using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialManager : GenericSingleton<TutorialManager>
{     
    [SerializeField] PlayerTutorialTexts PlayerTutorials;

    [SerializeField] LevelInitializerScriptableObject InitialLevelConstraints;
    
    [SerializeField] GameObject PlayerGameObject;
    [SerializeField] Text InstructionText;
    [SerializeField] GameObject InstructionPanel;
    [SerializeField] GameObject BlueRaptor;
    [SerializeField] GameObject RedRaptor;
    [SerializeField] GameObject TRex;
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject AmmoBox;
    [SerializeField] GameObject MediBox;

    private IEnumerator TutorialCoroutine;  

    
    protected override void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        InitialSetup(); 

        StartTutorial();
    } 

   

    private void InitialSetup()
    {        
       WeaponService.Instance.UnlockTheWeapon(WeaponsID.Revolver); 
       WeaponService.Instance.UnlockTheWeapon(WeaponsID.ShotGun); 
       WeaponService.Instance.UnlockTheWeapon(WeaponsID.AssaultRifle);  
       
        AddInitialItemToInventory();
    }


    void AddInitialItemToInventory()
    {
        for(int i = 0; i<InitialLevelConstraints.InventoryBulletsLists.Length; i++)
       { 
           InventoryItem item = InitialLevelConstraints.InventoryBulletsLists[i].InventoryItem;
           int quantity =  InitialLevelConstraints.InventoryBulletsLists[i].InitialQuantity;
           int maxLimit =  InitialLevelConstraints.InventoryBulletsLists[i].InitialMaxLimit;
    
           InventoryService.Instance.AddItemSlotToProjectiles(item,quantity,maxLimit);
       }

       for(int i = 0; i<InitialLevelConstraints.InventoryMedicalKitLists.Length; i++)
       { 
           InventoryItem item = InitialLevelConstraints.InventoryMedicalKitLists[i].InventoryItem;
           int quantity =  InitialLevelConstraints.InventoryMedicalKitLists[i].InitialQuantity;
           int maxLimit =  InitialLevelConstraints.InventoryMedicalKitLists[i].InitialMaxLimit;

           InventoryService.Instance.AddItemToHealthKits(item,quantity,maxLimit);
       }

    }


    private void StartTutorial()
    {
        TutorialCoroutine = Tutorial();
        StartCoroutine(TutorialCoroutine);
    }


    IEnumerator Tutorial()
    {   
       GameplayUIManager.Instance.DisableSelectedWeaponIcon();
       InstructionPanel.SetActive(true);
       InstructionText.text = PlayerTutorials.IntroductionText;

       yield return new WaitForSeconds(5f);
       
       PlayerGameObject.SetActive(true);
       MainCamera.SetActive(false);

       InstructionText.text = PlayerTutorials.MoveInstruction;
        
       yield return new WaitForSeconds(5f);

       InstructionText.text = PlayerTutorials.SprintUnstructionText;
      
       yield return new WaitForSeconds(5f);

       InstructionText.text = PlayerTutorials.CrouchUnstructionText;

       yield return new WaitForSeconds(5f);
         
       InstructionText.text = PlayerTutorials.WeaponSwitchText;
 
       yield return new WaitForSeconds(5f);
       GameplayUIManager.Instance.EnableSelectedWeaponIcon();
       InstructionText.text = PlayerTutorials.WeaponAim;

       yield return new WaitForSeconds(5f);

       InstructionText.text = PlayerTutorials.WeaponShootText;

       yield return new WaitForSeconds(5f);

       InstructionText.text = PlayerTutorials.WeaponDescText;

       yield return new WaitForSeconds(5f);

       InstructionText.text = PlayerTutorials.InventoryDescpText;

       yield return new WaitForSeconds(5f);

       InstructionText.text = PlayerTutorials.InstructionSuccessText;

       yield return new WaitForSeconds(5f);

       InstructionText.text = PlayerTutorials.DinosaursIntroductionText;

       yield return new WaitForSeconds(5f);
     
       
       PlayerGameObject.SetActive(false);
       GameplayUIManager.Instance.SetCrossHair(true);
       MainCamera.SetActive(true);
       BlueRaptor.SetActive(true);


       InstructionText.text = PlayerTutorials.BlueRaptorEnemyDescp;

       yield return new WaitForSeconds(8f);

       BlueRaptor.SetActive(false);
       RedRaptor.SetActive(true);

       InstructionText.text = PlayerTutorials.RedRaptorEnemyDescp;

       yield return new WaitForSeconds(8f);

       RedRaptor.SetActive(false);
       TRex.SetActive(true);

       InstructionText.text = PlayerTutorials.TrexEnemyDescp;

       yield return new WaitForSeconds(5f);
       
       
       TRex.SetActive(false);
       InstructionText.text = PlayerTutorials.SuppliesDescpText;
      

       AmmoBox.SetActive(true);

       yield return new WaitForSeconds(7f);

       AmmoBox.SetActive(false);
       MediBox.SetActive(true);
    
       yield return new WaitForSeconds(7f);

       PlayerGameObject.SetActive(true);
       MainCamera.SetActive(false);
       MediBox.SetActive(false);
       GameplayUIManager.Instance.SetCrossHair(false);
       InstructionText.text = PlayerTutorials.AchievementsDescpText;

       yield return new WaitForSeconds(3f);
       
       InstructionText.text = PlayerTutorials.TutorialFinishedText;


       yield return new WaitForSeconds(3f);

       InstructionPanel.SetActive(false);
 
    //    Load Main GamePlay 
       if(GameData.GetTutorialState() == 0 )
       {
         LevelManager.Instance.LoadScene(2);
         GameData.SetTutorialState(1);

       }
       else if( GameData.GetTutorialState() == 1)
       {
         LevelManager.Instance.LoadScene(0);
       }

    }


   
    
}
