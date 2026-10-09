using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 2, 15); // Kameran etäisyys pelaajasta
    public float smoothSpeed = 0.125f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        // Tasainen siirtymä kohteeseen
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(playerTransform);
    }
}

