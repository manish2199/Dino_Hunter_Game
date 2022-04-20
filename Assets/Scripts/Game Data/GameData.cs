using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
   // boolean Values
   //Difficulty
   private static string EasyDifficulty = "EasyDifficulty";
   private static string MediumDifficulty = "MediumDifficulty";
   private static string HardDifficulty = "HardDifficulty";  
  
   // Weapons 
   private static string RevolverUnlocked = "RevolverUnlocked";
   private static string ShotGunUnlocked = "ShotGunUnlocked";
   private static string AssualtRifleUnlocked = "AssualtRifleUnlocked";  
   
   // Achievements
//    private static string RevolverUnlocked = "RevolverUnlocked";
//    private static string ShotGunUnlocked = "ShotGunUnlocked";
//    private static string AssualtRifleUnlocked = "AssualtRifleUnlocked";  

   // Tutorial 
   private static string TutorialState = "TutorialPlayed";  


   // int values
   // Player HighScores
   private static string EasyDifficultyHighScore = "EasyDifficultyHighScore";
   private static string MediumDifficultyHighScore = "MediumDifficultyHighScore";
   private static string HardDifficultyHighScore = "HardDifficultyHighScore";


   public static void SetTutorialState(int state)
   {
       PlayerPrefs.SetInt(GameData.TutorialState,state);
   } 

   public static int GetTutorialState()
   {
       return PlayerPrefs.GetInt(GameData.TutorialState);
   }



   #region Difficulties
   public static void SetEasyDifficulty(int state)
   {
       PlayerPrefs.SetInt(GameData.EasyDifficulty,state);
   } 

   public static int GetEasyDifficulty()
   {
       return PlayerPrefs.GetInt(GameData.EasyDifficulty);
   }

    public static void SetMediumDifficulty(int state)
   {
       PlayerPrefs.SetInt(GameData.MediumDifficulty,state);
   } 

   public static int GetMediumDifficulty()
   {
       return PlayerPrefs.GetInt(GameData.MediumDifficulty);
   }

     public static void SetHardDifficulty(int state)
   {
       PlayerPrefs.SetInt(GameData.HardDifficulty,state);
   } 

   public static int GetHardDifficulty()
   {
       return PlayerPrefs.GetInt(GameData.HardDifficulty);
   }
   #endregion
   


   
   #region Weapons
   public static void SetRevolverUnlocked(int state)
   {
       PlayerPrefs.SetInt(GameData.RevolverUnlocked,state);
   } 

   public static int GetRevolverUnlocked()
   {
       return PlayerPrefs.GetInt(GameData.RevolverUnlocked);
   }

    public static void SetShotgunUnlocked(int state)
   {
       PlayerPrefs.SetInt(GameData.ShotGunUnlocked,state);
   } 

   public static int GetShotgunUnlocked()
   {
       return PlayerPrefs.GetInt(GameData.ShotGunUnlocked);
   }

     public static void SetAssualtRifleUnlocked(int state)
   {
       PlayerPrefs.SetInt(GameData.AssualtRifleUnlocked,state);
   } 

   public static int GetAssualtRifleUnlocked()
   {
       return PlayerPrefs.GetInt(GameData.AssualtRifleUnlocked);
   }
   #endregion



   #region Highscores 
   public static void SetEasyDifficultyHighScore(int score)
   {
       PlayerPrefs.SetInt(GameData.EasyDifficultyHighScore,score);
   } 

   public static int GetEasyDifficultyHighscore()
   {
       return PlayerPrefs.GetInt(GameData.EasyDifficultyHighScore);
   }

    public static void SetMediumDifficultyHighScore(int score)
   {
       PlayerPrefs.SetInt(GameData.MediumDifficultyHighScore,score);
   } 

   public static int GetMediumDifficultyHighScore()
   {
       return PlayerPrefs.GetInt(GameData.MediumDifficultyHighScore);
   }

     public static void SetHardDifficultyHighScore(int score)
   {
       PlayerPrefs.SetInt(GameData.HardDifficultyHighScore,score);
   } 

   public static int GetHardDifficultyHighScore()
   {
       return PlayerPrefs.GetInt(GameData.HardDifficultyHighScore);
   }
   #endregion

}

