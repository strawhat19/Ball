using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelIconSwitch : MonoBehaviour {
    public Sprite level1Sprite;
    public Sprite level2Sprite;
    public Sprite level3Sprite;
    private Image imageComponent;

    private void Start() {
        SetLevelIcon();
    }

    private void SetLevelIcon() {
        imageComponent = GetComponent<Image>();
        if (GlobalData.Level == 1) imageComponent.sprite = level1Sprite;
        else if (GlobalData.Level == 2) imageComponent.sprite = level2Sprite;
        else if (GlobalData.Level == 3) imageComponent.sprite = level3Sprite;
    }
}