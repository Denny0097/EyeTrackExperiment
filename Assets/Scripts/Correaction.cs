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
    public TMP_Text breakword;

    public DataManager CorrData;
    public LogMessage _logMessage = new LogMessage();

    bool _CorreactionStart = false;
    int count = 1;
    float RowStep = 200;
    float ColStep = 200;
    float canvas_dist = 100;

    //控制模式：f:每兩秒換點 ＆ t:按左手按鈕換點
    bool ControlMode = false;

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

        //設定是否開啟ControlMode
        if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Left, CommonUsages.triggerButton)&&ControlMode == false)
        {
            ControlMode = true;
        }

        if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Left, CommonUsages.triggerButton))
        {
            ControlMode = true;
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
                    _logMessage.message = "dot position(" + RowStep.ToString() + ",0," + canvas_dist.ToString() + "), right";
                    CorrData.SaveLogMessage(_logMessage);
                    yield return null;
                    break;
                case 2:
                    target.anchoredPosition = new Vector3(-RowStep, 0, 0);
                    _logMessage.message = "dot position(-" + RowStep.ToString() + ",0," + canvas_dist.ToString() + "), left";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 3:
                    target.anchoredPosition = new Vector3(0, ColStep, 0);
                    _logMessage.message = "dot position(0," + ColStep.ToString() + "," + canvas_dist.ToString() + "), top";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 4:
                    target.anchoredPosition = new Vector3(0, -ColStep, 0);
                    _logMessage.message = "dot position(0,-" + ColStep.ToString() + "," + canvas_dist.ToString() + "), buttom";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 5:
                    target.anchoredPosition = new Vector3(RowStep, ColStep, 0);
                    _logMessage.message = "dot position(" + RowStep.ToString() + "," + ColStep.ToString() + "," + canvas_dist.ToString() + "), upper right";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 6:
                    target.anchoredPosition = new Vector3(RowStep, -ColStep, 0);
                    _logMessage.message = "dot position(" + RowStep.ToString() + ",-" + ColStep.ToString() + "," + canvas_dist.ToString() + "), lower right";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 7:
                    target.anchoredPosition = new Vector3(-RowStep, ColStep, 0);
                    _logMessage.message = "dot position(-" + RowStep.ToString() + "," + ColStep.ToString() + "," + canvas_dist.ToString() + "), upper left";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 8:
                    target.anchoredPosition = new Vector3(-RowStep, -ColStep, 0);
                    _logMessage.message = "dot position(-" + RowStep.ToString() + ",-" + ColStep.ToString() + "," + canvas_dist.ToString() + "), lower left";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 9:
                    target.anchoredPosition = new Vector3(0, 0, 0);
                    _logMessage.message = "dot position(0,0," + canvas_dist.ToString() + "), center";
                    CorrData.SaveLogMessage(_logMessage);

                    break;

                case 10:
                    target.anchoredPosition = new Vector3(100, 0, 0);
                    _logMessage.message = "dot position(100,0," + canvas_dist.ToString() + "), GameTarget_Right";
                    CorrData.SaveLogMessage(_logMessage);

                    break;
                case 11:
                    target.anchoredPosition = new Vector3(-100, 0, 0);
                    _logMessage.message = "dot position(-100,0," + canvas_dist.ToString() + "), GameTarget_Left";
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
        breakword.text = "矯正結束\n準備開始實驗說明\n等待期間請勿按任何按鍵";
        yield return new WaitForSeconds(4.0f);
        breakword.text = "";
        gamecontrol.SetActive(true);
        self.SetActive(false);


    }

}
