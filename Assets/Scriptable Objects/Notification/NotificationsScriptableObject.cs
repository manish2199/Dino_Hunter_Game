using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewNotificationsScriptableObject")]
public class NotificationsScriptableObject : ScriptableObject
{  
   public Notification[] Notifications;
}

[System.Serializable]
public class Notification
{
    public NotificationType NotificationType;
 
    public string MSGText;
}

public enum NotificationType
{
    None,
    LowAmmo,
    OutOfAmmo,
    FullAmmo,
    OutOfHealthKit,
    FullHealthKit,
    PlayerHealthFull,
    CollectItemNotification,
    ItemCollectedNotification
}
