using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Locking the Cursorto the middle of the Sceenand making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //control rotation around x axis (look up and down)
        xRotation -= mouseY; 

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        //apply both rotation
        transform.localRotation = Quaternion.Euler(xRotation, yRotation,0f);
    }
}
