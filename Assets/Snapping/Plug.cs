using UnityEngine;
using System.Collections;

public class Plug : MonoBehaviour
{
	public Material overlap = null;
	public Material overlapNon = null;

	public Renderer parentRenderer = null;

	void Awake()
	{
		parentRenderer = transform.parent.GetComponent<Renderer>();
		parentRenderer.material = overlapNon;
	}

	void OnTriggerEnter(Collider other)
	{
		parentRenderer.material = overlap;
	}

	void OnTriggerExit(Collider other)
	{
		parentRenderer.material = overlapNon;
	}
}
