using System;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Core.Sequencer
{
    [CustomNodeGraphEditor(typeof(SequenceGraph))]
    public class SequenceGraphEditor : NodeGraphEditor
    {
        public override void OnCreate()
        {
            base.OnCreate();
            CreateRootNode();
        }

        public override void OnWindowFocus()
        {
            base.OnWindowFocus();
            CreateRootNode();
        }

        private void CreateRootNode()
        {
            if (target.nodes.Find(n => n is SequenceNode) == null)
            {
                SequenceNode node = target.AddNode<SequenceNode>();
                node.name = "Sequence Node";
                AssetDatabase.AddObjectToAsset(node, target);
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
        }

        public override void OnGUI()
        {
            SequenceGraph sequenceGraph = (SequenceGraph)target;
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            if (GUILayout.Button("Force Update", EditorStyles.toolbarButton))
            {
                Save();
            }

            GUILayout.Label(new GUIContent($"Selected Generator: {sequenceGraph.name}"));
            GUILayout.EndHorizontal();
        }

        private void Save()
        {
            SequenceGraph sequenceGraph = (SequenceGraph)target;
            sequenceGraph.ConvertGraphToSequence();
        }

        public override string GetNodeMenuName(Type type)
        {
            if (typeof(ICommandNode).IsAssignableFrom(type))
            {
                return $"Commands/{type.Name}";
            }
            return null;
        }
    }
}