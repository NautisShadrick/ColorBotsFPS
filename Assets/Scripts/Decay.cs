using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public float Delay=1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Delay);
    }
}
