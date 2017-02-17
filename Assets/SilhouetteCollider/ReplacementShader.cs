using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ReplacementShader : MonoBehaviour
{
	[SerializeField]
	protected Shader m_Shader = null;
	protected Camera m_Camera = null;

	void Awake()
	{
		m_Camera = GetComponent<Camera>();
	}

	void Start()
	{
		m_Camera.SetReplacementShader(m_Shader, "RenderType");
	}

	void OnEnable()
	{
		m_Camera.SetReplacementShader(m_Shader, "RenderType");
	}

	void OnDisable()
	{
		m_Camera.ResetReplacementShader();
	}
}
