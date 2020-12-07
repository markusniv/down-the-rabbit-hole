using UnityEditor;
using UnityEngine;

/// <summary>
/// Displays Custom info such as current states.
/// </summary>
[CustomEditor(typeof(Character), true)]
public class CharacterScriptEditor : Editor
{
    void OnEnable()
    {
        var script = (Character)target;
        if (script?.Movement == null || script?.Combat == null) return;
        script.Movement.OnStateChange += StateUpdate;
        script.Combat.OnStateChange += StateUpdate;
    }

    void StateUpdate(State state)
    {
        Repaint();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var script = (Character)target;
        if (script?.Combat == null || script?.Movement == null) return;
        EditorGUILayout.LabelField("Combat State", script.Combat.CurrentState?.GetType().FullName);
        EditorGUILayout.LabelField("Movement State", script.Movement.CurrentState?.GetType().FullName);
    }
}