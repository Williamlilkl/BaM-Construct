using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles/windows";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneWindows);

        string assetBundleDirectory2 = "Assets/AssetBundles/android";
        if (!Directory.Exists(assetBundleDirectory2))
        {
            Directory.CreateDirectory(assetBundleDirectory2);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory2,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.Android);
    }
}