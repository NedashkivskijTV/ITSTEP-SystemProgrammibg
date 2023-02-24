using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TPLDemo.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            CommandRun = new RelayCommand(arg => RunButton_Click());
        }

        public ICommand CommandRun { get; private set; }

        private string value = "";

        public string Value
        {
            get => value;
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
                }
            }
        }

        private bool isenabled = true;

        public bool Isenabled
        {
            get => isenabled;
            set
            {
                if (!this.value.Equals(value))
                {
                    isenabled = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Isenabled)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        //С использованием привязок Async Await
        private async void RunButton_Click()
        {
            Isenabled = false;
            Value = "";

            await Task.Run(() =>
            {
                Thread.Sleep(5000);

                Isenabled = true;
                Value = "Обработка с привязкой выполнена";
            });

            
        }
        //С использованием привязок Async Await
    }
}
