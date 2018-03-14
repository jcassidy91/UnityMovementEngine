using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    float moveSpeed = 6f;
    public GameObject cam;

    float smoothTime = 0.06f;
    float smoothingAngle;
    float currentRotation;

    CharacterController body;
    Vector3 velocity;
    Vector3 smoothingVelocity;
    float gravity = -12f;

    void Start()
    {
        body = GetComponent<CharacterController>();
    }

    void Update () {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(xInput, 0, zInput);

        if (moveInput.magnitude > 0)
        {
            Move(moveInput.x, moveInput.z);
        }
	}

    void Move(float x, float z)
    {
        float yAngle = Mathf.Atan2(-z, x) * Mathf.Rad2Deg + cam.transform.localEulerAngles.y;
        float targetAngle = yAngle;
        currentRotation = Mathf.SmoothDampAngle(currentRotation, targetAngle, ref smoothingAngle, smoothTime);
        transform.localEulerAngles = currentRotation * Vector3.up;
        Vector3 targetVelocity = transform.right * moveSpeed;
        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref smoothingVelocity, smoothTime);

        if (!body.isGrounded)
        {
            velocity += Vector3.up * gravity;
        }

        body.Move(velocity * Time.deltaTime);        
    }
}
