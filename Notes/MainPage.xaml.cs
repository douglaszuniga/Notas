using System;
using BackEnd;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Notes
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //Loaded += MainPageLoaded;
        }

        //void MainPageLoaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    var collection = Resources["noteCollection"] as NoteCollection;
        //    if (collection != null) collection.UpdateNoteCollection();
        //}

        private void ApplicationBarIconButtonClick(object sender, EventArgs e)
        {
            NavigateToEditPage();
        }

        private void NavigateToEditPage()
        {
            string uriPath = "/Edit.xaml";
            if (lbNotes.SelectedItem != null)
            {
                var note = (Note) lbNotes.SelectedItem;
                uriPath += "?item=" + (note.Id);
            }
            NavigationService.Navigate(new Uri(uriPath, UriKind.RelativeOrAbsolute));
        }

        private void EditAppBarOptionClick(object sender, EventArgs e)
        {
            NavigateToEditPage();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            lbNotes.SelectedIndex = -1;
            var applicationBarIconButton = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
            if (applicationBarIconButton != null)
                applicationBarIconButton.IsEnabled = false;
            var collection = Resources["noteCollection"] as NoteCollection;
            if (collection != null) collection.UpdateNoteCollection();
        }

        private void LbNotesSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //EditAppBarOption.IsEnabled = true;
            var applicationBarIconButton = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
            if (applicationBarIconButton != null)
                applicationBarIconButton.IsEnabled = true;
        }
    }
}