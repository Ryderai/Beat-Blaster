using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public float rate = 3; // Seconds per enemy
    public List<Transform> spawnSpots;

    float timer;

    void Start() {
        timer = rate;
    }

    void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            Instantiate(enemy, spawnSpots[Random.Range(0, spawnSpots.Count - 1)]);
            timer = rate;
        }
    }
}