using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DifficultySymbolSwitcher : MonoBehaviour {
    private Movement characterSettings;
    public Sprite easySprite;
    public Sprite mediumSprite;
    public Sprite hardSprite;

    private Image imageComponent;
    private Difficulties difficulty;

    private void Start() {
        SetDifficulty();
        SetImage();
    }

    void SetDifficulty() {
        GameObject character = GameObject.FindGameObjectWithTag("Character");
        if (character != null) characterSettings = character.GetComponent<Movement>();
        difficulty = characterSettings.difficulty;
    }

    private void SetImage() {
        imageComponent = GetComponent<Image>();
        if (difficulty == Difficulties.Easy) {
            imageComponent.sprite = easySprite;
        } else if (difficulty == Difficulties.Medium) {
            imageComponent.sprite = mediumSprite;
        } else if (difficulty == Difficulties.Hard) {
            imageComponent.sprite = hardSprite;
        } else {
            imageComponent.sprite = easySprite;
        }
    }
}