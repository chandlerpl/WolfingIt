using System;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInstance : MonoBehaviour
{
    public static CanvasInstance instance;

    void Start()
    {
        instance = this;
    }
}
