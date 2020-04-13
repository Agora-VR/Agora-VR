using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

[AddComponentMenu("Agora VR/Android Plugin/Plugin Wrapper")]
public class PluginWrapper : MonoBehaviour
{
    // For Inspector View Only
    [Header("Plugin & Device Names")]
    [ReadOnly] public string _pluginName = "com.rutgersece.capstone2020.agoravr.blelibrary.AndroidBLE";
    private const string pluginName = "com.rutgersece.capstone2020.agoravr.blelibrary.AndroidBLE";

    [SerializeField] private string BLEDeviceName = "AgoraVR-BLE"; //TODO: Placeholder device name
    [SerializeField] private string serviceUUID = "8bff20de-32fb-4350-bddb-afe103ef9640";
    [SerializeField] private string heartRateUUID = "1c8dd778-e8c3-45b0-a9f3-48c33a400315";
    [SerializeField] private string pulseOximetryUUID = "b8ae0c39-6204-407c-aa43-43087ec29a63";

    [Header("Textboxes")]
    [SerializeField] private TextMeshProUGUI BLESetupSuccess;
    [SerializeField] private TextMeshProUGUI BLETarget;
    [SerializeField] private TextMeshProUGUI ConnectDiscover;
    [SerializeField] private TextMeshProUGUI HeartRate;
    [SerializeField] private TextMeshProUGUI PulseOximetry;

    [Header("Read Data Interval")]
    [SerializeField] private float delay = 1;

    private bool stop = false;
    private static AndroidJavaClass _pluginClass;
    private static AndroidJavaObject _pluginInstance;

    public static AndroidJavaClass PluginClass
    {
        get {
            if(_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
            }
            return _pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance
    {
        get {
            if(_pluginInstance == null)
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    void Start()
    {
        checkPermissions(); // Make sure we have the correct permissions for BLE

        if(Input.location.isEnabledByUser) {
            StartCoroutine(startBLE());         // Starts setup immediately if permissions are already enabled.
        } else {
            StartCoroutine(waitForLocation());  // For first time opening of app.
        }
    }

    private IEnumerator startBLE()
    {
        // Get Android activity for context.
        AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivityObject = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");

        // Setup BLE using plugin calls.
        PluginInstance.Call("setContext", currentActivityObject);
        BLESetupSuccess.text = String.Format("BLE Setup Success: {0}", PluginInstance.Call<bool>("BLEsetup", BLEDeviceName));
        BLETarget.text = String.Format("Target Device Name: {0}", BLEDeviceName);

        // Connect to device after a few seconds of scan time.
        yield return new WaitForSeconds(5);
        PluginInstance.Call("connectToDevice", serviceUUID, heartRateUUID, pulseOximetryUUID);
        StartCoroutine(discover());
    }

    // Gets data from plugin at intervals of 'delay' seconds.
    private IEnumerator getData()
    {
        HeartRate.text = String.Format("Heart Rate: {0}", PluginInstance.Get<int>("heartRate"));
        PulseOximetry.text = String.Format("Blood Oxygenation: {0:f}", PluginInstance.Get<float>("pulseOximetry"));

        yield return new WaitForSeconds(delay);
        StartCoroutine(getData());
    }

    // Show on UI if services are discovered (takes several seconds)
    private IEnumerator discover()
    {
        if (stop) {
            // do nothing
        } else if (PluginInstance.Get<bool>("discovered")) {
            ConnectDiscover.text = "Services Discovered.";
            StartCoroutine(getData());
        } else {
            ConnectDiscover.text = "Discovering Services...";
            yield return new WaitForSeconds(1);
            StartCoroutine(discover());
        }
    }

    // Delays for 10 seconds for the user to accept location permission.
    private IEnumerator waitForLocation()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(startBLE());
    }

    private void checkPermissions()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    public void scanButton()
    {
        reset();
        stop = false;
        StartCoroutine(startBLE());
    }

    public void reset()
    {
        stop = true;
        StopCoroutine(discover());
        StopCoroutine(getData());
        PluginInstance.Call("close");
        BLESetupSuccess.text = "BLE Setup Success: ";
        BLETarget.text = "Target Device Name: ";
        ConnectDiscover.text = "Disconnected.";
        HeartRate.text = "Heart Rate: ";
        PulseOximetry.text = "Blood Oxygenation: ";
    }

    void OnApplicationQuit()
    {
        PluginInstance.Call("close");
    }

}
