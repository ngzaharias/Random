using UnityEngine;
using System.Collections;

public static class ExtendedMonoBehaviour
{
	public static T GetComponentFromChild<T>(this MonoBehaviour target, string name)
	{
		Transform child = target.transform.FindChild(name);
		return child.gameObject.GetComponent<T>();
	}
}
