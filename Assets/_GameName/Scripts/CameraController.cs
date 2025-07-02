using UnityEngine;

public class FollowPlayerRotationCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 3, -6);
    public float followSpeed = 5f;
    public float rotationSmoothTime = 0.1f;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentAngle;
    private float angleVelocity;

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate the desired rotation angle behind the player
        float targetAngle = player.eulerAngles.y;

        // Smoothly interpolate the camera's rotation to follow the player
        currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref angleVelocity, rotationSmoothTime);

        // Convert angle to rotation and apply it to the offset
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 0.05f);
        Vector3 offset_xRotation = new Vector3(0,2,0);
        // Always look at the player
        transform.LookAt(player.position+offset_xRotation + Vector3.up * 1.5f);
    }
}
