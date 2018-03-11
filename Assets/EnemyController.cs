using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {
	private NavMeshAgent _navAgent;
	private GameObject _player;

	// Use this for initialization
	void Start () {
		_navAgent = GetComponent<NavMeshAgent>();
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		_navAgent.SetDestination(_player.transform.position);
	}
}
