using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Notebook
{
	[System.Serializable]
	public class Category
	{
		const int DEFAULT_PAGE_COUNT = 10;

		[SerializeField]
		private bool debug_isFoldedOut = false;

		[SerializeField]
		public string name = "BLANK";
		[SerializeField]
		public PageType type = PageType.BLANK;

		public int m_pageCurrent = -1;

		public List<Page> m_pages = null;

		public RectTransform m_transform = null;

		public RectTransform Instantiate()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "Category: " + name;
			gameObject.SetActive(false);

			m_transform = gameObject.AddComponent<RectTransform>();
			m_transform.sizeDelta = Vector2.zero;

			return m_transform;
		}

		public void InstantiatePages(int count = 0)
		{
			m_pages.Clear();

			if (m_transform == null)
			{
				Debug.LogError("Notebook: Instantiate the Category before instantiation its pages!");
				return;
			}

			// saved pages should have been loaded by this point
			// by default we want X number of pages to start
			count = Mathf.Max(count, DEFAULT_PAGE_COUNT);
			for (int i = 0; i < count; ++i)
			{
				Page page = PageFactory.Instance.Instantiate(type);
				page.gameObject.name = "Page: " + (i + 1).ToString();
				page.transform.SetParent(m_transform, false);
				m_pages.Add(page);
			}
		}
	}
}
