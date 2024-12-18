using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetSniffer : MonoBehaviour
{
    // Start is called before the first frame update

    private bool groundyWoundy = false;
    public bool IsGrounded() { return groundyWoundy; }
    void OnTriggerEnter(Collider collider) { if(!collider.isTrigger) groundyWoundy = true; }
    void OnTriggerStay(Collider collider) { if(!collider.isTrigger) groundyWoundy = true; }
    void OnTriggerExit(Collider collider) { groundyWoundy = false; }
    void Start() {GetComponent<MeshRenderer>().enabled=false;}
}
