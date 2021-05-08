using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PersonManager : BaseManager, IPersonInfoView
{
    public Dictionary<int, List<PersonInfoBean>> dicPersonInfo = new Dictionary<int, List<PersonInfoBean>>();
    public Dictionary<string, GameObject> dicPersonModel = new Dictionary<string, GameObject>();

    public List<GameObject> listPerson = new List<GameObject>();

    protected PersonInfoController personInfoController;

    private void Awake()
    {
        InitData();
    }

    public void InitData()
    {
        personInfoController = new PersonInfoController(this, this);
        personInfoController.GetAllPersonInfoData(InitPersonInfo);
    }

    public void InitPersonInfo(List<PersonInfoBean> listData)
    {
        dicPersonInfo.Clear();
        for (int i = 0; i < listData.Count; i++)
        {
            PersonInfoBean itemInfo = listData[i];
            if (dicPersonInfo.TryGetValue(itemInfo.person_number, out List<PersonInfoBean> listPersonInfo))
            {
                listPersonInfo.Add(itemInfo);
                dicPersonInfo[itemInfo.person_number] = listPersonInfo;
            }
            else
            {
                dicPersonInfo.Add(itemInfo.person_number, new List<PersonInfoBean>() { itemInfo });
            }
        }
    }

    public PersonInfoBean GetRandomPersonInfoByNumber(int number)
    {
        if (dicPersonInfo.TryGetValue(number, out List<PersonInfoBean> listPersonInfo))
        {
            return RandomUtil.GetRandomDataByList(listPersonInfo);
        }
        else
        {
            return null;
        }
    }

    public PersonInfoBean GetPersonInfoById(long id)
    {
        foreach (var itemValue in dicPersonInfo.Values)
        {
            for (int i = 0; i < itemValue.Count; i++)
            {
                PersonInfoBean personInfo = itemValue[i];
                if (personInfo.id == id)
                {
                    return personInfo;
                }
            }
        }
        return null;
    }

    public GameObject GetPersonModel(string personName)
    {
        return GetModel(dicPersonModel, "game/person", personName, "Assets/Prefabs/Game/" + personName + ".prefab");
    }

    public void GetPersonInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    public void GetPersonInfoFail(string failMsg, Action action)
    {
    }
}