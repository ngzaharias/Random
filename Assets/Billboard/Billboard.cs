using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	public Transform Target = null;
	public float angle = 0.0f;

	// Update is called once per frame
	void Update ()
	{
		if (Target == null)
			return;

		Vector3 forward = Target.forward;
		Quaternion rotation = Target.rotation * Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = rotation;
	}
}
