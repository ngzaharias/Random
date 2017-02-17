using UnityEngine;
using System.Collections;

static public class TextureCompare
{
	public delegate void FinishedDelegate();

	static public IEnumerator Evaluate(RenderTexture TextureA, RenderTexture TextureB, FinishedDelegate Delegate)
	{
		Texture2D backgroundRead = new Texture2D(TextureA.width, TextureA.height);
		Texture2D foregroundRead = new Texture2D(TextureB.width, TextureB.height);

		RenderTexture.active = TextureA;
		backgroundRead.ReadPixels(new Rect(0, 0, TextureA.width, TextureA.height), 0, 0);
		backgroundRead.Apply();

		RenderTexture.active = TextureB;
		foregroundRead.ReadPixels(new Rect(0, 0, TextureB.width, TextureB.height), 0, 0);
		foregroundRead.Apply();

		string log = "";
		string colours = "";

		int target = 0;
		int overlap = 0;
		int overdraw = 0;
		for (int x = 0; x < TextureA.width; ++x)
		{
			for (int y = 0; y < TextureA.height; ++y)
			{
				Color ColourA = backgroundRead.GetPixel(x, y);
				Color ColourB = foregroundRead.GetPixel(x, y);

				bool isBackground = ColourA.r > 0.5f;
				bool isForeground = ColourB.g > 0.5f;

				if (isBackground)
					++target;
				if (isBackground == true && isForeground == true)
					++overlap;
				if (isBackground == false && isForeground == true)
					++overdraw;

				colours += "b: " + ColourA + "\t f: " + ColourB + "\n";
			}
			yield return null;
		}

		log += "t: " + target + "\t op: " + overlap + "\t ow: " + overdraw;

		Debug.Log(log);
		Debug.Log(colours);

		yield return null;
	}
}
