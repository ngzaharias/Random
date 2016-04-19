using UnityEngine;
using System.Collections;

namespace Notebook
{
	public enum PageType
	{
		BLANK = 0,
		NOTES,
	}

	public class Page : MonoBehaviour
	{
		[HideInInspector]
		protected Category category = null;

		public virtual void Load(Page page)
		{
		}

		public static PageType StringToType(string name)
		{
			switch (name)
			{
				default: return PageType.BLANK;
			}
		}

		public static string TypeToString(PageType type)
		{
			switch (type)
			{
				default: return "BLANK";
			}
		}

	}
}