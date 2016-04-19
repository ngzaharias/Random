using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour
{
	public Server server = null;
	public Client client = null;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.S) == true)
		{
			server.gameObject.SetActive(true);
			client.gameObject.SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.C) == true)
		{
			server.gameObject.SetActive(false);
			client.gameObject.SetActive(true);
		}
	}
}
