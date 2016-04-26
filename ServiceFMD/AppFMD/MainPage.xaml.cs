using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Threading;
using System.Runtime.InteropServices;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

namespace AppFMD
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Settings settings;
        private ObservableCollection<Film> listAllFilms;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.settings = new Settings();

            this.listAllFilms = new ObservableCollection<Film>();

            GetListFilmsComputer();

            ListBoxDownloadFile.ItemsSource = this.listAllFilms;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: préparer la page pour affichage ici.

            // TODO: si votre application comporte plusieurs pages, assurez-vous que vous
            // gérez le bouton Retour physique en vous inscrivant à l’événement
            // Événement Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si vous utilisez le NavigationHelper fourni par certains modèles,
            // cet événement est géré automatiquement.
        }

        private async Task<MemoryStream> WCFRestServiceCall(String methodRequestType, String methodName, String bodyParam = "")
        {
            if (isIP(settings.IpComputer))
            {
                string ServiceURI = "http://" + settings.IpComputer + "/FilmRESTService.svc/" + methodName;
                System.Diagnostics.Debug.WriteLine(ServiceURI);
                //string ServiceURI = "http://localhost:51588/FilmRESTService.svc/" + methodName + "/";
                HttpClient httpClient = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage(methodRequestType == "GET" ? HttpMethod.Get : HttpMethod.Post, ServiceURI);

                if (!string.IsNullOrEmpty(bodyParam))
                {
                    request.Content = new StringContent(bodyParam, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string returnString = await response.Content.ReadAsStringAsync();
                byte[] data = Encoding.UTF8.GetBytes(returnString);
                MemoryStream stream = new MemoryStream(data);

                return stream;
            }
            return null;
        }

        private async void GetListFilmsComputer()
        {
            while(true)
            {
                LoadFilmsList();
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private async void LoadFilmsList()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Film>));
            MemoryStream memStream = await WCFRestServiceCall("GET", "GetFilmList/", "");

            if (memStream != null)
            {
                List<Film> list = (List<Film>)serializer.ReadObject(memStream);
                listAllFilms.Clear();

                if (list != null)
                {
                    foreach (Film f in list)
                    {
                        listAllFilms.Add(f);
                        System.Diagnostics.Debug.WriteLine(f.FilmId + " " + f.FilmPourcent);
                    }
                }
            }
        }

        private void AddNewFileDownloadPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DownloadFileForm));
        }

        private void SettingsPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsForm));
        }

        public Boolean isIP(String ipStr)
        {
            Match match = Regex.Match(ipStr, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            return match.Success;
        }

        private void BrowsePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPageLink));
        }

        private void ListBoxDownloadFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Film f = ListBoxDownloadFile.SelectedItem as Film;

            if (f != null)
            {
                System.Diagnostics.Debug.WriteLine(f.FilmTitle + " " + f.FilmPourcent);
            }
        }
    }
}
