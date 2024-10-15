using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform camtarget;

    // Update is called once per frame
    void Update()
    {
        InputManager.Instance.CameraCon(this.transform, camtarget);
    }
}
