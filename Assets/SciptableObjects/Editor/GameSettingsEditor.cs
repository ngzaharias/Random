using UnityEditor;

public class GameSettingsEditor : EditorWindow
{
	[MenuItem("Assets/Create/Game Settings Object")]
	static public void Create()
	{
		ScriptableObjectUtility.CreateAsset<GameSettingsObject>();
	}
}
