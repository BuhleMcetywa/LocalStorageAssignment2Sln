using LocalStorageAssignment2.Models;
using Microsoft.Maui;
using Newtonsoft.Json;

namespace LocalStorageAssignment2
{
    public partial class MainPage : ContentPage
    {
        private const string FilePath = "profile.json";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var profile = new Profile();

            {
                profile.Name = NameEntry.Text;
                profile.Surname = SurnameEntry.Text;
                profile.EmailAddress = EmailEntry.Text;
                profile.Bio = BioEditor.Text;
            };

            try
            {
                var json = JsonConvert.SerializeObject(profile);
                var file = Path.Combine(FileSystem.AppDataDirectory, FilePath);
                await File.WriteAllTextAsync(file, json);
                await DisplayAlert("Success", "Profile saved successfully", "OK");

            }


            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save profile: {ex.Message}", "OK");

            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var profile = await LoadProfile();
            if (profile != null)
            {
                profile.Name = NameEntry.Text;
                profile.Surname = SurnameEntry.Text;
                profile.EmailAddress = EmailEntry.Text;
                profile.Bio = BioEditor.Text;

            }
        }
        private async Task<Profile> LoadProfile()
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
                await DisplayAlert("Error", $"Failed to select picture: {ex.Message}", "Ok");
            }
        }
    }


}

