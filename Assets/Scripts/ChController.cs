using UnityEngine;
using System.Collections;

public class ChController : MonoBehaviour {

    public GameObject CPrefab;
    public bool CanMoveOnAir;
    private bool gravityNormal = false;

    public float Speed = 5.0f;
    public Vector3 MoveDirection = Vector3.zero;

    void Start(){
        gravityNormal = true;

	    if (CPrefab == null){
		    CPrefab = GameObject.FindGameObjectWithTag("MainCamera");
		    //Debug.Log(CPrefab.GetComponent("CameraScr").charObj);
		    CPrefab.GetComponent<CameraController>().charObj = transform;
		    CPrefab.GetComponent<CameraController>().Initialize(transform);
	    }
	    else
	    {
		    CPrefab.GetComponent<CameraController>().charObj = transform;
		    CPrefab.GetComponent<CameraController>().Initialize(transform);
	    } 
    } 

    void Update () {
        if (Input.GetButtonDown("Change Gravity"))
        {
            changeGravity();
        }
    }
	
    public void changeGravity()
    {   
        constantForce.force = new Vector3(constantForce.force.x, -constantForce.force.y, constantForce.force.z);
    }

    private bool IsGrounded()
    {
        Vector3 groundDirection;
        if (gravityNormal)
            groundDirection = -Vector3.up;
        else
            groundDirection = Vector3.up;
        return Physics.Raycast(transform.position, groundDirection, 2);
    }

    void FixedUpdate (){
   
        if (Input.GetButton("Rotate"))
        {
            transform.Rotate(0,Input.GetAxisRaw("Rotate")*100*Time.deltaTime,0);
        } 
	    Movement();
    }

    void Movement()
    {
        if ( (Input.GetAxis("Horizontal") == 0 ) || (Input.GetAxis("Vertical") == 0))
        {
            CPrefab.transform.rotation = Quaternion.Lerp(CPrefab.transform.rotation, this.transform.rotation, Time.deltaTime * 8f);
            //CPrefab.transform.rotation = this.transform.rotation;
            MoveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), MoveDirection.y, Input.GetAxisRaw("Vertical"));
        }
        if (IsGrounded())
            this.transform.Translate((MoveDirection.normalized * Speed) * Time.deltaTime);
    }
}
