using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public GameObject parent; // Should have a CharacterController
    public float speed;

    private CharacterController controller;

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
            Vector3 direction = player.transform.position - parent.transform.position;
            direction.y = 0f;
            direction.Normalize();

            // Move
            controller.Move(direction * speed * Time.deltaTime);

            // Rotate
            if (direction != Vector3.zero)
            {
                parent.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
