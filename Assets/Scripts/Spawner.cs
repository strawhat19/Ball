using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {
    public Transform portal; // Assign the portal transform in the inspector
    public GameObject objectToShoot; // Assign your enemy prefab in the inspector
    public float spawnInterval = 2.0f; // Time between spawns
    public float launchForce = 1500.0f; // Force to apply to the spawned enemies

    private float timer; // To track time between spawns
    private Movement characterSettings;
    private float spawnDelay = 4.0f;
    public float enemyLifeTime = 2.0f;
    private EnemiesTracker enemiesCount;

    void Start() {
        SetSpawnDelay();
        SetEnemiesTracker();
    }

    void SetSpawnDelay() {
        GameObject character = GameObject.FindGameObjectWithTag("Character");
        if (character != null) characterSettings = character.GetComponent<Movement>();
        if (characterSettings != null) spawnDelay = characterSettings.spawnDelayDuration;
    }
   
    void SetEnemiesTracker() {
        GameObject enemiesTracker = GameObject.FindGameObjectWithTag("EnemiesTracker");
        if (enemiesTracker != null) enemiesCount = enemiesTracker.GetComponent<EnemiesTracker>();
    }

    private void Update() {
        SetSpawnDelay();
        // SetEnemiesTracker();
        timer += Time.deltaTime;
        if (timer >= (spawnInterval + spawnDelay)) {
            SpawnEnemy();
            timer = 0;
        }
    }

    private void SpawnEnemy() {
        if (portal != null) {
            GameObject objectShot = Instantiate(objectToShoot, portal.position, Quaternion.identity);
            Rigidbody rb = objectShot.GetComponent<Rigidbody>();
            if (rb != null) {
                enemiesCount.AddEnemy();
                Vector3 shootDirection = portal.up;
                rb.AddForce(shootDirection * launchForce);
            }
            Destroy(objectShot, enemyLifeTime);
        } else {
            Debug.LogWarning("Portal transform is not assigned in the inspector!");
        }
    }

    // Draw Trajectory Path by calculating launchForce
    void OnDrawGizmos() {
        if (portal != null) {
            Gizmos.color = Color.red;
            Vector3 initialVelocity = portal.up * (launchForce * 20) / 1000; // Adjusted for Rigidbody mass if necessary
            DrawTrajectory(portal.position, initialVelocity);
        }
    }

    void DrawTrajectory(Vector3 start, Vector3 initialVelocity) {
        Vector3 previousPoint = start;
        int steps = 100; // Increase the number of steps for a smoother line
        float timeStep = 0.1f; // Time interval between points

        for (int i = 1; i <= steps; i++) {
            float simulationTime = i * timeStep;
            Vector3 displacement = initialVelocity * simulationTime + Physics.gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = start + displacement;
            Gizmos.DrawLine(previousPoint, drawPoint);
            previousPoint = drawPoint;
        }
    }
}