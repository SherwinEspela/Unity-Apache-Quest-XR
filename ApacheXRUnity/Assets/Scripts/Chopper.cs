using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chopper : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] InputActionReference inputLeftStick;
    [SerializeField] InputActionReference inputRightStick;

    [SerializeField] float moveSpeed = 15.0f;
    [SerializeField] float rotateSpeed = 15.0f;
    [SerializeField] float altitudeStrength = 350f;

    private Vector2 moveDirection;
    private float altitudeDirection;

    void Update()
    {
        moveDirection = inputLeftStick.action.ReadValue<Vector2>();
        
        float rotateY = inputRightStick.action.ReadValue<Vector2>().x;
        if (rotateY != 0f) transform.Rotate(0f, rotateY * rotateSpeed * Time.deltaTime, 0f);

        altitudeDirection = inputRightStick.action.ReadValue<Vector2>().y ;
    }

    private void FixedUpdate()
    {
        float movementMultiplier = moveSpeed * Time.deltaTime;
        Vector3 forwardDirection = transform.forward * moveDirection.y * movementMultiplier;
        Vector3 rightDirection = transform.right * moveDirection.x * movementMultiplier;
        
        Vector3 direction = forwardDirection + rightDirection;
        rigidBody.velocity = direction.normalized;

        Vector3 upwardDirection = transform.up * altitudeDirection * altitudeStrength * Time.deltaTime;
        rigidBody.AddForce(upwardDirection, ForceMode.Force);
    }

    // ControlerButtonsMapper callbacks
    //public void OnPrimaryThumbstickUp()
    //{
    //    //Debug.Log("OnPrimaryThumbstickUp......");
    //    //moveDirectionY = 1;
    //}

    //public void OnPrimaryThumbstickDown()
    //{
    //    //Debug.Log("OnPrimaryThumbstickDown......");
    //    //moveDirectionY = -1;
    //}

    //public void OnPrimaryThumbstickRight()
    //{
    //    //moveDirectionX = 1;
    //}

    //public void OnPrimaryThumbstickLeft()
    //{
    //    //moveDirectionX = -1;
    //}
}
