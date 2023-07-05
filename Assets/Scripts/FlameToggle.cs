using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameToggle : MonoBehaviour
{
    Renderer meshrenderer;
    void Start()
    {
        meshrenderer = GetComponent<SkinnedMeshRenderer>();
        meshrenderer.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
           meshrenderer.enabled = true;
        }
        else{
            meshrenderer.enabled = false;
        }
    }
}
