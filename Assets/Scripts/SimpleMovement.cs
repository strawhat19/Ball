using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovment : MonoBehaviour {
    public float speed = 5.0f;
    // void Start() {
        
    // }

    void Update() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}