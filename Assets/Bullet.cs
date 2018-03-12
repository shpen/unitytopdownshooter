using UnityEngine;

public class Bullet : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
		Destroy(gameObject);
	}
}
