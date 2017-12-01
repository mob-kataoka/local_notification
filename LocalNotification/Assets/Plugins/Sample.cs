using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Sample : MonoBehaviour
{

#if UNITY_IOS
	[DllImport ("__Internal")]
	private static extern void Initialize ();
	[DllImport ("__Internal")]
	private static extern void SetLocalNotification (int id, string title, string msg);
	[DllImport ("__Internal")]
	private static extern void RemovePendingLocalNotification (int id);
	[DllImport ("__Internal")]
	private static extern void ClearLocalNotification ();

	// Use this for initialization
	void Start () {
		count = 0;

		Initialize ();
	}

	int count = 0;

	public void AddLocalNotification()
	{
		count++;
		SetLocalNotification (CreateNotificationId (count), "ほげほげ", "通知をおくります " + count);
	}

	public void CancelLocalNotification()
	{
		RemovePendingLocalNotification ( CreateNotificationId (count) );
	}
#elif UNITY_ANDROID

#endif

	private const int NOTIFICATION_ID = 1000000000;
	private const int NOTIFICATION_ID_2 =  1000000;
	private const int NOTIFICATION_ID_3 =     1000;
	private const int NOTIFICATION_ID_4 =        1;

	private static int CreateNotificationId(int n1, int n2 = 1, int n3 = 1)
	{
		return NOTIFICATION_ID + (NOTIFICATION_ID_2 * n1) + (NOTIFICATION_ID_3 * n2) + (NOTIFICATION_ID_4 * n3);
	}


}
