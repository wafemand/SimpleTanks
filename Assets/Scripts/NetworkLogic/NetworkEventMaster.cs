using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkEventMaster : MonoBehaviour {
    public int localPort;
    Queue<NetworkPackage> packageQueue;
    NetworkInterface networkInterface;
    public NetworkInterface NetworkInterface {
        get {
            return networkInterface;
        }
    }

    private void NetworkInterface_OnRecieveData(NetworkPackage package) {
        switch (package.packageType) {
            case NetworkDataType.Moving:
                if (OnMovingPackage != null) {
                    OnMovingPackage((MovingPackage)package);
                }
                break;
            case NetworkDataType.Shooting:
                break;
            case NetworkDataType.Register:
                if (OnRegisterPackage != null) {
                    OnRegisterPackage((RegisterPackage)package, package.sender);
                }
                break;
            case NetworkDataType.Confirm:
                break;
            case NetworkDataType.TankLoad:
                if (OnNewTank != null) {
                    OnNewTank((RegisterPackage)package);
                }
                break;
            default:
                break;
        }
    }


    private void Update() {
        networkInterface.Update();
        while (packageQueue.Count > 0) {
            NetworkInterface_OnRecieveData(packageQueue.Dequeue());
        }
    }


    private void Start() {
        packageQueue = new Queue<NetworkPackage>();
        networkInterface = new NetworkInterface(localPort, packageQueue);
    }


    public delegate void MovingPackageEvent(MovingPackage package);
    public delegate void RegisterPackageEvent(RegisterPackage package, EndPoint endPoint);
    public delegate void NewTankEvent(RegisterPackage package);

    public event NewTankEvent OnNewTank;
    public event MovingPackageEvent OnMovingPackage;
    public event RegisterPackageEvent OnRegisterPackage;
}
