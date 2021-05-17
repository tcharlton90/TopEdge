using UnityEditor;
using System.Linq;
using System;
using System.IO;

static void PerformBuild()
    {
        Console.WriteLine(":: Performing build");
 
        var buildTarget = GetBuildTarget();
 
        if (buildTarget == BuildTarget.Android) {
            HandleAndroidAppBundle();
            HandleAndroidBundleVersionCode();
            HandleAndroidKeystore();
        }
 
        var buildPath      = GetBuildPath();
        var buildName      = GetBuildName();
        var buildOptions   = GetBuildOptions();
        var fixedBuildPath = GetFixedBuildPath(buildTarget, buildPath, buildName);
 
        SetScriptingBackendFromEnv(buildTarget);
 
        var buildReport = BuildPipeline.BuildPlayer(GetEnabledScenes(), fixedBuildPath, buildTarget, buildOptions);
 
        if (buildReport.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            throw new Exception($"Build ended with {buildReport.summary.result} status");
 
        Console.WriteLine(":: Done with build");
    }