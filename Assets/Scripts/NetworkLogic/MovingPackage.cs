using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class MovingPackage : NetworkPackage {
    public float tankX, tankY;
    public float mouseX, mouseY;
    public float angle;
}
