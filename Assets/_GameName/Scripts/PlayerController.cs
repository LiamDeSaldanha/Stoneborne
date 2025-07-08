using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float throwSpeed = 20.0f;
    private float turnSpeed = 100.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {







        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Physics-based movement and rotation
        Vector3 moveDirection = transform.forward * throwSpeed * verticalInput * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, horizontalInput * turnSpeed * Time.deltaTime, 0f);

        rb.MovePosition(rb.position + moveDirection);
        rb.MoveRotation(rb.rotation * deltaRotation);


    }


}
