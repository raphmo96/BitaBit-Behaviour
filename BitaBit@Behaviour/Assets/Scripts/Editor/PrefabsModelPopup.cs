using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PrefabsModelPopup : EditorWindow
{
    private int m_DepthValue = 0;
    private string m_PathToModel;

    private List<string> m_DirFiles = new List<string>();
    private GenericMenu m_Menu;

    [MenuItem("Tools/Prefab models")]
    private static void Init()
    {
        PrefabsModelPopup window = ScriptableObject.CreateInstance<PrefabsModelPopup>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 350, 250);
        window.ShowPopup();
    }

    private void SetupModels()
    {
        string[] parts;
        string path = "Assets/Prefabs";
        Object PrefabtoBeAdded;
        if (!AssetDatabase.IsValidFolder(path)) AssetDatabase.CreateFolder("Assets", "Prefabs");
        parts = m_PathToModel.Split('/');

        for (int i = 1; i < parts.Length; i++)
        {
            if (!AssetDatabase.IsValidFolder(path + "/" + parts[i]))
            {
                AssetDatabase.CreateFolder(path, parts[i]);
            }
            path += "/" + parts[i];
        }

        string[] aFilePaths = Directory.GetFiles(m_PathToModel.Replace("/", "\\"));

        foreach (string sFilePath in aFilePaths)
        {
            if (Path.GetExtension(sFilePath) == ".FBX" || Path.GetExtension(sFilePath) == ".fbx" && Path.GetExtension(sFilePath) != ".meta")
            {
                Debug.Log(Path.GetExtension(sFilePath));

                GameObject objectAsset = AssetDatabase.LoadAssetAtPath(sFilePath, typeof(Object)) as GameObject;
                string newPath = path;
                parts = objectAsset.name.Split('_');

                for (int i = 0; i < m_DepthValue; i++)
                {
                    if (!AssetDatabase.IsValidFolder(newPath + "/" + parts[i]))
                    {
                        AssetDatabase.CreateFolder(newPath, parts[i]);
                    }
                    newPath += "/" + parts[i];
                    if (m_DepthValue > parts.Length) break;
                }
                newPath += "/";
                PrefabtoBeAdded = AssetDatabase.LoadAssetAtPath(newPath + objectAsset.name + ".prefab", typeof(Object));
                if (PrefabtoBeAdded == null)
                {
                    PrefabtoBeAdded = PrefabUtility.CreateEmptyPrefab(newPath + objectAsset.name + ".prefab");
                    DestroyImmediate(PrefabUtility.ReplacePrefab(objectAsset as GameObject, PrefabtoBeAdded).GetComponent<Animator>(), true);
                }
            }
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Models name prefix must be seperated with '_'");
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Enter subfolder max depth: ");
        GUILayout.Space(10);
        m_DepthValue = EditorGUILayout.IntSlider("", m_DepthValue, 0, 10);
        GUILayout.Space(10);
        EditorGUILayout.TextArea("This will create a sub folder for each prefix of\n the model's namedecided upon the depth level.");
        GUILayout.Space(20);
        //m_PathToModel = EditorGUILayout.TextField("Path: ", m_PathToModel);
        if (GUILayout.Button("Select Folder"))
        {
            InstantiateMenu("Assets").ShowAsContext();
        }
        GUILayout.Space(10);
        EditorGUILayout.TextArea(m_PathToModel);
        GUILayout.Space(20);
        if (GUILayout.Button("Cancel"))
        {
            //Debug.Log(files);
            this.Close();
        }
        if (GUILayout.Button("Confirm"))
        {
            SetupModels();
            this.Close();
        }
    }

    private GenericMenu InstantiateMenu(string a_Root)
    {
        m_PathToModel = a_Root;
        if (m_Menu == null)
        {
            m_Menu = new GenericMenu();
            getChildrenDirectories(a_Root);
            for (int i = 0; i < m_DirFiles.Count; i++)
            {
                AddMenuItemForPath(ref m_Menu, m_DirFiles[i]);
            }
        }
        return m_Menu;
    }

    void AddMenuItemForPath(ref GenericMenu a_Menu, string a_Path)
    {
        // the menu item is marked as selected if it matches the current value of m_Color
        a_Menu.AddItem(new GUIContent(a_Path), m_PathToModel.Equals(a_Path), OnPathSelected, a_Path);
    }

    void OnPathSelected(object a_Path)
    {
        m_PathToModel = (string)a_Path;
    }

    private void getChildrenDirectories(string path)
    {
        string[] files;
        files = Directory.GetDirectories(path);
        if (files.Length == 0)
        {
            string[] test = path.Split('\\');
            path = "";
            for (int i = 0; i < test.Length; i++)
            {
                path += i == 0 ? test[i] : "/" + test[i];
            }
            m_DirFiles.Add(path);
        }
        else
        {
            for (int i = 0; i < files.Length; i++)
            {
                getChildrenDirectories(files[i]);
            }
        }
    }
}
