using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EarthBehaviour))]
public class EarthBeahaviourInspector : Editor
{

	Vector2 vectors;
	Mesh mesh;
	public Vector3[] mappedPoints;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Utilities", EditorStyles.boldLabel);
		vectors = EditorGUILayout.Vector2Field("UV Pos", vectors);
		mesh = (UnityEngine.Mesh)EditorGUILayout.ObjectField("Mesh", mesh, typeof(Mesh));

		if (GUILayout.Button("Convert to Localspace"))
		{
			Vector3[] mappedPoints2 = mesh.GetMappedPoints(vectors);
			mappedPoints = mappedPoints2;
		}
		if (mappedPoints != null)
			for (int i = 0; i < mappedPoints.Length; i++)
				EditorGUILayout.Vector3Field("Output Pos " + i, mappedPoints[i]);
	}
}
