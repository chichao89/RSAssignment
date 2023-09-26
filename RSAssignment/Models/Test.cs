using System.Collections.ObjectModel;

namespace RSAssignment.Models
{
    public class Test
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public ObservableCollection<Test> Children { get; } = new ObservableCollection<Test>();

        public Test(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        public Test()
        {
            Major = 0;
            Minor = 0;
            Children = new ObservableCollection<Test>();
        }
    }
}
