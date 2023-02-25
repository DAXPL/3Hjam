using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private CharacterController controller;
    [SerializeField] private float sensitivity = 1f;

    [SerializeField] private float playerSpeed = 1f;

    [SerializeField] private Vector3 movementInput;
    [SerializeField] private Vector2 mouseRaw;

    float rotX;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if(controller==null)
        {
            Debug.LogWarning("No CharacterController!");
        }
    }

    void Update()
    {
        if(controller)
        {
            Vector3 movement = transform.right * movementInput.x + transform.forward * movementInput.z;
            controller.Move(movement * Time.deltaTime);
        }
        if (cam)
        {
            float mouseX = (mouseRaw.x * sensitivity) * Time.deltaTime;
            float mouseY = (mouseRaw.y * sensitivity) * Time.deltaTime;

            rotX -= mouseY;
            rotX = Mathf.Clamp(rotX, -90f, 90f);
            cam.transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void InputMovement(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        movementInput.x= input.x * playerSpeed;
        movementInput.z = input.y * playerSpeed;
    }
    public void InputMouse(InputAction.CallbackContext ctx)
    {
        mouseRaw = ctx.ReadValue<Vector2>();
    }
}
