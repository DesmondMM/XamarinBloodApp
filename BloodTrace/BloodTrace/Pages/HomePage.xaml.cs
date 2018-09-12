using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodTrace.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        private void Tblogout_Clicked(object sender, EventArgs e)
        {
            Settings.UserName = "";
            Settings.Password = "";
            Settings.AccessToken = "";
            Navigation.InsertPageBefore(new SigninPage(), this);
            Navigation.PopAsync();
        }

        private void TapFindBlood_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindBloodGroup());
        }

        private void TapRegisterBlood_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterBlood());
        }

        private void TapLatestDonors_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LatestDonors());
        }

        private void TapHelp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Help());
        }
    }
}