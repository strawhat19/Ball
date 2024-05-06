using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour {
    public static float score = 0;
    public TextMeshProUGUI scoreText;

    void Update() {
        CalculateScore();
        string updatedScore = GlobalData.RemoveDotZeroZero(score.ToString("F2"));
        scoreText.text = $"Score: {updatedScore}%";
    }

    float CalculateScore() {
        if (GlobalData.Damage == 0 && GlobalData.Enemies == 0) return score;

        float levelMultiplier = GlobalData.LevelMultiplier;
        float difficultyMultiplier = GlobalData.DifficultyMultiplier;
        float deaths = GlobalData.Deaths; // ?? -100f * GlobalData.Deaths;
        float damage = GlobalData.Damage; // ?? 1000f / (GlobalData.Damage + 10f);
        float enemies = GlobalData.Enemies; // ?? 500f / (GlobalData.Enemies + 5f);
        float healing = GlobalData.Healing; // ?? 300f * (GlobalData.Healing / (GlobalData.Healing + 100f));

        float calculatedScore = (((healing * 5) * (difficultyMultiplier * levelMultiplier)) - (((damage / 100) + enemies + deaths) / 100));
        if (calculatedScore < score) return score;
        score = calculatedScore;
        GlobalData.Score = score;
        return score;
    }
}