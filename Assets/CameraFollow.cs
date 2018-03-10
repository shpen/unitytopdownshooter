using UnityEngine;

public class CameraFollow : MonoBehaviour {
	[SerializeField] Transform _target;

	[SerializeField] float _smoothSpeed = 5f;

	private Vector3 _initialPos;

	private void Start() {
		_initialPos = transform.position;
	}

	void LateUpdate() {
		transform.position = Vector3.Lerp(transform.position, _initialPos + _target.position, _smoothSpeed * Time.deltaTime);
	}
}
