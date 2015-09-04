using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using PicBoy.Core.Logic;
using PicBoy.Core.Models;
using PicBoy.Utility;

namespace PicBoy.Forms
{
    /// <summary>
    /// Interaction logic for ManageEvent.xaml
    /// </summary>
    public partial class ManageEvent
    {
        /// <summary>
        /// Event to save or change.
        /// </summary>
        public Event ManagedEvent { get; set; }

        /// <summary>
        /// Represents a list of events with notifications for WPF ListBox.
        /// </summary>
        public readonly ObservableCollection<Event> Events;

        /// <summary>
        /// Represents business logic of creating and reading events.
        /// </summary>
        public readonly EventWorker EventWorker;

        /// <summary>
        /// Prevents a default instance of the <see cref="ManageEvent"/> class from being created.
        /// </summary>
        private ManageEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageEvent"/> class.
        /// </summary>
        /// <param name="events">Observable collection of events to work with.</param>
        public ManageEvent(ObservableCollection<Event> events)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Events = events;
            EventWorker = new EventWorker();
            InitializeComponent();

            if (ManagedEvent == null)
            {
                ManagedEvent = new Event
                {
                    Name = "",
                    DateBegin = DateTime.Now.AddMinutes(15),
                    DateEnd = DateTime.Now.EndOfDay()
                };
            }
        }

        /// <summary>
        /// Saves event to storage, and returns us to main window.
        /// </summary>
        private void SaveEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckIntersectionAndContinue()) return;
                EventWorker.AddEvent(ManagedEvent);
                Events.Add(ManagedEvent);
                CloseChildWindow();
            }
            catch (EventWorker.EventValidationException ex)
            {
                var msg = ex.Message;
                MessageBox.Show(msg, "Validation error.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                MessageBox.Show(msg, "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveEventChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckIntersectionAndContinue()) return;
                EventWorker.SaveEventChanges(ManagedEvent);
                var findEvent = Events.FirstOrDefault(f => f.Id == ManagedEvent.Id);
                Events.Remove(findEvent);
                Events.Add(ManagedEvent);
                CloseChildWindow();
            }
            catch (EventWorker.EventValidationException ex)
            {
                var msg = ex.Message;
                MessageBox.Show(msg, "Validation error.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                MessageBox.Show(msg, "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckIntersectionAndContinue()
        {
            var intersectionMessage = EventWorker.CheckIntersectionDates(ManagedEvent);
            if (intersectionMessage.Equals(string.Empty)) return true;
            var result = MessageBox.Show(intersectionMessage, "Validation question.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result != MessageBoxResult.No;
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