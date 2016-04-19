using UnityEditor;

namespace Notebook
{
	public class PageFactorEditor : Editor
	{
		static private PageFactory target = null;

		// Creation of the Asset
		[MenuItem("Assets/Create/Page Factory")]
		public static void CreateAsset()
		{
			ScriptableObjectUtility.CreateAsset<PageFactory>();
		}
	}
}