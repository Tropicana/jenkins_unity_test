using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

class MyEditorScript {
	static string[] SCENES = FindEnabledEditorScenes();
	
	static string APP_NAME = "JenkinsUnityTest01";
	static string TARGET_DIR = "/Users/Shared/Jenkins/Desktop/Test1";
	
	[MenuItem ("Custom/CI/Build Mac OS X")]
	static void PerformMacOSXBuild ()
	{
		string target_dir = APP_NAME ;//+ ".app";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.iPhone,BuildOptions.None);
	}
	
	private static string[] FindEnabledEditorScenes() {
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}
	
	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
		if (res.Length > 0) {
			throw new System.Exception("BuildPlayer failure: " + res);
		}
	}
}
