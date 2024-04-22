using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorChange : MonoBehaviour {
    public Color newColor = Color.black; // Default color is set to red, but you can change it in the Inspector

    void Start() {
        ChangeColor(newColor);
    }

    public void ChangeColor(Color color) {
        Renderer renderer = GetComponent<Renderer>(); // Get the Renderer component of the GameObject
        if (renderer != null)
        {
            renderer.material.color = color; // Set the material color
        }
    }
}