using Newtonsoft.Json;
using SharedModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AirportPage : ContentPage
    {
        public AirportPage()
        {
            InitializeComponent();
            //set binding context to Airport data type
            this.BindingContext = new Airport();

            airportButton.Clicked += async (s, e) =>
            {
                var code = airportCode.Text;
                var result = await GetAirportAsync(code);
             
                this.BindingContext = result;
            };
        }

        public async Task<Airport> GetAirportAsync(string code)
        {

            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var address = $"https://xamarindemomobileappservice20190509010416.azurewebsites.net/api/airport/{code}";

            var response = await client.GetAsync(address);

            var airportJson = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Airport>(airportJson) as Airport;

            return result;

        }

    }
}