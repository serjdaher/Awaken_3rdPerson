using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float rotationSpeed = 0.0f;
    int moveXAnimationParameterID;
    int moveZAnimationParameterID;

    private Animator animator;
    // private CharacterController characterController;

    // Manually assign the camera in the inspector.
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject beam;


    void Awake()
    {
        animator = GetComponent<Animator>();

        // Initialize the Walking float for the blend trees.
        animator.SetFloat("Walking", 0.5f);

        // Assign an ID to MoveX and MoveZ to avoid calling them by string everytime, and avoid mistakes.
        moveXAnimationParameterID = Animator.StringToHash("MoveX");
        moveZAnimationParameterID = Animator.StringToHash("MoveZ");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Just to avoid having an empty Start()
        Debug.Log("Let's Start");
        // characterController = GetComponent<CharacterController>();

        // Turn the Laser beam off for when she is not shooting.
        beam.SetActive(false);
    }

     //Update is called once per frame
    void Update()
    {

        // Get generic WASD and Arrow inputs implemented in Unity
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Make the character follow the camera direction when moving forward.
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection; // Change according to the rotation of the camera only on the Y Axis.
        movementDirection.Normalize();

        // Set the MoveX and MoveZ floats in the Animator
        animator.SetFloat(moveXAnimationParameterID, horizontalInput);
        animator.SetFloat(moveZAnimationParameterID, verticalInput);

        // Make the character move forward.
        transform.Translate(moveSpeed * Time.deltaTime * movementDirection, Space.World);

        //transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;

        // Move character towards direction of input.
        if (movementDirection != Vector3.zero) // If the character is moving:
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        PlayerAnimations();

        // Stop the character from moving if pressing F
        if (Input.GetKeyDown(KeyCode.F))
        {
            moveSpeed = 0f;
        }
    }

    void PlayerAnimations()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            moveSpeed = 12.0f;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            moveSpeed = 6.0f;
            animator.SetBool("IsRunning", false);
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsWalking", false);
            beam.SetActive(true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            beam.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("IsAiming", true);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsAiming", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("IsPickup", true);
        }
        // Check if the PickUpRelic is done to switch the boolean.
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("PickUpRelic"))
        {
            animator.SetBool("IsPickup", false);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

