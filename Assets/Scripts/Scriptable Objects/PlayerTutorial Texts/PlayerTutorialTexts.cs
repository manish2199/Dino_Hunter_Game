using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewTutorialTextScriptableObject")]
public class PlayerTutorialTexts : ScriptableObject
{
   public string IntroductionText;

   public string MoveInstruction;

   public string JumpUnstructionText;

   public string SprintUnstructionText; 
  
   public string CrouchUnstructionText; 

   public string WeaponShootText;

   public string WeaponAim;
   
   public string WeaponSwitchText;

   public string WeaponDescText;

   public string InventoryDescpText;

   public string InstructionSuccessText;

   public string DinosaursIntroductionText;
  
   public string BlueRaptorEnemyDescp;

   public string RedRaptorEnemyDescp;
   
   public string TrexEnemyDescp;

   public string SuppliesDescpText;

   public string AchievementsDescpText;

   public string TutorialFinishedText;

}
