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

    // Button AudioSource 
    [SerializeField] AudioSource ButtonAdioSource;
    [SerializeField] AudioClip ButtonClickClip; 


    void Start()
    {
       InitializeGamePrefs();
    }


    private void InitializeGamePrefs()
    {
        if(!PlayerPrefs.HasKey("GameInitialized"))
        { 
            GameData.SetTutorialState(0);

            GameData.SetEasyDifficulty(1);
            GameData.SetMediumDifficulty(0);
            GameData.SetMediumDifficulty(0);

            GameData.SetEasyDifficultyHighScore(0);
            GameData.SetMediumDifficultyHighScore(0);
            GameData.SetHardDifficultyHighScore(0);

            GameData. SetShotgunUnlocked(0);
            GameData.SetAssualtRifleUnlocked(0);

            PlayerPrefs.SetInt("GameInitialized" ,666);
        }
    }



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
      PlayButtonClickAudio();
      
      if(GameData.GetTutorialState() == 0)
      {
         // LoadScene 1 
      }
      if(GameData.GetTutorialState() == 1)
      {
         // LoadScene 2 
      }

    }
 

    #region  Difficulty Menu
    public void DifficultyMenuButton()
    {
       PlayButtonClickAudio();
       
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
        PlayButtonClickAudio();
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
        PlayButtonClickAudio();
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
        PlayButtonClickAudio();
        ActivateHardSign();
        GameData.SetEasyDifficulty(0);
        GameData.SetMediumDifficulty(0);
        GameData.SetHardDifficulty(1);
    }         

    public void DifficultyMenuExitButton()
    {
       PlayButtonClickAudio(); 

       DifficultyMenuPanel.SetActive(false);

       EnableMainMenu();
    }
    #endregion
 

    #region  HighScore
    public void HighScoresMenuButton()
    {   
      PlayButtonClickAudio();

        SetHighScore();

        DisableMainMenu();
         
        HighScoreMenuPanel.SetActive(true);
        
    }

    private void SetHighScore()
    {   
        if(GameData.GetEasyDifficulty() == 1)
        {
            HighscoreText.text = GameData.GetEasyDifficultyHighScore().ToString();
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
       PlayButtonClickAudio();
    
       HighScoreMenuPanel.SetActive(false);

       EnableMainMenu();
    }
    #endregion

    private void PlayButtonClickAudio()
    {
        ButtonAdioSource.clip = ButtonClickClip;
        ButtonAdioSource.Play();
    }
   

    public void ControlsButton()
    {
       PlayButtonClickAudio();

       DisableMainMenu();

       ControlsMenuPanel.SetActive(true);
    }

    public void ControlMenuExitButton()
    {  
       PlayButtonClickAudio();

       ControlsMenuPanel.SetActive(false);

       EnableMainMenu();
    }


    public void QuitButton()
    { 
       PlayButtonClickAudio();
     
        Application.Quit();
    }
    
}









public enum DifficultyType
{
    Easy,
    Medium,
    Hard
}