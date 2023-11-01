using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Wave.OpenXR;
using TMPro;


public class Correaction : MonoBehaviour
{
   
    public RectTransform target;
    public GameObject instruction;
    public GameObject targetdot;
    public GameObject self;
    public GameObject gamecontrol;
    private float speed;

    public DataManager CorrData;
    public LogMessage _logMessage = new LogMessage();

    bool _CorreactionStart = false;
    int count = 1;
    float RowStep = 200;
    float ColStep = 100;
    // Start is called before the first frame update
    void Start()
    {
        instruction.SetActive(true);
        _logMessage.message = "order of presentation : 右 左 上 下 右上 右下 左上 左下 中";
        CorrData.SaveLogMessage(_logMessage);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = target.anchoredPosition;

        

        if ((Input.anyKey||InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton))&&!_CorreactionStart)
        {
            _CorreactionStart = true;
            instruction.SetActive(true);
            StartCoroutine(TargetShow(currentPosition));
        }

    }

    private IEnumerator TargetShow(Vector3 currentPosition)
    {
        instruction.SetActive(false);
        PlayerPrefs.SetInt("GetData", 1);

        while (count <= 11)
        {
            
            switch (count)
            {
                case 1:
                    _logMessage.message = "dot position(200,0,50), right";
                    CorrData.SaveLogMessage(_logMessage);
                    yield return null;
                    break;
                case 2:
                    target.anchoredPosition = new Vector3(-RowStep, 0, 0);
                    _logMessage.message = "dot position(-200,0,50), left";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 3:
                    target.anchoredPosition = new Vector3(0, ColStep, 0);
                    _logMessage.message = "dot position(0,100,50), top";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 4:
                    target.anchoredPosition = new Vector3(0, -ColStep, 0);
                    _logMessage.message = "dot position(0,-100,50), buttom";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 5:
                    target.anchoredPosition = new Vector3(RowStep, ColStep, 0);
                    _logMessage.message = "dot position(200,100,50), upper right";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 6:
                    target.anchoredPosition = new Vector3(RowStep, -ColStep, 0);
                    _logMessage.message = "dot position(200,-100,50), lower right";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 7:
                    target.anchoredPosition = new Vector3(-RowStep, ColStep, 0);
                    _logMessage.message = "dot position(-200,100,50), upper left";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 8:
                    target.anchoredPosition = new Vector3(-RowStep, -ColStep, 0);
                    _logMessage.message = "dot position(-200,-100,50), lower left";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 9:
                    target.anchoredPosition = new Vector3(0, 0, 0);
                    _logMessage.message = "dot position(0,0,50), center";
                    CorrData.SaveLogMessage(_logMessage);

                    break;

                case 10:
                    target.anchoredPosition = new Vector3(150, 0, 0);
                    _logMessage.message = "dot position(150,0,50), GameTarget_Right";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 11:
                    target.anchoredPosition = new Vector3(-150, 0, 0);
                    _logMessage.message = "dot position(-100,0,50), GameTarget_Left";
                    CorrData.SaveLogMessage(_logMessage);

                    break;


            }


            count++;

            targetdot.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            targetdot.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }

        targetdot.SetActive(false);
        PlayerPrefs.SetInt("GetData", 0);
        gamecontrol.SetActive(true);
        self.SetActive(false);


    }

}
