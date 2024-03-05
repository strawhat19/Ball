using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject objectToFollow; // The character game object // The Object We Want To Follow
    private Vector3 offset; // Offset distance between the camera and the character

    void Start() {
        // Calculate the initial offset by subtracting the character's position from the camera's position.
        // Here, we access the Transform component of the Character to get its position.
        if (objectToFollow != null) offset = transform.position - objectToFollow.transform.position;
    }

    void LateUpdate() {
        // Set the position of the camera to the character's position plus the offset.
        // Again, accessing the Transform component of the Character to get its current position.
        if (objectToFollow != null) transform.position = objectToFollow.transform.position + offset;
    }
}