using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    public float jumpForce = 5f; // Stuff we want unity to show
    public float restartThreshold = -5f;
    public float torqueAmount = 1f;
    private Rigidbody rb; // Stuff we dont want unity to show
    private bool isGrounded;

    void OnCollisionStay() {
        isGrounded = true;
    }

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision Function");
        if (collision.gameObject.CompareTag("Finish")) { // Name of Colliding Object
            Debug.Log("Collision Detected");
        }
    }

    void Start() {
        Debug.Log("Movement.cs Script added to: " + gameObject.name); // Check to make sure we are connected to a game object
        rb = GetComponent<Rigidbody>(); // Check for rigid body on the game object, then store the rigid body from the game object in a variable
    }

    void Update() {

        float moveHorizontal = Input.GetAxis("Horizontal"); // When user clicks A or D // Left Arrow or Right Arrow
        Vector3 torque = new Vector3(0f, 0f, -moveHorizontal) * torqueAmount; // 3 Axes Movement, X and Y are 0, but Z is our rotation
        rb.AddTorque(torque); // Add it back to the game object rigid body which we have stored in a variable

        if (isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y < restartThreshold) {
            RestartLevel();
        }

    }
}