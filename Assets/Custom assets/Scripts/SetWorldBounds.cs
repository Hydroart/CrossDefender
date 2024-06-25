using System;
using UnityEngine;

public class SetWorldBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bounds = GetComponent<SpriteRenderer>().bounds;
        Globals.WorldBounds = bounds;
    }
}

