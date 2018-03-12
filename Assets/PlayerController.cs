using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	[SerializeField] float _speed = 6.0F;
	[SerializeField] float _jumpSpeed = 8.0F;
	[SerializeField] float _gravity = 20.0F;

	[SerializeField] GameObject _bulletPrefab;
	[SerializeField] Transform _bulletSpawn;

	private Camera _mainCamera;

	private Vector3 _moveDirection = Vector3.zero;
	private Vector3 _moveInput = Vector3.zero;
	private Vector3 _moveInputRaw = Vector3.zero;

	// Mouse look
	private Ray _cameraMouseRay;
	private Plane _groundPlane = new Plane(Vector3.up, Vector3.zero);
	private Vector3 _mouseLookTarget;
	private float _mouseLookRayLength;

	private CharacterController _controller;

	private void Start() {
		_controller = GetComponent<CharacterController>();
		_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update() {
		Move();
		Look();
		Shoot();
	}

	private void Move() {
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
	}

	private void Look() {
		Vector3 controllerLookInput = new Vector3(Input.GetAxis("Look Horizontal"), 0, Input.GetAxis("Look Vertical"));
		if (controllerLookInput.magnitude > Mathf.Epsilon) {
			LookAt(controllerLookInput);
			return;
		}

		_cameraMouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
		_groundPlane.Raycast(_cameraMouseRay, out _mouseLookRayLength);
		_mouseLookTarget = _cameraMouseRay.GetPoint(_mouseLookRayLength);
		LookAt(_mouseLookTarget - transform.position);

		// Checking raw fixes keyboard rotation bugg, but controller still doesn't work right
//		_moveInputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
//		if (_moveInputRaw != Vector3.zero) {
//			LookAt(_moveInput);
//		}
	}

	private void LookAt(Vector3 at) {
		transform.rotation = Quaternion.LookRotation(at, Vector3.up);
	}

	private void Shoot() {
		if (Input.GetButtonDown("Fire1")) {
			// Create the Bullet from the Bullet Prefab
			var bullet = Instantiate (
				_bulletPrefab,
				_bulletSpawn.position,
				_bulletSpawn.rotation
			);

			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15;

			// Destroy the bullet after 2 seconds
			Destroy(bullet, 2.0f);
		}
	}
}
