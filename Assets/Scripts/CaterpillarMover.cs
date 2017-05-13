using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaterpillarMover : MonoBehaviour {

    public List<GameObject> leftCaterpillars;
    public List<GameObject> rigthCaterpillars;


    public float forwardSpeed = 0.1f;
    public float backwardSpeed = 0.1f;

    void InitControl() {
        CaterpillarController controller = GetComponent<CaterpillarController>();

        controller.onForwardLeft += Controller_onForwardLeft;
        controller.onForwardRigth += Controller_onForwardRigth;
        controller.onBackwardLeft += Controller_onBackwardLeft;
        controller.onBackwardRigth += Controller_onBackwardRigth;
        controller.onTeleportate += Teleportation;
    }

    private void Controller_onBackwardRigth() {
        foreach (GameObject caterpillar in rigthCaterpillars) {
            caterpillar.GetComponent<Rigidbody2D>().AddForce(
                -caterpillar.transform.up * forwardSpeed);
        }
    }

    private void Controller_onBackwardLeft() {
        foreach (GameObject caterpillar in leftCaterpillars) {
            caterpillar.GetComponent<Rigidbody2D>().AddForce(
                -caterpillar.transform.up * forwardSpeed);
        }
    }

    private void Controller_onForwardRigth() {
        foreach (GameObject caterpillar in rigthCaterpillars) {
            caterpillar.GetComponent<Rigidbody2D>().AddForce(
                caterpillar.transform.up * forwardSpeed);
        }
    }

    private void Controller_onForwardLeft() {
        foreach (GameObject caterpillar in leftCaterpillars) {
            caterpillar.GetComponent<Rigidbody2D>().AddForce(
                caterpillar.transform.up * forwardSpeed);
        }
    }

    void Start() {
        InitControl();
    }

    void Teleportation(Vector3 pos, float angle, Vector3 mousePos) {
        transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, angle));
    }
}
