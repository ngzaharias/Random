using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
	// Sub Bass: 20 to 60Hz
	// Bass: 60 to 250Hz
	// Low Midrange: 250 to 500Hz
	// Midrange: 500 to 2kHz
	// Upper Midrange: 2k to 4kHz
	// Presence: 4k to 6kHz
	// Brilliance: 6k to 20kHz

	public int Samples = 1024;
	public FFTWindow Window = FFTWindow.BlackmanHarris;
	public GameObject Prefab = null;
	protected AudioVisualiserSegment[] Segments = null;

	void Awake()
	{
		Segments = FindObjectsOfType<AudioVisualiserSegment>();
	}

	void Start()
	{
		for (int i = 0; i < Segments.Length; ++i)
			Segments[i].Rebuild();
	}

	void Update()
	{
		float[] Spectrum = new float[Samples];
		AudioListener.GetSpectrumData(Spectrum, 0, Window);

		for (int i = 0; i < Segments.Length; ++i)
			Segments[i].UpdateSegment(Spectrum);
	}
}
