/*
* FileName: PersonInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-08-13:42:21 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PersonInfoModel : BaseMVCModel
{
    protected PersonInfoService servicePersonInfo;

    public override void InitData()
    {
        servicePersonInfo = new PersonInfoService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<PersonInfoBean> GetAllPersonInfoData()
    {
        List<PersonInfoBean> listData = servicePersonInfo.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public PersonInfoBean GetPersonInfoData()
    {
        PersonInfoBean data = servicePersonInfo.QueryData();
        if (data == null)
            data = new PersonInfoBean();
        return data;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<PersonInfoBean> GetPersonInfoDataById(long id)
    {
        List<PersonInfoBean> listData = servicePersonInfo.QueryDataById(id);
        return listData;
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetPersonInfoData(PersonInfoBean data)
    {
        servicePersonInfo.UpdateData(data);
    }

}