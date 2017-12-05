package com.mobcast.localnotification;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import java.util.Calendar;

/**
 * Created by kataoka on 2017/11/14.
 */

public class LocalNotificationClient {
    private  static  final  String TAG = LocalNotificationClient.class.getSimpleName();
    public LocalNotificationClient(){}

    public void AddLocalNotification(int notificationId, String title, String message, int interval)
    {
        Context context = UnityPlayer.currentActivity.getApplicationContext();

        Intent clickIntent = new Intent(context, LocalNotificationReceiver.class);
        clickIntent.putExtra("TITLE", title);
        clickIntent.putExtra("MESSAGE", message);
        clickIntent.putExtra("NOTIFICATION_ID", notificationId);

        PendingIntent sender = PendingIntent.getBroadcast(context, notificationId, clickIntent, PendingIntent.FLAG_UPDATE_CURRENT);

        Calendar cal = Calendar.getInstance();
        cal.setTimeInMillis(System.currentTimeMillis());
        cal.add(Calendar.SECOND, interval);

        AlarmManager alarm = (AlarmManager)context.getSystemService(Context.ALARM_SERVICE);
        alarm.set(AlarmManager.RTC_WAKEUP, cal.getTimeInMillis(), sender);

        Log.d(TAG, "AddLocalNotification " + notificationId + title + message);
    }

    public void RemovePendingLocalNotification(int notificationId)
    {
        Context context = UnityPlayer.currentActivity.getApplicationContext();

        Intent intent = new Intent(context, LocalNotificationReceiver.class);
        PendingIntent pendingIntent = PendingIntent.getBroadcast(context, notificationId, intent, PendingIntent.FLAG_CANCEL_CURRENT);

        AlarmManager alarm = (AlarmManager)context.getSystemService(Context.ALARM_SERVICE);
        alarm.cancel(pendingIntent);

        Log.d(TAG, "RemovePendingLocalNotification " + notificationId);
    }
}
