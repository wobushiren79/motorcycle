using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PersonManager : BaseManager, IPersonInfoView
{
    public Dictionary<int, List<PersonInfoBean>> dicPersonInfo = new Dictionary<int, List<PersonInfoBean>>();
    public Dictionary<string, GameObject> dicPersonModel = new Dictionary<string, GameObject>();

    public List<Person> listPerson = new List<Person>();

    protected PersonInfoController personInfoController;

    public GameObject personContainer;

    private void Awake()
    {
        InitData();
    }

    public void InitData()
    {
        if (personContainer == null)
        {
            personContainer = new GameObject();
            personContainer.tag = TagInfo.Tag_PersonContainer;
            personContainer.name = TagInfo.Tag_PersonContainer;
            personContainer.transform.position = new Vector3(0, 0, 0);
            personContainer.transform.SetParent(transform);
        }

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

    public void AddPerson(Person person)
    {
        listPerson.Add(person);
    }

    public void RemovePerson(Person person)
    {
        listPerson.Remove(person);
    }

    public void ClearAllPerson()
    {
        for (int i = 0; i < listPerson.Count; i++)
        {
            Person person = listPerson[i];
            Destroy(person.gameObject);
        }
        listPerson.Clear();
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