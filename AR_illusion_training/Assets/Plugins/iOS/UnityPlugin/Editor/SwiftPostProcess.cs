using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Diagnostics;

using System.IO;
using System.Linq;

public static class SwiftPostProcess
{

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            var projPath = buildPath + "/Unity-Iphone.xcodeproj/project.pbxproj";
            var proj = new PBXProject();
            proj.ReadFromFile(projPath);

            string plistPath = Path.Combine(buildPath, "Info.plist");
            // var targetGuid = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
            var targetGuid = proj.GetUnityMainTargetGuid();

            proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

            proj.AddBuildProperty(proj.ProjectGuid(), "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/Plugins/iOS/UnityPlugin/Source/UnityPlugin-Bridging-Header.h");
            proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "UnityPlugin-Swift.h");

            proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks $(PROJECT_DIR)/lib/$(CONFIGURATION) $(inherited)");
            proj.AddBuildProperty(targetGuid, "FRAMERWORK_SEARCH_PATHS",
                "$(inherited) $(PROJECT_DIR) $(PROJECT_DIR)/Frameworks");
            proj.AddBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            proj.AddBuildProperty(targetGuid, "DYLIB_INSTALL_NAME_BASE", "@rpath");
            proj.AddBuildProperty(targetGuid, "LD_DYLIB_INSTALL_NAME",
                "@executable_path/../Frameworks/$(EXECUTABLE_PATH)");
            proj.AddBuildProperty(targetGuid, "DEFINES_MODULE", "YES");
            proj.AddBuildProperty(targetGuid, "SWIFT_VERSION", "4.0");
            proj.AddBuildProperty(targetGuid, "COREML_CODEGEN_LANGUAGE", "Swift");

            proj.WriteToFile(projPath);

            var manager = new ProjectCapabilityManager(projPath, "Entitlements.entitlements", null, proj.GetUnityMainTargetGuid());
            manager.AddHealthKit();
            manager.WriteToFile();

            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;

            rootDict.SetString("NSHealthShareUsageDescription", "You can check your exercise record on Apple Fitness");
            rootDict.SetString("NSHealthUpdateUsageDescription", "You can check your exercise record on Apple Fitness");

            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }

}