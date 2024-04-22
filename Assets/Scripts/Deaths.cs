using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Deaths : MonoBehaviour {
    public static int deaths = 0;
    public TextMeshProUGUI deathsText;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        deathsText.text = $"Deaths: {deaths}";
    }

    public void AddDeath() {
        deaths++;
    }
}