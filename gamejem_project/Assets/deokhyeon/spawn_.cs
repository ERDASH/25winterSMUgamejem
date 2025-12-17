// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using System.Collections.Generic;

public class spawn_ : MonoBehaviour
{
    public List<GameObject> circlePrefabs; // List to store multiple circle prefabs
public float moveSpeed = 5f; // Speed for moving the box
public Transform spawnPoint; // Position where the circles will be spawned

private void Update()
{
// Move the box left and right using arrow keys
float horizontalInput = Input.GetAxis("Horizontal");
Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
transform.Translate(movement);

// Spawn a random circle when the spacebar is pressed
if (Input.GetKeyDown(KeyCode.Space))
{
SpawnRandomCircle();
}
}

private void SpawnRandomCircle()
{
if (circlePrefabs.Count > 0)
{
// Select a random circle from the list
int randomIndex = Random.Range(0, circlePrefabs.Count);
GameObject randomCircle = circlePrefabs[randomIndex];

// Spawn the selected circle at the spawn point
Instantiate(randomCircle, spawnPoint.position, Quaternion.identity);
}
else
{
Debug.LogWarning("circlePrefabs list is empty!");
}
}
        
}
