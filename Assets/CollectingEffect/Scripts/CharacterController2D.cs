using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	public float m_MaxSpeed = 5;
	public float m_JumpForce = 5;

	private Rigidbody2D m_Rigidbody2D;
	private bool m_Jump = false;
	private bool m_Grounded = false;

	// Use this for initialization
	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_Jump && m_Grounded)
		{
			m_Jump = Input.GetButtonDown("Jump");
		}
	}

	private void FixedUpdate()
	{
		// Read the inputs.
		float move = Input.GetAxis("Horizontal");
		m_Grounded = false;
		if (Mathf.Abs(m_Rigidbody2D.velocity.y) < 0.1f) {
			m_Grounded = true;
		}

		// Move the character
		m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

		if (m_Jump && m_Grounded) {
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			m_Jump = false;
		}
	}
}
