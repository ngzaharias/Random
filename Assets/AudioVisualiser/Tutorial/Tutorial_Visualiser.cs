using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Visualiser : MonoBehaviour
{
	[SerializeField] private AudioPeer audioPeer;
	[SerializeField] private GameObject cubePrefab;
	[SerializeField] private int sampleCount;
	[SerializeField] private float cubeSize = 2.0f;
	private GameObject[] cubes;

	void Start ()
	{
		cubes = new GameObject[sampleCount];

		float distance = 100.0f;
		float rotation = 360.0f / cubes.Length;
		for (int i = 0; i < cubes.Length; ++i)
		{
			GameObject cube = Instantiate(cubePrefab);
			cube.name = string.Format("SampleCube[{0}]", i);
			cube.transform.SetParent(transform);
			cube.transform.rotation = Quaternion.Euler(0.0f, rotation * i, 0.0f);
			cube.transform.position = cube.transform.forward * distance;
			cubes[i] = cube;
		}
	}
	
	void Update ()
	{
		float[] samples = audioPeer.Samples;
		for (int i = 0; i < cubes.Length; ++i)
		{
			if (i >= samples.Length)
				break;

			cubes[i].transform.localScale = new Vector3(1.0f, samples[i] * cubeSize, 1.0f);
		}
	}
}
