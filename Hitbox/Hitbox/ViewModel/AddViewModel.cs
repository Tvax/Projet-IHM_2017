using Hitbox.Events;
using Hitbox.Library;
using Hitbox.Website;
using System;

namespace Hitbox.ViewModel {
    public class AddViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private Streamer _streamer;
        private bool _valid;

        public Streamer Streamer {
            get { return _streamer; }
            set { _streamer = value; }
        }

        public bool Valid {
            get { return _valid; }
            set { _valid = value; }
        }

        public AddViewModel(Streamer streamer) {
            _streamer = streamer;

            OKCommand = new DelegateCommand(OnOKAction, CanExecuteOK);
            CancelCommand = new DelegateCommand(OnCancelAction, CanExecuteCancel);
        }

        #region OnActions
        private void OnCancelAction(object o) {
            //Valid = false;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void OnOKAction(object o) {

            //if(usernamevalid)

            Streamer.LoadStreamerInfo();
            //Valid = true;
            _streamer = Streamer;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        #endregion

        #region CanExecute
        private bool CanExecuteCancel(object o) {
            return true;
        }
        private bool CanExecuteOK(object o) {
            return true;
        }
        #endregion
    }
}