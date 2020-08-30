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

            b_web_.AddLocalCallback("v_toggle_flyout_", v_toggle_flyout_);
            b_web_.AddLocalCallback("v_get_location_", v_get_location_);
            
            b_web_.ContentType = WebViewContentType.LocalFile;
            b_web_.Source = "HTML/home.html";
        }

        void v_toggle_flyout_(string p_str_)
        {
            DisplayAlert("Wassalny", "ok", "Cancle");
            Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented; 
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
                l_msg_ = string.Join<double>(
                    ",",
                    (new double[] 
                    { 
                        s_loc_.Latitude,
                        s_loc_.Longitude,
                        (double)s_loc_.Accuracy }));
            }

            string l_scr_ = "f_set_location_('" + l_msg_ + "');";
            await b_web_.InjectJavascriptAsync(l_scr_);
        }
    }
}