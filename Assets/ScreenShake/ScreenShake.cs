using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
	public float duration = 1.0f;
	public float speed = 1.0f;
	public float magnitude = 1.0f;
	public AnimationCurve XShakeCurve = AnimationCurve.Linear(0.0f, -1.0f, 0.1f, 1.0f);

	private bool isShakeActive = false;

	void Update()
	{
		if (Input.GetMouseButton(0) == true
		&& isShakeActive == false)
		{
			StartCoroutine(Shake());
		}
	}

	IEnumerator Shake()
	{
		double elapsed = 0.0f;
		Vector3 startOffset = transform.position;

		isShakeActive = true;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			float x = XShakeCurve.Evaluate(Time.time * speed);

			Vector3 localPosition = transform.localPosition;
			localPosition.x = x * magnitude;
			transform.localPosition = localPosition;
			yield return null;
		}

		isShakeActive = false;
		transform.localPosition = startOffset;
		yield return null;
	}
}
