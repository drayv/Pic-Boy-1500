using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PicBoy.Core.Logic;
using PicBoy.Core.Models;

namespace PicBoy.Forms
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AddEvent"/> class.
        /// </summary>
        /// <param name="events">Observable collection of events to work with.</param>
        public AddEvent(ObservableCollection<Event> events)
        {
            Events = events;
            EventWorker = new EventWorker();
            InitializeComponent();
        }

        /// <summary>
        /// Saves event to storage, and returns us to main window.
        /// </summary>
        private void SaveEvent_Click(object sender, RoutedEventArgs e)
        {
            Events.Add(new Event { DateBegin = DateTime.Now, Name = "test event." });
            CloseChildWindow();
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
        /// Returns us to main window without any changes in storage.
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseChildWindow();
        }

        /// <summary>
        /// Returns us to main window without any changes in storage.
        /// </summary>
        private void CloseChildWindow()
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