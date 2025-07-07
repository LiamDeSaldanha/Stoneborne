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
    void Update()
    {



      



        
        //Move forward
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed * verticalInput);
        transform.Rotate(Vector3.up,Time.deltaTime*turnSpeed * horizontalInput);
        
    }

    
}
