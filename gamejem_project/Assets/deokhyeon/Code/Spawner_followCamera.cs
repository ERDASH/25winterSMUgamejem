using UnityEngine;

public class Spawner_followCamera : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's Transform
        public float moveSpeed = 5f; // Speed for moving the spawner
        public Vector3 offset; // Offset from the camera's position

        private void Update()
        {
        // Move the spawner left and right using arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(movement);

        // Update the spawner's position to follow the camera's vertical movement
        if (cameraTransform != null)
        {
        transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offset.y, transform.position.z);
        }
        }
}
