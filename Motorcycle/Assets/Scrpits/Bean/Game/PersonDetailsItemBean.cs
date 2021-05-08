using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class PersonDetailsItemBean 
{
    public Vector3Bean position;
    public Vector3Bean angle;
    public Vector3Bean size;

    public List<PersonDetailsItemPartBean> listPartData = new List<PersonDetailsItemPartBean>();
}