using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Deaths : MonoBehaviour {
    public static int deaths = 0;
    public TextMeshProUGUI deathsText;

    void Update() {
        GlobalData.Deaths = deaths;
        deathsText.text = $"Deaths: {deaths}";
    }

    public void AddDeath() {
        deaths++;
    }
}