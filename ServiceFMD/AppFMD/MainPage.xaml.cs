using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

namespace AppFMD
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            List<String> ListFile = new List<String>();
            ListFile.Add("Coucou");
            ListFile.Add("Hello");

            this.ListBoxDownloadFile.DataContext = ListFile;
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

        private async Task<String> WCFRestServiceCall(String methodRequestType, String methodName, String bodyParam = "")
        {
            string ServiceURI = "http://192.168.178.20:51589/Service1.svc/rest/" + methodName;
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(methodRequestType == "GET" ? HttpMethod.Get : HttpMethod.Post, ServiceURI);

            if (!string.IsNullOrEmpty(bodyParam))
            {
                request.Content = new StringContent(bodyParam, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await httpClient.SendAsync(request);
            string returnString = await response.Content.ReadAsStringAsync();

            return returnString;
        }

        private void AddNewFileDownloadPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DownloadFileForm));
        }

        private void SettingsPage_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(SettingsForm));
        }

        /*private async void BtnGetData_Click(object sender, RoutedEventArgs e)
        {
            String data = await WCFRestServiceCall("POST", "GetData", "19");

            var dialog = new MessageDialog(data);
            await dialog.ShowAsync();
        }*/
    }
}
