using System.Windows;
using Hitbox.Website;
using Hitbox.ViewModel;

namespace Hitbox {
    public partial class Window_modify : Window {
        public ModifyViewModel ViewModel;

        public Window_modify(Streamer streamer) {
            InitializeComponent();
            ViewModel = new ModifyViewModel(streamer);
            DataContext = ViewModel;
        }
    }
}
