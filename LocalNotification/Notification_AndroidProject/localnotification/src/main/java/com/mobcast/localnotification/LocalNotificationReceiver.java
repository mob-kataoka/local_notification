package com.mobcast.localnotification;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v4.app.NotificationCompat;
import android.util.Log;
import android.content.Context;
import android.content.Intent;

import com.unity3d.player.UnityPlayer;

/**
 * Created by kataoka on 2017/11/14.
 */

public class LocalNotificationReceiver extends BroadcastReceiver{
    private static final String TAG = LocalNotificationReceiver.class.getSimpleName();

    @Override
    public void onReceive(Context context, Intent intent)
    {
        String title    = intent.getStringExtra("TITLE");
        String message  = intent.getStringExtra("MESSAGE");
        Integer notificationId = intent.getIntExtra("NOTIFICATION_ID", 0);

        // 通知タップで起動するIntent
        Intent clickIntent = new Intent(context, UnityPlayer.currentActivity.getClass())
                .setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_RESET_TASK_IF_NEEDED | Intent.FLAG_ACTIVITY_EXCLUDE_FROM_RECENTS);
        PendingIntent contentIntent = PendingIntent.getActivity(context, notificationId, clickIntent, PendingIntent.FLAG_UPDATE_CURRENT | PendingIntent.FLAG_ONE_SHOT);

        final PackageManager pm = context.getPackageManager();
        ApplicationInfo applicationInfo = null;
        try
        {
            applicationInfo = pm.getApplicationInfo(context.getPackageName(), PackageManager.GET_META_DATA);
        }catch (PackageManager.NameNotFoundException e)
        {
            e.printStackTrace();
            return;
        }
        final int appIconResId = applicationInfo.icon;
        Bitmap largeIcon = BitmapFactory.decodeResource(context.getResources(), appIconResId);

        Notification notification = new NotificationCompat.Builder(context)
                .setContentIntent(contentIntent)
                .setTicker(title)
                .setSmallIcon(appIconResId)
                .setContentTitle(title)
                .setContentText(message)
                .setLargeIcon(largeIcon)
                .setWhen(System.currentTimeMillis())
                .setDefaults(Notification.DEFAULT_ALL)
                .setAutoCancel(true)
                .build();

        NotificationManager manager = (NotificationManager) context.getSystemService(Service.NOTIFICATION_SERVICE);
        manager.notify(notificationId, notification);
    }
}
