using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private CharacterController controller;
    [SerializeField] private float sensitivity = 1f;

    [SerializeField] private float playerSpeed = 1f;

    private Vector3 movementInput;
    private Vector2 mouseRaw;

    float rotX;
    bool canMove = true;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    float nextStep = 0f;
    public float stepOffset = 1f;
    private AudioSource ass;
    [SerializeField] private AudioClip[] steps;
    [SerializeField] private AudioClip death;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        ass = GetComponent<AudioSource>();
        if (controller==null)
        {
            Debug.LogWarning("No CharacterController!");
        }
        deathScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    void Update()
    {
        if(canMove == false) { return; }
        if(controller)
        {
            Vector3 movement = transform.right * movementInput.x + transform.forward * movementInput.z;
            controller.Move(movement * Time.deltaTime);
            if(movement.magnitude > 0.1f && Time.time >= nextStep)
            {
                nextStep = Time.time + stepOffset;
                ass.PlayOneShot(steps[Random.Range(0,steps.Length)]) ;
            }
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

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, 2137))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.TryGetComponent(out Interact interacted))
                {
                    interacted.Interaction();
                }
            }
        }
    }

    public void Death()
    {
        ass.PlayOneShot(death);
        canMove = false;
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Win()
    {
        canMove = false;
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
}
