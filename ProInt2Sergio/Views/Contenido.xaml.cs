using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProInt2Sergio.Services;
using ProInt2Sergio.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Newtonsoft.Json;

namespace ProInt2Sergio.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contenido : ContentPage
    {
        RestService _servicio;
        Prediccion prediccionServer;
        string imagen64;
        public Contenido()
        {
            InitializeComponent();
            _servicio = new RestService();
            imagen64 = String.Empty;
        }

        async void ConsultarUna(object sender, EventArgs e)
        {
            Predecir prediccionNueva = new Predecir();
            prediccionNueva.nombreImagen = "imagen1";
            prediccionNueva.imagen = imagen64;
            var json = JsonConvert.SerializeObject(prediccionNueva);
            prediccionServer = await _servicio.PostImagenPredecir("https://murmuring-falls-94553.herokuapp.com/reconocimiento", json);
            Console.WriteLine(prediccionServer.ToString());
            BindingContext = prediccionServer;
        }

        private async void TomarFoto(object sender, EventArgs e)
        {
            Image imagen = new Image();
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
            Console.WriteLine(file.Path);
            if (file == null)
                return;

            ImageList.Children.Clear();
            var image = new Image { Aspect = Aspect.AspectFit };
            image.Source = ImageSource.FromFile(file.Path);
            ImageList.Children.Add(image);

            byte[] b = System.IO.File.ReadAllBytes(file.Path);
            imagen64 = Convert.ToBase64String(b);
        }

        private async void CargarFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                SaveMetaData = true
            });


            if (file == null)
                return;

            ImageList.Children.Clear();
            var image = new Image {  Aspect = Aspect.AspectFit };
            image.Source = ImageSource.FromFile(file.Path);
            ImageList.Children.Add(image);

            byte[] b = System.IO.File.ReadAllBytes(file.Path);
            imagen64 = Convert.ToBase64String(b);
        }
    }
}