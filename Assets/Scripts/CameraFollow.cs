using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject Character; // The character game object
    private Vector3 offset; // Offset distance between the camera and the character

    void Start() {
        // Calculate the initial offset by subtracting the character's position from the camera's position.
        // Here, we access the Transform component of the Character to get its position.
        offset = transform.position - Character.transform.position;
    }

    void LateUpdate() {
        // Set the position of the camera to the character's position plus the offset.
        // Again, accessing the Transform component of the Character to get its current position.
        transform.position = Character.transform.position + offset;
    }
}