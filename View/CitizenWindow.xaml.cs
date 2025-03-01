using Project.Enums;
using System.Collections.ObjectModel;
using System.Windows;
namespace Project.View
{
    public partial class CitizenWindow : Window
    {
        public CitizenWindow()
        {
            InitializeComponent();
        }

        public ObservableCollection<Role> Roles { get; } = new ObservableCollection<Role>(Enum.GetValues(typeof(Role)).Cast<Role>());
    }
}