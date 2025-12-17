using UnityEngine;

public class Spawner_movement : MonoBehaviour
{
    
    public float moveSpeed = 5f; // Speed for moving the box



    private void Update()
    {
        // Move the box left and right using arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(movement);    
    }

}
