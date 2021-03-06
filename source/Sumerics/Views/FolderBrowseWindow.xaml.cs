﻿namespace Sumerics.Views
{
    using MahApps.Metro.Controls;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

	/// <summary>
	/// Interaction logic for FolderBrowseWindow.xaml
	/// </summary>
	public partial class FolderBrowseWindow : MetroWindow
    {
        #region ctor

        public FolderBrowseWindow()
        {
			InitializeComponent();
		}

        #endregion

        #region Events

        async void ClearSelected(Object sender, RoutedEventArgs e)
        {
            var view = sender as ListView;

            if (view != null)
            {
                view.SelectedIndex = -1;
                //This hack seems strange but unfortunately it is the way to go
                await Task.Delay(10);
                Current.Focus();
            }
        }

        #endregion
    }
}
