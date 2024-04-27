using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DifficultyTracker : MonoBehaviour {
    private Difficulties difficulty;
    private Movement characterSettings;
    public TextMeshProUGUI difficultyText;

    void SetDifficultyLevel() {
        GameObject character = GameObject.FindGameObjectWithTag("Character");
        if (character != null) characterSettings = character.GetComponent<Movement>();
        difficulty = characterSettings.difficulty;
    }

    void Start() {
        SetDifficultyLevel();
    }

    void Update() {
        GlobalData.Difficulty = difficulty;
        difficultyText.text = $"Difficulty: {difficulty}";
    }

    public void SetDifficulty(Difficulties diff) {
        difficulty = diff;
    }
}