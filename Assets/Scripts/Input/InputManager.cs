using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    /// <summary>
    /// Mouse movement
    /// </summary>
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    /// <summary>
    /// camera
    /// </summary>
    public float plerp = 0.2f;
    public float rlerp = 0.1f;
    
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //MouseMovement();
    }

    public void MouseMovement(Transform cam)
    {
        xRotation += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yRotation += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation = Mathf.Clamp(yRotation,275, 355);

        //applying both rotations
        cam.localRotation = Quaternion.Euler(-yRotation, xRotation, 0f);
    }

    public void CameraCon(Transform camOrigin ,Transform camTarget)
    {
        camOrigin.position = Vector3.Lerp(camOrigin.position, camTarget.position, plerp);
        camOrigin.rotation = Quaternion.Lerp(camOrigin.rotation, camTarget.rotation, rlerp);
    }


}
