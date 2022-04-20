using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    protected void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        //  InitializeTheGame(); 
        PlayerPrefs.DeleteAll(); 
    }


    private void InitializeTheGame()
    {
        if(!PlayerPrefs.HasKey("InitializedGame"))
        {
            GameData.SetEasyDifficulty(1);
            GameData.SetMediumDifficulty(0);
            GameData.SetHardDifficulty(0);


            GameData.SetEasyDifficultyHighScore(0);
            GameData.SetMediumDifficultyHighScore(0);
            GameData.SetHardDifficultyHighScore(255);
          
           PlayerPrefs.SetInt("InitializedGame",1);
        }
    }
     
}
