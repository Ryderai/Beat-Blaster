using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {
    Rigidbody2D rb;
    LineRenderer lineRenderer;

    public GameObject deathMenu;

    public ParticleSystem deathEffect;
    public AudioSource laserSound;
    public AudioSource jumpSound;
    public AudioSource deathSound;
    // public AudioSource killSound;

    public int score;
    public TextMeshProUGUI scoreText;
    public float jumpPower = 1f;
    public float laserTime = 0.1f;
    public float laserCooldown = 1f;
    // public float jumpCooldown = 0.2f;

    float laserTimer;
    float laserCooldownTimer;
    bool onCooldown = false;
    bool jump = false;
    bool fire = false;
    RaycastHit2D hit;

    void Start() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        laserTimer = laserTime;
        laserCooldownTimer = laserCooldown;
        lineRenderer.enabled = false;
        PlayerVars.score = 0;

        deathEffect.Stop();

    }

    void Update() {
        score = PlayerVars.score;
        scoreText.SetText("" + score);

        if (score > PlayerPrefs.GetInt("HighScore", 0)) {
            PlayerPrefs.SetInt("HighScore", score);
        }

        if (Input.GetMouseButtonDown(1)) {
            jump = true;
            jumpSound.Play();
        }
        if (Input.GetMouseButtonDown(0)) {
            fire = true;
            hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, 30f, LayerMask.GetMask("Wall", "Enemy"));
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lineRenderer.SetPosition(0, transform.position);
            // Debug.Log(transform.position);
            lineRenderer.SetPosition(1, hit.point);
            lineRenderer.enabled = true;

            laserSound.Play();
        }
    }

    void FixedUpdate() {
        if (onCooldown) {
            laserCooldownTimer -= Time.deltaTime;
            if (laserCooldownTimer <= 0) {
                onCooldown = false;
                laserCooldownTimer = laserCooldown;
            }
        }

        if (jump) {
            // Debug.Log("Jumping!");
            // Apply reversed clamped vector of mouse position as force to rb
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos -= transform.position;
            mousePos.z = 0f;
            Vector3 jumpDir = mousePos.normalized * -1;
            // Debug.Log(jumpDir);

            rb.velocity = jumpDir * jumpPower;
            jump = false;
        } else if (fire && !onCooldown) {
            laserTimer -= Time.deltaTime;

            if (laserTimer <= 0) {
                fire = false;
                lineRenderer.enabled = false;
                laserTimer = laserTime;
                onCooldown = true;
            }

            if (hit.transform.gameObject != null) {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                    hit.transform.gameObject.GetComponent<Enemy>().Destruct();
                    // if (!killSound.isPlaying) killSound.Play();
                }
            }
        }
        if (lineRenderer.enabled) lineRenderer.SetPosition(0, transform.position);
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.name + " collided!");
        if (col.gameObject.layer == 6 || col.gameObject.layer == 7) {
            deathSound.Play();
            Destroy(gameObject);
            // Play Death Particles
            deathEffect.transform.position = rb.transform.position;
            deathEffect.Play();
            // Lose Screen
        }
    }

    void OnDestroy() {
        deathMenu.SetActive(true);
    }
}