using UnityEngine;
using System.Collections.Generic;

public class AudioVisualiserSegment : MonoBehaviour
{
	public int SpectrumStart = 0;
	public int SpectrumFinish = 1024;
	public float Height = 1.0f;
	public float Width = 32.0f;
	public float Speed = 5.0f;

	public int ObjectCount = 8;
	public GameObject ObjectPrefab = null;
	protected List<GameObject> Objects = new List<GameObject>();

	public void Rebuild()
	{
		if (ObjectPrefab == null)
			return;

		for (int i = 0; i < Objects.Count; ++i)
			Objects[i].SetActive(i < ObjectCount);

		for (int i = Objects.Count; i < ObjectCount; ++i)
		{
			GameObject Object = Instantiate(ObjectPrefab);
			Object.transform.SetParent(transform);
			Objects.Add(Object);
		}

		float Half = Width / 2;
		float Single = Width / ObjectCount;
		for (int i = 0; i < ObjectCount; ++i)
		{
			Vector3 Scale = new Vector3(Single * 0.9f, 0.0f, 1.0f);
			Vector3 Position = new Vector3(i * Single - Half + (Single/2), 0.0f, 0.0f);
			Objects[i].transform.localScale = Scale;
			Objects[i].transform.localPosition = Position;
		}
	}

	public void UpdateSegment(float[] Spectrum)
	{
		if (Objects.Count == 0)
			return;

		int ObjectStart = Mathf.Min(SpectrumStart, Spectrum.Length);
		int ObjectFinish = Mathf.Min(SpectrumFinish, Spectrum.Length);
		float Increment = (ObjectFinish - ObjectStart) / (float)ObjectCount;
		for (int i = 0; i < Objects.Count; ++i)
		{
			int Start = ObjectStart + Mathf.FloorToInt(i * Increment);
			int Finish = ObjectStart + Mathf.CeilToInt((i+1) * Increment);

			float Value = 0.0f;
			for (int j = Start; j < Finish; ++j)
				Value += Spectrum[j];
			Value /= Finish - Start;

			Vector3 Scale = Objects[i].transform.localScale;
			Scale.y = Mathf.Lerp(Scale.y, Value * Height, Time.deltaTime * Speed);
			Objects[i].transform.localScale = Scale;
		}
	}
}