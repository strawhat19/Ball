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
    private DamageTracker playerDamage;
    private HealingTracker playerHealing;

    void Start() {
        currentHealth = maxHealth;

        GameObject damageTracker = GameObject.FindGameObjectWithTag("DamageTracker");
        if (damageTracker != null) playerDamage = damageTracker.GetComponent<DamageTracker>();

        GameObject healingTracker = GameObject.FindGameObjectWithTag("HealingTracker");
        if (healingTracker != null) playerHealing = healingTracker.GetComponent<HealingTracker>();
    }

    public void SimulateDamage() {
        if (Input.GetKeyDown(KeyCode.Space)) { // Press Space to simulate taking damage
            Debug.Log("Taking Damage");
            TakeDamage(10);
        }
    }

    public void HealDamage(float healing) {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(SmoothTransitionToNewHealth(false));
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(SmoothTransitionToNewHealth(true));
    }

    void Update() {
        // SimulateDamage();
    }

    IEnumerator SmoothTransitionToNewHealth(bool damage) {
        float timeToChange = 0.5f; // The duration of the change
        float elapsed = 0f;
        
        float currentWidth = healthBarRect.sizeDelta.x;
        float targetWidth = (float)currentHealth / maxHealth * 100; // Calculate new width based on current health
        
        while (elapsed < timeToChange) {
            elapsed += Time.deltaTime;
            float newWidth = Mathf.Lerp(currentWidth, targetWidth, elapsed / timeToChange);
            healthBarRect.sizeDelta = new Vector2(newWidth, healthBarRect.sizeDelta.y);
            healthText.text = $"Health: {newWidth:F2}%";
            if (damage) {
                float damageToAdd = (float)(currentWidth - newWidth) / 100;
                playerDamage.AddDamage(damageToAdd);
            } else {
                float healingToAdd = (float)(newWidth - currentWidth) / 100;
                playerHealing.AddHealing(healingToAdd);
            }
            yield return null;
        }
        
        // Ensure the final width is exactly what it should be after the transition
        healthBarRect.sizeDelta = new Vector2(targetWidth, healthBarRect.sizeDelta.y);
    }
}