using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	Rigidbody2D rigid2D;

	Animator animator;

	float junpForce = 680.0f;
	float walkForce = 30.0f;
	float maxWalkSpeed = 2.0f;
	float threshold = 0.2f;

	// Use this for initialization
	void Start ()
	{
		this.rigid2D = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.Space)) {
			jumpUp ();
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			moveLeftRight (1);
		}
		if (Input.acceleration.x > this.threshold) {
			moveLeftRight (1);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveLeftRight (-1);
		}
		if (Input.acceleration.x < -this.threshold) {
			moveLeftRight (-1);
		}
		if (transform.position.y < -6.0f) {
			Debug.Log ("GameOver");
			SceneManager.LoadScene ("ClearScene");
		}

		float speedx = Math.Abs (this.rigid2D.velocity.x);

		if (this.rigid2D.velocity.y == 0) {
			this.animator.speed = speedx / 2.0f;
		} else {
			this.animator.speed = 1.0f;
		}
	}

	void jumpUp ()
	{
		if (this.rigid2D.velocity.y == 0f) {
			this.animator.SetTrigger ("JumpTrigger");
			this.rigid2D.AddForce (transform.up * this.junpForce);
		}

	}

	void moveLeftRight (int leftright)
	{
		float speedx = Math.Abs (this.rigid2D.velocity.x);

		if (speedx < this.maxWalkSpeed) {
			this.rigid2D.AddForce ((transform.right * leftright * this.walkForce));
		}
		if (leftright != 0) {
			transform.localScale = new Vector3 (leftright, 1, 1);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("ゴール");
		SceneManager.LoadScene ("ClearScene");
	}
}
