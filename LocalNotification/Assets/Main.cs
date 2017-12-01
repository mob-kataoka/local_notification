﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

	[SerializeField]
	private SampleClientObject sampleClientObject;

	iOSTest.LocalNotification iLN;

	// Use this for initialization
	void Start ()
	{
#if UNITY_IOS
		iLN = new iOSTest.LocalNotification ();
		iLN.Initialize ();
#else
		sampleClientObject.Initialize ();
#endif
	}

	private int nId = 0;
	public void SetLocalNotification ()
	{
		nId++;
#if UNITY_IOS
		iLN.Add ("ほげほげ", "ほげほげほげりんこ");
#else
		sampleClientObject.SetLocalNotificationInterval (nId, "てすと " + nId, "ほんぶん", 10);
#endif
	}

	public void CancelNotification()
	{
		sampleClientObject.CancelLocalNotification (nId);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
