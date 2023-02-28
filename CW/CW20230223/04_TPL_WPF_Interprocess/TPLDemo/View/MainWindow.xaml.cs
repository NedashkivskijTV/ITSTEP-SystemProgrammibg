using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TPLDemo.ViewModel;

namespace TPLDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /*Using Task Parallel Library*/

            ////Var1 Без использования привязок
            //RunButton.Click += RunButton_Click;
            ////Var1 Без использования привязок

            //Var2 С использованием привязок
            DataContext = new MainWindowViewModel();
            //Var2 С использованием привязок

        }

        //private void RunButton_Click(object sender, RoutedEventArgs e)
        //{
        //    RunButton.IsEnabled = false;
        //    Tb.Text = "";

        //    var task = Task.Run(() =>
        //    {
        //        Thread.Sleep(5000);
        //    });

        //    // Тут может быть код

        //    task.ContinueWith((t) =>
        //    {
        //        Dispatcher.Invoke(() =>
        //        {
        //            RunButton.IsEnabled = true;
        //            Tb.Text = "Обработка без  привязки выполнена";
        //        });
        //    });
        //}
    }
}
