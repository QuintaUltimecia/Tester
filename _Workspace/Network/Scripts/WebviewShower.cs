using UnityEngine;
using System.Collections;

namespace Network
{
    public class WebviewShower : MonoBehaviour
    {
        private ServerRequest _serverRequest;
        private UniWebViewController _webview;
        private OneSignalManager _onesignal;

        public void Init(UniWebViewController webview, OneSignalManager oneSignal, ServerRequest serverRequest)
        {
            _webview = webview;
            _onesignal = oneSignal;
            _serverRequest = serverRequest;
        }

        public void OpenWebview(ServerFeatures data)
        {
            DeeplinkFeatures dataDeepLink = new DeeplinkFeatures();

            _onesignal.InitOneSignal(data);
            StartCoroutine(StartWebview(data, dataDeepLink));
        }

        private IEnumerator StartWebview(ServerFeatures data, DeeplinkFeatures dataDeepLink)
        {
            yield return new WaitForSeconds(1f);

            string link = CreateLink(data.webview, dataDeepLink, data);
            CustomDebug.LogFile($"Webview show with link: {link}");
            _webview.GetWebview().Show();
            _webview.GetWebview().Load(link);
        }

        private string CreateLink(string webview, DeeplinkFeatures deeplinkFeatures, ServerFeatures data)
        {
            string newWebview = "";

            for (int i = 0; i < webview.Length; i++)
            {
                if (webview[i] == '?')
                {
                    newWebview += webview[i];
                    break;
                }
                else
                {
                    newWebview += webview[i];
                }
            }

            string parameters =
                $"external_id={_serverRequest.GetClient().app_client_id}&" +
                $"creative_id=null&" +
                $"ad_campaign_id=null&" +
                $"source=null&" +
                $"sub_id_1={deeplinkFeatures.sub1}&" +
                $"sub_id_2={deeplinkFeatures.sub2}&" +
                $"sub_id_3={deeplinkFeatures.sub3}&" +
                $"sub_id_4={deeplinkFeatures.sub4}&" +
                $"sub_id_5={deeplinkFeatures.sub5}&" +
                $"stream_id={deeplinkFeatures.partnerStream}&" +
                $"campaign_name={deeplinkFeatures.partnerName}&" +
                $"advertising_id={_serverRequest.GetClient().app_advertising_id}&" +
                $"adid=null&" +
                $"install_referrer={_serverRequest.InstallReferrer}&" +
                $"push_data={_onesignal.GetAdditinalData()}";

            newWebview += parameters;

            return newWebview;
        }
    }

}