using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayahMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private double angle;
    public float sensitivity;
    public float jumpPower = 10;
    public bool boringifyMovement = false;
    float cos;
    float sin;
    bool xMove;
    bool zMove;
    Vector3 velocity;
    public float gravityScale;
    private float downVelocity;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started!");

    }

    // Update is called once per frame
    void Update()
    {
        
        velocity = new Vector3(0, 0, 0);
        xMove = false;
        zMove = false;
        angle = gameObject.transform.rotation.eulerAngles[1] * Math.PI / 180;
        cos = (float)(speed * Math.Cos(angle));
        sin = (float)(speed * Math.Sin(angle));
        if (Input.GetKey(KeyCode.W))
        {
            velocity += new Vector3(sin, 0f, cos) * Time.deltaTime;
            zMove = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += new Vector3(-1*cos, 0f, sin) * Time.deltaTime;
            xMove = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += new Vector3(-1*sin, 0f, -1*cos) * Time.deltaTime;
            zMove = !zMove;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += new Vector3(cos, 0f, -1*sin) * Time.deltaTime;
            xMove = !xMove;
        }
        if (xMove && zMove && boringifyMovement) velocity = velocity / ((float)(Math.Sqrt(2)));


        //if(touching floor stuff)
        downVelocity += gravityScale;
        if (Input.GetKey(KeyCode.Space)) jump();
        velocity += Vector3.down * downVelocity * Time.deltaTime;
        gameObject.transform.position += velocity;
        

        //mouse stuff;
        gameObject.transform.Rotate(new Vector3(0f,Input.GetAxis("Mouse X")*sensitivity,0f));

        //Fix goofy unity mechanics
        //gameObject.tranform.rotation = Quaternion.Slerp();
        
        //gameObject.transform.rotation.x = 0f;
        //gameObject.transform.rotation.z = 0f;

    }

    void jump()
    {
        downVelocity = -1*jumpPower;
        
    }

    void OnCollisionEnter(Collision other){
        Debug.Log("Bomba!");
        //return if the dimension is wrong
        Vector3 normal = other.contacts[0].normal;
        Debug.Log(FindLen(normal));
        //gameObject.transform.position += normal * Time.deltaTime * downVelocity;
        if (VectDiv(normal, FindLen(normal)).Equals(Vector3.up))
        {
            downVelocity = 0;
            Debug.Log("Floor!");
        }
        
    }

    float FindLen(Vector3 vect)
    {
        return (float)Math.Sqrt((vect.x * vect.x) + (vect.y * vect.y) + (vect.z * vect.z));
    }

    Vector3 VectDiv(Vector3 vect, float scalar)
    {
        return new Vector3(vect[0] / scalar, vect[1]/scalar, vect[2]/scalar);
    }
}
