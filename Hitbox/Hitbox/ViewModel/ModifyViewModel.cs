using Hitbox.Events;
using Hitbox.Library;
using Hitbox.Website;
using System;

namespace Hitbox.ViewModel {
    public class ModifyViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private Streamer _streamer;
        private bool _ans;

        public Streamer Streamer {
            get { return _streamer; }
            set { _streamer = value; }
        }
        
        public bool Ans {
            get { return _ans; }
            set { _ans = value; }
        }

        public ModifyViewModel(Streamer streamer) {
            _streamer = streamer;
            OKCommand = new DelegateCommand(OnOKAction, CanExecuteOK);
            CancelCommand = new DelegateCommand(OnCancelAction, CanExecuteCancel);
        }

        #region OnActions
        private void OnCancelAction(object o) {
            _ans = false;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void OnOKAction(object o) {
            //if(usernamevalid)

            Streamer.ListProfilePicture.Clear();
            Streamer.LoadStreamerInfo();
            _ans = true;
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
