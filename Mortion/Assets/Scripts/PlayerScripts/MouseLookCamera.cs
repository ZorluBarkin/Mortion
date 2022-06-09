using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookCamera : MonoBehaviour
{

    public float mouseSensitivityX = 100f;

    float XRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityX * Time.deltaTime;

        //XRotation = XRotation - mouseY;
        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
    }
}
