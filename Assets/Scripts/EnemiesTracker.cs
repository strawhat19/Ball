using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemiesTracker : MonoBehaviour {
    public static int enemies = 0;
    public TextMeshProUGUI enemiesText;

    void Update() {
        GlobalData.Enemies = enemies;
        enemiesText.text = $"Enemies: {enemies}";
    }

    public void AddEnemy() {
        enemies++;
    }
}