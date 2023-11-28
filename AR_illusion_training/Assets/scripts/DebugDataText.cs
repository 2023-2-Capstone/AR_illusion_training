using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugDataText : MonoBehaviour
{
    public TextMeshProUGUI OutputText;
    public DataSystem DataSys;
    
    void Start()
    {
        OutputTextFormat(0f,0,0,0);
    }

    void Update()
    {
        OutputTextFormat(DataSys.GetDeviceSpeed(),DataSys.GetDeviceDirX(),DataSys.GetDeviceDirY(),DataSys.GetDeviceDirZ());
    }

    void OutputTextFormat(float a, double b, double c, double d){
        OutputText.text = string.Format(
            "{0,-8}\n{1,-8}\n{2,-8}\n{3,-8}\n{4,-9}\n{5,-8}\n{6,-9}\n{7,-8}",
            "DeviceAccel",a,"DeviceDirX",b,"DeviceDirY",c,"DeviceDirZ",d
            );
    }
}
