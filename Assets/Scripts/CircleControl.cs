using System.IO;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CircleControl : MonoBehaviour
{
    public GameObject intro;
    public GameObject Crosshair;
    public GameObject circle_center;
    public GameObject circle_right;
    public GameObject circle_left;
    public GameObject StartWords;


    public Material material1;
    public Material material2;

    int count = 1;

    private string RoundSetPath;
    private string RoundSet;

    
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
        RoundSetPath = $"{Application.dataPath}/Output.csv";

        //set init material
        //circle_center.GetComponent<Renderer>().material = material1;

        /*if (circle_center != null)
        {
            circle_center.GetComponent<Renderer>().material = material1;
        }*/
        StartCoroutine(ShowAndHideUI());
        

    }

    private IEnumerator ShowAndHideUI()
    {
        //AnyKey to start
        if (Input.anyKeyDown)
        {
            //turn off intro view
            intro.SetActive(false);

            StreamWriter streamWriter = File.CreateText(RoundSetPath);
            while (count <= 188)
            {
                //output message: round detail
                RoundSet = $"round {count}:\n";

                //first 8 round are traning
                if (count == 8)
                {
                    //message for traning finished 
                    StartWords.SetActive(true);
                    yield return new WaitForSeconds(10f);
                    StartWords.SetActive(false);
                    yield return new WaitForSeconds(2.0f);

                }
                //color choose
                if (Random.Range(0, 2) == 0)
                {
                    circle_center.GetComponent<Renderer>().material = material1;
                    RoundSet += "color :purple ,";
                }
                else
                {
                    circle_center.GetComponent<Renderer>().material = material2;
                    RoundSet += "color :brown ,";
                }

                //crosshair
                Crosshair.SetActive(true);
                yield return new WaitForSeconds(1.0f);

                Crosshair.SetActive(false);
                yield return new WaitForSeconds(0.2f);

                //center dot
                circle_center.SetActive(true);
                yield return new WaitForSeconds(1.0f);

                circle_center.SetActive(false);
                yield return new WaitForSeconds(0.2f);

                //////////////////
                //!! something is wrong !!
                //////////////////
                //R or L dot :position choose
                if (Random.Range(0, 2) == 0)
                {
                    Debug.Log("Displaying rightObject");
                    circle_right.SetActive(true);
                    RoundSet += "position :right\n";

                    //output to console
                    Debug.Log(RoundSet);

                    if (circle_center.GetComponent<Renderer>().material == material1)
                        Debug.Log("should RIGHT\n");
                    else
                        Debug.Log("should LEFT\n");
                }
                else
                {
                    Debug.Log("Displaying leftObject");
                    circle_left.SetActive(true);
                    RoundSet += "position :left\n";

                    //output to console
                    Debug.Log(RoundSet);

                    if (circle_center.GetComponent<Renderer>().material == material1)
                        Debug.Log("should LEFT\n");
                    else
                        Debug.Log("should RIGHT\n");
                }

                //message save
                streamWriter.WriteLine(RoundSet);



                yield return new WaitForSeconds(1.0f);

                circle_right.SetActive(false);
                circle_left.SetActive(false);

                yield return new WaitForSeconds(1.0f);

                //next round
                count++;

            }
            streamWriter.Close();
        }
    }
}
