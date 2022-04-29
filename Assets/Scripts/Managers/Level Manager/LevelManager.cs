using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
   [SerializeField] GameObject LevelLoaderCanvas;

   [SerializeField] Image LoadingBar;

   private float TargetFillAmount;

   public AudioSource LoadingAudioSource;

    protected override void Awake()
    { 
        if( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

   }

   public async void LoadScene(int levelIndex)
   {
       LoadingBar.fillAmount = 0f;
       TargetFillAmount = 0;
       LoadingAudioSource.Play();

       var scene = SceneManager.LoadSceneAsync(levelIndex);
       scene.allowSceneActivation = false;
       LevelLoaderCanvas.SetActive(true);

       do
       { 
          await Task.Delay(1500);
          TargetFillAmount = scene.progress;
       }while(scene.progress < 0.9f);
  
       await Task.Delay(3000);
       
       scene.allowSceneActivation = true;
       LoadingAudioSource.Stop();

       await Task.Delay(500);
       LevelLoaderCanvas.SetActive(false);
   }

   private void Update()
   {
       LoadingBar.fillAmount = Mathf.MoveTowards(LoadingBar.fillAmount, TargetFillAmount , 2 * Time.deltaTime);
   }

}


 