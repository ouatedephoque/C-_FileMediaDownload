using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace AppFMD
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SettingsForm : Page
    {
        private Settings settings;

        public SettingsForm()
        {
            this.InitializeComponent();

            this.settings = Settings.Instance;

            String[] ip = settings.IpComputer.Split(':');
            TextIpPC.Text = ip[0];
            TextPathPC.Text = settings.PathComputer;

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            GoPrevious();
            e.Handled = true;
        }

        private void GoPrevious()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param>Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void BtnSaveParameters_Click(object sender, RoutedEventArgs e)
        {
            settings.IpComputer = TextIpPC.Text.ToString();
            settings.PathComputer = TextPathPC.Text.ToString();

            GoPrevious();
        }

        private void TextIpPC_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            TextIpPC.Text = TextIpPC.Text.Replace(',', '.').Replace('-', '.');
            TextIpPC.SelectionStart = TextIpPC.Text.Length;
        }
    }
}
