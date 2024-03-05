using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {
    public Image fadeOutUIImage;
    public float fadeSpeed = 0.5f;

    public enum FadeDirection {
        In, //Alpha = 1
        Out // Alpha = 0
    }

    void Start() {
        StartCoroutine(Fade(FadeDirection.Out)); // Fade in at the start
    }

    public IEnumerator Fade(FadeDirection fadeDirection) {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;

        if (fadeDirection == FadeDirection.Out) {
            while (alpha >= fadeEndValue) {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        } else {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue) {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection) {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }

    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, int sceneToLoad) {
        yield return Fade(fadeDirection);
        SceneManager.LoadScene(sceneToLoad);
    }

    // Wrapper method for external calls
    public void RestartLevel() {
        StartCoroutine(FadeAndLoadScene(FadeDirection.In, SceneManager.GetActiveScene().buildIndex));
    }
}