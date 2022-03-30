using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 offset;
    private Vector3 newPosition;

    private void Start() {
        newPosition = playerPos.position + offset;
        transform.position = newPosition;
    }

    // Update is called once per frame
    private void LateUpdate() {
        newPosition = playerPos.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.5f);
        
        Vector3 newRotation = playerPos.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(newRotation, Vector3.up);
        transform.rotation = rotation;
    }
}
