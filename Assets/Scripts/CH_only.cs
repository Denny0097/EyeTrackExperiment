using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Wave.OpenXR;
using TMPro;

public class CH_only : MonoBehaviour
{
    public GameObject Crosshair;
    public GameObject NormalMode;
    public GameObject Sec_Show;
    int count;

    //0:normal 1:CH_Only
    private bool ModeChoose = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //等5s,如果輸入b則進入crosshair only mode，反之如果輸入a或5秒內沒有輸入，則normal
        if ((Input.GetKey(KeyCode.B) || InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Left, CommonUsages.triggerButton)))
        {
            NormalMode.SetActive(false);
            Crosshair.SetActive(true);
        }

    }
}
