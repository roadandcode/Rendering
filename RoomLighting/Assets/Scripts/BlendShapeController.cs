using System.Collections;
using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class BlendShapeController : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] Avtars;

    [Range(0, 100)]
    public float[] Controller = new float[52];


    string[] blendshape_names = new string[] {
        "browDownLeft", "browDownRight", "browInnerUp", "browOuterUpLeft", "browOuterUpRight", "cheekPuff", "cheekSquintLeft", "cheekSquintRight", "eyeBlinkLeft", "eyeBlinkRight", "eyeLookDownLeft", "eyeLookDownRight", "eyeLookInLeft", "eyeLookInRight", "eyeLookOutLeft", "eyeLookOutRight", "eyeLookUpLeft", "eyeLookUpRight", "eyeSquintLeft", "eyeSquintRight", "eyeWideLeft", "eyeWideRight", "jawForward", "jawLeft", "jawOpen", "jawRight", "mouthClose", "mouthDimpleLeft", "mouthDimpleRight", "mouthFrownLeft", "mouthFrownRight", "mouthFunnel", "mouthLeft", "mouthLowerDownLeft", "mouthLowerDownRight", "mouthPressLeft", "mouthPressRight", "mouthPucker", "mouthRight", "mouthRollLower", "mouthRollUpper", "mouthShrugLower", "mouthShrugUpper", "mouthSmileLeft", "mouthSmileRight", "mouthStretchLeft", "mouthStretchRight", "mouthUpperUpLeft", "mouthUpperUpRight", "noseSneerLeft", "noseSneerRight", "tongueOut"
    };
    Dictionary<string, float> blendShapeDictionary = new Dictionary<string, float>();




    private void Update()
    {
        MapDataOnModel();
    }



    private void MapDataOnModel()
    {

        if (Avtars.Length == 0)
            return;


        for (int i = 0; i < Mathf.Min(Controller.Length, blendshape_names.Length); i++)
        {
            blendShapeDictionary[blendshape_names[i]] = Controller[i];
        }



        foreach (var kvp in blendShapeDictionary)
        {
            string blendShapeName = kvp.Key;
            float blendShapeWeight = kvp.Value;

            foreach (var avtar in Avtars)
            {
                int blendShapeIndex = avtar.sharedMesh.GetBlendShapeIndex(blendShapeName);
                if (blendShapeIndex != -1)
                {
                    avtar.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);
                }
            }


            // // Use the blendShapeName to get the index of the blend shape and set its weight
            // int blendShapeIndexA1 = Avatar1.sharedMesh.GetBlendShapeIndex(blendShapeName);
            // int blendShapeIndexA2 = Avatar2.sharedMesh.GetBlendShapeIndex(blendShapeName);
            // int blendShapeIndexA3 = Avatar3.sharedMesh.GetBlendShapeIndex(blendShapeName);
            // int blendShapeIndexA4 = Avatar4.sharedMesh.GetBlendShapeIndex(blendShapeName);

            // if (blendShapeIndexA1 != -1)
            // {
            //     Avatar1.SetBlendShapeWeight(blendShapeIndexA1, blendShapeWeight);
            // }

            // if (blendShapeIndexA2 != -1)
            // {
            //     Avatar1.SetBlendShapeWeight(blendShapeIndexA2, blendShapeWeight);
            // }

            // if (blendShapeIndexA3 != -1)
            // {
            //     Avatar1.SetBlendShapeWeight(blendShapeIndexA3, blendShapeWeight);
            // }

            // if (blendShapeIndexA4 != -1)
            // {
            //     Avatar1.SetBlendShapeWeight(blendShapeIndexA4, blendShapeWeight);
            // }

        }
    }



}

