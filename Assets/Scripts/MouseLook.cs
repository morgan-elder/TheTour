using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 800f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks cursor to center of screen so it doesnt leave
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // *T.dT will rotate independent of current frame rate
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // look up and down. -= to decrease x rotation every frame based on mouse y
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamp up/down to 180degrees

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // allows for clamping
        playerBody.Rotate(Vector3.up * mouseX); // specify axis to rotate around
    }
}
