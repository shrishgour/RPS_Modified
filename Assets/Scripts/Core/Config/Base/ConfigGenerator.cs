#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class ConfigGenerator
{
    private static Texture2D scriptIcon = (
        EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D
        );

    [MenuItem("Assets/Create/Configs/Single Config C# Script", false, 2)]
    public static void GenerateSingleConfigScript()
    {
        string templetePath = ScriptGenerator.GetTemplatePath("BaseSingleConfig");
        CreateFromTemplate("SingleConfig.cs", templetePath, "Core.Config", "");
    }

    [MenuItem("Assets/Create/Configs/Multi Config C# Script", false, 3)]
    public static void GenerateMultiConfigScript()
    {
        string templetePath = ScriptGenerator.GetTemplatePath("BaseMultiConfig");
        CreateFromTemplate("MultiConfig.cs", templetePath, "Core.Config", "");
    }

    public static void CreateFromTemplate(string initialName, string templatePath, string namespaceTarget, string body)
    {
        DoCreateCodeFile action = ScriptableObject.CreateInstance<DoCreateCodeFile>();
        action.namespaceTarget = namespaceTarget;
        action.body = body;

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, action, initialName, scriptIcon, templatePath);
    }

    public class DoCreateCodeFile : UnityEditor.ProjectWindowCallback.EndNameEditAction
    {
        public string namespaceTarget;
        public string body;

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            Object obj = ScriptGenerator.CreateScript(pathName, resourceFile, namespaceTarget, body);
            ProjectWindowUtil.ShowCreatedAsset(obj);
        }
    }
}
#endif