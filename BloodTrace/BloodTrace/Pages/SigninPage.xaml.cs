using BloodTrace.Services;
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
	public partial class SigninPage : ContentPage
	{
		public SigninPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser(EntEmail.Text, EntPassword.Text);

            if (!response)
            {
                await DisplayAlert("Alert", "Something Wrong...", "Cancel");
            }
            else
            {
                Navigation.InsertPageBefore(new HomePage(), this);
                await Navigation.PopAsync();
            }
        }

        private void TapSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}