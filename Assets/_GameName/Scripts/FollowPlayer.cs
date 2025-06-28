using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
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
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        transform.Translate(lookDirection *Time.deltaTime);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

    }
}
