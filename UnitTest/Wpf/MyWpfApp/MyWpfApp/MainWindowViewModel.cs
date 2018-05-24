using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace MyWpfApp
{
    public class MainWindowViewModel : BindableBase
    {
        public int FirstNumber { get; set; }

        public int SecondNumber { get; set; }

        public int sum;
        public int Sum
        {
            get => sum;
            set => SetProperty(ref sum, value);
        }

        public ICommand CalculateSumCommand => new DelegateCommand(CalculateSum);

        private void CalculateSum()
        {
            Sum = FirstNumber + SecondNumber;
        }
    }
}
