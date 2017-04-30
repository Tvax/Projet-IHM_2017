using Hitbox.Events;
using Hitbox.Library;
using System;

namespace Hitbox.ViewModel {
    public class ErrorViewModel {
        private string _error;

        public DelegateCommand OKCommandError { get; set; }
        public string Error {
            set { _error = value; }
            get { return _error; }
        }

        public ErrorViewModel(string error) {
            OKCommandError = new DelegateCommand(OnOKActionError, CanExecuteOKError);
            _error = error;
        }

        private void OnOKActionError(object obj) {
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private bool CanExecuteOKError(object obj) {
            return true;
        }
    }
}
