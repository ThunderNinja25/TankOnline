using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        myRigidbody.AddForce(transform.forward * verticalInput * moveSpeed, ForceMode.VelocityChange);
        myRigidbody.rotation = Quaternion.Euler(0, transform.eulerAngles.y + horizontalInput, 0);
    }
}
