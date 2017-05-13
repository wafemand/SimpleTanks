using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

[RequireComponent(typeof(NetworkEventMaster))]
public class ServerLogic : MonoBehaviour {
    class ClientInfo {
        public int playerID;
        public int tankID;
        public EndPoint endPoint;
    }


    int freePlayerID = 0;
    public int localPort = 1337;
    List<ClientInfo> clients = new List<ClientInfo>();
    ClientInfo nextClient;
    NetworkInterface networkInterface;
    NetworkEventMaster networkEventMaster;
    

    void OnConnectNewClient(RegisterPackage package, EndPoint endPoint) {
        try {
            nextClient.tankID = package.tankID;
            nextClient.endPoint = endPoint;
            networkInterface.SendTo(new RegisterPackage() {
                playerID = nextClient.playerID,
                packageType = NetworkDataType.Register
            }, endPoint);

            foreach (var client in clients) {
                UploadTankTo(client, nextClient);
                UploadTankTo(nextClient, client);
            }
            clients.Add(nextClient);
            nextClient = new ClientInfo() { playerID = freePlayerID++ };
        }
        catch (Exception e) {
            Debug.Log(e);
            Debug.Log(e.StackTrace);
            Debug.Log(e.Message);
        }
    }


    void UploadTankTo(ClientInfo to, ClientInfo about) {
        networkInterface.SendTo(new RegisterPackage() {
            tankID = about.tankID,
            isImportant = false,
            packageType = NetworkDataType.TankLoad,
            playerID = about.playerID
        }, to.endPoint);
    }


    void OnMoving(MovingPackage package) {
        foreach (var client in clients) {
           /* Debug.Log(package.playerID);
            Debug.Log(package.tankX);
            Debug.Log(package.tankY);
            Debug.Log(package.angle);*/
            if (client.playerID != package.playerID) {
                networkInterface.SendTo(package, client.endPoint);
            }
        }
    }


    void InitNetwork() {
        networkEventMaster = GetComponent<NetworkEventMaster>();
        networkEventMaster.OnRegisterPackage += OnConnectNewClient;
        networkEventMaster.OnMovingPackage += OnMoving;
        networkInterface = networkEventMaster.NetworkInterface;
    }


    void InitServer() {
        nextClient = new ClientInfo() { playerID = freePlayerID++ };
    }


    private void Start() {
        InitServer();
        InitNetwork();
    }
}
