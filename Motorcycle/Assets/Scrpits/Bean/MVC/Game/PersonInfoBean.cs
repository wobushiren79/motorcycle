/*
* FileName: PersonInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-08-13:42:21 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class PersonInfoBean : BaseBean
{
    public int person_number;
    public string person_data;


    public PersonDetailsBean GetPersonDetilas()
    {
        PersonDetailsBean sceneDetails = JsonUtil.FromJson<PersonDetailsBean>(person_data);
        return sceneDetails;
    }

    public void SetPersonDetilas(PersonDetailsBean sceneDetails)
    {
        person_data = JsonUtil.ToJson(sceneDetails);
    }
}
