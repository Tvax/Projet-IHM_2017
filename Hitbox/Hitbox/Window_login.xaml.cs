using Hitbox.ViewModel;
using Hitbox.Website;
using System;
using System.Windows;

namespace Hitbox {
    /// <summary>
    /// Interaction logic for Window_login.xaml
    /// </summary>
    public partial class Window_login : Window {
        public LoginViewModel ViewModel { get; set; }

        public Window_login(Member member) {
            ViewModel = new LoginViewModel(member);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
