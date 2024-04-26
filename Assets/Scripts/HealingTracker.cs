using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealingTracker : MonoBehaviour {
    public static float healing = 0;
    public TextMeshProUGUI healingText;

    void Start() {

    }

    void Update() {
        string updatedHealing = GlobalData.RemoveDotZeroZero(healing.ToString("F2"));
        healingText.text = $"Healing: {updatedHealing}%";
    }

    public void AddHealing(float healingToAdd) {
        float newHP = healing + healingToAdd;
        if (newHP > healing) {
            healing += healingToAdd;
        }
    }
}