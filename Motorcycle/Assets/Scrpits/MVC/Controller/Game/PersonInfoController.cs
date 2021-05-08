/*
* FileName: PersonInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-08-13:42:21 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PersonInfoController : BaseMVCController<PersonInfoModel, IPersonInfoView>
{

    public PersonInfoController(BaseMonoBehaviour content, IPersonInfoView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public PersonInfoBean GetPersonInfoData(Action<PersonInfoBean> action)
    {
        PersonInfoBean data = GetModel().GetPersonInfoData();
        if (data == null) {
            GetView().GetPersonInfoFail("没有数据",null);
            return null;
        }
        GetView().GetPersonInfoSuccess<PersonInfoBean>(data,action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllPersonInfoData(Action<List<PersonInfoBean>> action)
    {
        List<PersonInfoBean> listData = GetModel().GetAllPersonInfoData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetPersonInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetPersonInfoSuccess<List<PersonInfoBean>>(listData, action);
        }
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="action"></param>
    public void GetPersonInfoDataById(long id,Action<PersonInfoBean> action)
    {
        List<PersonInfoBean> listData = GetModel().GetPersonInfoDataById(id);
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetPersonInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetPersonInfoSuccess(listData[0], action);
        }
    }
} 