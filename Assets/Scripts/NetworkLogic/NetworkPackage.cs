using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class NetworkPackage {
    public NetworkDataType packageType;
    public bool isImportant = false;
    public int packageID;
    public int playerID;
    [NonSerialized]
    public EndPoint sender;

    public static byte[] Serialize(NetworkPackage package) {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, package);
        return stream.ToArray();
    }

    public static NetworkPackage Deserialize(byte[] data) {
        MemoryStream stream = new MemoryStream(data);
        BinaryFormatter formatter = new BinaryFormatter();
        return (NetworkPackage)formatter.Deserialize(stream);
    }


}
