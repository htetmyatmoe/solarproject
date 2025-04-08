using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    public float orbitSpeed = 10f;
    public bool isAuto = false;

    // Update is called once per frame
    void Update()
    {
        if(isAuto){
              transform.Rotate(Vector3.up, orbitSpeed * Time.deltaTime); 
        }
     
    }


    public void SetAutoMode(bool active){
        isAuto = active;
//Reset from starting point on mode switching
        if(!active){
            transform.rotation = Quaternion.identity;
        }

    }
}
