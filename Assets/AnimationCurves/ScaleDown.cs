using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum State
{
	SNAP,
	MINUS,
	MULTIPLY,
	LERP,
	ANIMATE,
	CURVE,
}

public class ScaleDown : MonoBehaviour
{
	public State state = State.SNAP;
	bool isActive = false;

	public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			transform.localScale = Vector3.one;
			isActive = true;
		}

		if (isActive)
		{
			switch (state)
			{
				case State.SNAP: Snap(); break;
				case State.MINUS: Minus(); break;
				case State.MULTIPLY: Multiply(); break;
				case State.LERP: StartCoroutine(Lerp()); break;
				case State.ANIMATE: Animate(); break;
				case State.CURVE: StartCoroutine(Curve()); break;
			}
		}
	}

	void Snap()
	{
		transform.localScale = Vector3.zero;
	}

	void Minus()
	{
		if (transform.localScale.x < 0)
		{
			transform.localScale = Vector3.zero;
			isActive = false;
			return;
		}

		transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

	}

	void Multiply()
	{
		if (transform.localScale.x < 0)
		{
			transform.localScale = Vector3.zero;
			isActive = false;
			return;
		}

		transform.localScale = transform.localScale * 0.95f;
	}

	IEnumerator Lerp()
	{
		isActive = false;

		Vector3 Start = transform.localScale;
		Vector3 Target = Vector3.zero;

		float timer = 0.0f;
		float duration = 10.0f;
		while (timer < duration)
		{
			timer += Time.deltaTime;
			transform.localScale = Vector3.Lerp(Start, Target, timer/duration);
			yield return null;
		}

		yield return null;
	}

	void Animate()
	{
		GetComponent<Animator>().SetTrigger("scale");
		isActive = false;
	}

	IEnumerator Curve()
	{
		isActive = false;

		Vector3 Start = transform.localScale;
		Vector3 Target = Vector3.zero;
		float timer = 0.0f;
		float duration = 2.0f;
		while (timer < duration)
		{
			timer += Time.deltaTime;
			float t = curve.Evaluate(timer/duration);
			Vector3 value = Vector3.Lerp(Start, Target, t);

			transform.localScale = new Vector3(t, t, t);
			yield return null;
		}
		yield return null;
	}
}
