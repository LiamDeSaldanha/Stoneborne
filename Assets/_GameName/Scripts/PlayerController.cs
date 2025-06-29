using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speedOfBus = 20.0f;
    private float turnSpeed = 100.0f;
    private float horizontalInput;
    private float verticalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Move forward
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speedOfBus*verticalInput);
        transform.Rotate(Vector3.up,Time.deltaTime*turnSpeed * horizontalInput);

    }
}
