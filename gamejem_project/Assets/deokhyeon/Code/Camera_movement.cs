using UnityEngine;
using System.Linq;

public class Camera_movement : MonoBehaviour
{
        private Vector3 velocity = Vector3.zero; // SmoothDamp 계산용 참조 변수
        public float smoothTime = 0.3f; // 목표 도달까지 걸리는 대략적인 시간
        public float smoothSpeed = 0.125f; // Camera movement speed
        public Vector3 offset; // Camera offset
        public Prefab_spawner prefabSpawner; // Reference to the Prefab_spawner script
        public float heightThreshold;

        private void LateUpdate()
        {
            // Find all active GameObjects in the scene
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            // Filter objects to find those with names starting with "Dongle" and exclude the preview dongle
            var placedDongles = allObjects.Where(obj => obj.name.StartsWith("Dongle") && obj != prefabSpawner.currentPreview).ToArray();

            if (placedDongles.Length == 0) return;

            // Find the dongle with the highest y position
            GameObject highestDongle = placedDongles.OrderByDescending(d => d.transform.position.y).FirstOrDefault();
            if (highestDongle.GetComponent<DongleMerge>().isGrounded == true)
            {
                if (highestDongle != null)
                    {
                    // Calculate the desired camera position
                    Vector3 desiredPosition = new Vector3(transform.position.x, highestDongle.transform.position.y + offset.y, transform.position.z);
                    // Smoothly move the camera to the desired position
                    //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                    transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
                    }
            }
        }

}

