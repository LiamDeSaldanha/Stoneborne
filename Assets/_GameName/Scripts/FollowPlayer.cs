using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public GameObject parent; // Should have a CharacterController

    private CharacterController controller;



    public float moveSpeed = 5f;
    public float turnSpeed = 180f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.4f;

    private Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundMask;

    

    

    void Start()
    {
        player = GameObject.Find("Player");
        controller = parent.GetComponent<CharacterController>();
    }

    void Update()
    {
        // Nothing needed here for now
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            player.GetComponent<PlayerController>().inCombat = true;

            // Ground check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Small downward force to stay grounded
            }

            Vector3 direction = player.transform.position - parent.transform.position;
            direction.y = 0f;
            direction.Normalize();

            // Move
            controller.Move(direction * moveSpeed * Time.deltaTime);

            // Rotate
            if (direction != Vector3.zero)
            {
                parent.transform.rotation = Quaternion.LookRotation(direction);
            }

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<PlayerController>().inCombat = false;
    }
}
