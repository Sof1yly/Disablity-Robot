using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particle_Spin : MonoBehaviour
{
    public Vector3 Speed;
    public float amp;
    public float freq;
    void Start()
    {
        
    }

  
    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime);
        transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);
    }
}
