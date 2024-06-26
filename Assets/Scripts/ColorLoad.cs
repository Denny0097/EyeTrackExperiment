using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorLoad : MonoBehaviour
{

    public Color backgroundColor = new Color(0.5f, 0.5f, 0.5f);
    public Camera _camera;
    private string CoverPath = Application.persistentDataPath + "Image/Background.png";
    public RawImage _target_L;
    public RawImage _target_R;

    public TMP_InputField _ColorOfTarget;

    private void Start()
    {

        //_camera.clearFlags = CameraClearFlags.SolidColor;
        //_camera.backgroundColor = backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TargetColorLoad()
    {
        Color newColor = _target_L.color;
        newColor.r = newColor.g = newColor.b = float.Parse(_ColorOfTarget.text);
        _target_L.color = newColor;
        _target_R.color = newColor;
    }



    private void LoadImageFromFile(string filePath, RawImage image)
    {

        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                image.texture = texture;
                image.SetNativeSize(); // 可選，根據圖片的原始尺寸調整 RawImage 的大小
            }
            else
            {
                Debug.LogError("Failed to load texture from " + filePath);
            }
        }
        else
        {
            Debug.LogError("File does not exist at " + filePath);
        }
    }


}
