using Hitbox.Events;
using Hitbox.Library;
using System;

namespace Hitbox.ViewModel {
    public class ErrorViewModel {
        private string _error;

        public string Error {
            set { _error = value; }
            get { return _error; }
        }

        public ErrorViewModel(string error) {
            _error = error;
        }
    }
}