using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playah4D : MonoBehaviour
{
    public float playah4dPos = 0f;
    public float speed = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) playah4dPos += speed * Time.deltaTime;
        if(Input.GetMouseButton(1)) playah4dPos -= speed * Time.deltaTime;
        playah4dPos = Mathf.Clamp(playah4dPos, -100f, 100f);
    }
}
