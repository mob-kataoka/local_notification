using System;
#if UNITY_IOS
using iLocalNotification 	= UnityEngine.iOS.LocalNotification;
using iNotificationServices 	= UnityEngine.iOS.NotificationServices;
using iNotificationType 	= UnityEngine.iOS.NotificationType;
#endif

namespace iOSTest
{
	public class LocalNotification
	{
		public void Initialize ()
		{
#if UNITY_IOS
			iNotificationServices.RegisterForNotifications (iNotificationType.Alert | iNotificationType.Badge | iNotificationType.Sound);
#endif
		}

		public void Add (string title, string message)
		{
#if UNITY_IOS
			iLocalNotification ln = new iLocalNotification ();
			ln.alertAction = title;
			ln.alertBody = message;
			ln.fireDate = DateTime.Now.AddSeconds (10d);

			iNotificationServices.ScheduleLocalNotification (ln);
#endif
		}
	}
}