using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Code.Tools
{
    public static class InternetHelper
    {
        private const string InternetCheckoutAddress = "https://google.com";

        public static bool HasInternet() => 
            Application.internetReachability != NetworkReachability.NotReachable;

        public static async UniTask<bool> HasInternetAccess()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return false;
        
            try
            {
                using UnityWebRequest request = UnityWebRequest.Get(InternetCheckoutAddress);
            
                request.timeout = 2;

                await request.SendWebRequest().ToUniTask();

                return request.responseCode == 204 || request.responseCode == 200;
            }
            catch
            {
                return false;
            }
        }
    }
}