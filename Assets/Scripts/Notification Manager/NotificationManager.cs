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

    private IEnumerator NotificationMessageCoroutine;

    private bool isAlradyShowing = false; 
   
    void Awake()
    {
        base.Awake();
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
