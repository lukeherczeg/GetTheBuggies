using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{

	public CharacterController2D controller;
	public Animator animator;
	[SerializeField] private Ground ground;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	bool lookingUp = false;
	bool gameOver = false;

	private float timeSinceDeath = -1000;

	private bool isJoyStick = false;

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool isCrouching) {
		animator.SetBool("IsCrouching", isCrouching);
	}

	public void OnLookingUp(bool isLookingUp) {
		animator.SetBool("IsLookingUp", isLookingUp);
	}
		
	// Update is called once per frame
	void Update()
	{
		if (gameOver)
		{
			if ((Time.time - timeSinceDeath > 2.5))
			{
				SceneManager.LoadScene(2);
			}
			ground.scrollSpeedVariable = 0;
		}
		else
		{

			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}
			else
			{
				if (controller.m_Rigidbody2D.velocity.y < -.2f)
				{
					animator.SetBool("IsFalling", true);
				}
				else
				{
					animator.SetBool("IsFalling", false);
				}
			}

			// One time check to see if a controller is being used
			if (!isJoyStick)
				isJoyStick = ((Input.GetAxis("VerticalJoystick") != 0) || (Input.GetAxis("CrouchJoystick") != 0));

			// Make sure they haven't switched back to keyboard
			if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Crouch"))
				isJoyStick = false;

			// Handle inputs
			if (isJoyStick)
			{
				if (Input.GetAxis("CrouchJoystick") > 0.1f)
				{
					crouch = true;
				}
				else if (Input.GetAxis("CrouchJoystick") < 0.1f)
				{
					crouch = false;
				}

				if (Input.GetAxis("VerticalJoystick") > 0.01f)
				{
					lookingUp = true;
				}
				else if (Input.GetAxis("VerticalJoystick") < 0.01f)
				{
					lookingUp = false;
				}
			}
			else
			{
				if (Input.GetButtonDown("Vertical"))
				{
					lookingUp = true;
				}
				else if (Input.GetButtonUp("Vertical"))
				{
					lookingUp = false;
				}

				if (Input.GetButtonDown("Crouch"))
				{
					crouch = true;
				}
				else if (Input.GetButtonUp("Crouch"))
				{
					crouch = false;
				}
			}

			// Moves player to the left, simulating movement to the right
			if (controller.m_Grounded)
				controller.m_Rigidbody2D.position -= new Vector2(ground.scrollSpeedVariable, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D trig)
	{
		if (trig.gameObject.name == "LeftLevelBound" || trig.gameObject.name == "The Hand")
		{
			gameOver = true;
			controller.Move(0, false, false, false);
			animator.SetBool("IsDead", true);
			animator.SetBool("IsJumping", false);
			animator.SetBool("IsFalling", false);
			ground.scrollSpeedVariable = 0;
			timeSinceDeath = Time.time;
		}
	}

	void FixedUpdate()
	{ 
		if (!gameOver) 
		{
			// Move our character
			controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, lookingUp);
			jump = false;
		}
	}
}