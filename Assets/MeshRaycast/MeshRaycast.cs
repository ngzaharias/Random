using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshRaycast : MonoBehaviour
{
	protected Mesh mesh = null;
	protected Camera cam = null;

	void Awake()
	{
		cam = Camera.main;
		MeshFilter filter = GetComponent<MeshFilter>();
		mesh = filter.sharedMesh;
	}

	void Update ()
	{
		Vector3[] vertices = mesh.vertices;

		string log = "";

		float distance = Vector3.Distance(transform.position, cam.transform.position);
		foreach (Vector3 vertex in vertices)
		{
			Vector3 vector = cam.transform.forward * distance;
			Vector3 point = transform.rotation * vertex;

			bool isHit = Physics.Raycast(point - vector, cam.transform.forward);

			log += isHit + "\n";
			Debug.DrawRay(point - vector, cam.transform.forward, (isHit) ? Color.green : Color.red);
		}

		if (Input.GetMouseButtonDown(0))
			Debug.Log(log);
	}
}
