using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerIndicator : MonoBehaviour
{
    Color playahCol;
    public Playah4D bigP;
    Material material;
    float playah4dPos;
    public Transform camTrans;
    Vector3 viewLine;
    Vector3 perpLine;
    public float offset = 10f;
    Quaternion rotation;
    Vector3 eulerRot;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (bigP != null)
        {
            playah4dPos = bigP.playah4dPos;
            playahCol = PlattyMechs.posToRgb(playah4dPos);
            material.color = playahCol;
        }
        if(camTrans != null){
            rotation = camTrans.rotation;
            gameObject.transform.rotation = rotation;
            //y comp = sin(-x angle)
            // get xz comp
            //z comp = cos(y angle)
            //x comp = sin(y angle)
            eulerRot = rotation.eulerAngles;
            eulerRot.x += 20f;
            float yOffset = Mathf.Sin(eulerRot.x * Mathf.PI / -180f);
            float xzPart = Mathf.Sqrt(1f-(yOffset*yOffset));
            viewLine = new Vector3(Mathf.Sin(eulerRot.y * Mathf.PI / 180f)*xzPart, yOffset, xzPart*Mathf.Cos(eulerRot.y * Mathf.PI / 180f));
            //Debug.Log(viewLine.x +" "+ viewLine.y+" "+viewLine.z);
            /*
            eulerRot = eulerRot - Vector3.right*90f;
            float perpYOffset = Mathf.Sin(eulerRot.x * Mathf.PI / -180f);
            float perpXzPart = Mathf.Sqrt(1f-(perpYOffset*perpYOffset));
            perpLine = new Vector3(Mathf.Sin(eulerRot.y * Mathf.PI / 180f)*perpXzPart, perpYOffset, perpXzPart*Mathf.Cos(eulerRot.y * Mathf.PI / 180f));
            */
            gameObject.transform.position = camTrans.position + viewLine * offset; //+ perpLine*offset*0.5f;
        }
    }
}
