using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NotificationManager : GenericSingleton<NotificationManager>
{
    [SerializeField] NotificationsScriptableObject notificationsScriptableObject;

    [SerializeField] private GameObject NotificationPanel;

    [SerializeField]  private Text NotificationText; 

    [SerializeField] GameObject AchievementPanel;

    [SerializeField] Text AchievementAcomplishedText;

    [SerializeField] Image UnlockItemImage; 

    private IEnumerator NotificationMessageCoroutine;

    private IEnumerator AchivementCompleteCoroutine;

    private bool isAlradyShowing = false; 
   
   protected override void Awake()
    {
      if(Instance == null)
      {
         Instance = this;
      }
    }

    public void ShowAchievementComplete(string achievementText , Sprite unlockItemSprite)
    {
        AchivementCompleteCoroutine =  AchievementComplete(achievementText,unlockItemSprite);
        StartCoroutine(AchivementCompleteCoroutine);
    }

    IEnumerator AchievementComplete(string achievementText , Sprite unlockItemSprite)
    {
        AchievementAcomplishedText.text = achievementText;
        UnlockItemImage.sprite = unlockItemSprite;
      
        AchievementPanel.SetActive(true);

        yield return new WaitForSeconds(5f);

        AchievementPanel.SetActive(false);
        AchievementAcomplishedText.text = " ";
        UnlockItemImage.sprite = null;
    }


 
    public void ShowNotificationMsg(NotificationType NotificationType)
    {
       if(!isAlradyShowing)
      {
        Notification notification = GetNotification(NotificationType);
        NotificationMessageCoroutine = MsgCoroutine(notification);
        StartCoroutine(NotificationMessageCoroutine);
      } 
    }

    private Notification GetNotification(NotificationType NotificationType)
    { 
        Notification temp = null;
        for(int i = 0 ; i<notificationsScriptableObject.Notifications.Length; i++)
        {
            if(notificationsScriptableObject.Notifications[i].NotificationType == NotificationType)
            {
                temp = notificationsScriptableObject.Notifications[i];
            }
        }
        return temp;
    } 

    private IEnumerator MsgCoroutine(Notification  Notification)
    {
       NotificationPanel.SetActive(true);
       isAlradyShowing = true;
       NotificationText.text = Notification.MSGText + "  ";

       yield return new WaitForSeconds(1f);

       isAlradyShowing = false;
       NotificationPanel.SetActive(false);
       NotificationText.text = " ";
    }

}
