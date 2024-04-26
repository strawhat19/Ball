using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour {
    private TextMeshProUGUI levelText;

    void SetLevel() {
        GlobalData.Level = SceneManager.GetActiveScene().buildIndex + 1;
        TextMeshProUGUI levelText = GetComponent<TextMeshProUGUI>();
        levelText.text = "Level " + GlobalData.Level;
    }

    void Start() {
        SetLevel();
    }
}