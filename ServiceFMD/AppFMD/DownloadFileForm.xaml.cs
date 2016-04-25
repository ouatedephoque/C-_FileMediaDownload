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
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace AppFMD
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class DownloadFileForm : Page
    {
        private Settings settings;
        private Film filmToDownload;

        public DownloadFileForm()
        {
            this.InitializeComponent();

            this.settings = new Settings();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if(rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.filmToDownload = e.Parameter as Film;

            if(this.filmToDownload == null)
            {
                this.filmToDownload = new Film();
                this.filmToDownload.FilmLink = "";
            }

            TextBoxUrl.Text = this.filmToDownload.FilmLink;
        }

        private async Task<String> WCFRestServiceCall(String methodRequestType, String methodName, String bodyParam = "")
        {
            //if (isIP(settings.IpComputer))
            //{
            string ServiceURI = "http://" + settings.IpComputer + ":51589/FilmRESTService.svc/" + methodName + "";
            //string ServiceURI = "http://localhost:51588/FilmRESTService.svc/" + methodName + "";
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(methodRequestType == "GET" ? HttpMethod.Get : HttpMethod.Post, ServiceURI);

            if (!string.IsNullOrEmpty(bodyParam))
            {
                request.Content = new StringContent(bodyParam, Encoding.UTF8, "application/json");
            }
            System.Diagnostics.Debug.WriteLine(bodyParam);

            System.Diagnostics.Debug.WriteLine(request.Content);
            System.Diagnostics.Debug.WriteLine(request.ToString());

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string returnString = await response.Content.ReadAsStringAsync();
            byte[] data = Encoding.UTF8.GetBytes(returnString);
            MemoryStream stream = new MemoryStream(data);

            return returnString;
            //}
            //return null;
        }

        private async void ButtonDownload_Click(object sender, RoutedEventArgs e)
        {
            String url = TextBoxUrl.Text;

            this.filmToDownload.FilmTitle = TextBoxTitle.Text.ToString();
            this.filmToDownload.FilmLink = url;
            this.filmToDownload.FilmPourcent = 0;
            this.filmToDownload.FilmExtension = FilmTools.getExtensionFilmOrDefault(url);

            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Film));
            jsonSerializer.WriteObject(stream, this.filmToDownload);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            String memStream = await WCFRestServiceCall("POST", "PostAddFilm/New", reader.ReadToEnd());
            
            System.Diagnostics.Debug.WriteLine(memStream);
        }
    }
}
