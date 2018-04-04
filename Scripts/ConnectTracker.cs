using Assets.ConnectSdk.Scripts;
using Assets.ConnectSdk.Scripts.Unity;
using UnityEngine;

// ReSharper disable UnusedMember.Local
// ReSharper disable once CheckNamespace
public class ConnectTracker : MonoBehaviour
{
    private static IConnectTrackerProxy _instance;

    [Tooltip("Android API Key for Connected Interactive - REQUIRED")] public string AndroidApiKey = "";
    [Tooltip("iOS API Key for Connected Interactive - REQUIRED")] public string iOSApikey = "";

    [Tooltip("In test mode, all API calls are simulated. Default: false")] [ContextMenuItem("Reset", "ResetTestMode")] public bool TestMode;

    private static IConnectTrackerProxy Instance
    {
        get { return _instance ?? (_instance = CreateInstance()); }
    }

    private void ResetTestMode()
    {
        TestMode = false;
    }

    void RegisterReferrer(string referrer)
    {
        Instance.RegisterReferrer(referrer);
    }

    // Use this for initialization
    void Start()
    {
        // Keep SDK active throughout scene loads
        DontDestroyOnLoad(this);

        string apiKey = string.Empty;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            apiKey = iOSApikey;    
        }
        else
        {
            apiKey = AndroidApiKey;
        }

        Instance.Init(apiKey: apiKey, sandbox: TestMode);
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Instance.Update();
    }

    public void Init()
    {
    }

    private static IConnectTrackerProxy CreateInstance()
    {
        return new ConnectTrackerUnity();
    }

    public static void TrackEvent(string key, string value = "")
    {
        Instance.TrackEvent(key, value);
    }
}