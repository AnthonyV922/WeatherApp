using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
	
	public partial class ForecastPage : ContentPage
	{
		public ForecastPage ()
		{
			InitializeComponent();
            BindingContext = new Forecast();
        }

        private async void GetForecastButton_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ZipOrCity.Text))
            {
                ObservableCollection<Forecast> forecast = new ObservableCollection<Forecast>();
                forecast = await WeatherResults.GetForecast(ZipOrCity.Text);
               
                // Binds data to page
                BindingContext = forecast.First();
                GetWeatherButton.Text = "Enter Zip or City";
                
                //Displys forecast collection
                ListViewItem.ItemsSource = forecast;
            }
        }

        private async void CoordinateButton_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(LatitudeEntry.Text) && !String.IsNullOrEmpty(LongitudeEntry.Text))
            {
                ObservableCollection<Forecast> forecast = new ObservableCollection<Forecast>();
                forecast =  await WeatherResults.GetCoordinateForecast(LatitudeEntry.Text, LongitudeEntry.Text);
                
                // Binds data to page
                BindingContext = forecast.First();
                CoordinateButton.Text = "Coordinates";
                
                // Displays forecast collection
                ListViewItem.ItemsSource = forecast;
            }
        }
    }
}