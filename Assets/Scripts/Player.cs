using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Rigidbody2D rb;
    LineRenderer lineRenderer;

    public ParticleSystem deathEffect;

    public float jumpPower = 1f;
    public float laserTime = 0.1f;
    public float laserCooldown = 1f;
    // public float jumpCooldown = 0.2f;

    float laserTimer;
    float laserCooldownTimer;
    bool onCooldown = false;
    bool jump = false;
    bool fire = false;

    void Start() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        laserTimer = laserTime;
        laserCooldownTimer = laserCooldown;
        lineRenderer.enabled = false;

        deathEffect.Stop();

    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) jump = true;
        if (Input.GetMouseButtonDown(0)) fire = true;
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
            mousePos.z = 0f;
            Vector3 jumpDir = mousePos.normalized * -1;
            // Debug.Log(jumpDir);

            rb.velocity = jumpDir * jumpPower;
            jump = false;
        } else if (fire && !onCooldown) {
            laserTimer -= Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 30f, LayerMask.GetMask("Wall"));
            // Debug.Log(hit.point);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            lineRenderer.enabled = true;
            if (laserTimer <= 0) {
                fire = false;
                lineRenderer.enabled = false;
                laserTimer = laserTime;
                onCooldown = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.name + " collided!");
        if (col.gameObject.layer == 6) {
            Destroy(gameObject);
            // Play Death Particles
            deathEffect.transform.position = rb.transform.position;
            deathEffect.Play();
            // Lose Screen
        }
    }
}