using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] Transform target;
    void Update()
    {
        InputManager.Instance.MouseMovement(this.transform);
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, 1f);
    }
}
