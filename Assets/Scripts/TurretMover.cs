using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMover : MonoBehaviour {

    public float RotationSpeed = 30f;
    Rigidbody2D rigidbody2d;

    float AngleNormalize(float angle) {
        return angle - (360 * Mathf.Floor(angle / 360));
    }

    float AngleDist(float angleFrom, float angleTo) {
        angleTo = AngleNormalize(angleTo);
        angleFrom = AngleNormalize(angleFrom);
        return Mathf.Min(Mathf.Abs(angleFrom - angleTo), 360 - Mathf.Abs(angleFrom - angleTo));
    }

    float CircleDirection(float angleFrom, float angleTo) //Сделяль. Переделяль
    {
        angleTo = AngleNormalize(AngleNormalize(angleTo) - AngleNormalize(angleFrom));
        if (angleTo > 180) {
            return -1;
        }
        else {
            return 1;
        }
    }

    void MoveTurret() {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Camera.main.transform.position.z;
        Vector3 direction = Camera.main.ScreenToWorldPoint(mouse) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        if (AngleDist(rigidbody2d.rotation, angle) < RotationSpeed * Time.fixedDeltaTime) {
            rigidbody2d.MoveRotation(angle);
        }
        else {
            rigidbody2d.MoveRotation(
                rigidbody2d.rotation +
                (CircleDirection(rigidbody2d.rotation, angle) * RotationSpeed +
                GetComponent<HingeJoint2D>().connectedBody.angularVelocity) * Time.fixedDeltaTime);
        }
    }

    void FixedUpdate() {
        MoveTurret();
    }

    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
