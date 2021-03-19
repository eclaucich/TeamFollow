using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;

public class Panel : MonoBehaviour
{
    protected string nombrePanel = "";

    public bool hasPublicity = true;

    protected void SetPublicity()
    {
        BannerAdGameObject bannerAd = MobileAds.Instance.GetAd<BannerAdGameObject>("BannerPrueba");
        if(hasPublicity)
        {
            CanvasController.instance.bannerAdRect.gameObject.SetActive(true);
            bannerAd.LoadAd();
            Debug.Log("PUBLICIDAD ACTIVADA");
        }
        else
        {
            CanvasController.instance.bannerAdRect.gameObject.SetActive(false);
            bannerAd.DestroyAd();
            Debug.Log("PUBLICIDAD DESACTIVADA");
        }
    }

}
