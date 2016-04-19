using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Notebook
{

	[ExecuteInEditMode]
	public class Notebook : MonoBehaviour
	{
		[SerializeField]
		private Sprite m_coverFront = null;
		[SerializeField]
		private Sprite m_coverInsideLeft = null;
		[SerializeField]
		private Sprite m_coverInsideRight = null;
		[SerializeField]
		private Sprite m_coverBack = null;
		[SerializeField]
		private List<Category> m_categories = new List<Category>();
		private int m_categoryCurrent = -1;

		private RectTransform parentTransform = null;

		private RectTransform pagesTransform = null;
		private RectTransform pagesLeftTransform = null;
		private RectTransform pagesRightTransform = null;

		private Image coverBackImage = null;
		private Image coverInsideLeftImage = null;
		private Image coverInsideRightImage = null;
		private Image coverFrontImage = null;

		void Awake()
		{
			parentTransform	= GetComponent<RectTransform>();

			// setup the images
			coverBackImage	= this.GetComponentFromChild<Image>("CoverBack");
			coverInsideLeftImage = this.GetComponentFromChild<Image>("CoverInsideLeft");
			coverInsideRightImage = this.GetComponentFromChild<Image>("CoverInsideRight");
			coverFrontImage = this.GetComponentFromChild<Image>("CoverFront");

			// cleanup any remenants
			pagesTransform = this.GetComponentFromChild<RectTransform>("Pages");
			pagesLeftTransform = this.GetComponentFromChild<RectTransform>("LeftPages");
			pagesRightTransform = this.GetComponentFromChild<RectTransform>("RightPages");

			// setup the categories
			GenerateCategories();
		}

		public void GenerateCategories()
		{
			// cleanup previous categories and pages
			pagesTransform.DestroyImmediateChildren();
			pagesLeftTransform.DestroyImmediateChildren();
			pagesRightTransform.DestroyImmediateChildren();

			// create categories
			// create pages
			// load existing pages
			for (int i = 0; i < m_categories.Count; ++i)
			{
				RectTransform categoryTransform = m_categories[i].Instantiate();
				categoryTransform.SetParent(pagesTransform, false);
				m_categories[i].InstantiatePages();
			}
		}

		public void GenerateCovers()
		{
			coverFrontImage.sprite = m_coverFront;
			coverInsideLeftImage.sprite = m_coverInsideLeft;
			coverInsideRightImage.sprite = m_coverInsideRight;
			coverBackImage.sprite = m_coverBack;
		}

		public void ClearPages()
		{
			if (m_categoryCurrent < 0 || m_categoryCurrent > m_categories.Count)
				return;

			Category category = m_categories[m_categoryCurrent];
			Page[] left = pagesLeftTransform.GetComponentsInChildren<Page>();
			foreach (Page page in left)
			{
				page.transform.SetParent(category.m_transform, false);
			}

			Page[] right = pagesRightTransform.GetComponentsInChildren<Page>();
			foreach (Page page in right)
			{
				page.transform.SetParent(category.m_transform, false);
			}
		}

		public void TurnCategory(int amount)
		{
			int index = m_categoryCurrent + amount;
			TurnToCategory(index);
		}

		public void TurnToCategory(int index)
		{
			if (index < 0 || index > m_categories.Count)
				return;

			ClearPages();

			m_categoryCurrent = index;
			TurnToPage(m_categories[m_categoryCurrent].m_pageCurrent);
		}

		public void TurnPage(int amount)
		{
			if (m_categoryCurrent < 0 || m_categoryCurrent > m_categories.Count)
				return;

			Category category = m_categories[m_categoryCurrent];
			int index = category.m_pageCurrent + amount;
			TurnToPage(index);
		}

		public void TurnToPage(int index)
		{
			if (m_categoryCurrent < 0 || m_categoryCurrent > m_categories.Count)
				return;

			int left = index - (index % 2);
			int right = index + 1;

			Category category = m_categories[m_categoryCurrent];
			if (left >= 0 && left < category.m_pages.Count)
			{
				ClearPages();
				Page page = category.m_pages[left];
				page.transform.SetParent(pagesLeftTransform, false);
				category.m_pageCurrent = left;
			}

			// we only clear on the left because if a right exists a left must too
			if (right >= 0 && right < category.m_pages.Count)
			{
				Page page = category.m_pages[right];
				page.transform.SetParent(pagesRightTransform, false);
			}
		}
	}
}