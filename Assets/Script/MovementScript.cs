using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementScript : MonoBehaviour
{

    public float moveSpeed = 5;
    public float rotateSpeed = 25;

    [SerializeField]
    Rigidbody rb;

    private void Start()
    {
        rb =  transform.parent.GetComponent<Rigidbody>(); 
    }
    private void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        //Debug.Log(Mathf.Sin(transform.eulerAngles.y / 180 * Mathf.PI) + " - " + Mathf.Cos(transform.eulerAngles.y / 180 * Mathf.PI));
        //var InputAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed;

        //rb.AddForce(-transform.forward * InputAxis.z);

        //rb.AddForce(-transform.right * InputAxis.x);

        //transform.LookAt(rb.gameObject.GetComponent<PlayerScript>().cameraOnPlayer.transform.position);

        ////Debug.Log(rb.velocity + "  " + rb.velocity.magnitude);
        //if(rb.velocity.magnitude > 0.1)
        //rb.transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z));


        rb.transform.Rotate(new Vector3(0, Input.GetAxisRaw("Horizontal") * moveSpeed * 10 * Time.deltaTime, 0));

        Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime);
        move = transform.TransformDirection(move);
        rb.velocity = move * moveSpeed;

    }
}
