using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpHeight;

    private float gravity = -50f;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 1; //bc endless runner

        //always faces forward:
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore); //QueryTriggerInteraction.Ignore = ignores triggers

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0; //reset gravity application
        }
        else
        {
            //add gravity:
            velocity.y += gravity * Time.deltaTime;
        }

        //jump:
        if( isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //constantly moves forward:
        characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);

        //gravity applied:
        characterController.Move(velocity * Time.deltaTime);

    }
}
