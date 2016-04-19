using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Notebook
{
	

	public class PageFactory : ScriptableObject
	{
		public static PageFactory m_instance = null;
		public static PageFactory Instance
		{
			get
			{
				if (m_instance == null)
					m_instance = (PageFactory)Resources.Load("PageFactory", typeof(PageFactory));
				if (m_instance == null)
					Debug.LogError("PageFactory doesn't exsist yet! Go to ->Assets/Create/Page Factory");
				return m_instance;
			}
		}

		public Page pageBlankPrefab = null;
		public PageNotes pageNotesPrefab = null;

		public Page Instantiate(PageType type)
		{
			// instantiate gameobject
			Page page = null;
			switch (type)
			{
				case PageType.NOTES: page = Instantiate<PageNotes>(pageNotesPrefab); break;
				default: page = Instantiate<Page>(pageBlankPrefab); break;
			}
			page.gameObject.name = Page.TypeToString(type);

			// setup positioning
			RectTransform transform = page.GetComponent<RectTransform>();
			transform.anchorMin = Vector2.zero;
			transform.anchorMax = Vector2.one;
			transform.offsetMin = Vector2.zero; // left & bottom
			transform.offsetMax = -Vector2.zero; // right & top

			// add specified page

			return page;
		}
	}
}