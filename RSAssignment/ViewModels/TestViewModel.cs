using RSAssignment.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;

namespace RSAssignment.ViewModels
{
    public enum CheckState
    {
        Checked,
        Unchecked,
        Indeterminate
    }

    public class TestViewModel : ViewModelBase
    {
        private CheckState _checkState;
        private ObservableCollection<TestViewModel> _children = new ObservableCollection<TestViewModel>();
        private bool _isExpanded;

        public Test Test { get; }
        public string DisplayName => $"Test {Test.Major}.{Test.Minor}";

        public CheckState CheckState
        {
            get => _checkState;
            set
            {
                if (_checkState != value)
                {
                    _checkState = value;
                    OnPropertyChanged();

                    if (Children.Any())
                    {
                        foreach (var child in Children)
                        {
                            child.CheckState = value; // This updates the child states when parent is checked/unchecked
                        }
                    }
                    else
                    {
                        UpdateParentStateOnly();
                    }
                }
            }
        }

        public ObservableCollection<TestViewModel> Children
        {
            get => _children;
            set
            {
                if (_children != value)
                {
                    foreach (var child in _children)
                    {
                        child.PropertyChanged -= Child_PropertyChanged;
                    }

                    _children = value;

                    foreach (var child in _children)
                    {
                        child.PropertyChanged += Child_PropertyChanged;
                    }

                    OnPropertyChanged();
                }
            }
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CheckUncheckCommand { get; }

        public TestViewModel(Test test, bool isParent)
        {
            Test = test;
            CheckState = CheckState.Unchecked;
            CheckUncheckCommand = new RelayCommand(_ => ToggleCheck());
        }

        private void ToggleCheck()
        {
            CheckState = CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
        }

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CheckState))
            {
                UpdateParentStateOnly();
            }
        }

        private void UpdateParentStateOnly()
        {
            if (Children.Count > 0)
            {
                bool allChecked = Children.All(child => child.CheckState == CheckState.Checked);
                bool allUnchecked = Children.All(child => child.CheckState == CheckState.Unchecked);
                _checkState = allChecked ? CheckState.Checked : (allUnchecked ? CheckState.Unchecked : CheckState.Indeterminate);
                OnPropertyChanged(nameof(CheckState));
            }
        }
    }
}
