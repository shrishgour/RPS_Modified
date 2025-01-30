using XNodeEditor;

namespace Core.Sequencer.Commands
{
    [CustomNodeEditor(typeof(ChatCommandNode))]
    public class ChatCommandNodeEditor : NodeEditor
    {
        private ChatCommandNode _commandNode;

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            //if (_commandNode == null)
            //    _commandNode = target as ChatCommandNode;

            //serializedObject.Update();

            //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("previousNode"));
            //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
            //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("nextNode"));

            //serializedObject.ApplyModifiedProperties();
        }
    }
}