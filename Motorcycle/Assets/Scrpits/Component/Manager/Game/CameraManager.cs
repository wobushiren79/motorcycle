using UnityEditor;
using UnityEngine;

public class CameraManager : BaseManager
{
   protected Camera _mainCamera;
   public Camera mainCamera  
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }

}