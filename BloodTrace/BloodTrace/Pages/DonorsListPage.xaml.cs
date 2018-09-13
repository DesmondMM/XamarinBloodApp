using BloodTrace.Models;
using BloodTrace.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodTrace.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonorsListPage : ContentPage
	{
        private ObservableCollection<BloodUser> BloodUsers;
        private string _bloodGroup;
        private string _country;

        public DonorsListPage (string country, string bloodType)
		{
			InitializeComponent ();
            BloodUsers = new ObservableCollection<BloodUser>();
            _bloodGroup = bloodType;
            _country = country;
            FindBloodDonors();
        }

        private async void FindBloodDonors()
        {
            ApiServices apiServices = new ApiServices();
            var bloodUsers = await apiServices.FindBlood(_country, _bloodGroup);
            foreach (var bloodUser in bloodUsers)
            {
                BloodUsers.Add(bloodUser);
            }

            LvBloodDonors.ItemsSource = BloodUsers;
        }

        private void LvBloodDonors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedDonor = e.SelectedItem as BloodUser;
            Navigation.PushAsync(new DonorProfilePage(selectedDonor));
        }
    }
}