using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private float moveSpeed;

    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

        if (!IsOwner) return;

        SetMoveDirectionRpc(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    [Rpc(SendTo.Server)]
    private void SetMoveDirectionRpc(float horizontal, float vertical)
    {
        horizontalInput = horizontal;
        verticalInput = vertical;
    }

    private void FixedUpdate()
    {
        myRigidbody.AddForce(transform.forward * verticalInput * moveSpeed, ForceMode.VelocityChange);
        //myRigidbody.velocity = transform.forward * verticalInput * moveSpeed;
        myRigidbody.rotation = Quaternion.Euler(0, transform.eulerAngles.y + horizontalInput, 0);
    }
}
