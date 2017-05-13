using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using UnityEngine;
using System.ComponentModel;
using System.Threading;

public class NetworkInterface {
    //public float timeLimit = 5;     //time limit for disconnect (seconds)
    public int localPort;

    //int freePackageID = 0;         //Used for important packages
    //int localPackageID = 0;        //Used for important packages
    //float idleTime = 0;             //if idleTime > timeLimit then channel is disabled
    UdpClient client;
    Queue<NetworkPackage> packageQueue; 


    public NetworkInterface(int localPort, Queue<NetworkPackage> packageQueue) {//For server
        client = new UdpClient(localPort);
        this.localPort = ((IPEndPoint)client.Client.LocalEndPoint).Port;
        this.packageQueue = packageQueue;
    }


    public void SendTo(NetworkPackage package, EndPoint to) {
        byte[] datagram = NetworkPackage.Serialize(package);
        try {
            client.Send(datagram, datagram.Length, (IPEndPoint)to);
        }
        catch (Exception e) {
            Debug.Log(e);
            Debug.Log(e.StackTrace);
            Debug.Log(e.Message);
        }
    }


    public void Update() {
        while (client.Available > 0) {
            try {
                IPEndPoint sender = null;
                byte[] data = client.Receive(ref sender);
                ProcessData(data, sender);
            }
            catch (Exception e) {
                Debug.Log(e);
                Debug.Log(e.StackTrace);
                Debug.Log(((SocketException)e).ErrorCode);
            }
        }
    }


    void ProcessData(byte[] data, EndPoint sender) {
        try {
            NetworkPackage package = NetworkPackage.Deserialize(data);
            package.sender = sender;
            if (package.isImportant) {
                throw new System.NotImplementedException();
                /*if (package.packageID >= freePackageID) {
                    freePackageID = package.packageID + 1;
                    OnRecieveData(package, sender);
                }
                NetworkPackage confirm = new NetworkPackage() {
                    packageID = package.packageID,
                    packageType = NetworkDataType.Confirm,
                    isImportant = false
                };
                SendTo(confirm, sender);*/
            }
            else {
                RecieveData(package);
            }
        }
        catch (Exception e){
            Debug.Log(e);
            Debug.Log(e.StackTrace);
            Debug.Log(e.Message);
        }
    }

    private void RecieveData(NetworkPackage package) {
        packageQueue.Enqueue(package);
    }


    void SendImportant(NetworkPackage package) {
        package.isImportant = true;
        throw new NotImplementedException();
    }
}