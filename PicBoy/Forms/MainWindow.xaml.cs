using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using PicBoy.Core.Logic;
using PicBoy.Core.Models;

namespace PicBoy.Forms
{
    /// <summary>
    /// The DrayvCo Pic-Boy-1500.
    /// Personal Information Calendar.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Represents a list of events with notifications for WPF ListBox.
        /// </summary>
        public readonly ObservableCollection<Event> Events;

        /// <summary>
        /// Represents business logic of creating and reading events.
        /// </summary>
        public readonly EventWorker EventWorker;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            InitializeComponent();
            LogoText.Text = @"    ____  _            ____                   _____________  ____ 
   / __ \(_)____      / __ )____  __  __     <  / ____/ __ \/ __ \
  / /_/ / / ___/_____/ __  / __ \/ / / /_____/ /___ \/ / / / / / /
 / ____/ / /__/_____/ /_/ / /_/ / /_/ /_____/ /___/ / /_/ / /_/ / 
/_/   /_/\___/     /_____/\____/\__, /     /_/_____/\____/\____/  
                               /____/                             ";

            EventWorker = new EventWorker();
            Events = new ObservableCollection<Event>(EventWorker.GetAll());
            EventList.ItemsSource = Events;
        }

        /// <summary>
        /// Opens dialog to create new event.
        /// </summary>
        private void NewEvent_Click(object sender, RoutedEventArgs e)
        {
            var manageEventForm = new ManageEvent(Events)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Width = Width,
                Height = Height,
                LogoText = { Text = LogoText.Text }
            };

            Visibility = Visibility.Collapsed;
            manageEventForm.ShowDialog();
        }

        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            var currentEvent = ((FrameworkElement)sender).DataContext as Event;

            try
            {
                EventWorker.DeleteEvent(currentEvent);
                Events.Remove(currentEvent);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                MessageBox.Show(msg, "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditEvent_Click(object sender, RoutedEventArgs e)
        {
            var currentEvent = ((FrameworkElement)sender).DataContext as Event;
            var manageEventForm = new ManageEvent(Events)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Width = Width,
                Height = Height,
                LogoText = {Text = LogoText.Text},
                SaveEvent = {Visibility = Visibility.Collapsed},
                SaveEventChanges = {Visibility = Visibility.Visible},
                ManagedEvent = currentEvent
            };

            Visibility = Visibility.Collapsed;
            manageEventForm.ShowDialog();
        }

        /// <summary>
        /// Represents ability to drag window.
        /// </summary>
        private void LogoText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Closes the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}