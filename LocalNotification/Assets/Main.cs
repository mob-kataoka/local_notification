﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
	private LocalNotificationPlugin plugin;

	private int id = 0;


	// Use this for initialization
	void Start ()
	{
		id = 0;

		LocalNotificationPlugin.Init ();
	}

	public void SetLocalNotification ()
	{
		id++;

		LocalNotificationPlugin.Add (id, "通知タイトル " + id, "通知本文", 15);
	}

	public void CancelNotification()
	{
		LocalNotificationPlugin.Remove (id);
	}
}
