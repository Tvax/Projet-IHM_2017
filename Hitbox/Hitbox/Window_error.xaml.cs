using Hitbox.ViewModel;
using System.Windows;

namespace Hitbox {
    public partial class Window_error : Window {
        public ErrorViewModel ViewModel { get; set; }

        public Window_error(string errorString) {
            ViewModel = new ErrorViewModel(errorString);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
