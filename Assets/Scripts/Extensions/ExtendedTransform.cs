using UnityEngine;
using System.Collections;

public static class ExtendedTransform
{
	public static void DestroyChildren(this Transform target, float t = 0.0f)
	{
		GameObject[] children = target.GetComponentsInChildren<GameObject>(true);
		for (int i = children.Length - 1; i >= 0; --i)
		{
			if (children[i] != target)
			{
				Object.Destroy(children[i], t);
			}
		}
	}

	public static void DestroyImmediateChildren(this Transform target, bool allowDestroyingAssets = false)
	{
		Transform[] children = target.GetComponentsInChildren<Transform>(true);
		for (int i = children.Length - 1; i >= 0; --i)
		{
			if (children[i] != target)
			{
				Object.DestroyImmediate(children[i].gameObject, allowDestroyingAssets);
			}
		}
	}
}
