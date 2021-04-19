using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    NavMeshAgent agent;
    Transform player;

    public ParticleSystem death;

    void Start() {
        if (GameObject.Find("Player") != null) player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update() {
        if (player != null) {
            agent.SetDestination(player.position);
        }
    }

    void OnDisable() {
        Destroy(gameObject, 0.5f);
    }

    public void Destruct() {
        if (gameObject.activeSelf) {
            Instantiate(death, transform.position, Quaternion.identity);
            PlayerVars.score++;
        }
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}