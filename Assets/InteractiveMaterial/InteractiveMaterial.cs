using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractiveMaterial : MonoBehaviour
{
	private Material material;
	private Texture2D texture;

	public float brushSize = 30.0f;
	public Texture2D brushTexture = null;

	void Awake()
	{
		texture = new Texture2D(512, 512);
		material = GetComponent<Image>().material;
		material.mainTexture = texture;
	}

	void Start()
	{
		for (int y = 0; y < texture.height; y++)
		{
			for (int x = 0; x < texture.width; x++)
			{
				texture.SetPixel(x, y, Color.black);
			}
		}
		texture.Apply();
	}
	
	void FixedUpdate()
	{
		if (Input.GetMouseButton(0) == true)
		{
			Vector2 size = new Vector2(brushSize, brushSize);
			Vector2 point = Input.mousePosition;
			point = new Vector2(point.x / Screen.width, point.y / Screen.height);
			point = new Vector2(point.x * texture.width, point.y * texture.height);

			//PaintTexture(point, brushTexture);
			PaintRadius(point, (int)brushSize);
		}
	}

	void PaintBox(Vector2 point, Vector2 size)
	{
		Vector2 half	= size / 2.0f;
		int xbegin		= (int) (point.x - half.x);
		int xend		= (int) (point.x + half.x);
		for (int x = xbegin; x < xend; ++x)
		{
			int ybegin	= (int) (point.y - half.y);
			int yend	= (int) (point.y + half.y);
			for (int y = ybegin; y < yend; ++y)
			{
				texture.SetPixel(x, y, Color.white);
			}
		}
		texture.Apply();
	}

	void PaintRadius(Vector2 point, int radius)
	{
		int radiusHalf = radius / 2;
		int radiusSqr = radius * radius;
		point = point - new Vector2(radiusHalf, radiusHalf);

		for (int x = 0; x < radius; ++x)
		{
			for (int y = 0; y < radius; ++y)
			{
				Vector2 pixel = point + new Vector2(x, y);
				float distanceSqr = Vector2.SqrMagnitude(point - pixel);
				if (distanceSqr < radiusSqr)
				{
					texture.SetPixel((int)point.x + x, (int)point.y + y, Color.white);
				}
			}
		}

		texture.Apply();
	}

	void PaintTexture(Vector2 point, Texture2D brush)
	{
		int tX = (int)point.x - (brush.width / 2);
		int tY = (int)point.y - (brush.height / 2);

		for (int bX = 0; bX < brush.width; ++bX)
		{
			for (int bY = 0; bY < brush.height; ++bY)
			{
				Color colour = brush.GetPixel(bX, bY);
				texture.SetPixel(tX + bX, tY + bY, colour);
			}
		}
		texture.Apply();
	}
}
