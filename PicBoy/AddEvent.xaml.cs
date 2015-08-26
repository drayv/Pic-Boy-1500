using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PicBoy.Core.Logic;
using PicBoy.Core.Models;

namespace PicBoy
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent
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
        /// Prevents a default instance of the <see cref="AddEvent"/> class from being created.
        /// </summary>
        private AddEvent()
        {
        }

        public AddEvent(ObservableCollection<Event> events, EventWorker eventWorker)
        {
            Events = events;
            EventWorker = eventWorker;
            InitializeComponent();
        }

        private void SaveEvent_Click(object sender, RoutedEventArgs e)
        {
            Events.Add(new Event {DateBegin = DateTime.Now, Name = "test event."});
            Close_Click(sender, e);
        }

        private void LogoText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Owner.Width = Width;
            Owner.Height = Height;
            Owner.Left = Left;
            Owner.Top = Top;
            Owner.Visibility = Visibility.Visible;

            Close();
        }
    }
}