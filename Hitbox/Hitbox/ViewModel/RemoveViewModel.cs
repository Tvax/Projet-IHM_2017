using Hitbox.Events;
using Hitbox.Library;
using System;

namespace Hitbox.ViewModel {
    public class RemoveViewModel : NotifyPropertyChangedBase {
        public DelegateCommand Yes { get; set; }
        public DelegateCommand No { get; set; }

        private bool _ans;

        public bool Ans {
            get { return _ans; }
            set { _ans = value; }
        }

        public RemoveViewModel(bool ans) {
            _ans = ans;
            Yes = new DelegateCommand(OnYesAction, CanExecuteYes);
            No = new DelegateCommand(OnNoAction, CanExecuteNo);
        }

        #region CanExecute
        private bool CanExecuteYes(object obj) {
            return true;
        }
        private bool CanExecuteNo(object obj) {
            return true;
        }
        #endregion

        #region OnActions
        private void OnNoAction(object obj) {
            Ans = false;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private void OnYesAction(object obj) {
            Ans = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        #endregion
    }
}
