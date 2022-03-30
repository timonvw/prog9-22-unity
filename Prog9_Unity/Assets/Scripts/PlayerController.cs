using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IReset
{
    [Header("Player")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask groundMask;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeedReducer;
    [SerializeField] private float fallSpeedIncreaser;
    private Vector3 forceDirection = Vector3.zero;
    private float verticalMoveDirection;
    private float horizontalMoveDirection;
    private bool isGrounded;
    private Vector3 startPos;

    private void Start() {
        startPos = transform.position;
    }

    void FixedUpdate() {
        GroundCheck();
        Movement();
    }

    private void Movement() {
        // Push ball in given directions
        forceDirection.x = horizontalMoveDirection;
        forceDirection.z = verticalMoveDirection;
        
        // Reduce the movement speed when we are in the air
        if (isGrounded) {
            rb.AddForce(forceDirection * speed, ForceMode.Impulse);
        } else {
            rb.AddForce((forceDirection * speed) * airSpeedReducer, ForceMode.Impulse);
        }

        // Increase the acceleration as we fall
        if (rb.velocity.y < 0f) {
            rb.velocity -= Vector3.down * Physics.gravity.y * fallSpeedIncreaser * Time.fixedDeltaTime;
        }
        
        // Cap our movement speed
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0f;
        if (horizontalVelocity.sqrMagnitude > speed * speed) {
            rb.velocity = horizontalVelocity.normalized * speed + Vector3.up * rb.velocity.y;
        }
    }

    private void GroundCheck() {
        Vector3 sphereLocation = transform.position;
        sphereLocation.y += transform.lossyScale.y / 2;
        isGrounded = Physics.CheckSphere(sphereLocation, 1.5f, groundMask);
    }

    private void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Enemy")) {
            Rigidbody enemyRb = coll.gameObject.GetComponent<Rigidbody>();
            Vector3 forceDir = coll.gameObject.transform.position - transform.position;
            enemyRb.AddForce(forceDir * (6 / enemyRb.mass), ForceMode.Impulse);
        }
    }
    
    private void ResetPhysics() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void OnReset() {
        transform.position = startPos + Vector3.up;
        ResetPhysics();
    }
    
    // Event callbacks
    public void OnMove(float directionX, float directionY) {
        horizontalMoveDirection = directionX;
        verticalMoveDirection = directionY;

        if (directionX == 0f && directionY == 0f) {
            // ResetPhysics();
        }
    }
    public void OnJump(bool jump) {
        if(isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
