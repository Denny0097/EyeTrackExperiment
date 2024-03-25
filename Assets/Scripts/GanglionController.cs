using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabFrame2023;
using System.Threading;
using Newtonsoft.Json;

public class GanglionController : MonoBehaviour
{
//    private AndroidJavaClass unityClass;
//    private AndroidJavaObject unityActivity;
//    private AndroidJavaObject _pluginInstance;

//    /// <summary>
//    /// �ثe�O�_���P Ganglion �]�Ƴs�u
//    /// </summary>
//    public bool IsConnected { get; private set; } = false;
//    public bool IsUsingEEG { get; private set; } = false;
//    public bool IsUsingImpedance { get; private set; } = false;


//    protected Ganglion_EEGData _lastEegData;
//    protected Ganglion_ImpedanceData _currentImpedanceData;

//    public bool connectionStatus => IsConnected;

//    GanglionConfig _config;

//    protected bool _doWriteEegData = false;
//    protected bool _doWriteImpedanceData = false;

//    public string tag;

//#if UNITY_ANDROID
//    //private AndroidJavaObject _pluginInstance;
//#endif
//    Coroutine _checkConnectedCoroutine;


//    #region IManager
//    public void ManagerInit()
//    {
//#if UNITY_ANDROID
//        // Init
//        _config = LabTools.GetConfig<GanglionConfig>();
//        _currentImpedanceData = new Ganglion_ImpedanceData(5);

//        // Plugin
//        _pluginInstance = new AndroidJavaObject("com.xrlab.ganglion_plugin.PluginInstance");
//        if (_pluginInstance == null)
//            Debug.Log("Plugin Error");
//        _pluginInstance.CallStatic("receiveUnityActivity", AndroidHelper.CurrentActivity);


//        // Preferred Ganglion Name
//        if (!string.IsNullOrEmpty(_config.PreferredDeviceName))
//            _pluginInstance.Call("SetPreferredGanglionName", _config.PreferredDeviceName);

//        // Do connect!
//        // if(_config.AutoConnectOnInit)
//        //     Connect();

//        // Check Connected Coroutine
//#endif

//        StartCoroutine(GanglionConnnect());
//        _checkConnectedCoroutine = StartCoroutine(CheckConnected());
//    }

//    public IEnumerator ManagerDispose()
//    {
//        StopStreamData();
//        StopStreamImpedance();

//        StopCoroutine(_checkConnectedCoroutine);
//        _checkConnectedCoroutine = null;

//        try
//        {
//            Disconnect();
//        }
//        catch
//        {
//            Debug.LogError("[Ganglion] Disconnect failed");
//        }

//#if UNITY_ANDROID                
//        _pluginInstance.Dispose();
//        _pluginInstance = null;
//#endif        

//        // _config = null;
//        yield return 0;
//    }

//    #endregion


//    void Connect()
//    {
//        if (!IsConnected)
//        {
//#if UNITY_ANDROID
//            _pluginInstance.Call("Init");
//#endif
//        }
//        Debug.Log("[Ganglion] Start Connect...");
//    }

//    /// <summary>
//    /// �����ˬd�s�u���A�A�é��_�u�ɹ��խ��s�s�u
//    /// </summary>
//    IEnumerator CheckConnected()
//    {
//        bool lastIsConnected = false;
//        while (true)
//        {
//#if UNITY_ANDROID
//            IsConnected = _pluginInstance.Get<bool>("mConnected");
//            IsUsingEEG = _pluginInstance.Get<bool>("mUseEeg");
//            IsUsingImpedance = _pluginInstance.Get<bool>("mUseImpedance");
//#endif
//            if (!IsConnected && lastIsConnected)
//            {
//                Debug.LogWarning("���q�w�_�u�I\nGanglion connection lost!");
//            }
//            lastIsConnected = IsConnected;

//            yield return null;
//        }
//    }

//    void Disconnect()
//    {
//        if (IsConnected)
//        {
//#if UNITY_ANDROID
//            _pluginInstance.Call("Disconnect");
//#endif
//        }
//        Debug.Log("[Ganglion] Disconnected");
//    }

//    #region Android Plugin Callback            
//    /// <summary>
//    /// (DON'T CALL THIS MANULLY!!!) This will be called by the Android Plugin.
//    /// </summary>
//    /// <param name="json"></param>
//    public void ReceiveData(string json) // called by Android plugin
//    {
//        Debug.Log("receive");
//        string[] values = json.Split('|');
//        _lastEegData = new Ganglion_EEGData(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);
//        EEGDataClass temp1 = new EEGDataClass(_lastEegData.ch1_1.ToString(), _lastEegData.ch2_1.ToString(), _lastEegData.ch3_1.ToString(), _lastEegData.ch4_1.ToString(), tag);
//        EEGDataClass temp2 = new EEGDataClass(_lastEegData.ch1_2.ToString(), _lastEegData.ch1_2.ToString(), _lastEegData.ch1_2.ToString(), _lastEegData.ch1_2.ToString(), tag);

//        if (PlayerPrefs.GetInt("GetData") == 1)
//        {
//            SaveLocalFile.Instance.Save(temp1);
//            SaveLocalFile.Instance.Save(temp2);
//        }
//        // if(_doWriteEegData)
//        //     LabDataManager.Instance.WriteData(_lastEegData);        
//    }

//    /// <summary>
//    /// (DON'T CALL THIS MANULLY!!!) This will be called by the Android Plugin.
//    /// </summary>
//    /// <param name="json"></param> 
//    public void ReceiveImpedance(string json) // called by Android plugin
//    {
//        // "ch|imp_value"
//        string[] values = json.Split('|');
//        _currentImpedanceData.ImpedanceValues[int.Parse(values[0])] = int.Parse(values[1]);
//        // if(_doWriteEegData)
//        //     LabDataManager.Instance.WriteData(_currentImpedanceData);   
//    }
//    #endregion

//    #region Public Function    
//    //[Obsolete("�� SDK �ΡCUse InitGanglion instead")]
//    public void InitGanglion()
//    {
//        ManagerInit();
//    }
//    /// <summary>
//    /// �}�l�O�� EEG
//    /// </summary>
//    /// <param name="autoWriteLabData">�۰ʧ⦬�쪺�ƾڰe�� LabDataManager �x�s</param>
//    public void StreamData(bool autoWriteLabData = true)
//    {
//        Debug.Log("stream");
//        if (!IsConnected)
//        {
//            Debug.LogWarning("[Ganglion] Not connected!");
//            return;
//        }
//        _doWriteEegData = autoWriteLabData;
//#if UNITY_ANDROID
//        _pluginInstance.Call("StreamData");
//#endif
//    }
//    /// <summary>
//    /// ����O�� EEG
//    /// </summary>
//    public void StopStreamData()
//    {
//        _doWriteEegData = false;
//#if UNITY_ANDROID
//        if (IsUsingEEG)
//            _pluginInstance.Call("StopStreamData");
//#endif
//    }
//    /// <summary>
//    /// �}�l�O������
//    /// </summary>
//    /// <param name="autoWriteLabData">�۰ʧ⦬�쪺�ƾڰe�� LabDataManager �x�s</param>
//    public void StreamImpedance(bool autoWriteLabData = true)
//    {
//        if (!IsConnected)
//        {
//            Debug.LogWarning("[Ganglion] Not connected!");
//            return;
//        }
//        _doWriteImpedanceData = autoWriteLabData;
//#if UNITY_ANDROID
//        _pluginInstance.Call("StreamImpedance");
//#endif
//    }
//    /// <summary>
//    /// ����O������
//    /// </summary>
//    public void StopStreamImpedance()
//    {
//        _doWriteImpedanceData = false;
//#if UNITY_ANDROID
//        if (IsUsingImpedance)
//            _pluginInstance.Call("StopStreamImpedance");
//#endif
//    }

//    /// <summary>
//    /// ����̪�@�������q��ơA�i�ର null
//    /// </summary>
//    public Ganglion_EEGData GetEegData()
//    {
//        return _lastEegData;
//    }
//    public double GetEegData(int ch)
//    {
//        switch (ch)
//        {
//            case 1:
//                return _lastEegData.ch1_1;
//            case 2:
//                return _lastEegData.ch2_1;
//            case 3:
//                return _lastEegData.ch3_1;
//            case 4:
//                return _lastEegData.ch4_1;
//            default:
//                return -1;
//        }
//    }
//    /// <summary>
//    /// ����ثe�����ܸ��
//    /// </summary>
//    public Ganglion_ImpedanceData GetImpedanceData()
//    {
//        return _currentImpedanceData;
//    }
//    public double GetImpedanceData(int ch)
//    {
//        return _currentImpedanceData.ImpedanceValues[ch];
//    }

//    /// <summary>
//    /// ��ʶ}�l�s�u
//    /// (�ثe�L�k�b�_�u��A���s�u�A�q�Ъ`�N)
//    /// </summary>
//    public void ManualConnect()
//    {
//        Connect();
//    }

//    /// <summary>
//    /// ��ʶi���_�u
//    /// (�ثe�L�k�b�_�u��A���s�u�A�q�Ъ`�N)
//    /// </summary>
//    public void ManualDisconnect()
//    {
//        Disconnect();
//    }
//    #endregion

    void Start()
    {
        GanglionManager.Instance.ManagerInit();
        StartCoroutine(GanglionConnnect());
    }
    IEnumerator GanglionConnnect()
    {
        while (!GanglionManager.Instance.IsConnected)
        {
            Debug.Log("ganglion reconnect... ");
            GanglionManager.Instance.ManualConnect();
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Ganglion Connected");
    }
}
