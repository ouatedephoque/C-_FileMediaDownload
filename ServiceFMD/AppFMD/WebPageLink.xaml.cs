using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class WebPageLink : Page
    {
        public WebPageLink()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            returnBack();
            e.Handled = true;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void WebViewDivx_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if(FilmTools.isAuthorizedFilm(args.Uri.ToString()))
            {
                String extension = FilmTools.getExtensionFilm(args.Uri.ToString());
                Frame.Navigate(typeof(DownloadFileForm), new Film() { FilmExtension = extension, FilmLink = args.Uri.ToString() });
            }
        }

        private void ButtonBackHome_Click(object sender, RoutedEventArgs e)
        {
            returnBack();
        }

        private void returnBack()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }
    }
}
