using System.Collections;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour {
    private void Start() {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}