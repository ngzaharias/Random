using UnityEngine;

public class GameSettingsObject : ScriptableObject
{
	static private GameSettingsObject m_Instance = null;
	static public GameSettingsObject Instance
	{
		get
		{
			if (m_Instance == null)
				m_Instance = (GameSettingsObject)Resources.Load("GameSettingsObject", typeof(GameSettingsObject));
			return m_Instance;
		}
	}

	public AnimationCurve m_Curve1 = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve m_Curve2 = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve m_Curve3 = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve m_Curve4 = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve m_Curve5 = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve m_Curve6 = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve m_Curve7 = AnimationCurve.Linear(0, 0, 1, 1);
}
