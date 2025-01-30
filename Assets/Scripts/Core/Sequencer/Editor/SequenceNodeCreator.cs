using System.IO;
using UnityEditor;
using UnityEngine;

public class SequenceNodeCreator
{
    private static Texture2D scriptIcon =
        (EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);

    [MenuItem("Assets/Create/Sequencer/Command C# Script", false, 89)]
    public static void GenerateSequencerCommandScript()
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets("Sequence_Command.cs");

        string commandTempletePath = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);

        DoCreateSequencerCommandCodeFile action = ScriptableObject.CreateInstance<DoCreateSequencerCommandCodeFile>();

        action.namespaceTarget = "Core.Sequencer.Commands";
        action.nodeNamespaceTarget = "Core.Sequencer.Commands";
        action.body = "";

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            action,
            "New Command.cs",
            scriptIcon,
            commandTempletePath
            );
    }

    internal class DoCreateSequencerCommandCodeFile : UnityEditor.ProjectWindowCallback.EndNameEditAction
    {
        public string namespaceTarget;
        public string nodeNamespaceTarget;
        public string body;

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets("Sequence_CommandNode.cs");
            string commandNodeTempletePath = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);

            Object o = ScriptGenerator.CreateScript(pathName, resourceFile, namespaceTarget, body);

            Object obj = ScriptGenerator.CreateScript($"Assets/Scripts/Sequencer/Nodes/{Path.GetFileNameWithoutExtension(pathName)}Node.cs",
                commandNodeTempletePath,
                nodeNamespaceTarget,
                Path.GetFileNameWithoutExtension(pathName)
                );

            UnityEditor.ProjectWindowUtil.ShowCreatedAsset(obj);
            UnityEditor.ProjectWindowUtil.ShowCreatedAsset(o);
        }
    }
}
