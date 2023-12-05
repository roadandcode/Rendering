using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.UI;

public class TCPClient : MonoBehaviour
{
    private TcpClient tcpClient;
    private NetworkStream networkStream;
    private byte[] receiveBuffer = new byte[1024];
    private bool dataReceived = false;
    private string receivedData = "";


    [SerializeField] SkinnedMeshRenderer[] Avtars;

    string[] blendshape_names = new string[] {
        "browDownLeft", "browDownRight", "browInnerUp", "browOuterUpLeft", "browOuterUpRight", "cheekPuff", "cheekSquintLeft", "cheekSquintRight", "eyeBlinkLeft", "eyeBlinkRight", "eyeLookDownLeft", "eyeLookDownRight", "eyeLookInLeft", "eyeLookInRight", "eyeLookOutLeft", "eyeLookOutRight", "eyeLookUpLeft", "eyeLookUpRight", "eyeSquintLeft", "eyeSquintRight", "eyeWideLeft", "eyeWideRight", "jawForward", "jawLeft", "jawOpen", "jawRight", "mouthClose", "mouthDimpleLeft", "mouthDimpleRight", "mouthFrownLeft", "mouthFrownRight", "mouthFunnel", "mouthLeft", "mouthLowerDownLeft", "mouthLowerDownRight", "mouthPressLeft", "mouthPressRight", "mouthPucker", "mouthRight", "mouthRollLower", "mouthRollUpper", "mouthShrugLower", "mouthShrugUpper", "mouthSmileLeft", "mouthSmileRight", "mouthStretchLeft", "mouthStretchRight", "mouthUpperUpLeft", "mouthUpperUpRight", "noseSneerLeft", "noseSneerRight", "tongueOut"
    };
    Dictionary<string, float> blendShapeDictionary = new Dictionary<string, float>();



    private void Start()
    {
        ConnectToServer("127.0.0.1", 8080);

    }

    private void ConnectToServer(string serverIP, int serverPort)
    {
        try
        {
            tcpClient = new TcpClient(serverIP, serverPort);
            networkStream = tcpClient.GetStream();

            // Start receiving data in a separate thread or coroutine
            StartReceiving();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error connecting to server: {e.Message}");
        }
    }

    private void StartReceiving()
    {
        // You should implement a loop to continuously receive data
        // In a real application, you might want to use a separate thread or coroutine for this
        if (networkStream.CanRead)
        {
            networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, OnReceiveData, null);
        }
    }

    private void OnReceiveData(IAsyncResult result)
    {
        try
        {
            int bytesRead = networkStream.EndRead(result);
            if (bytesRead > 0)
            {
                receivedData = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
                dataReceived = true;
                // Continue receiving data
                StartReceiving();
            }
            else
            {
                // Connection closed by the server
                Debug.Log("Connection closed by the server");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error receiving data: {e.Message}");
        }
    }


    // You can send data to the server using a similar approach

    private void OnDestroy()
    {
        // Close the TCP client when the GameObject is destroyed or the application is closed
        if (tcpClient != null)
        {
            tcpClient.Close();
        }
    }

    private void Update()
    {
        // Process received data on the main thread
        if (dataReceived)
        {
            mapdataOnModel(HandleReceivedString(receivedData));
            dataReceived = false;
        }
    }


    void mapdataOnModel(float[] outputData)
    {
        if (Avtars.Length == 0)
            return;


        //float[] blendShapeValues = new float[52];

        for (int i = 0; i < Mathf.Min(outputData.Length, blendshape_names.Length); i++)
        {
            blendShapeDictionary[blendshape_names[i]] = outputData[i];
        }




        foreach (var kvp in blendShapeDictionary)
        {
            string blendShapeName = kvp.Key;
            float blendShapeWeight = kvp.Value;


            #region Old

            //if(blendShapeName == "jawOpen")
            //         {
            //	blendShapeWeight = 1f;

            //}

            // // Use the blendShapeName to get the index of the blend shape and set its weight
            // int blendShapeIndex = Avatar.sharedMesh.GetBlendShapeIndex(blendShapeName);

            // if (blendShapeIndex != -1)
            // {
            //     Avatar.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight * 100f);
            // }
            // else
            // {
            //     //				Debug.LogError("Blend shape not found: " + blendShapeName);
            // }

            #endregion


            foreach (var avtar in Avtars)
            {
                int blendShapeIndex = avtar.sharedMesh.GetBlendShapeIndex(blendShapeName);
                if (blendShapeIndex != -1)
                {
                    avtar.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight * 100);
                }
            }
        }
    }

    public float[] HandleReceivedString(string receivedString)
    {
        string[] stringValues = receivedString.Trim().Split(' ');

        float[] floatValues = new float[stringValues.Length];

        for (int i = 0; i < stringValues.Length; i++)
        {
            if (float.TryParse(stringValues[i], out float floatValue))
            {
                floatValues[i] = floatValue;
            }
            else
            {
                Debug.LogError("Failed to parse float value");
            }
        }

        return floatValues;

        // Now floatValues is a float array containing the parsed values
        // You can use floatValues as needed in your Unity application
    }
}
