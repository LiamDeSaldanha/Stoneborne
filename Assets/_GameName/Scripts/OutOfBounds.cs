using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        // Optional: exclude the player or important objects
        if (!other.CompareTag("Player"))
        {

            Debug.Log("Out of bounds triggered");
            //Destroy(gameObject);
        }
    }
}
