using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class LocalNotificationPlugin
{
	#region inner classes, enum, and structs
#if UNITY_IOS
	[DllImport ("__Internal")]
	private static extern void Initialize ();
	[DllImport ("__Internal")]
	private static extern void AddLocalNotification (int notificationId, String title, String message, int interval);
	[DllImport ("__Internal")]
	private static extern void RemovePendingLocalNotification (int notificationId);
#elif UNITY_ANDROID
	private static AndroidJavaObject notification;
#endif
	private const int NOTIFICATION_ID = 1000000000;
	private const int NOTIFICATION_ID_2 = 1000000;
	private const int NOTIFICATION_ID_3 = 1000;
	private const int NOTIFICATION_ID_4 = 1;

	public static int CreateNotificationId (int n1, int n2 = 1, int n3 = 1)
	{
		return NOTIFICATION_ID + (NOTIFICATION_ID_2 * n1) + (NOTIFICATION_ID_3 * n2) + (NOTIFICATION_ID_4 * n3);
	}

	#endregion

	#region Public Method

	private static bool isInit = false;

	public static void Init ()
	{
		if(isInit)
		{
			return;
		}
		isInit = true;

#if UNITY_IOS
		Initialize ();
#elif UNITY_ANDROID
		notification = new AndroidJavaObject ("com.mobcast.localnotification.LocalNotificationClient");
#endif
	}

	public static void Add (int id, string title, string msg, int interval)
	{
#if UNITY_IOS
		AddLocalNotification (id, title, msg, interval);
#elif UNITY_ANDROID
		notification.Call ("AddLocalNotification", id, title, msg, interval);
#endif
	}

	public static void Remove (int id)
	{
#if UNITY_IOS
		RemovePendingLocalNotification (id);
#elif UNITY_ANDROID
		notification.Call ("RemovePendingLocalNotification", id);
#endif
	}

#endregion
}