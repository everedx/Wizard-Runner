using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    float deltaTime;
    Text fpsText;
    [SerializeField] bool limitFR;
    [SerializeField] bool isDebug;
    // Start is called before the first frame update
    void Start()
    {
        if(limitFR)
            Application.targetFrameRate = 60;
        else
            Application.targetFrameRate = -1;
        deltaTime = 0;
        fpsText = GetComponent<Text>();
        if (Application.isMobilePlatform)
            QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDebug)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = Mathf.Ceil(fps).ToString();
        }
        
    }
}
