using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using System.Collections;
using System.Collections.Generic;

namespace Notebook
{
	[CustomEditor(typeof(Notebook))]
	public class NotebookEditor : Editor
	{
		private Notebook targetClass = null;
		private SerializedObject targetObject = null;

		private SerializedProperty m_coverFront;        // Sprite
		private SerializedProperty m_coverInsideLeft;   // Sprite
		private SerializedProperty m_coverInsideRight;  // Sprite
		private SerializedProperty m_coverBack;         // Sprite

		private SerializedProperty m_categories;        // List<Category>

		void OnEnable()
		{
			targetClass = (Notebook)target;
			targetObject = new SerializedObject(targetClass);

			m_coverFront = targetObject.FindProperty("m_coverFront");
			m_coverInsideLeft = targetObject.FindProperty("m_coverInsideLeft");
			m_coverInsideRight = targetObject.FindProperty("m_coverInsideRight");
			m_coverBack = targetObject.FindProperty("m_coverBack");

			m_categories = targetObject.FindProperty("m_categories");
		}

		public override void OnInspectorGUI()
		{
			targetObject.Update();

			GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
			InspectorCovers();
			GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
			InspectorCategories();
			GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
			InspectorDeveloper();

			ApplyChanges();
		}

		private void ApplyChanges()
		{
			targetObject.ApplyModifiedProperties();
			if (GUI.changed == true)
			{
				targetClass.GenerateCovers();
			}
		}

		private void InspectorCovers()
		{
			GUILayout.Label("Covers: ", EditorStyles.boldLabel);
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Cover Front: ");
			m_coverFront.objectReferenceValue
				= EditorGUILayout.ObjectField(m_coverFront.objectReferenceValue
				, typeof(Sprite)
				, false);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Cover Inside Left: ");
			m_coverInsideLeft.objectReferenceValue
				= EditorGUILayout.ObjectField(m_coverInsideLeft.objectReferenceValue
				, typeof(Sprite)
				, false);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Cover Inside Right: ");
			m_coverInsideRight.objectReferenceValue
				= EditorGUILayout.ObjectField(m_coverInsideRight.objectReferenceValue
				, typeof(Sprite)
				, false);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Cover Back: ");
			m_coverBack.objectReferenceValue
				= EditorGUILayout.ObjectField(m_coverBack.objectReferenceValue
				, typeof(Sprite)
				, false);
			EditorGUILayout.EndHorizontal();
		}

		private void InspectorCategories()
		{
			GUILayout.Label("Categories: ", EditorStyles.boldLabel);
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			if (GUILayout.Button("Add"
				, EditorStyles.miniButtonLeft
				, GUILayout.MaxWidth(64.0f)) == true)
			{
				m_categories.InsertArrayElementAtIndex(m_categories.arraySize);
			}
			if (GUILayout.Button("Remove"
				, EditorStyles.miniButtonRight
				, GUILayout.MaxWidth(64.0f)) == true)
			{
				m_categories.DeleteArrayElementAtIndex(m_categories.arraySize - 1);
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			++EditorGUI.indentLevel;
			for (int i = 0; i < m_categories.arraySize; ++i)
			{
				SerializedProperty category = m_categories.GetArrayElementAtIndex(i);
				SerializedProperty isFoldedOut = category.FindPropertyRelative("debug_isFoldedOut");
				SerializedProperty name = category.FindPropertyRelative("name");
				SerializedProperty type = category.FindPropertyRelative("type");

				EditorGUILayout.BeginHorizontal();
				isFoldedOut.boolValue = EditorGUILayout.Foldout(isFoldedOut.boolValue, "Category: " + name.stringValue);

				if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.MaxWidth(22.0f)) == true)
				{
					m_categories.MoveArrayElement(i, i - 1);
					EditorGUILayout.EndHorizontal();
					continue;
				}

				if (GUILayout.Button("-"
					, EditorStyles.miniButtonMid
					, GUILayout.MaxWidth(22.0f)) == true)
				{
					m_categories.MoveArrayElement(i, i + 1);
					EditorGUILayout.EndHorizontal();
					continue;
				}

				if (GUILayout.Button("x"
					, EditorStyles.miniButtonRight
					, GUILayout.MaxWidth(22.0f)) == true)
				{
					m_categories.DeleteArrayElementAtIndex(i);
					EditorGUILayout.EndHorizontal();
					continue;
				}
				EditorGUILayout.EndHorizontal();

				if (isFoldedOut.boolValue == false)
					continue;

				name.stringValue = EditorGUILayout.TextField("Name: ", name.stringValue);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Page Type: ");
				type.enumValueIndex = (int)(PageType)EditorGUILayout.EnumPopup((PageType)type.enumValueIndex);
				EditorGUILayout.EndHorizontal();
			}
			--EditorGUI.indentLevel;
		}

		private void InspectorDeveloper()
		{
			GUILayout.Label("Developer: ", EditorStyles.boldLabel);
			EditorGUILayout.Space();

			if (GUILayout.Button("Force Generate Categories & Pages"
				, EditorStyles.miniButton) == true)
			{
				targetClass.GenerateCategories();
			}
			if (GUILayout.Button("Force Generate Covers"
				, EditorStyles.miniButton) == true)
			{
				targetClass.GenerateCovers();
			}
			EditorGUILayout.Space();

			GUILayout.Label("Categories: ");
			GUILayout.Label("- Turn To: ");
			EditorGUILayout.BeginHorizontal();
			for (int i = 0; i < m_categories.arraySize; ++i)
			{
				SerializedProperty category = m_categories.GetArrayElementAtIndex(i);
				SerializedProperty name = category.FindPropertyRelative("name");

				if (GUILayout.Button(name.stringValue
				, EditorStyles.miniButton) == true)
				{
					targetClass.TurnToCategory(i);
				}

				if (i % 5 == 4)
				{
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
				}

			}
			EditorGUILayout.EndHorizontal();

			GUILayout.Label("Pages: ");
			if (GUILayout.Button("Clear Pages"
				, EditorStyles.miniButton) == true)
			{
				targetClass.ClearPages();
			}
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Turn Left"
				, EditorStyles.miniButtonLeft) == true)
			{
				targetClass.TurnPage(-2);
			}
			if (GUILayout.Button("Turn Right"
				, EditorStyles.miniButtonRight) == true)
			{
				targetClass.TurnPage( 2);
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}
	}
}