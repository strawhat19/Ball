using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;
    public TextMeshProUGUI healthText;
    public RectTransform healthBarRect;

    void Start() {
        currentHealth = maxHealth;
    }

    public void HealDamage(float healing) {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(SmoothTransitionToNewHealth());
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(SmoothTransitionToNewHealth());
    }

    void Update() {
        // if (Input.GetKeyDown(KeyCode.Space)) { // Press Space to simulate taking damage
        //     Debug.Log("Taking Damage");
        //     TakeDamage(10);
        // }
    }

    IEnumerator SmoothTransitionToNewHealth() {
        float timeToChange = 0.5f; // The duration of the change
        float elapsed = 0f;
        
        float currentWidth = healthBarRect.sizeDelta.x;
        float targetWidth = (float)currentHealth / maxHealth * 100; // Calculate new width based on current health
        
        while (elapsed < timeToChange) {
            elapsed += Time.deltaTime;
            float newWidth = Mathf.Lerp(currentWidth, targetWidth, elapsed / timeToChange);
            healthBarRect.sizeDelta = new Vector2(newWidth, healthBarRect.sizeDelta.y);
            healthText.text = $"Health: {newWidth}%";
            yield return null;
        }
        
        // Ensure the final width is exactly what it should be after the transition
        healthBarRect.sizeDelta = new Vector2(targetWidth, healthBarRect.sizeDelta.y);
    }
}