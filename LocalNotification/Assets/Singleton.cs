using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null) {
				string objName = typeof (T).ToString ();
				GameObject obj = GameObject.Find (objName);
				if (obj == null)
				{
					obj = GameObject.Find (objName + "(Clone)");
				}

				if(obj == null)
				{
					obj = new GameObject (objName);
				}

				instance = obj.GetComponent<T> ();
				if(instance == null)
				{
					instance = obj.AddComponent<T> ();
				}
				DontDestroyOnLoad (obj);
			}
			return instance;
		}
	}
}
