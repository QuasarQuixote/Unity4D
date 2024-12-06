using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayahRigidMovement : MonoBehaviour
{
    public float jumpPower = 10f;
    public float speed = 3f;
    public Rigidbody thisBody;
    public float sensitivity = 1f;
    //private bool grounded = false;
    Vector3 appliedVelocity;
    Vector3 currentSpawn;
    Vector3 defaultSpawn;
    //public BoxCollider feet;
    // Start is called before the first frame update
    void Start()
    {
        thisBody = GetComponent<Rigidbody>();
        appliedVelocity = new Vector3();
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center
        Cursor.visible = false;  // Hide the cursor

        //checkpoint shenanigans
        defaultSpawn = new Vector3(-3, 3, 0);
        currentSpawn = defaultSpawn;
    }

    // Update is called once per frame
    void Update()
    {

        CalcAppliedVelocity();
        thisBody.velocity = new Vector3(appliedVelocity[0], thisBody.velocity[1], appliedVelocity[2]);
        //Debug.Log(thisBody.velocity[0]+" "+ thisBody.velocity[1] + " " + thisBody.velocity[2]);

        //rotate about Y-axis
        gameObject.transform.Rotate(new Vector3(0f, sensitivity * Input.GetAxis("Mouse X"), 0f));

        if (Input.GetKeyDown(KeyCode.Space) && gameObject.transform.GetChild(0).GetComponent<FeetSniffer>().IsGrounded())
        {
            
            thisBody.velocity += Vector3.up * jumpPower;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            Cursor.visible = true;  // Make the cursor visible again
        }

        //more checkpoint stuff
        if (Input.GetKeyDown(KeyCode.C)) currentSpawn = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
        if (Input.GetKeyDown(KeyCode.X)) currentSpawn = defaultSpawn;
        //Player falls
        if (gameObject.transform.position.y < -20) {
            gameObject.transform.position = currentSpawn;
            thisBody.velocity += Vector3.up * thisBody.velocity.y * -1;
        }

        
    }
    
    void CalcAppliedVelocity()
    {
        appliedVelocity = new Vector3(0f, 0f, 0f);
        float angle = gameObject.transform.rotation.eulerAngles.y * Mathf.PI / 180;
        //Debug.Log(angle);
        float cos = Mathf.Cos(angle) * speed;
        float sin = Mathf.Sin(angle) * speed;
        bool shouldScale = true;
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if (horiz!=0)
        {
            //Debug.Log(horiz);
            shouldScale = !shouldScale;
            appliedVelocity += new Vector3(horiz * cos, 0f, -1 * sin * horiz);
        }
        if (vert != 0)
        {
            //Debug.Log(vert);
            shouldScale = !shouldScale;
            appliedVelocity += new Vector3(vert * sin, 0f, cos * vert);
        }
        if (shouldScale) appliedVelocity = appliedVelocity * (1/Mathf.Sqrt(2));
        //Debug.Log(appliedVelocity[0] + " " + appliedVelocity[1] + " " + appliedVelocity[2]);
    }

    
}
