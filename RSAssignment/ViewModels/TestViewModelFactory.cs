using RSAssignment.Models;
using RSAssignment.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class TestViewModelFactory
{
    public TestViewModel CreateParentTest(int major, List<int>? minorValues = null)

    {
        var parentTest = new TestViewModel(new Test { Major = major }, true);  


        if (minorValues != null)
        {
            parentTest.Children = new ObservableCollection<TestViewModel>();

            foreach (var minor in minorValues)
            {
                parentTest.Children.Add(new TestViewModel(new Test { Major = major, Minor = minor }, false));  // Assuming this is a child node

            }
        }

        return parentTest;
    }
}
