using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate;

    private void Update()
    {
        isFirstUpdate = false;
        Loader.LoaderCallBack();
    }

}

