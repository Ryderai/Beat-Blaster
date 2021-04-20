using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    NavMeshAgent agent;
    Transform player;

    public ParticleSystem death;
    public AudioSource killSound;

    void Start() {
        if (GameObject.Find("Player") != null) player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        killSound = GameObject.Find("KillSound").GetComponent<AudioSource>();

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
            Debug.Log("Killed");
            killSound.Play();
            Instantiate(death, transform.position, Quaternion.identity);
            PlayerVars.score++;
            gameObject.SetActive(false);
        }
    }
}