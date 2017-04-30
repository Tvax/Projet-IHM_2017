using Hitbox.ViewModel;
using Hitbox.Website;
using System.Windows;

namespace Hitbox {

    public partial class Window_add : Window {

        public AddViewModel ViewModel { get; set; }

        public Window_add(Streamer streamer) {
            InitializeComponent();
            ViewModel = new AddViewModel(streamer);
            DataContext = ViewModel;
        }
    }
}
