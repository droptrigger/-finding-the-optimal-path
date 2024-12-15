using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace test
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            int x = Int32.Parse(InputX.Text);
            int y = Int32.Parse(InputY.Text);

            MapWindow mapWindow = new MapWindow(x, y);
            mapWindow.Show();
            this.Close();
        }
    }
}
