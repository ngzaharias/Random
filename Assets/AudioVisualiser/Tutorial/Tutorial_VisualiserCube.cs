using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_VisualiserCube : MonoBehaviour
{
	[SerializeField] private AudioPeer audioPeer;
	[SerializeField] private int band = 0;
	[SerializeField] private float scale = 100.0f;
	[SerializeField] private bool useAmplitude = false;
	[SerializeField] private bool useBuffer = true;

	private MeshRenderer cached_Renderer;
	private Material cached_Material;

	void Awake()
	{
		cached_Renderer = GetComponentInChildren<MeshRenderer>(true);
		cached_Material = cached_Renderer.material;
	}

	void Update ()
	{
		float amplitude = (useBuffer) ? audioPeer.GetAmplitudeBuffer() : audioPeer.GetAmplitude();
		float audioBand = (useBuffer) ? audioPeer.GetAudioBandBuffer(band) : audioPeer.GetAudioBand(band);
		float frequencyBand = (useBuffer) ? audioPeer.GetFrequencyBandBuffer(band) : audioPeer.GetFrequencyBand(band);
		Color colour = cached_Material.GetColor("_Color");

		Vector3 localScale = (useAmplitude) ? new Vector3(amplitude * scale, amplitude * scale, amplitude * scale) : new Vector3(1.0f, 1.0f + frequencyBand * scale, 1.0f);
		Color emissionColor = (useAmplitude) ? new Color(amplitude, amplitude, amplitude) : new Color(audioBand, audioBand, audioBand);

		transform.localScale = localScale;
		cached_Material.SetColor("_EmissionColor", colour * emissionColor);
	}
}
