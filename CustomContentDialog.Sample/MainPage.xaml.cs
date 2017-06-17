using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CustomContentDialog.Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private ContentDialog.CustomContentDialog _dialog;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _dialog = new ContentDialog.CustomContentDialog
            {
                HorizontalDialogContentAlignment = HorizontalAlignment.Center,
                VerticalDialogContentAlignment = VerticalAlignment.Center,
                Content = GenerateContent(),
                BlurBackgroundActive = false,
                DialogBackground = new SolidColorBrush(Colors.AntiqueWhite),
                BlurAmount = 13
            };

            _dialog.Show();
        }

        private UIElement GenerateContent()
        {
            var grid = new Grid()
            {
                FlowDirection = FlowDirection.RightToLeft,
                Background = new SolidColorBrush(Colors.LightGreen),
                Height = 224,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Colors.LightGray)
            };

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                var title = new TextBlock()
                {
                    Text = "Welcome",
                    Margin = new Thickness(5, 36, 5, 5),
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Foreground = new SolidColorBrush(Colors.White)
                };

                grid.Children.Add(title);
                Grid.SetRow(title, 0);
            

            var message = new TextBlock()
            {
                Text = "Hi guys, This is Custom content dialog control.",
                Margin = new Thickness(15, 35, 15, 5),
                FontSize = 18,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Right,
                FlowDirection = FlowDirection.LeftToRight,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.White)
            };

            grid.Children.Add(message);
            Grid.SetRow(message, 1);

            var girBotton = new Grid()
            {
                Background = new SolidColorBrush(Colors.White)
            };

            girBotton.ColumnDefinitions.Add(new ColumnDefinition());
            girBotton.ColumnDefinitions.Add(new ColumnDefinition());

            var btnYes = new Button()
            {
                Content ="Yes",
                Background = new SolidColorBrush(Colors.White),
                Foreground = new SolidColorBrush(Colors.DodgerBlue),
                Padding = new Thickness(4),
                BorderThickness = new Thickness(1, 0, 0, 0),
                BorderBrush = new SolidColorBrush(Color.FromArgb(27, 192, 192, 192)),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(0),
                FontSize = 14,
                Height = 54,
                Width = 160
            };

            btnYes.Click += delegate (object sender, RoutedEventArgs args)
            {
                _dialog.Hide();
            };

            girBotton.Children.Add(btnYes);
            Grid.SetColumn(btnYes, 1);

            var btnNo = new Button()
            {
                Content = "No",
                BorderThickness = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Padding = new Thickness(4),
                Margin = new Thickness(0),
                FontSize = 14,
                Background = new SolidColorBrush(Colors.White),
                Foreground = new SolidColorBrush(Colors.DarkRed),
                Height = 54,
                Width = 160
            };

            btnNo.Click += delegate (object sender, RoutedEventArgs args)
            {
                _dialog.Hide();
            };

            girBotton.Children.Add(btnNo);
            Grid.SetColumn(btnNo, 0);

            grid.Children.Add(girBotton);
            Grid.SetRow(girBotton, 2);

            return grid;
        }
    }
}
