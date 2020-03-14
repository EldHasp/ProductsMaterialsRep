using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StatisticsWpf
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ModelStatistic model = new ModelStatistic();
            ViewModelStatistic viewModel = new ViewModelStatistic(model);
            new MainWindow()
            {
                DataContext = viewModel
            }
            .Show();
        }
    }
}
