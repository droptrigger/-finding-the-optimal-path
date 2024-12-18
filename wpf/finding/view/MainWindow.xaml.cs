using System;
using System.Windows;
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

            if (x > (int)InMemory.MaxMapSize.MAX_X_MAP || y > (int)InMemory.MaxMapSize.MAX_Y_MAP)
            {
                MessageBox.Show($"Максимальное значение X: {(int)InMemory.MaxMapSize.MAX_X_MAP}\n" +
                    $"Максимальное значение Y: {(int)InMemory.MaxMapSize.MAX_Y_MAP}\n");
            }
            else if (x < 0 || y < 0)
            {
                MessageBox.Show("Размеры карты не могут быть меньше нуля!");
            }
            else
            {
                MapWindow mapWindow = new MapWindow(x, y);
                mapWindow.Show();
                this.Close();
            }
        }
    }
}
