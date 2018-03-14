using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public float sensitivity = 15f;
    public float targetDistance = 2;
    public GameObject target;

    float pitch = 0;
    float pitchMin = -40;
    float pitchMax = 60;
    float yaw = 0;

    Vector3 smoothingVector;
    Vector3 targetRotation;
    Vector3 currentRotation;
    float smoothTime = 0.12f;

    void LateUpdate () {
        pitch -= sensitivity * Input.GetAxis("Mouse Y");
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        targetRotation = new Vector3(pitch, yaw, 0);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothingVector, smoothTime);
        transform.localEulerAngles = currentRotation;

        transform.position = target.transform.position - targetDistance * transform.forward + Vector3.up * 1f;
    }
}
