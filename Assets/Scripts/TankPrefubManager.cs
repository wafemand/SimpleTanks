using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPrefubManager : MonoBehaviour {
    public List<GameObject> prefubs;


    public GameObject SpawnNewTank(int tankID, Vector3 pos, Quaternion rot) {
        return Instantiate(prefubs[tankID], pos, rot);
    }

    public GameObject SpawnNewTank(int tankID) {
        return Instantiate(prefubs[tankID]);
    }


    // Use this for initialization
    void Start () {
        //SpawnNewTank(0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
