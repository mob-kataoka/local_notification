using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class XcodePostProcess : MonoBehaviour {

	internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
	{
		if (Directory.Exists (dstPath))
			Directory.Delete (dstPath);
		if (File.Exists (dstPath))
			File.Delete (dstPath);

		Directory.CreateDirectory (dstPath);

		foreach (var file in Directory.GetFiles (srcPath))
			File.Copy (file, Path.Combine (dstPath, Path.GetFileName (file)));

		foreach (var dir in Directory.GetDirectories (srcPath))
			CopyAndReplaceDirectory (dir, Path.Combine (dstPath, Path.GetFileName (dir)));
	}

	[PostProcessBuild(100)]
	public static void OnPostProcessBuild(BuildTarget target, string path)
	{
		if(target == BuildTarget.iOS)
		{
			ProcessForiOS (path);
		}
	}

	private static void ProcessForiOS(string path)
	{
		//string pjPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
		//PBXProject pj = new PBXProject ();
		//pj.ReadFromString (File.ReadAllText (pjPath));

		//string target = pj.TargetGuidByName ("Unity-iPhone");


		//File.WriteAllText (pjPath, pj.WriteToString ());
	}
}
