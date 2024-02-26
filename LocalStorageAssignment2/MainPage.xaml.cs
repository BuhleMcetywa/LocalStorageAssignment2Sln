using LocalStorageAssignment2.Models;
using Microsoft.Maui;
using Newtonsoft.Json;

namespace LocalStorageAssignment2
{
    public partial class MainPage : ContentPage
    {
        private const string FilePath = "profile.json";
        private Profile _profile;

        public Profile MyProfile { get { return _profile; }
            set { _profile = value;

                OnPropertyChanged();
            
            } 
        } 

        public MainPage()
        {
            InitializeComponent();
            _profile = new Profile();
            LoadProfileData();
            BindingContext = MyProfile;

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await SaveProfileAsync(_profile);
        }

        private async void LoadProfileData() 

        {
            try
            {
                var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profile.json");


                if (File.Exists(file))
                {
                    var json = File.ReadAllText(file);
                    MyProfile = JsonConvert.DeserializeObject<Profile>(json);
                }

                else
                {
                    MyProfile = new Profile();
                }

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to load profile: {ex.Message}", "OK");
            }
        }

        private async Task SaveProfileAsync(Profile profile)
        {
            var json = JsonConvert.SerializeObject(profile);
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profile.json");
            await File.WriteAllTextAsync(filePath, json);
        }

        private async Task<Profile> LoadProfileAsync()
        {
            var file = Path.Combine(FileSystem.AppDataDirectory, FilePath);
            if (File.Exists(file))
            {
                var json = await File.ReadAllTextAsync(file);
                return JsonConvert.DeserializeObject<Profile>(json);

            }

            return null;
        }

       
        private async void OnSelectPictureClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Select Picture" });
                if (result != null)
                {
                    profileImage.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);

					

					(BindingContext as Profile).PicturePath = result.FullPath;
                }
            }

            catch (Exception ex)
            {
                

            }

        }

       
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if (MyProfile != null)
                {
                 
                    await SaveProfileAsync(MyProfile);

                    await DisplayAlert("Success", "Profile saved successfully.", "OK");
                }
                else
                {
                    // Handle the case where the profile is null
                    await DisplayAlert("Error", "Profile is null.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save profile: {ex.Message}", "OK");
            }
        }



    }


}

