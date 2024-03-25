using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using LabFrame2023;


//namespace Lab.Tool.Concussion
//{
public class SaveLocalFile : MonoBehaviour
{
    public string time = "";
    public string timeEEGFile = "";
    public string Directory_path = "";

    public string UserId = "";
    public string MotiondataId = "";

    //新增ID
    public static int flag_ID = 0;

    public enum taskEnum
    {
        ssvep,
        eyeTask1,
        eyeTask2,
        Null
    }
    /// <summary>
    /// 爛做法 writer如果能套builder pattern?
    /// 若增加 要一同更改下面save over
    /// </summary>
    //public StreamWriter writer = null, writer2 = null, writer3 = null;// writer 1 :eeg, writer 2 :text, writer 3 :eye tracking

    public void renewTimeEEGFile()
    {
        timeEEGFile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    }
    public taskEnum taskStatus = taskEnum.Null;

    private static SaveLocalFile s_Instance;
    public static SaveLocalFile Instance
    {
        get
        {
            if (s_Instance != null)
            {
                //Debug.Log("DEBUG: PrefabSingleton [case1]: returning loaded singleton");
                return s_Instance;      // Early return the created instance 
            }

            // Find the active singleton already created
            // reference: https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
            s_Instance = FindObjectOfType<SaveLocalFile>();       // note: this is use during the Awake() logic
            if (s_Instance != null)
            {
                //Debug.Log("DEBUG: PrefabSingleton [case2]: Found the active object in memory");
                return s_Instance;
            }

            CreateDefault();     // create new game object 

            return s_Instance;
        }
    }

    static void CreateDefault()
    {
        SaveLocalFile prefab = Resources.Load<SaveLocalFile>("Prefab/SaveLocalFile");
        s_Instance = Instantiate(prefab);       // No need to care about the position, rotation, ...
        s_Instance.gameObject.name = "PrefabSingleton";
        SaveLocalFile.Instance.time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        SaveLocalFile.Instance.Directory_path = DateTime.Now.ToString("MMddHHmm");
        flag_ID = 0;
    }

    public static DirectoryInfo SafeCreateDirectory(string path)
    {
        //Generate if you don't check if the directory exists
        if (Directory.Exists(path))
        {
            return null;
        }
        return Directory.CreateDirectory(path);
    }
    //debug use
    public bool isAlive()
    {
        if (s_Instance != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //public void Save(string Directory_path, string data)
    //{
    //    //Data storage
    //    SafeCreateDirectory(Application.persistentDataPath + "/" + Directory_path);
    //    string json = JsonUtility.ToJson(data);
    //    var writer = new StreamWriter(Application.persistentDataPath + "/" + Directory_path + "/date.json");
    //    writer.Write(json);
    //    writer.Flush();
    //    writer.Close();
    //}
    //public void Save(EEGDataClass data)
    //{
    //    //新增ID
    //    if (flag_ID == 0)
    //    {
    //        Directory_path = Directory_path /*+ socketIO.checkId*/;
    //        flag_ID = 1;
    //    }

    //    if (timeEEGFile == "")
    //    {
    //        timeEEGFile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    //    }
    //    //string filename = "concussionpico_vrca_" + Directory_path + "_" + taskStatus.ToString() + "_eeg.json";
    //    string filename = "HTC_" + Directory_path + "_" + MotiondataId + "_" + UserId + "_eeg.json";
    //    //Debug.Log(filename);
    //    //Data storage
    //    //SafeCreateDirectory("/storage/emulated/0/LabData/concussionPico/ForSend" + "/" + Directory_path);
    //    SafeCreateDirectory("/storage/emulated/0/LabData/EyeTrackExperiment/ForSend" + "/" + time);
    //    SafeCreateDirectory("/storage/emulated/0/LabData/EyeTrackExperiment/ForStore" + "/" + time);
    //    //Debug.Log(data.ch1);
    //    //Debug.Log(data.ch2);
    //    //Debug.Log(data.ch3);
    //    //Debug.Log(data.ch4);
    //    //Debug.Log(data.timeStamp);
    //    string jsonData = JsonUtility.ToJson(data);
    //    Debug.Log(jsonData);
    //    using (StreamWriter writer = new StreamWriter("/storage/emulated/0/LabData/EyeTrackExperiment/ForSend" + "/" + time + "/" + filename, append: true))
    //    {
    //        writer.Write(jsonData);
    //        writer.Write("\r\n");
    //        writer.Flush();
    //    }
    //    //File.WriteAllText(Application.persistentDataPath + "/" + Directory_path + "/" + filename, jsonData);
    //}

    public void Save(FocusEyeData data)
    {
        if (time == "")
        {
            time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        }
        string filename = "";
        switch (taskStatus)

        {
            case taskEnum.eyeTask1:
                //Debug.Log(taskStatus.ToString());
                //filename = "concussionpico_vrca_" + Directory_path + "_eye_eyeTask1.json";
                break;
            case taskEnum.eyeTask2:
                //filename = "concussionpico_vrca_" + Directory_path + "_eye_eyeTask2.json";
                //Debug.Log(taskStatus.ToString());
                break;
            default:
                filename = "HTC_" + Directory_path + "_" + MotiondataId + "_" + UserId + "_eye.json";
                break;
        }

        //Debug.Log(filename);
        SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D/ForSend" + "/" + time);
        SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D/ForStore" + "/" + time);
        string jsonData = JsonUtility.ToJson(data);
        using (StreamWriter writer = new StreamWriter("/storage/emulated/0/LabData/Eyetrack3D/ForSend" + "/" + time + "/" + filename, append: true))
        {
            writer.Write(jsonData);
            writer.Write("\r\n");
        }
    }

    public void Save(LabGarminData data)
    {
        //新增ID
        if (flag_ID == 0)
        {
            Directory_path = Directory_path /*+ socketIO.checkId*/;
            flag_ID = 1;
        }

        string filename = "Eyetrack3D_" + Directory_path + "_" + MotiondataId + "_" + UserId + "_garmin.json";
        //Data storage
        //SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D/ForSend" + "/" + time);
        //SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D/ForSend" + "/" + time);
        string jsonData = JsonUtility.ToJson(data);
        using (StreamWriter writer = new StreamWriter("/storage/emulated/0/LabData/Eyetrack3D/ForSend" /*+ "/" + time */ + Correaction.foldername + "/" + filename, append: true))
        {
            writer.Write(jsonData);
            writer.Write("\r\n");
        }
    }

    public void Save(LogMessage data)
    {
        //新增ID
        if (flag_ID == 0)
        {
            Directory_path = Directory_path /*+ socketIO.checkId*/;
            flag_ID = 1;
        }

        string filename = "HTC_" + Directory_path + "_" + MotiondataId + "_" + UserId + "_tag.json";
        //Data storage
        SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D/ForSend" + "/" + time);
        SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D/ForSend" + "/" + time);
        string jsonData = JsonUtility.ToJson(data);
        using (StreamWriter writer = new StreamWriter("/storage/emulated/0/LabData/Eyetrack3D/ForSend" + "/" + time + "/" + filename, append: true))
        {
            writer.Write(jsonData);
            writer.Write("\r\n");
        }
    }

    //public void Save(string _time)
    //{
    //    if (time == "")
    //    {
    //        time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    //    }
    //    //string filename = "concussionpico_vrca_" + Directory_path + "_targetTime.json";
    //    string filename = "drugtreatment_" + Directory_path + "_targetTime.json";
    //    //Debug.Log(filename);
    //    //Data storage
    //    //SafeCreateDirectory("/storage/emulated/0/LabData/concussionPico/ForSend" + "/" + Directory_path);
    //    SafeCreateDirectory("/storage/emulated/0/LabData/DrugTreatment/ForSend" + "/" + Directory_path);

    //    //string jsonData = JsonUtility.ToJson(data);
    //    //Debug.Log(jsonData);
    //    if (writer2 == null)
    //    {
    //        //writer2 = new StreamWriter("/storage/emulated/0/LabData/concussionPico/ForSend" + "/" + Directory_path + "/" + filename, append: true);
    //        writer2 = new StreamWriter("/storage/emulated/0/LabData/DrugTreatment/ForSend" + "/" + Directory_path + "/" + filename, append: true);
    //    }

    //    writer2.Write(_time);
    //    writer2.Write("\r\n");
    //    writer2.Flush();
    //    writer2.Close();
    //    writer2 = null;

    //    //File.WriteAllText(Application.persistentDataPath + "/" + Directory_path + "/" + filename, jsonData);
    //}
    void Awake()
    {
        // Debug.Log("DEBUG: PrefabSingleton: Awake() begin. " + InfoGameObject());
        if (Instance != this)
        {
            // Debug.Log("DEBUG: PrefabSingleton: will destroy the extra gameObject " + InfoGameObject());
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);      // note: s_Instance become null when switch scene if not place this line of code
    }
    void Start()
    {
        Debug.Log("DEBUG: PrefabSingleton: Start() begin. " /*+ InfoGameObject()*/);
        SaveLocalFile.Instance.Directory_path = "Eyetrack3D"; //遊戲名稱
        Debug.Log("ID：" + SaveLocalFile.Instance.Directory_path);
        SaveLocalFile.Instance.time = DateTime.Now.ToString("MMddHHmm");
        SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D" + "/ForSend");
        SafeCreateDirectory("/storage/emulated/0/LabData/Eyetrack3D" + "/ForStore");
    }
    private void Update()
    {
        //Debug.Log("ID：" + socketIO.checkId);

        //Debug.Log("singleton object:" + isAlive());
    }
    /// <summary>
    /// call this after finish write file
    /// 爛做法 writer如果能套builder pattern?
    /// </summary>
    /// <param name="_num">1:eeg, 2:text, 3:eye tracking</param>
    /// <returns>void</returns>
    //public void saveOver(int _num)
    //{
    //    switch (_num)
    //    {
    //        case 1:
    //            writer.Flush();
    //            writer.Close();
    //            writer = null;
    //            break;
    //        case 2:
    //            writer2.Flush();
    //            writer2.Close();
    //            writer2 = null;
    //            break;
    //        case 3:
    //            writer3.Flush();
    //            writer3.Close();
    //            writer3 = null;
    //            break;
    //        default:
    //            break;
    //    }


    //}

}
//}

