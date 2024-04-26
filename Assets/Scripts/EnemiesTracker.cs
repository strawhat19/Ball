using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemiesTracker : MonoBehaviour {
    public static int enemies = 0;
    public TextMeshProUGUI enemiesText;

    void Start() {

    }

    void Update() {
        enemiesText.text = $"Enemies: {enemies}";
    }

    public void AddEnemy() {
        enemies++;
    }
}