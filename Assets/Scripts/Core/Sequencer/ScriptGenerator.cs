using System.IO;
using System.Text;
using UnityEngine;

public static class ScriptGenerator
{
    public static string GetTemplatePath(string templateName)
    {
#if UNITY_EDITOR
        string[] guids = UnityEditor.AssetDatabase.FindAssets($"{templateName}.cs");
        if (guids.Length == 0)
        {
            Debug.LogWarning($"{templateName}.cs.txt not found in asset database");
            return string.Empty;
        }
        return UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
#else
        return "";
#endif
    }

    public static Object CreateScript(string pathName, string templatePath, string namespaceTarget, string body = "")
    {
#if UNITY_EDITOR
        string className = Path.GetFileNameWithoutExtension(pathName).Replace(" ", string.Empty);
        string templateText = string.Empty;

        UTF8Encoding encoding = new UTF8Encoding(true, false);

        if (File.Exists(templatePath))
        {
            StreamReader reader = new StreamReader(templatePath);
            templateText = reader.ReadToEnd();
            reader.Close();

            templateText = templateText.Replace("#CLASSNAME#", className);
            templateText = templateText.Replace("#NAMESPACE#", namespaceTarget);
            templateText = templateText.Replace("#NOTRIM#", string.Empty);
            templateText = templateText.Replace("#BODY#", body);

            StreamWriter writer = new StreamWriter(Path.GetFullPath(pathName), false, encoding);
            writer.Write(templateText);
            writer.Close();

            UnityEditor.AssetDatabase.ImportAsset(pathName);
            return UnityEditor.AssetDatabase.LoadAssetAtPath(pathName, typeof(object));
        }
        else
        {
            Debug.LogError($"The template file was not found: {templatePath}");
            return null;
        }
#else
            return null;
#endif
    }
}
