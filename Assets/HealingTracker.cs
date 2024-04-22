using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealingTracker : MonoBehaviour {
    public static float healing = 0;
    public TextMeshProUGUI healingText;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        healingText.text = $"Healing: {healing}";
    }

    public void AddHealing(float healingToAdd) {
        healing += healingToAdd;
    }
}