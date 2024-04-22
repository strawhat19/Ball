using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    public float jumpForce = 5f; // Stuff we want unity to show
    public float restartThreshold = -5f;
    public float torqueAmount = 1f;
    private Rigidbody rb; // Stuff we dont want unity to show
    private bool isGrounded;
    private bool isColliding = false;
    public SceneFader sceneFader;
    private Health playerHealth;
    public float damageMinimum = 5.0f;
    public float damageMaximum = 35.0f;
    private Deaths playerDeaths;
    private bool isRestarting = false;
    private ParticleSystem healParticles;
    private ParticleSystem damageParticles;

    void OnCollisionStay() {
        isGrounded = true;
    }

    void GameOverAnimation() {
        RestartLevel();
    }

    void StopHealAnimation() {
        healParticles.Stop();
    }
    
    void StopDamageAnimation() {
        damageParticles.Stop();
    }

    void Start() {
        // Debug.Log("Movement.cs Script added to: " + gameObject.name); // Check to make sure we are connected to a game object
        rb = GetComponent<Rigidbody>(); // Check for rigid body on the game object, then store the rigid body from the game object in a variable
        GameObject health = GameObject.FindGameObjectWithTag("Health");
        if (health != null) playerHealth = health.GetComponent<Health>();

        GameObject deaths = GameObject.FindGameObjectWithTag("Deaths");
        if (deaths != null) playerDeaths = deaths.GetComponent<Deaths>();

        GameObject healing = GameObject.FindGameObjectWithTag("Healing");
        if (healing != null) healParticles = healing.GetComponent<ParticleSystem>();
        if (healParticles != null) StopHealAnimation();
        
        GameObject damage = GameObject.FindGameObjectWithTag("Damage");
        if (damage != null) damageParticles = damage.GetComponent<ParticleSystem>();
        if (damageParticles != null) StopDamageAnimation();
    }

    void GoToLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneFader sceneFader = FindObjectOfType<SceneFader>();
        if (sceneFader != null) {
            sceneFader.GoToLevel();
        } else {
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    void RestartLevel() {
        if (!isRestarting) {
            isRestarting = true;
            playerDeaths.AddDeath();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneFader sceneFader = FindObjectOfType<SceneFader>();
            if (sceneFader != null) {
                sceneFader.RestartLevel();
            } else {
                SceneManager.LoadScene(currentSceneIndex);
            }
        }
    }

    private void OnTriggerEnter(Collider trigger) {
        if (trigger.CompareTag("Enemy")) {
            Debug.Log("Narrowly dodged an enemy!");
            if (!isColliding && healParticles != null && playerHealth.currentHealth < 100) {
                healParticles.Play(); // Play healing animation
                float hpToHeal = Random.Range(damageMinimum, damageMaximum);
                playerHealth.HealDamage(hpToHeal);
            }
        }
    }

    private void OnTriggerExit(Collider trigger) {
        if (trigger.CompareTag("Enemy")) {
            if (healParticles != null) {
                Invoke("StopHealAnimation", 0.25f); // Stop the healing animation shortly after exiting the trigger
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        bool hitByEnemy = collision.gameObject.CompareTag("Enemy");
        bool reachesFinishLine = collision.gameObject.CompareTag("Finish");

        if (hitByEnemy) {
            isColliding = true;
            // Debug.Log("Collision Detected With " + collision.gameObject);
            float damage = Random.Range(damageMinimum, damageMaximum);
            playerHealth.TakeDamage(damage);
            // healParticles.Play();
            damageParticles.Play();
        }

        if (reachesFinishLine) {
            // Debug.Log("Collision Detected With " + collision.gameObject);
            Invoke("GoToLevel", 1f); // Restart Level after a 1 Second Delay
        }
    }

    void OnCollisionExit(Collision collision) {
        bool hitByEnemy = collision.gameObject.CompareTag("Enemy");
        if (hitByEnemy) {
            // Debug.Log("Collision Detected With " + collision.gameObject);
            // Invoke("StopHealAnimation", 0.2f);
            Invoke("StopDamageAnimation", 0.2f);
            isColliding = false;
        }
    }

    void Update() {
        float moveHorizontal = Input.GetAxis("Horizontal"); // When user clicks A or D // Left Arrow or Right Arrow
        Vector3 torque = new Vector3(0f, 0f, -moveHorizontal) * torqueAmount; // 3 Axes Movement, X and Y are 0, but Z is our rotation
        rb.AddTorque(torque); // Add it back to the game object rigid body which we have stored in a variable

        if (isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y < restartThreshold) {
            GameOverAnimation();
        } else if (playerHealth.currentHealth <= 0) {
            Invoke("GameOverAnimation", 1f);
        }
    }
}