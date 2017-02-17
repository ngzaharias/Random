using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	protected float m_Speed = 1.0f;
	[SerializeField]
	protected AnimationCurve m_SpeedCurve = AnimationCurve.Linear(0, 0, 1, 1);
	protected float m_SpeedTimer = 0.0f;

	[SerializeField]
	protected float m_JumpForce = 100.0f;

	[SerializeField]
	protected SpriteRenderer m_Sprite = null;

	protected Animator m_Animator = null;
	protected Rigidbody2D m_Rigidbody = null;

	// animation states
	protected bool isWalking = false;
	protected bool isJumping = false;

	void Awake()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		m_SpeedTimer += Time.deltaTime;

		ProcessInput();
		UpdateAnimations();
	}

	void ProcessInput()
	{
		if (Input.GetKey(KeyCode.A) == true)
		{
			//if (m_Sprite.flipX == false)
			//	m_SpeedTimer = 0.0f;

			isWalking = true;
			float speed = m_Speed * m_SpeedCurve.Evaluate(m_SpeedTimer);
			transform.Translate(-speed, 0.0f, 0.0f);
			m_Sprite.flipX = true;
		}
		else if (Input.GetKey(KeyCode.D) == true)
		{
			//if (m_Sprite.flipX == true)
			//	m_SpeedTimer = 0.0f;

			isWalking = true;
			float speed = m_Speed * m_SpeedCurve.Evaluate(m_SpeedTimer);
			transform.Translate(speed, 0.0f, 0.0f);
			m_Sprite.flipX = false;
		}
		else
		{
			isWalking = false;
			m_SpeedTimer = 0.0f;
		}

		if (Input.GetKeyDown(KeyCode.Space) == true)
		{
			isWalking = false;
			isJumping = true;
			m_Rigidbody.AddForce(Vector2.up * m_JumpForce);
		}
		else
		{
			isJumping = false;
		}
	}

	void UpdateAnimations()
	{
		if (m_Animator == null)
			return;

		m_Animator.SetBool("isWalking", isWalking);
		m_Animator.SetBool("isJumping", isJumping);
	}
}
