using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSettings {
    public enum ControlKey {
        ForwardLeft,
        BackwardLeft,
        ForwardRight,
        BackwardRight
    }


    KeyCode[] keyCodes;


    void Start() {
        keyCodes = new KeyCode[Enum.GetValues(typeof(ControlKey)).Length];
        keyCodes[(int)ControlKey.ForwardLeft] = KeyCode.Q;
        keyCodes[(int)ControlKey.ForwardRight] = KeyCode.E;
        keyCodes[(int)ControlKey.BackwardLeft] = KeyCode.A;
        keyCodes[(int)ControlKey.BackwardRight] = KeyCode.D;
    }


    public List<ControlKey> GetPressedKeys() {
        List<ControlKey> ret = new List<ControlKey>();
        foreach(int key in Enum.GetValues(typeof(ControlKey))) {
            if (Input.GetKey(keyCodes[key])) {
                ret.Add((ControlKey)key);
            }
        }
        return ret;
    }


    public Vector3 GetMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
