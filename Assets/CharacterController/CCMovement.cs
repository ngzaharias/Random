using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MeshRenderer))]
public class CCMovement : MonoBehaviour
{

	public Material materialColliding = null;
	public Material materialNotColliding = null;

	CharacterController controller = null;
	MeshRenderer renderer = null;

	// Use this for initialization
	void Awake()
	{
		controller = GetComponent<CharacterController>();
		renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		controller.SimpleMove(new Vector3(horizontal, 0.0f, 0.0f));
	}

	void OnTriggerEnter(Collider collider)
	{
		renderer.material = materialColliding;
	}

	void OnTriggerExit(Collider collider)
	{
		renderer.material = materialNotColliding;
	}
}
