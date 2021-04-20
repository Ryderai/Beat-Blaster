using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour {

    public AudioSource music;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // gameObject.SetActive(false);
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }

    void OnEnable() {
        music.Stop();
    }
}