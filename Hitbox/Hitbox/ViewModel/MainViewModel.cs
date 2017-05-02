using Hitbox.Events;
using Hitbox.Library;
using Hitbox.Website;
using System;
using System.Collections.ObjectModel;

//TODO add banner in background?
//TODO exception while adding streamer: 
//json error, streamer doesnt exists
//less than 5 follows
//views displays "false" change it to "0"

namespace Hitbox.ViewModel {
    class MainViewModel : NotifyPropertyChangedBase {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ModCommand { get; set; }
        public DelegateCommand RmCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        private Window_add _winAdd { get; set; }
        private Window_remove _winRm { get; set; }
        private Window_modify _winMod { get; set; }
        private ObservableCollection<Streamer> _listStreamers;
        private Streamer _streamer;

        public ObservableCollection<Streamer> ListStreamers {
            get { return _listStreamers; }
            set { _listStreamers = value; }
        }
        public Streamer Streamer {
            get { return _streamer; }
            set {
                _streamer = value;
                NotifyPropertyChanged("Streamer");
                NotifyPropertyChanged("ListStreamers");
            }
        }

        public MainViewModel() {
            AddCommand = new DelegateCommand(OnAddAction, CanExecuteAdd);
            //RmCommand = new DelegateCommand(OnRemoveAction, CanExecuteRemove);
            //ModCommand = new DelegateCommand(OnModAction, CanExecuteMod);

            ListStreamers = new ObservableCollection<Streamer>();
            //Streamer = new Streamer();
            //Streamer Streamer2 = new Streamer();
            //ListStreamers.Add(Streamer);
            //ListStreamers.Add(Streamer2);
        }

        #region OnActions
        private void OnAddAction(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;

            _winAdd = new Window_add(_streamer = new Streamer());
            _winAdd.Name = "Add";
            _winAdd.ShowDialog();
         
            _listStreamers.Add(_winAdd.ViewModel.Streamer);
            NotifyPropertyChanged("Streamer");
            NotifyPropertyChanged("ListStreamers");
        }
        #endregion

        #region CanExecute
        private bool CanExecuteAdd(object o) {
            return true;
        }
        #endregion

        #region CloseEvents
        private void CloseAddView(object sender, EventArgs e) {
            _winAdd.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseAddView;
        }
        #endregion
    }
}