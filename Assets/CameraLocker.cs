using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float sensitivity=3;
    public Transform player;
    public float vert=0.2f;
    private float camDiff;
    private float camY;
    private float vertRot;
    //public float fov = 90f;
    //public Camera cam;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        //camY = gameObject.transform.rotation.eulerAngles
        //camDiff = Input.GetAxis("Mouse Y") * sensitivity;
        vertRot -= Input.GetAxis("Mouse Y") * sensitivity;
        vertRot = Mathf.Clamp(vertRot, -90f, 90f);
        gameObject.transform.position = player.position + new Vector3(0f, vert, 0f);
        gameObject.transform.localEulerAngles = new Vector3(vertRot, player.rotation.eulerAngles[1], player.rotation.eulerAngles[2]);
        /*
        gameObject.transform.Rotate(new Vector3(0f, player.rotation.eulerAngles[1] - gameObject.transform.rotation.eulerAngles[1], 0f));
        gameObject.transform.Rotate(new Vector3(-1 * Input.GetAxis("Mouse Y") * sensitivity, 0f, 0f));
        gameObject.transform.Rotate(new Vector3(0f, 0f, -1 * gameObject.transform.rotation.eulerAngles[2]));*/
    }

    //void Awake(){ cam.fieldOfView = fov; }
}
