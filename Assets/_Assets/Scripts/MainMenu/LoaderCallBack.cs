using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate;

    private void Update()
    {
        isFirstUpdate = false;
        Loader.LoaderCallBack();
    }

}

