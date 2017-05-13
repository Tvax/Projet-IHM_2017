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
        private bool _ans;
        private Window_error _winEr;

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
            RmCommand = new DelegateCommand(OnRmAction, CanExecuteRm);
            ModCommand = new DelegateCommand(OnModAction, CanExecuteMod);

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
            try
            {
                _winAdd.ShowDialog();

            
                if (_winAdd.ViewModel.Ans && !string.IsNullOrWhiteSpace(_winAdd.ViewModel.Streamer.Name) && !string.IsNullOrEmpty(_winAdd.ViewModel.Streamer.Name))
                    _listStreamers.Add(_winAdd.ViewModel.Streamer);
            
            NotifyPropertyChanged("Streamer");
            NotifyPropertyChanged("ListStreamers");
            }
            catch (Exception ){

                _winEr = new Window_error("Unknown streamer !");
                _winEr.ShowDialog();
            }
        }

        private void OnRmAction(object o) {
            
            if (Streamer != null)
            {
                ButtonPressedEvent.GetEvent().Handler += CloseRmView;
                _winRm = new Window_remove(_ans);
                _winRm.Name = "Remove";
                _winRm.ShowDialog();

                if (_winRm.ViewModel.Ans)
                    ListStreamers.Remove(Streamer);
            }
        }


        private void OnModAction(object obj) {


            if (Streamer != null)
            {
                ButtonPressedEvent.GetEvent().Handler += CloseModView;
                string nameTmp = Streamer.Name;

                _winMod = new Window_modify(Streamer);
                _winMod.Name = "Modify";
                try
                {
                    _winMod.ShowDialog();

                    if (!_winMod.ViewModel.Ans || string.IsNullOrWhiteSpace(_winMod.ViewModel.Streamer.Name) || string.IsNullOrEmpty(_winMod.ViewModel.Streamer.Name))
                        Streamer.Name = nameTmp;

                    NotifyPropertyChanged("Streamer");
                    NotifyPropertyChanged("ListStreamers");
                }

                catch (Exception)
                {

                    _winEr = new Window_error("Unknown streamer !");
                    _winEr.ShowDialog();
                }
            }
        }
        #endregion

        #region CanExecute
        private bool CanExecuteAdd(object o) {
            return true;
        }
        private bool CanExecuteMod(object o) {
            return true;
        }
        private bool CanExecuteRm(object o) {
            return true;
        }
        #endregion

        #region CloseEvents
        private void CloseAddView(object sender, EventArgs e) {
            _winAdd.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseAddView;
        }
        private void CloseRmView(object sender, EventArgs e) {
            _winRm.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseRmView;
        }
        private void CloseModView(object sender, EventArgs e) {
            _winMod.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseModView;
        }
        #endregion
    }
}