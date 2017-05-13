using UnityEngine;
using System.Collections;

public class CaterpillarController : MonoBehaviour {
    public delegate void ForwardLeftEvent();
    public delegate void BackwardLeftEvent();
    public delegate void ForwardRigthEvent();
    public delegate void BackwardRigthEvent();
    public delegate void TeleportateEvent(Vector3 pos, float rot, Vector3 mousePos);
    

    public event ForwardLeftEvent onForwardLeft;
    public event BackwardLeftEvent onBackwardLeft;
    public event ForwardRigthEvent onForwardRigth;
    public event BackwardRigthEvent onBackwardRigth;
    public event TeleportateEvent onTeleportate;

    public void ForwardLeft() { onForwardLeft(); }
    public void BackwardLeft() { onBackwardLeft(); }
    public void ForwardRigth() { onForwardRigth(); }
    public void BackwardRigth() { onBackwardRigth(); }
    public void Teleportate(Vector3 pos, float rot, Vector3 mousePos) {
        if (onTeleportate != null) {
            onTeleportate(pos, rot, mousePos);
        }
        else {
            Debug.Log("kek");
        }
    }
}
