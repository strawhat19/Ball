using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovment : MonoBehaviour {
    private Rigidbody objectBody;
    public float movementSpeed = 5.0f;
    public float jumpForce = 7.5f;

    void Start() {
        objectBody = GetComponent<Rigidbody>();
    }

    void Update() {
        float dirX = Input.GetAxisRaw("Horizontal"); 
        objectBody.velocity = new Vector3(dirX * movementSpeed, objectBody.velocity.y, objectBody.velocity.z);

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) {
            objectBody.velocity = new Vector3(objectBody.velocity.x, jumpForce, objectBody.velocity.z);
        }
    }
}