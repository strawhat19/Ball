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

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= spawnInterval) {
            SpawnEnemy();
            timer = 0;
        }
    }

    private void SpawnEnemy() {
        if (portal != null) {
            GameObject objectShot = Instantiate(objectToShoot, portal.position, Quaternion.identity);
            Rigidbody rb = objectShot.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 shootDirection = portal.up; // Use the portal's local up direction
                rb.AddForce(shootDirection * launchForce); // Launch the enemy in the portal's upward direction
            }
        } else {
            Debug.LogWarning("Portal transform is not assigned in the inspector!");
        }
    }
}