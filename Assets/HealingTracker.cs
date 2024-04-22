using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealingTracker : MonoBehaviour {
    public static float healing = 0;
    public float startingPos = -420;
    public TextMeshProUGUI healingText;
    public GameObject healingTracker;
    public RectTransform trackerRect;
    // Start is called before the first frame update
    void Start() {
        string healingDisplay = $"Healing: {healing}%";
        healingText.text = healingDisplay;
        Debug.Log("Num Chars 1: " + healingDisplay.Length);
        // GameObject healingTracker = GameObject.FindGameObjectWithTag("HealingTracker");
        // if (healingTracker != null) trackerRect = healingTracker.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        string healingDisplay = $"Healing: {healing}%";
        healingText.text = healingDisplay;

        // Adjust the position or size of the text container based on the length of the number
        AdjustTextContainer(healingDisplay);
    }

    public void AddHealing(float healingToAdd) {
        float newHP = healing + healingToAdd;
        if (newHP > healing) {
            healing += healingToAdd;
        }
    }

    private void AdjustTextContainer(string text) {
        int numberOfCharacters = text.Length;
        float newXPos = (float)startingPos - (numberOfCharacters * 30);
        trackerRect.offsetMax = new Vector2(newXPos, trackerRect.offsetMax.y);
        // Debug.Log("Num Chars: " + numberOfCharacters);
        // Debug.Log("New X Pos: " + newXPos);
    }
}