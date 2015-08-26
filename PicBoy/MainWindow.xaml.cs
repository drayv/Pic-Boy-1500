using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PicBoy.Core.Logic;
using PicBoy.Core.Models;

namespace PicBoy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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

        public MainWindow()
        {
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

        private void NewEvent_Click(object sender, RoutedEventArgs e)
        {
            var addEventForm = new AddEvent(Events, EventWorker)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Width = Width,
                Height = Height,
                LogoText = { Text = LogoText.Text }
            };

            Visibility = Visibility.Collapsed;
            addEventForm.ShowDialog();
        }

        private void LogoText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}