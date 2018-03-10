using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	[SerializeField] float _speed = 6.0F;
	[SerializeField] float _jumpSpeed = 8.0F;
	[SerializeField] float _gravity = 20.0F;

	private Vector3 _moveDirection = Vector3.zero;
	private Vector3 _moveInput = Vector3.zero;

	private CharacterController _controller;

	private void Start() {
		_controller = GetComponent<CharacterController>();
	}

	void Update() {
		_moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (_controller.isGrounded) {
			_moveDirection = _moveInput * _speed;
			if (Input.GetButton("Jump")) {
				_moveDirection.y = _jumpSpeed;
			}
		} else {
			_moveDirection.y -= _gravity * Time.deltaTime;
		}
		_controller.Move(_moveDirection * Time.deltaTime);
		Debug.Log(_moveInput);
		if (_moveInput != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation(_moveInput, Vector3.up);
		}
	}
}
