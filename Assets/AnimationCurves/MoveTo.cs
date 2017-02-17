using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour
{
	public float duration = 1.0f;
	public Vector3 end;

	float timer = 0.0f;
	Vector3 start;

	void Start ()
	{
		start = transform.position;
	}
	
	void Update ()
	{
		if (timer < duration)
		{
			AnimationCurve curve = GameSettingsObject.Instance.m_Curve1;

			timer += Time.deltaTime;
			float length = timer / duration;
			float t = curve.Evaluate(length);
			transform.position = Vector3.LerpUnclamped(start, end, t);
		}

	}
}
