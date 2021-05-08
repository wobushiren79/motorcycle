using UnityEditor;
using UnityEngine;
using DG.Tweening;
public class PersonHandler : BaseHandler<PersonHandler, PersonManager>
{

    public void SetPersonNumber(GameObject objParent, int totalNumber)
    {
        PersonInfoBean personInfo = manager.GetRandomPersonInfoByNumber(totalNumber);
        SetPersonNumber(objParent, personInfo);
    }

    public void SetPersonNumber(GameObject objParent, PersonInfoBean personInfo)
    {
        PersonDetailsBean personDetails = personInfo.GetPersonDetilas();
        Transform tfPersonContainer = objParent.transform.Find(TagInfo.Tag_PersonContainer);
        if (tfPersonContainer == null)
        {
            GameObject obj = new GameObject();
            obj.tag = TagInfo.Tag_PersonContainer;
            obj.name = TagInfo.Tag_PersonContainer;
            obj.transform.position = new Vector3(0, 0, 0);
            obj.transform.SetParent(objParent.transform);
            tfPersonContainer = obj.transform;
        }
        if (tfPersonContainer.childCount < personInfo.person_number)
        {
            int offsetNumber = personInfo.person_number - tfPersonContainer.childCount;
            for (int i = 0; i < offsetNumber; i++)
            {
                GameObject objPerson = manager.GetPersonModel("Person");
                Instantiate(tfPersonContainer.gameObject, objPerson);
            }
        }
        for (int i = 0; i < tfPersonContainer.childCount; i++)
        {
            Transform tfChild = tfPersonContainer.transform.GetChild(i);
            if (i >= personInfo.person_number)
            {
#if UNITY_EDITOR
                DestroyImmediate(tfChild.gameObject);
#else
                Destroy(tfChild.gameObject);
#endif
            }
            else
            {
                PersonDetailsItemBean personDetailsItem = personDetails.listPersonData[i];

                if (Application.isPlaying)
                {
                    tfChild.DOLocalMove(personDetailsItem.position.GetVector3(), 0.5f);
                    tfChild.DOScale(personDetailsItem.size.GetVector3(), 0.5f);
                    tfChild.DOLocalRotate(personDetailsItem.angle.GetVector3(), 0.5f);
                }
                else
                {
                    tfChild.localPosition = personDetailsItem.position.GetVector3();
                    tfChild.localScale = personDetailsItem.size.GetVector3();
                    tfChild.localEulerAngles = personDetailsItem.angle.GetVector3();
                }

                Transform[] listPartTF = tfChild.GetComponentsInChildren<Transform>();
                for (int f = 0; f < listPartTF.Length; f++)
                {
                    Transform tfItemPart = listPartTF[f];
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