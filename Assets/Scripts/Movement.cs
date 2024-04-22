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
    public SceneFader sceneFader;
    private Health playerHealth;
    public float damageMinimum = 5.0f;
    public float damageMaximum = 35.0f;
    private Deaths playerDeaths;
    private bool isRestarting = false;

    void OnCollisionStay() {
        isGrounded = true;
    }

    void GameOverAnimation() {
        RestartLevel();
    }

    void Start() {
        // Debug.Log("Movement.cs Script added to: " + gameObject.name); // Check to make sure we are connected to a game object
        rb = GetComponent<Rigidbody>(); // Check for rigid body on the game object, then store the rigid body from the game object in a variable
        GameObject health = GameObject.FindGameObjectWithTag("Health");
        if (health != null) playerHealth = health.GetComponent<Health>();
        GameObject deaths = GameObject.FindGameObjectWithTag("Deaths");
        if (deaths != null) playerDeaths = deaths.GetComponent<Deaths>();
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

    void OnCollisionEnter(Collision collision) {
        bool hitByEnemy = collision.gameObject.CompareTag("Enemy");
        bool reachesFinishLine = collision.gameObject.CompareTag("Finish");

        if (hitByEnemy) {
            // Debug.Log("Collision Detected With " + collision.gameObject);
            float damage = Random.Range(damageMinimum, damageMaximum);
            playerHealth.TakeDamage(damage);
        }

        if (reachesFinishLine) {
            // Debug.Log("Collision Detected With " + collision.gameObject);
            Invoke("GoToLevel", 1f); // Restart Level after a 1 Second Delay
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