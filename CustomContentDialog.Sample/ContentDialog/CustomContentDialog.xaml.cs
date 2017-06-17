using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomContentDialog.Sample.ContentDialog
{
    public sealed partial class CustomContentDialog : UserControl
    {

        #region Events

        public event EventHandler Closed;

        public event EventHandler Opened;

        #endregion

        #region Properties

        public new static readonly DependencyProperty MarginProperty = DependencyProperty.Register(
            "Margin", typeof(Thickness), typeof(CustomContentDialog), new PropertyMetadata(default(Thickness)));

        public new Thickness Margin
        {
            get { return (Thickness)GetValue(MarginProperty); }
            set
            {
                SetValue(MarginProperty, value);
                Popup.VerticalOffset = Margin.Top;
                Popup.HorizontalOffset = Margin.Right;
            }
        }


        public static readonly DependencyProperty DialogBackgroundProperty = DependencyProperty.Register(
            "DialogBackground", typeof(Brush), typeof(CustomContentDialog), new PropertyMetadata(default(Brush)));

        public Brush DialogBackground
        {
            get { return (Brush)GetValue(DialogBackgroundProperty); }
            set { SetValue(DialogBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BlurBackgroundActiveProperty = DependencyProperty.Register(
            "BlurBackgroundActive", typeof(bool), typeof(CustomContentDialog), new PropertyMetadata(default(bool)));

        public bool BlurBackgroundActive
        {
            get { return (bool)GetValue(BlurBackgroundActiveProperty); }
            set
            {
                SetValue(BlurBackgroundActiveProperty, value);
                if (value)
                {
                    LayoutRoot.Background = new SolidColorBrush(Colors.Transparent);
                    BackDrop.Visibility = Visibility.Visible;
                }
                else
                {
                    LayoutRoot.Background = DialogBackground;
                    BackDrop.Visibility = Visibility.Collapsed;
                }
            }
        }

        public new static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(UIElement), typeof(CustomContentDialog), new PropertyMetadata(default(UIElement)));

        public new UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set
            {
                SetValue(ContentProperty, value);
                GridContent.Children.Add(value);
            }
        }

        public static readonly DependencyProperty IsCanDismissWithClickOutProperty = DependencyProperty.Register(
            "IsCanDismissWithClickOut", typeof(bool), typeof(CustomContentDialog), new PropertyMetadata(true));

        public bool IsCanDismissWithClickOut
        {
            get { return (bool)GetValue(IsCanDismissWithClickOutProperty); }
            set { SetValue(IsCanDismissWithClickOutProperty, value); }
        }

        public static readonly DependencyProperty HorizontalDialogContentAlignmentProperty = DependencyProperty.Register(
            "HorizontalDialogContentAlignment", typeof(HorizontalAlignment), typeof(CustomContentDialog), new PropertyMetadata(Windows.UI.Xaml.HorizontalAlignment.Center));

        public HorizontalAlignment HorizontalDialogContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalDialogContentAlignmentProperty); }
            set
            {
                SetValue(HorizontalDialogContentAlignmentProperty, value);
                GridContent.HorizontalAlignment = value;
            }
        }

        public static readonly DependencyProperty VerticalDialogContentAlignmentProperty = DependencyProperty.Register(
            "VerticalDialogContentAlignment", typeof(VerticalAlignment), typeof(CustomContentDialog), new PropertyMetadata(Windows.UI.Xaml.VerticalAlignment.Center));

        public VerticalAlignment VerticalDialogContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalDialogContentAlignmentProperty); }
            set
            {
                SetValue(VerticalDialogContentAlignmentProperty, value);
                GridContent.VerticalAlignment = value;
            }
        }

        public static readonly DependencyProperty BlurAmountProperty = DependencyProperty.Register(
            "BlurAmount", typeof(double), typeof(CustomContentDialog), new PropertyMetadata(15));

        public double BlurAmount
        {
            get { return (double)GetValue(BlurAmountProperty); }
            set
            {
                SetValue(BlurAmountProperty, value);
                BackDrop.BlurAmount = value;
            }
        }

        #endregion

        public CustomContentDialog()
        {
            this.InitializeComponent();

            LayoutUpdated += OnLayoutUpdated;
        }

        private void OnLayoutUpdated(object sender, object o)
        {
            LayoutRoot.Width = GetScreenResolutionInfo().Width - (Margin.Right + Margin.Left);
            LayoutRoot.Height = GetScreenResolutionInfo().Height - (Margin.Top + Margin.Bottom);

            BackgroundGrid.Width = LayoutRoot.Width;
            BackgroundGrid.Height = LayoutRoot.Height;
        }

        private void PopupOnClosed(object sender, object o)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        private void PopupOnOpened(object sender, object o)
        {
            Opened?.Invoke(this, EventArgs.Empty);
        }

        public void Show()
        {
            if (Popup != null)
            {
                Popup.IsOpen = true;
                FocusOnFirstTabStop();
            }
        }

        public void Hide()
        {
            if (Popup != null)
            {
                Popup.IsOpen = false;
            }
        }

        private void FocusOnFirstTabStop()
        {
            var control = FocusManager.FindFirstFocusableElement(Content);
            if (control is Control)
            {
                var tabControl = control as Control;
                tabControl.Focus(FocusState.Programmatic);
            }
        }

        public static Size GetScreenResolutionInfo()
        {
            var size = new Size(((Frame)Window.Current.Content).ActualWidth, ((Frame)Window.Current.Content).ActualHeight);
            return size;
        }


        private void LayoutRoot_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (IsCanDismissWithClickOut)
            {
                Hide();
            }
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            if (IsCanDismissWithClickOut)
            {
                Hide();
            }
        }
    }
}
