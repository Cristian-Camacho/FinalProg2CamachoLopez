using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CanEditMultipleObjects]
[CustomEditor(typeof(Node))]
public class CustomNodeInspector : Editor
{
    private Node _node;

    private void OnEnable()
    {
        _node = (Node)target;
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(_node.transform.position, Vector3.up, _node.radiusNeight);
    }
}
