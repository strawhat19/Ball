using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab; // Assign your enemy prefab in the inspector
    public float spawnInterval = 2.0f; // Time between spawns
    public float launchForce = 1500.0f; // Force to apply to the spawned enemies

    private float timer; // To track time between spawns

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= spawnInterval) {
            SpawnEnemy();
            timer = 0;
        }
    }

    private void SpawnEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        if (rb != null) {
            // Calculate the direction towards the player
            // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Vector3 targetDirection = (PlayerPosition() - transform.position).normalized;
            rb.AddForce(targetDirection * launchForce); // Launch the enemy towards the player
        }
    }

    private Vector3 PlayerPosition() {
        // Find the player in the game
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            return player.transform.position;
        }
        return Vector3.zero;
    }
}