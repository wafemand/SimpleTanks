using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(NetworkEventMaster))]
public class NetworkPlayerManager : MonoBehaviour {
    NetworkInterface networkInterface;
    NetworkEventMaster networkEventMaster;
    Dictionary<int, CaterpillarController> networkPlayers;
    public TankPrefubManager tankPrefubManager;
    public string serverAddr = "127.0.0.1";
    public int serverPort = 1337;
    public int localTankID = 0;
    public int localPlayerID;
    public GameObject localTank;
    EndPoint serverPoint;


    void Start() {
        serverPoint = new IPEndPoint(IPAddress.Parse(serverAddr), serverPort);
        networkPlayers = new Dictionary<int, CaterpillarController>();
        networkEventMaster = GetComponent<NetworkEventMaster>();
        networkEventMaster.OnRegisterPackage += NetworkEventMaster_OnRegisterPackage;
        networkEventMaster.OnMovingPackage += NetworkEventMaster_OnMovingPackage;
        networkEventMaster.OnNewTank += NetworkEventMaster_OnNewTank;
        networkInterface = networkEventMaster.NetworkInterface;
        networkInterface.SendTo(new RegisterPackage(){
            tankID = localTankID,
            isImportant = false,
            packageType = NetworkDataType.Register,
        }, serverPoint);
        localTank.GetComponent<CaterpillarLocalPrayer>().onMoving += OnMovingLocalTank;
    }

    private void OnMovingLocalTank() {
        Transform tankTransform = localTank.GetComponent<Transform>();
        networkInterface.SendTo(new MovingPackage() {
            tankX = tankTransform.position.x,
            tankY = tankTransform.position.y,
            angle = tankTransform.rotation.eulerAngles.z,
            mouseX = 228,
            mouseY = 228
        }, serverPoint);
    }

    private void NetworkEventMaster_OnNewTank(RegisterPackage package) {
        GameObject newTank = Instantiate(tankPrefubManager.prefubs[package.tankID]);
        networkPlayers[package.playerID] = newTank.GetComponent<CaterpillarController>();
    }

    private void NetworkEventMaster_OnMovingPackage(MovingPackage package) {
        Debug.Log(new Vector3(package.tankX, package.tankY));
        Debug.Log(package.angle);
        networkPlayers[package.playerID].Teleportate(
            new Vector3(package.tankX, package.tankY),
            package.angle,
            new Vector3(package.mouseX, package.mouseY));
    }

    private void NetworkEventMaster_OnRegisterPackage(RegisterPackage package, EndPoint endPoint) {
        localPlayerID = package.playerID;
    }
}

