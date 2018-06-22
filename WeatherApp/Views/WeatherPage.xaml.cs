using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{

    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();

            BindingContext = new Weather();
        }



        private async void GetWeatherButton_Clicked(object sender, EventArgs e)
        {
            // Checks if there is an entry and displays using either zip or city 
            if (!String.IsNullOrEmpty(ZipOrCity.Text))
            {
                
                    Weather weather = await WeatherResults.GetWeather(ZipOrCity.Text);
                    
                    // Binds weather results to the page
                    BindingContext = weather;
                    GetWeatherButton.Text = "Enter Zip or City";
                

            }
        }

        private async void CoordinateButton_Clicked(object sender, EventArgs e)
        {   
            // Checks if there is an entry and then displays weather information
            if (!String.IsNullOrEmpty(LatitudeEntry.Text) && !String.IsNullOrEmpty(LongitudeEntry.Text))
            {

                Weather weather = await WeatherResults.GetCoordinatesWeather(LatitudeEntry.Text, LongitudeEntry.Text);
                
                // Binds weather results to the page
                BindingContext = weather;
                GetWeatherButton.Text = "Enter Coordinates";

            }
        }
    }
}