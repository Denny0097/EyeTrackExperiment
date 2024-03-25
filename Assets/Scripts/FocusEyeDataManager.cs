using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Essence.Eye;
using Wave.XR;
using LabFrame2023;


public class FocusEyeData: LabDataBase
{
    //Combined Eye:
    public Vector3 CombinedEyeOrigin;
    public Vector3 CombindedEyeDirectionNormalized;

    //Left Eye:
    //
    public Vector3 LeftEyeOrigin;
    //
    public Vector3 LeftEyeDirectionNormalized;
    //睜眼閉眼
    public float LeftEyeOpenness;
    //瞳孔直徑
    public float LeftEyePupilDiameter;
    //瞳孔位置
    public Vector2 LeftEyePupilPositionInSensorArea;

    //Right Eye:
    public Vector3 RightEyeOrigin;
    public Vector3 RightEyeDirectionNormalized;
    public float RightEyeOpenness;
    public float RightEyePupilDiameter;
    public Vector2 RightEyePupilPositionInSensorArea;

    public FocusEyeData()
    {
        CombinedEyeOrigin = new Vector3();
        CombindedEyeDirectionNormalized = new Vector3();
        LeftEyeOrigin = new Vector3();
        LeftEyeDirectionNormalized = new Vector3();
        LeftEyeOpenness = 0f;
        LeftEyePupilDiameter = 0f;
        LeftEyePupilPositionInSensorArea = new Vector2();
        RightEyeOrigin = new Vector3();
        RightEyeDirectionNormalized = new Vector3();
        RightEyeOpenness = 0f;
        RightEyePupilDiameter = 0f;
        RightEyePupilPositionInSensorArea = new Vector2();
    }
}

public class FocusEyeDataManager : MonoBehaviour
{
    FocusEyeData eyeData;

    public /*FocusEyeData*/ void GetEyeData()
    {
        FocusEyeData data = new FocusEyeData();
        bool _isCombinedEyeOriginAvailable = EyeManager.Instance.GetCombinedEyeOrigin(out data.CombinedEyeOrigin);
        bool _isCombindedEyeDirectionNormalizedAvailable = EyeManager.Instance.GetCombindedEyeDirectionNormalized(out data.CombindedEyeDirectionNormalized);
        bool _isLeftEyeOriginAvailable = EyeManager.Instance.GetLeftEyeOrigin(out data.LeftEyeOrigin);
        bool _isLeftEyeDirectionNormalizedAvailable = EyeManager.Instance.GetLeftEyeDirectionNormalized(out data.LeftEyeDirectionNormalized);
        bool _isLeftEyeOpennessAvailable = EyeManager.Instance.GetLeftEyeOpenness(out data.LeftEyeOpenness);
        bool _isLeftEyePupilDiameterAvailable = EyeManager.Instance.GetLeftEyePupilDiameter(out data.LeftEyePupilDiameter);
        bool _isLeftEyePupilPositionInSensorAreaAvailable = EyeManager.Instance.GetLeftEyePupilPositionInSensorArea(out data.LeftEyePupilPositionInSensorArea);
        bool _isRightEyeOriginAvailable = EyeManager.Instance.GetRightEyeOrigin(out data.RightEyeOrigin);
        bool _isRightEyeDirectionNormalizedAvailable = EyeManager.Instance.GetRightEyeDirectionNormalized(out data.RightEyeDirectionNormalized);
        bool _isRightEyeOpennessAvailable = EyeManager.Instance.GetRightEyeOpenness(out data.RightEyeOpenness);
        bool _isRightEyePupilDiameterAvailable = EyeManager.Instance.GetRightEyePupilDiameter(out data.RightEyePupilDiameter);
        bool _isRightEyePupilPositionInSensorAreaAvailable = EyeManager.Instance.GetRightEyePupilPositionInSensorArea(out data.RightEyePupilPositionInSensorArea);



        /*text.text = ($"CombinedEyeOrigin: {data.CombinedEyeOrigin}, " +
                        $"CombindedEyeDirectionNormalized: {data.CombindedEyeDirectionNormalized}, " +
                        $"LeftEyeOrigin: {data.LeftEyeOrigin}, " +
                        $"LeftEyeDirectionNormalized: {data.LeftEyeDirectionNormalized}, " +
                        $"LeftEyeOpenness: {data.LeftEyeOpenness}, " +
                        $"LeftEyePupilDiameter: {data.LeftEyePupilDiameter}, " +
                        $"LeftEyePupilPositionInSensorArea: {data.LeftEyePupilPositionInSensorArea}, " +
                        $"RightEyeOrigin: {data.RightEyeOrigin}, " +
                        $"RightEyeDirectionNormalized: {data.RightEyeDirectionNormalized}, " +
                        $"RightEyeOpenness: {data.RightEyeOpenness}, " +
                        $"RightEyePupilDiameter: {data.RightEyePupilDiameter}, " +
                        $"RightEyePupilPositionInSensorArea: {data.RightEyePupilPositionInSensorArea}");*/

        if (_isCombinedEyeOriginAvailable)
        {
            eyeData.CombinedEyeOrigin = data.CombinedEyeOrigin;
        }
        if (_isCombindedEyeDirectionNormalizedAvailable)
        {
            eyeData.CombindedEyeDirectionNormalized = data.CombindedEyeDirectionNormalized;
        }
        if (_isLeftEyeOriginAvailable)
        {
            eyeData.LeftEyeOrigin = data.LeftEyeOrigin;
        }
        if (_isLeftEyeDirectionNormalizedAvailable)
        {
            eyeData.LeftEyeDirectionNormalized = data.LeftEyeDirectionNormalized;
        }
        if (_isLeftEyeOpennessAvailable)
        {
            eyeData.LeftEyeOpenness = data.LeftEyeOpenness;
        }
        if (_isLeftEyePupilDiameterAvailable)
        {
            eyeData.LeftEyePupilDiameter = data.LeftEyePupilDiameter;
        }
        if (_isLeftEyePupilPositionInSensorAreaAvailable)
        {
            eyeData.LeftEyePupilPositionInSensorArea = data.LeftEyePupilPositionInSensorArea;
        }
        if (_isRightEyeOriginAvailable)
        {
            eyeData.RightEyeOrigin = data.RightEyeOrigin;
        }
        if (_isRightEyeDirectionNormalizedAvailable)
        {
            eyeData.RightEyeDirectionNormalized = data.RightEyeDirectionNormalized;
        }
        if (_isRightEyeOpennessAvailable)
        {
            eyeData.RightEyeOpenness = data.RightEyeOpenness;
        }
        if (_isRightEyePupilDiameterAvailable)
        {
            eyeData.RightEyePupilDiameter = data.RightEyePupilDiameter;
        }
        if (_isRightEyePupilPositionInSensorAreaAvailable)
        {
            eyeData.RightEyePupilPositionInSensorArea = data.RightEyePupilPositionInSensorArea;
        }

        if (PlayerPrefs.GetInt("GetData") == 1)
        {
            LabDataManager.Instance.WriteData(eyeData);
        }
        
        //return eyeData;
    }

    // Start is called before the first frame update
    void Start()
    {
        eyeData = new FocusEyeData();
        if (EyeManager.Instance != null)
        {
            EyeManager.Instance.EnableEyeTracking = true;
        }
        InvokeRepeating("GetEyeData", 0f, .008f); //120hz
    }

    // Update is called once per frame
    void Update()
    {

    }
}
