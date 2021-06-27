using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADSManager : Singleton<ADSManager>
{
    private const string ANDROID_AD_ID = "4172745";
    private bool testMode = true;
    private string bannerID = "banner";

    private void Start()
    {
        Advertisement.Initialize(ANDROID_AD_ID, testMode);
        StartCoroutine(ShowBannerWhenInitialized());
    }

    private IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Show(bannerID);
  }
}