using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModelImportSettings : AssetPostprocessor
{
    private void OnPreprocessModel()
    {
        if (assetPath.Contains(".fbx"))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            modelImporter.generateSecondaryUV = true;

            if (!assetPath.Contains("Animations"))
                modelImporter.importAnimation = false;

        }
    }
}
