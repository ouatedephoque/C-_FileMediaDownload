using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

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
            this.settings = Settings.Instance;

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

        private async void GetListFilmsComputer()
        {
            while(true)
            {
                LoadFilmsList();
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        private async void LoadFilmsList()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Film>));
            MemoryStream memStream = await DbTools.WCFRestServiceCall(settings, "GET", "GetFilmList/", "");

            if (memStream != null)
            {
                List<Film> list = (List<Film>)serializer.ReadObject(memStream);
                listAllFilms.Clear();

                if (list != null)
                {
                    foreach (Film f in list)
                    {
                        listAllFilms.Add(f);
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

        private void BrowsePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPageLink));
        }

        private void ListBoxDownloadFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Film f = ListBoxDownloadFile.SelectedItem as Film;
        }

        private void RefreshData_Click(object sender, RoutedEventArgs e)
        {
            LoadFilmsList();
        }
    }
}
