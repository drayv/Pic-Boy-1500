﻿using System.Windows;
using System.Windows.Input;
using PicBoy.Core.DataAccess;

namespace PicBoy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LogoText.Text = @"    ____  _            ____                   _____________  ____ 
   / __ \(_)____      / __ )____  __  __     <  / ____/ __ \/ __ \
  / /_/ / / ___/_____/ __  / __ \/ / / /_____/ /___ \/ / / / / / /
 / ____/ / /__/_____/ /_/ / /_/ / /_/ /_____/ /___/ / /_/ / /_/ / 
/_/   /_/\___/     /_____/\____/\__, /     /_/_____/\____/\____/  
                               /____/                             ";

            var repo = new EventRepository();
            EventList.ItemsSource = repo.GetAll();
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