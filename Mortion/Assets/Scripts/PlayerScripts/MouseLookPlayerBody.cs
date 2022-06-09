using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookPlayerBody : MonoBehaviour
{
    
    float mouseX;

    float mouseSensitivityY = 100f;

    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivityY * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
