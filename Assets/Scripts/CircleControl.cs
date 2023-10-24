using System.IO;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using Wave.OpenXR;

public class LogMessage
{
    public string message;
}

public class CircleControl : MonoBehaviour
{
    public GameObject intro;
    public GameObject Crosshair;
    public GameObject circle_center;
    public GameObject circle_right;
    public GameObject circle_left;
    public GameObject StartWords;
    public GameObject EndWords;


    public Material material1;
    public Material material2;

    int count = 1;

    private string RoundSetPath;
    private string RoundSet;

    bool _gameStart = false;

    public DataManager _dataManager; 
    public LogMessage _logMessage = new LogMessage();
    public LogMessage _crossMessage = new LogMessage();
    public LogMessage _dotMessage = new LogMessage();
    public LogMessage _LRMessage = new LogMessage();



    private void Start()
    {
        /*  I dont know how to set
        intro = Gamebject.Find("InstroctionCanvas");
        Crosshair = GameObject.Find("Crosshair");
        circle_center = GameObject.Find("circle_center");
        circle_right = GameObject.Find("circle_right");
        circle_left = GameObject.Find("circle_left");
        StartWords = GameObject.Find("Canvas");

        //Material load
        material1 = Resources.Load<Material>("Materials/purple");
        material2 = Resources.Load<Material>("Materials/brown");
        */


        //Round set output path
        //RoundSetPath = $"{Application.dataPath}/Output.csv";


        //set init material
        //circle_center.GetComponent<Renderer>().material = material1;

        /*if (circle_center != null)
        {
            circle_center.GetComponent<Renderer>().material = material1;
        }*/
        //StartCoroutine(ShowAndHideUI());
        

    }

    private void Update()
    {
        if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) && !_gameStart)
        {
            _gameStart = true;
            StartCoroutine(ShowAndHideUI());
        }
    }

    private IEnumerator ShowAndHideUI()
    {
        //press right controller trigger to start
        //if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton))
        //{
            PlayerPrefs.SetInt("GetData", 1);
            //turn off intro view
            intro.SetActive(false);

        //StreamWriter streamWriter = File.CreateText(RoundSetPath);
        while (count <= 188)
            {
            //first 8 round are traning
            if (count == 9)
            {
                //message for traning finished 
                _logMessage.message = "practice over";
                _dataManager.SaveLogMessage(_logMessage);
                StartWords.SetActive(true);
                yield return new WaitForSeconds(10);
                StartWords.SetActive(false);
                yield return new WaitForSeconds(2);
            }

            //output message: round detail
            
            _logMessage.message = "round" + count.ToString() + " start";
            _dataManager.SaveLogMessage(_logMessage);

            
            
            //color choose
            if (Random.Range(0, 2) == 0)
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

            //crosshair
            //!!!!

            //測試setactive的跟show string的時間
            //save可以往後拉 避免多餘時間cost
            //_logMessage.message = "test";

            Crosshair.SetActive(true);
            _logMessage.message = "crosshair showing";

            _dataManager.SaveLogMessage(_logMessage);

            yield return new WaitForSeconds(1);

            Crosshair.SetActive(false);
            _logMessage.message = "crosshair hiding";
            _dataManager.SaveLogMessage(_logMessage);
            yield return new WaitForSeconds(0.2f);

            //center dot
            circle_center.SetActive(true);
            _logMessage.message = "indicator showing";
            _dataManager.SaveLogMessage(_logMessage);
            yield return new WaitForSeconds(1.0f);

            circle_center.SetActive(false);
            _logMessage.message = "indicator hiding";
            _dataManager.SaveLogMessage(_logMessage);
            yield return new WaitForSeconds(0.2f);

         
            //R or L dot :position choose
            if (Random.Range(0, 2) == 0)
            {
                circle_right.SetActive(true);
                _logMessage.message = "target showing: right";
                _dataManager.SaveLogMessage(_logMessage);



                if (circle_center.GetComponent<RawImage>().material == material1)
                {
                    _logMessage.message = "answer: right";
                    _dataManager.SaveLogMessage(_logMessage);
                }
                else
                {
                    _logMessage.message = "answer: left";
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
                    _logMessage.message = "answer: left";
                    _dataManager.SaveLogMessage(_logMessage);
                }
                else
                {
                    _logMessage.message = "answer: right";
                    _dataManager.SaveLogMessage(_logMessage);
                }
            }

                //message save
                //streamWriter.WriteLine(RoundSet);



            yield return new WaitForSeconds(1.0f);

            circle_right.SetActive(false);
            circle_left.SetActive(false);
            _logMessage.message = "target hiding, round " + count.ToString() + " over";
            _dataManager.SaveLogMessage(_logMessage);

            yield return new WaitForSeconds(1.0f);

            //next round
            count++;

        }
        //streamWriter.Close();
        if(count == 189)
        {
            PlayerPrefs.SetInt("GetData", 0);
            EndWords.SetActive(true);
        }
                
        //}
    }
}
