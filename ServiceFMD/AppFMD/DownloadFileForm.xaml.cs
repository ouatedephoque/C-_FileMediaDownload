using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.Data.Xml.Dom;
using Windows.Phone.UI.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

            this.settings = Settings.Instance;
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

        private async void ButtonDownload_Click(object sender, RoutedEventArgs e)
        {
            String url = TextBoxUrl.Text;
            String title = TextBoxTitle.Text;

            if (!url.Equals("") && !title.Equals(""))
            {
                this.filmToDownload.FilmTitle = title;
                this.filmToDownload.FilmLink = url;
                this.filmToDownload.FilmPourcent = 0;
                this.filmToDownload.FilmExtension = FilmTools.getExtensionFilmOrDefault(url);
                this.filmToDownload.FilmPathPC = settings.PathComputer;

                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Film));
                jsonSerializer.WriteObject(stream, this.filmToDownload);
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);

                MemoryStream memStream = await DbTools.WCFRestServiceCall(settings, "POST", "PostAddFilm/New", reader.ReadToEnd());

                Frame rootFrame = Window.Current.Content as Frame;

                if (rootFrame != null && rootFrame.CanGoBack)
                {
                    rootFrame.GoBack();
                }
            }
            else
            {
                ToastTemplateType toastTemplate = ToastTemplateType.ToastText01;
                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                XmlNodeList textElements = toastXml.GetElementsByTagName("text");
                textElements[0].AppendChild(toastXml.CreateTextNode("Veuillez remplir les deux champs"));
                var toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
        }
    }
}
