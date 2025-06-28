using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public float xBoundRange = 250.0f;
    public float zBoundRange = 250.0f;
    public float yBoundRange = 50.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -yBoundRange || transform.position.y > yBoundRange) { 
            Destroy(gameObject);
        }

        if (transform.position.x < -xBoundRange || transform.position.x > xBoundRange)
        {
            Destroy(gameObject);

        }
        if (transform.position.z < -zBoundRange || transform.position.z > zBoundRange)
        {
            Destroy(gameObject);

        }

    }
}
