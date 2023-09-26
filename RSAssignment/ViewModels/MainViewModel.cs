using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RSAssignment.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private TestViewModel parentTest;

        public ObservableCollection<TestViewModel> Tests { get; set; } = new ObservableCollection<TestViewModel>();

        public ICommand CollapseCommand { get; }
        public ICommand ExpandCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand StartCommand { get; }

        public MainViewModel()
        {
            CollapseCommand = new RelayCommand(param => Collapse());
            ExpandCommand = new RelayCommand(param => Expand());
            BackCommand = new RelayCommand(param => Back());
            StartCommand = new RelayCommand(param => Start());

            // Sample data population
            var factory = new TestViewModelFactory();
            var parentTest1 = factory.CreateParentTest(1, new List<int> { 1, 2, 3, 4 });
            var parentTest2 = factory.CreateParentTest(2, new List<int> { 1, 2, 3 });



            Tests.Add(parentTest1);
            Tests.Add(parentTest2);
        }

        private void Collapse()
        {
            foreach (var test in Tests)
            {
                CollapseTree(test);
            }
        }

        private void CollapseTree(TestViewModel testViewModel)
        {
            testViewModel.IsExpanded = false;
            foreach (var child in testViewModel.Children)
            {
                CollapseTree(child);
            }
        }

        private void Expand()
        {
            foreach (var testViewModel in Tests)
            {
                testViewModel.IsExpanded = true;
                ExpandTest(testViewModel);
            }
        }

        private void ExpandTest(TestViewModel testViewModel)
        {
            if (testViewModel.Children != null)
            {
                foreach (var child in testViewModel.Children)
                {
                    ExpandTest(child);
                }
            }
        }

        private void Back()
        {
            foreach (var testViewModel in Tests)
            {
                testViewModel.CheckState = CheckState.Unchecked;
            }
        }

        private void Start()
        {
            var checkedTests = GetCheckedTests(Tests);
            if (checkedTests.Count > 0)
            {
                MessageBox.Show($"Checked Tests: {string.Join(", ", checkedTests.Select(t => t.DisplayName))}");
            }
        }

        private List<TestViewModel> GetCheckedTests(IEnumerable<TestViewModel> testViewModels)
        {
            var checkedTests = new List<TestViewModel>();
            foreach (var testViewModel in testViewModels)
            {
                if (testViewModel.CheckState == CheckState.Checked)
                {
                    checkedTests.Add(testViewModel);
                }
                if (testViewModel.Children != null)
                {
                    checkedTests.AddRange(GetCheckedTests(testViewModel.Children));
                }
            }
            return checkedTests;
        }
    }
}
