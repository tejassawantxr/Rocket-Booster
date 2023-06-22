using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 10f; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space)){
            Debug.Log("Space bar pressed");
           rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }
    void ProcessRotation(){
        if (Input.GetKey(KeyCode.A)){
            Debug.Log("Rotate Left");
        }
        else if (Input.GetKey(KeyCode.D)){
            Debug.Log("Rotate Right");
        }
    }

}
