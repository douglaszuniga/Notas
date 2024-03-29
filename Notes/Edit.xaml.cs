﻿using System;
using System.Linq;
using System.Windows;
using BackEnd;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Notes
{
    public partial class Edit : PhoneApplicationPage
    {
        private bool _isEditMode;
        private Note _currentNote;
        private readonly PersistenceManager _manager;

        public Edit()
        {
            InitializeComponent();
            _manager = new PersistenceManager();
            Loaded += EditLoaded;
        }

        void EditLoaded(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "New Note";
            var deleteButton = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
            var pinButton = ApplicationBar.Buttons[2] as ApplicationBarIconButton;
            if (deleteButton != null && pinButton != null)
            {
                deleteButton.IsEnabled = false;
                pinButton.IsEnabled = false;
            }

            if (_isEditMode)
            {
                PageTitle.Text = "Edit Note";

                if (deleteButton != null && pinButton != null)
                {
                    deleteButton.IsEnabled = true;
                    pinButton.IsEnabled = true;
                }
            }
        }

        private void ApplicationBarSaveIconButtonClick(object sender, EventArgs e)
        {
            if (_manager != null)
                if (_isEditMode)
                {
                    _currentNote.Title = tbTitle.Text;
                    _currentNote.Content = tbContent.Text;

                    _manager.Update(_currentNote);    
                }
                else
                {
                    _currentNote = new Note {Title = tbTitle.Text, Content = tbContent.Text};

                    _manager.Add(_currentNote);
                }

            NavigateToBackPage();
        }

        private void NavigateToBackPage()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void ApplicationBarDeleteIconButtonClick(object sender, EventArgs e)
        {
            if (_manager != null) _manager.Delete(_currentNote);

            NavigateToBackPage();
        }

        private void ApplicationBarPinIconButtonClick(object sender, EventArgs e)
        {
            //NavigateToBackPage();
            if (ShellTile.ActiveTiles.Where((m) => m.NavigationUri == NavigationService.Source).Count() == 0)
            {
                StandardTileData newTile = new StandardTileData()
                {
                    Title = _currentNote.Title,
                    BackContent = _currentNote.Content
                };

                ShellTile.Create(NavigationService.Source, newTile);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string item = null;
            _isEditMode = false;

            if (NavigationContext.QueryString.TryGetValue("item", out item))
            {
                //call get note;
                if (_manager != null)
                {
                    _currentNote = _manager.Retrieve(Convert.ToInt32(item));
                    //set node properties to the UI
                    if (_currentNote == null)
                    {
                        tbContent.Text = string.Empty;
                        tbTitle.Text = string.Empty;
                    }
                    else
                    {
                        tbContent.Text = _currentNote.Content;
                        tbTitle.Text = _currentNote.Title;
                        _isEditMode = true;                                        
                    }
                }
            }
        }
    }
}