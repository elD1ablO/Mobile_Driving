using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationsHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string channelID = "notification Channel";
    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new()
        {
            Id = channelID,
            Name = "Notification Channel",
            Description = "Hey! There's lives restored!",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new()
        {
            Title = "Energy recharged",
            Text = "Your energy has recharged. you can come back and try to beat your Highscore =)",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, channelID);
    }
#endif
}
