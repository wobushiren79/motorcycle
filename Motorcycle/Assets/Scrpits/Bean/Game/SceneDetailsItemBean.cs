using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SceneDetailsItemBean
{
    public long buildingId;
    public Vector3Bean position;
    public Vector3Bean angle;
    public Vector3Bean size;
}