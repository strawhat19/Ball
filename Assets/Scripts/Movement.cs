using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    public bool godMode = false;
    public float jumpForce = 5f;
    public float restartThreshold = -5f;
    public float torqueAmount = 1f;
    private Rigidbody rb;
    private Rigidbody bigSpikesBody;
    private Rigidbody smallSpikesBody;
    private bool isGrounded;
    private bool isColliding = false;
    public SceneFader sceneFader;
    private Health playerHealth;
    public float damageMinimum = 5.0f;
    public float damageMaximum = 15.0f;
    private Deaths playerDeaths;
    private DifficultyTracker difficultyTracker;
    private bool isRestarting = false;
    private ParticleSystem healParticles;
    private ParticleSystem damageParticles;
    private ParticleSystem AOECircle;
    public float spawnDelayDuration = -0.75f;
    public Difficulties difficulty = Difficulties.Easy;
    public static readonly string SpawnDelay = "SpawnDelay";
    public static readonly string DamageMultiplier = "DamageMultiplier";

    public Dictionary<Difficulties, Dictionary<string, float>> GameModes = new Dictionary<Difficulties, Dictionary<string, float>> {
        { Difficulties.Easy, new Dictionary<string, float> { { DamageMultiplier, 1.0f }, { SpawnDelay, 1.25f } } },
        { Difficulties.Medium, new Dictionary<string, float> { { DamageMultiplier, 1.5f }, { SpawnDelay, 0.75f } } },
        { Difficulties.Hard, new Dictionary<string, float> { { DamageMultiplier, 2.0f }, { SpawnDelay, 0.0f } } },
    };

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

    void LogParameters() {
        Debug.Log("Difficulty " + difficulty);
        Debug.Log(SpawnDelay + " " + spawnDelayDuration);
        Debug.Log(DamageMultiplier + " " + GameModes[difficulty][DamageMultiplier]);
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        spawnDelayDuration = GameModes[difficulty][SpawnDelay];

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
        
        GameObject difficultyObject = GameObject.FindGameObjectWithTag("DifficultyTracker");
        if (difficultyObject != null) difficultyTracker = difficultyObject.GetComponent<DifficultyTracker>();
        if (difficultyTracker != null) difficultyTracker.SetDifficulty(difficulty);

        GameObject bigSpikes = GameObject.FindGameObjectWithTag("CharSpikesBig");
        if (bigSpikes != null) bigSpikesBody = bigSpikes.GetComponent<Rigidbody>();

        GameObject smallSpikes = GameObject.FindGameObjectWithTag("CharSpikesSmall");
        if (smallSpikes != null) smallSpikesBody = smallSpikes.GetComponent<Rigidbody>();

        // GameObject aoe = GameObject.FindGameObjectWithTag("AOE");
        // if (aoe != null) AOECircle = aoe.GetComponent<ParticleSystem>();
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
            bool hitting_spikes = trigger.bounds.size.magnitude < 5;
            bool player_can_heal = !isColliding && healParticles != null && playerHealth.currentHealth < 100;
            if (hitting_spikes) {
                isColliding = true;
                damageParticles.Play();
                if (godMode == false) {
                    float damage = (Random.Range(damageMinimum, damageMaximum) * GameModes[difficulty][DamageMultiplier]) / 2;
                    playerHealth.TakeDamage(damage);
                }
            } else if (player_can_heal && !hitting_spikes) {
                healParticles.Play(); // Play healing animation
                float hpToHeal = (Random.Range((damageMinimum / 2), (damageMaximum / 2)) * GameModes[difficulty][DamageMultiplier]);
                playerHealth.HealDamage(hpToHeal);
            }
        }
    }

    private void OnTriggerExit(Collider trigger) {
        if (trigger.CompareTag("Enemy")) {
            bool hitting_spikes = trigger.bounds.size.magnitude < 5;
            if (healParticles != null) {
                Invoke("StopHealAnimation", 0.25f); // Stop the healing animation shortly after exiting the trigger
            }
            if (hitting_spikes) {
                Invoke("StopDamageAnimation", 0.2f);
                isColliding = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        bool hitByEnemy = collision.gameObject.CompareTag("Enemy");
        bool reachesFinishLine = collision.gameObject.CompareTag("Finish");

        if (hitByEnemy) {
            isColliding = true;
            damageParticles.Play();
            if (godMode == false) {
                float damage = Random.Range(damageMinimum, damageMaximum) * GameModes[difficulty][DamageMultiplier];
                playerHealth.TakeDamage(damage);
            }
        }

        if (reachesFinishLine) {
            Invoke("GoToLevel", 1f); // Restart Level after a 1 Second Delay
        }
    }

    void OnCollisionExit(Collision collision) {
        bool hitByEnemy = collision.gameObject.CompareTag("Enemy");
        if (hitByEnemy) {
            Invoke("StopDamageAnimation", 0.2f);
            isColliding = false;
        }
    }

    void TakeDelayedDamageOverTime() {
        float damage = Random.Range(damageMinimum, damageMaximum) * GameModes[difficulty][DamageMultiplier];
        playerHealth.TakeDamage(damage / 15000);
        Debug.Log("Taking Damage " + playerHealth.currentHealth + "%");
    }

    void Update() {
        float moveHorizontal = Input.GetAxis("Horizontal"); // When user clicks A or D // Left Arrow or Right Arrow
        Vector3 torque = new Vector3(0f, 0f, -moveHorizontal) * torqueAmount; // 3 Axes Movement, X and Y are 0, but Z is our rotation
        rb.AddTorque(torque); // Add it back to the game object rigid body which we have stored in a variable
        if (bigSpikesBody != null) bigSpikesBody.AddTorque(torque * 10);
        if (smallSpikesBody != null) smallSpikesBody.AddTorque(torque * 15);

        if (isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y < restartThreshold) {
            GameOverAnimation();
        } else if (playerHealth.currentHealth <= 0) {
            Invoke("GameOverAnimation", 1f);
        }

        if (damageParticles.isPlaying) {
            Invoke("StopDamageAnimation", 1f);
        }
    }
}