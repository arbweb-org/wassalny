using System;
using System.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Threading.Tasks;
using Plugin.HybridWebView.Shared.Enumerations;

namespace p_wassalny
{
    public partial class _c_home : ContentPage
    {
        Location s_loc_ { get; set; } // Null by default

        public _c_home()
        {
            InitializeComponent();

            b_web_.AddLocalCallback("v_get_location_", v_get_location_);
            
            b_web_.ContentType = WebViewContentType.LocalFile;
            b_web_.Source = "HTML/home.html";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void v_get_location_(string p_str_)
        {
            v_set_location_async_();
            v_get_location_async_();
        }

        async Task v_set_location_async_()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    status = await Permissions.RequestAsync<Permissions.LocationAlways>();
                });
            }

            var l_req_ = new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Best,
                Timeout = new TimeSpan(0, 0, 3)
            };

            s_loc_ = await Geolocation.GetLocationAsync(l_req_);
        }

        async Task v_get_location_async_()
        {
            string l_msg_;

            if (s_loc_ == null)
            {
                l_msg_ = "Aquiring location... " + DateTime.Now.ToString();
            }
            else
            {
                l_msg_ =
                    "Location is: " + s_loc_.Latitude + "," +
                    s_loc_.Longitude +
                    " Accuracy: " + s_loc_.Accuracy;
            }

            string l_scr_ = "v_show_loc_('" + l_msg_ + "');";
            await b_web_.InjectJavascriptAsync(l_scr_);
        }
    }
}