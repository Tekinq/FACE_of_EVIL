using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour

{

    public float lifetime;

    void Start()
    {
        Destroy(gameObject,lifetime);
    }
   
}
