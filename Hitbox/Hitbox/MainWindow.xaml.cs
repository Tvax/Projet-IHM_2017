using Hitbox.ViewModel;
using System.Windows;

namespace Hitbox {
    public partial class MainWindow : Window {

        private MainViewModel _viewModel;

        public MainWindow() {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }
    }
}
