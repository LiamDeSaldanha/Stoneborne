using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    public GameObject parent;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            Vector3 lookDirection = (player.transform.position - parent.transform.position).normalized;

           

            // Move
            parent.transform.position += lookDirection * speed * Time.deltaTime;

            // Rotate
            if (lookDirection != Vector3.zero)
            {
                parent.transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }

    }

}
