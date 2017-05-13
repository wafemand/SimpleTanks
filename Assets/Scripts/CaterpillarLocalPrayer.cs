using UnityEngine;
using System.Collections;

public class CaterpillarLocalPrayer : MonoBehaviour {
    CaterpillarController controller;
    public delegate void MovingEvent();
    public event MovingEvent onMoving;

    void Start() {
        controller = GetComponent<CaterpillarController>();
    }
    
    void Update() {
        if (Input.GetKey(KeyCode.Q)) {
            controller.ForwardLeft();
            if (onMoving != null) {
                onMoving();
            }
        }
        if (Input.GetKey(KeyCode.E)) {
            controller.ForwardRigth();
            if (onMoving != null) {
                onMoving();
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            controller.BackwardRigth();
            if (onMoving != null) {
                onMoving();
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            controller.BackwardLeft();
            if (onMoving != null) {
                onMoving();
            }
        }
    }
}
