using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircleControl : MonoBehaviour
{
    public GameObject Crosshair = GameObject.Find("Crosshair");
    public GameObject circle_center = GameObject.Find("circle_center");
    public GameObject circle_right = GameObject.Find("circle_right");
    public GameObject circle_left = GameObject.Find("circle_left");
    public GameObject StartWords = GameObject.Find("canvas");

    public Material material1;
    public Material material2;
    int count = 1;

    private void Start()
    {

        circle_center.GetComponent<Renderer>().material = material1;
        StartCoroutine(ShowAndHideUI());
        

    }

    private IEnumerator ShowAndHideUI()
    {
        while (count <= 188) 
        {
            if (count == 8)
            {
                StartWords.SetActive(true);
                yield return new WaitForSeconds(10f);
                StartWords.SetActive(false);
                yield return new WaitForSeconds(2.0f);
            }
            if (Random.Range(0, 2) == 0)
            {
                circle_center.GetComponent<Renderer>().material = material1;
            }
            else
            {
                circle_center.GetComponent<Renderer>().material = material2;
            }

            Crosshair.SetActive(true);
            yield return new WaitForSeconds(1.0f);

            
            Crosshair.SetActive(false);
            yield return new WaitForSeconds(0.2f);

            circle_center.SetActive(true);
            yield return new WaitForSeconds(1.0f);

            circle_center.SetActive(false);
            yield return new WaitForSeconds(0.2f);

           
            if (Random.Range(0, 2) == 0)
            {
                Debug.Log("Displaying rightObject");
                circle_right.SetActive(true);
                
            }
            else
            {
                Debug.Log("Displaying leftObject");
                circle_left.SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);

            circle_right.SetActive(false);
            circle_left.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            count++;
        }
    }
}
