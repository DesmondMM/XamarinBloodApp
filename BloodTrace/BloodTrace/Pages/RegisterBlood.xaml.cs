using BloodTrace.Helpers;
using BloodTrace.Models;
using BloodTrace.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class RegisterBlood : ContentPage
	{
        public MediaFile file;
        public RegisterBlood ()
		{
			InitializeComponent ();
		}

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            var imageArray =FilesHelper.ReadFully(file.GetStream());
            file.Dispose();
            var country = PickerCountry.Items[PickerCountry.SelectedIndex];
            var bloodGroup = PickerBloodGroup.Items[PickerBloodGroup.SelectedIndex];

            DateTime dateTime = DateTime.Now;
            int d = Convert.ToInt32(dateTime.ToOADate());

            var bloodUser = new BloodUser()
            {
                Username = EntName.Text,
                Email = EntEmail.Text,
                Phone = EntPhone.Text,
                BloodGroup = bloodGroup,
                Country = country,
                ImageArray = imageArray,
                Date = d
            };
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.RegisterDonor(bloodUser);
            if (!response)
            {
                await DisplayAlert("Alert", "Something wrong", "Cancel");
            }
            else
            {
                await DisplayAlert("Hi", "Your record has beed added successfully", "Alright");
            }
        }

        private async void TapOpenCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            ImgDonor.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
    }
}