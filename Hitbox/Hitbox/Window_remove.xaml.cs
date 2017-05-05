using Hitbox.ViewModel;
using System.Windows;

namespace Hitbox {
    /// <summary>
    /// Interaction logic for Window_remove.xaml
    /// </summary>
    public partial class Window_remove : Window {

        public RemoveViewModel ViewModel { get; set; }

        public Window_remove(bool ans) {
            InitializeComponent();
            ViewModel = new RemoveViewModel(ans);
            DataContext = ViewModel;
        }
    }
}
