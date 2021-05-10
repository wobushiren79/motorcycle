using UnityEditor;
using UnityEngine;
using DG.Tweening;
public class PersonHandler : BaseHandler<PersonHandler, PersonManager>
{

    public void SetPersonNumber(int totalNumber)
    {
        PersonInfoBean personInfo = manager.GetRandomPersonInfoByNumber(totalNumber);
        SetPersonNumber(personInfo);
    }

    public void SetPersonNumber(PersonInfoBean personInfo)
    {
        PersonDetailsBean personDetails = personInfo.GetPersonDetilas();
        if (manager.personContainer.transform.childCount < personInfo.person_number)
        {
            int offsetNumber = personInfo.person_number - manager.personContainer.transform.childCount;
            for (int i = 0; i < offsetNumber; i++)
            {
                GameObject objPersonModel = manager.GetPersonModel("Person");
                GameObject objPerson= Instantiate(manager.personContainer, objPersonModel);
            }
        }
        for (int i = 0; i < manager.personContainer.transform.childCount; i++)
        {
            Transform tfChild = manager.personContainer.transform.GetChild(i);
            if (i >= personInfo.person_number)
            {
#if UNITY_EDITOR
                DestroyImmediate(tfChild.gameObject);
#else
                Destroy(tfChild.gameObject);
#endif
                i--;
            }
            else
            {
                PersonDetailsItemBean personDetailsItem = personDetails.listPersonData[i];
                Person person = CptUtil.AddCpt<Person>(tfChild.gameObject,out bool isNew);
                if (isNew)
                {
                    manager.AddPerson(person);
                }
                person.SetData(personDetailsItem.position.GetVector3(), personDetailsItem.bufferTime, isNew);
                if (Application.isPlaying)
                {
                    //tfChild.DOLocalMove(personDetailsItem.position.GetVector3(), 0.5f);
                    tfChild.DOScale(personDetailsItem.size.GetVector3(), 0.5f);
                    tfChild.DOLocalRotate(personDetailsItem.angle.GetVector3(), 0.5f);
                }
                else
                {
                    //tfChild.localPosition = personDetailsItem.position.GetVector3();
                    tfChild.localScale = personDetailsItem.size.GetVector3();
                    tfChild.localEulerAngles = personDetailsItem.angle.GetVector3();
                }

                Transform[] listPartTF = tfChild.GetComponentsInChildren<Transform>();
                for (int f = 0; f < listPartTF.Length; f++)
                {
                    Transform tfItemPart = listPartTF[f];
                    if (tfItemPart == tfChild)
                    {
                        continue;
                    }
                    for (int p = 0; p < personDetailsItem.listPartData.Count; p++)
                    {
                        PersonDetailsItemPartBean personDetailsItemPart = personDetailsItem.listPartData[p];
                        if (tfItemPart.name.Equals(personDetailsItemPart.partName))
                        {
                            if (Application.isPlaying)
                            {
                                tfItemPart.DOLocalMove(personDetailsItemPart.position.GetVector3(), 0.5f);
                                tfItemPart.DOScale(personDetailsItemPart.size.GetVector3(), 0.5f);
                                tfItemPart.DOLocalRotate(personDetailsItemPart.angle.GetVector3(), 0.5f);
                            }
                            else
                            {
                                tfItemPart.localPosition = personDetailsItemPart.position.GetVector3();
                                tfItemPart.localScale = personDetailsItemPart.size.GetVector3();
                                tfItemPart.localEulerAngles = personDetailsItemPart.angle.GetVector3();
                            }

                            break;
                        }
                    }
                }
            }
        }
    }

}