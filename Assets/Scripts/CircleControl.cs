using System.IO;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using Wave.OpenXR;
using TMPro;
using Wave.Essence.Eye;

public class LogMessage
{
    public string message;
}

public class CircleControl : MonoBehaviour
{
    public GameObject introP1;
    public GameObject introP2;
    public GameObject Crosshair;
    public GameObject circle_center;
    public GameObject circle_right;
    public GameObject circle_left;
    public GameObject StartWords;
    public GameObject EndWords;
    public TMP_Text BreakWords;

    public Material material1;
    public Material material2;

    int count = 1;


    //題數
    int quiztimes = 180;

    //題型
    int Q1 = 2;
    int Q2 = 2;
    int Q3 = 2;
    int Q4 = 2;
    int Qtype = 0;
    bool Roundoff = false;



    bool _gameStart = false;
    bool waitingforinput = false;

    public DataManager _dataManager; 
    public LogMessage _logMessage = new LogMessage();
    

    bool isGazeMid = false;

    private void Start()
    {

        introP1.SetActive(true);
    }

    private void Update()
    {

        if ((Input.anyKey || InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton)) && !_gameStart)
        //if (Input.anyKey &&!_gameStart)
        {
            introP1.SetActive(false);
            introP2.SetActive(true);
            
            _gameStart = true;
            StartCoroutine(ShowAndHideUI());
            
        }
        
    }
    

    private IEnumerator ShowAndHideUI()
    {
        //press right controller trigger to start

        //turn off intro view
        yield return StartCoroutine(WaitUntilInput());

        introP2.SetActive(false);

        PlayerPrefs.SetInt("GetData", 1);

        _logMessage.message = "practice start";
        _dataManager.SaveLogMessage(_logMessage);

        while (count <= (quiztimes + 8))
        {
            
            Roundoff = false;

            //first 8 round are traning
            if (count == 9)
            {
                //message for traning finished
                Q1 = Q2 = Q3 = Q4 = 5;
                _logMessage.message = "practice over";
                _dataManager.SaveLogMessage(_logMessage);
                StartWords.SetActive(true);
                yield return new WaitForSeconds(10);
                StartWords.SetActive(false);
                yield return new WaitForSeconds(2);
            }

            //output message: round detail

            _logMessage.message = "round" + (count - 8).ToString() + " start";
            _dataManager.SaveLogMessage(_logMessage);

            Qtype = Random.Range(0, 4);

            while(!Roundoff)
            {
                switch (Qtype)
                {
                    case 0:
                        if (Q1 > 0)
                        {
                            Q1--;
                            //Centor color
                            ColorDecision(0);

                            //Crosshair
                            yield return StartCoroutine(CrosshairShow());

                            //target show
                            TargetDecition(0);

                            Roundoff = true;
                        }
                        else
                        {
                            Qtype = (Qtype + 1) % 4;
                        }
                        break;

                    case 1:
                        if (Q2 > 0)
                        {
                            Q2--;
                            //Centor color
                            ColorDecision(0);

                            //Crosshair
                            yield return StartCoroutine(CrosshairShow());

                            //target show
                            TargetDecition(1);

                            Roundoff = true;
                        }
                        else
                        {
                            Qtype = (Qtype + 1) % 4;
                        }
                        break;


                    case 2:
                        if (Q3 > 0)
                        {
                            Q3--;
                            //Centor color
                            ColorDecision(1);

                            //Crosshair
                            yield return StartCoroutine(CrosshairShow());

                            //target show
                            TargetDecition(0);

                            Roundoff = true;
                        }
                        else
                        {
                            Qtype = (Qtype + 1) % 4;
                        }
                        break;


                    case 3:
                        if (Q4 > 0)
                        {
                            Q4--;
                            //Centor color
                            ColorDecision(1);

                            //Crosshair
                            yield return StartCoroutine(CrosshairShow());

                            //target show
                            TargetDecition(1);

                            Roundoff = true;
                        }
                        else
                        {
                            Qtype = (Qtype + 1) % 4;
                        }
                        break;
                }
            }


            yield return new WaitForSeconds(1.0f);

            circle_right.SetActive(false);
            circle_left.SetActive(false);
            _logMessage.message = "round" + (count - 8).ToString() + " over";
            _dataManager.SaveLogMessage(_logMessage);

            yield return new WaitForSeconds(1.0f);

            //next round
            count++;

            //quit
            if (count > quiztimes + 8)
            {
                EndWords.SetActive(true);
                PlayerPrefs.SetInt("GetData", 0);

                yield return new WaitForSeconds(2.0f);
                Application.Quit();

             }


            if (count > 9 && (count - 8 - 1) % 20 == 0)
            {
                //暫停收集
                PlayerPrefs.SetInt("GetData", 0);

                _logMessage.message = "break time";              
                _dataManager.SaveLogMessage(_logMessage);

                Q1 = Q2 = Q3 = Q4 = 5;

                yield return StartCoroutine(Take_A_Break());

                _logMessage.message = "restart";
                _dataManager.SaveLogMessage(_logMessage);
                //開始
                PlayerPrefs.SetInt("GetData", 1);
            }

        }
        //}
    }


    private void ColorDecision(int c)
    {
        if(c == 0)
        {
            circle_center.GetComponent<RawImage>().material = material1;
            _logMessage.message = "indicator color: purple";
            _dataManager.SaveLogMessage(_logMessage);

        }
        else
        {
            circle_center.GetComponent<RawImage>().material = material2;
            _logMessage.message = "indicator color: brown";
            _dataManager.SaveLogMessage(_logMessage);
        }

    }

    private void TargetDecition(int t)
    {
        if (t == 0)
        {
            circle_right.SetActive(true);
            _logMessage.message = "target showing: right";
            _dataManager.SaveLogMessage(_logMessage);



            if (circle_center.GetComponent<RawImage>().material == material1)
            {
                _logMessage.message = "round" + (count - 8).ToString() + " answer: right";
                _dataManager.SaveLogMessage(_logMessage);
            }
            else
            {
                _logMessage.message = "round" + (count - 8).ToString() + " answer: left";
                _dataManager.SaveLogMessage(_logMessage);
            }

        }
        else
        {
            circle_left.SetActive(true);
            _logMessage.message = "target showing: left";
            _dataManager.SaveLogMessage(_logMessage);

            //output to console

            if (circle_center.GetComponent<RawImage>().material == material1)
            {
                _logMessage.message = "round" + (count - 8).ToString() + " answer: left";
                _dataManager.SaveLogMessage(_logMessage);
            }
            else
            {
                _logMessage.message = "round" + (count - 8).ToString() + " answer: right";
                _dataManager.SaveLogMessage(_logMessage);
            }
        }
    }


    private IEnumerator CrosshairShow()
    {
        Crosshair.SetActive(true);
        _logMessage.message = "crosshair showing";
        _dataManager.SaveLogMessage(_logMessage);
        yield return new WaitForSeconds(0.5f);

        //確認視線回歸中心點
        yield return StartCoroutine(WaitGazeMid());

        Crosshair.SetActive(false);
        yield return new WaitForSeconds(0.2f);


        //center dot
        circle_center.SetActive(true);
        _logMessage.message = "indicator showing";
        _dataManager.SaveLogMessage(_logMessage);
        yield return new WaitForSeconds(1.0f);

        circle_center.SetActive(false);
        yield return new WaitForSeconds(0.2f);

    }


    //find the vector(0,0,1) of R/L sight
    private IEnumerator OriginFind()
    {
        while (count <= 10)
        {
            PlayerPrefs.SetInt("GetData", 1);

            //R_check
            circle_right.SetActive(true);
            _logMessage.message = "R_test";
            _dataManager.SaveLogMessage(_logMessage);
            //when active, dot move to get the user's R_origin



            yield return new WaitForSeconds(5.0f);


            //L_check
            circle_left.SetActive(true);
            _logMessage.message = "L_test";
            _dataManager.SaveLogMessage(_logMessage);
            //when active, dot move to get the user's L_origin




            yield return new WaitForSeconds(5.0f);
            

            PlayerPrefs.SetInt("GetData", 0);
            yield return null;
        }
    }

    private IEnumerator WaitGazeMid()
    {
        IsGazeMid();
        while (!isGazeMid)
        {
            yield return new WaitForSeconds(1f);
            IsGazeMid();
        }
        yield return null;
    }

    private void IsGazeMid()
    {
        FocusEyeData data = new FocusEyeData();
        EyeManager.Instance.GetLeftEyePupilPositionInSensorArea(out data.LeftEyePupilPositionInSensorArea);
        EyeManager.Instance.GetRightEyePupilPositionInSensorArea(out data.RightEyePupilPositionInSensorArea);
        if (data.LeftEyePupilPositionInSensorArea.x > 0.46 
            && data.LeftEyePupilPositionInSensorArea.x < 0.54
            && data.RightEyePupilPositionInSensorArea.x > 0.46
            && data.RightEyePupilPositionInSensorArea.x < 0.54)
        {
            isGazeMid = true;
        }
        else
        {
            isGazeMid = false;
        }
    }
    public void UpdateText(string newText)
    {
        // 更新文本
        BreakWords.text = newText;

    }

    private IEnumerator Take_A_Break()
    {

        BreakWords.text = "休息下\n已經完成" + (count - 8 - 1).ToString() + "(剩" + (quiztimes - count + 9).ToString() + "題)\n按鍵繼續";
        waitingforinput = true;


        //while (!Input.anyKey)
        while (!InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) || !Input.anyKey)
        {
            yield return null;
        }
        BreakWords.text = "";
        waitingforinput = false;

    }

    private IEnumerator WaitUntilInput()
    {
        yield return new WaitForSeconds(2.0f);
        while (!InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) || !Input.anyKey)
        //while (!Input.anyKey)
        {
            BreakWords.text = "";
            yield return null;
        }

    }

}
