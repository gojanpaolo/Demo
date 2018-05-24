using Xunit;

namespace MyWpfApp.Tests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void CalculateSumCommand_Executed_CalculatesSumFromFirstAndSecondNumber()
        {
            var viewModel = new MainWindowViewModel();
            viewModel.FirstNumber = 2;
            viewModel.SecondNumber = 3;
            viewModel.CalculateSumCommand.Execute(null);
            Assert.Equal(5, viewModel.Sum);
        }
    }
}
