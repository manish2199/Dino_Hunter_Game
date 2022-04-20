using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{   
    // Main Menu Content
    [SerializeField] GameObject Title;
    [SerializeField] GameObject MainMenuPanel;

    //BlurPanel
    [SerializeField] GameObject BlurPanel;

    //Difficulty Menu 
    [SerializeField] GameObject DifficultyMenuPanel;
    [SerializeField] GameObject EasyDifficultyTick;
    [SerializeField] GameObject MediumDifficultyTick;
    [SerializeField] GameObject HardDifficultyTick;


    //Difficulty Menu 
    [SerializeField] GameObject HighScoreMenuPanel;
    [SerializeField] TextMeshProUGUI HighscoreText;


    //Controls Menu 
    [SerializeField] GameObject ControlsMenuPanel;
    


    private void DisableMainMenu()
    {
       if(Title.activeInHierarchy)
       {
           Title.SetActive(false);
           MainMenuPanel.SetActive(false);
           BlurPanel.SetActive(true);
       }
    }

    private void EnableMainMenu()
    {
        if(!Title.activeInHierarchy)
       {
           Title.SetActive(true);
           MainMenuPanel.SetActive(true);
           BlurPanel.SetActive(false);
       }
    }



    public void PlayButton()
    {
      //Start the gameplay by showing loading screen
    }
 

    #region  Difficulty Menu
    public void DifficultyMenuButton()
    {
       DisableMainMenu();
        
       DifficultyMenuPanel.SetActive(true);
     
       SetDifficulty();
    
    }

    private void SetDifficulty()
    {
        if(GameData.GetEasyDifficulty() == 1)
        {
            SetInitialDifficulty(DifficultyType.Easy);
        }
        else if(GameData.GetMediumDifficulty() == 1)
        {
            SetInitialDifficulty(DifficultyType.Medium);
        }
        else if(GameData.GetHardDifficulty() == 1)
        {
            SetInitialDifficulty(DifficultyType.Hard);
        }
    }

    public void SetInitialDifficulty(DifficultyType difficulty)
    {
       switch(difficulty)
        {
           case DifficultyType.Easy :
                ActivateEasySign();
                break;
            case DifficultyType.Medium :
                ActivateMediumSign();
                break;
            case DifficultyType.Hard :
                ActivateHardSign();
                break;    
       }
    }

    private void ActivateEasySign()
    {
        EasyDifficultyTick.SetActive(true);
        MediumDifficultyTick.SetActive(false);
        HardDifficultyTick.SetActive(false);
    }

    public void SetEasyDifficulty()
    {   
        ActivateEasySign();
        GameData.SetEasyDifficulty(1);
        GameData.SetMediumDifficulty(0);
        GameData.SetHardDifficulty(0);
    }

    private void ActivateMediumSign()
    {
        EasyDifficultyTick.SetActive(false);
        MediumDifficultyTick.SetActive(true);
        HardDifficultyTick.SetActive(false);
    }

    public void SetMediumDifficulty()
    {
        ActivateMediumSign();
        GameData.SetEasyDifficulty(0);
        GameData.SetMediumDifficulty(1);
        GameData.SetHardDifficulty(0);

    } 

    private void ActivateHardSign()
    {
        EasyDifficultyTick.SetActive(false);
        MediumDifficultyTick.SetActive(false);
        HardDifficultyTick.SetActive(true);
    }

    public void SetHardDifficulty()
    {
        ActivateHardSign();
        GameData.SetEasyDifficulty(0);
        GameData.SetMediumDifficulty(0);
        GameData.SetHardDifficulty(1);
    }         

    public void DifficultyMenuExitButton()
    {
       DifficultyMenuPanel.SetActive(false);

       EnableMainMenu();
    }
    #endregion
 

    #region  HighScore
    public void HighScoresMenuButton()
    {
        SetHighScore();

        DisableMainMenu();
         
        HighScoreMenuPanel.SetActive(true);
        
    }

    private void SetHighScore()
    {   
        // print("Easy Difficulty" + GameData.GetEasyDifficulty());
        // print("Medium Difficulty" + GameData.GetMediumDifficulty());
        // print("Hard Difficulty" + GameData.GetHardDifficulty());

        if(GameData.GetEasyDifficulty() == 1)
        {
            HighscoreText.text = GameData.GetEasyDifficultyHighscore().ToString();
        }
        else if(GameData.GetMediumDifficulty() == 1)
        {
            HighscoreText.text = GameData.GetMediumDifficultyHighScore().ToString();
        }
        else if(GameData.GetHardDifficulty() == 1)
        {
            HighscoreText.text = GameData.GetHardDifficultyHighScore().ToString();
        }
    }

    public void HighScoreMenuExitButton()
    {
       HighScoreMenuPanel.SetActive(false);

       EnableMainMenu();
    }
    #endregion



    public void ControlsButton()
    {
       DisableMainMenu();

       ControlsMenuPanel.SetActive(true);
    }

    public void ControlMenuExitButton()
    {
       ControlsMenuPanel.SetActive(false);

       EnableMainMenu();
    }


    public void QuitButton()
    {
        Application.Quit();
    }
    
}









public enum DifficultyType
{
    Easy,
    Medium,
    Hard
}