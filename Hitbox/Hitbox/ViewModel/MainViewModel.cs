using Hitbox.Events;
using Hitbox.Library;
using Hitbox.Website;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

//TODO add banner in background?
//TODO mettre ca dans add  if (_winAdd.ViewModel.Ans && !string.IsNullOrWhiteSpace(_winAdd.ViewModel.Streamer.Name) && !string.IsNullOrEmpty(_winAdd.ViewModel.Streamer.Name)) _listStreamers.Add(_winAdd.ViewModel.Streamer);
// Et aussi dans modifier !
//TODO Check si user exists avant de l'add a la DB
//TODO Check si user != null or white before de l'add a DB


namespace Hitbox.ViewModel {
    class MainViewModel : NotifyPropertyChangedBase {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ModCommand { get; set; }
        public DelegateCommand RmCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand QuitCommand { get; set; }

        private Window_add _winAdd { get; set; }
        private Window_remove _winRm { get; set; }
        private Window_modify _winMod { get; set; }
        private Window_error _winEr;
        private Window_login _winLog;

        private ObservableCollection<Streamer> _listStreamers;
        private Streamer _streamer;
        private Member _member;
        private bool _ans;

        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection _connection;

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
            ButtonPressedEvent.GetEvent().Handler += CloseLogView;
            _winLog = new Window_login(_member = new Member());
            _winLog.ShowDialog();
            ListStreamers = new ObservableCollection<Streamer>();
            LoadStreamers();
            //if (User.Username == null || User.Password == null) App.Current.Shutdown();

            AddCommand = new DelegateCommand(OnAddAction, CanExecuteAdd);
            RmCommand = new DelegateCommand(OnRmAction, CanExecuteRm);
            ModCommand = new DelegateCommand(OnModAction, CanExecuteMod);
            SaveCommand = new DelegateCommand(OnSaveAction, CanExecuteSave);
            QuitCommand = new DelegateCommand(OnQuitAction, CanExecuteQuit);
            

        }



        #region Database
        private void LoadStreamers() {
            server = "185.28.20.242";
            database = "u552241586_hit";
            uid = "u552241586_tvax";
            password = "motdepasse";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            _connection = new MySqlConnection(connectionString);

            Select();
        }

        private void Select() {
            string query = "SELECT * FROM `Streamer` WHERE Username = '" + _member.Name + "'";

            //Create a list to store the result
            List<Streamer> list = new List<Streamer>();

            if (OpenConnection()) {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read()) {
                    ListStreamers.Add(new Streamer {
                        Name = (string)dataReader["name"],
                        Viewers = (string)dataReader["viewers"],
                        Followers = (string)dataReader["followers"],
                        Live = (string)dataReader["live"],
                        SubActivated = (string)dataReader["sub"],
                        ProfilePic = new BitmapImage(new Uri((string)dataReader["picture"])),
                    });

                    foreach (Streamer s in ListStreamers) {
                        s.GetLastFollowers();
                    }
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
        }

        public void Save() {
            if (OpenConnection()) {

                MySqlCommand dropAll = new MySqlCommand("DELETE FROM Streamer WHERE Username = '" + _member.Name + "'", _connection);
                dropAll.ExecuteNonQuery();

                if (ListStreamers.Count > 0) {

                    foreach (Streamer s in ListStreamers) {
                        string query = "INSERT INTO Streamer VALUES('" + _member.Name + "','" + s.Name + "','" + s.Viewers + "','" + s.Followers + "','" + s.Live + "','" + s.SubActivated + "','" + s.ProfilePic + "');";

                        try {
                            MySqlCommand cmd = new MySqlCommand(query, _connection);
                            cmd.ExecuteNonQuery();
                        }
                        catch {
                            _winEr = new Window_error("Duplicate streamer name !");
                            _winEr.ShowDialog();
                        }
                    }

                    CloseConnection();
                }
            }
        }

        private bool OpenConnection() {
            try {
                _connection.Open();
                return true;
            }
            catch (MySqlException e) {
                switch (e.Number) {
                    case 0:
                        _winEr = new Window_error("Cannot connect to server.");
                        _winEr.ShowDialog();
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        _winEr = new Window_error("Invalid username/password, please try again");
                        _winEr.ShowDialog();
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection() {
            try {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex) {
                _winEr = new Window_error(ex.Message);
                _winEr.ShowDialog();
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        #endregion

        #region OnActions
        private void OnAddAction(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;

            _winAdd = new Window_add(_streamer = new Streamer());
            _winAdd.Name = "Add";
            try {
                _winAdd.ShowDialog();


                if (_winAdd.ViewModel.Ans && !string.IsNullOrWhiteSpace(_winAdd.ViewModel.Streamer.Name) && !string.IsNullOrEmpty(_winAdd.ViewModel.Streamer.Name))
                    _listStreamers.Add(_winAdd.ViewModel.Streamer);

                NotifyPropertyChanged("Streamer");
                NotifyPropertyChanged("ListStreamers");
            }
            catch (Exception) {
                _winAdd.Close();
                _winEr = new Window_error("Unknown streamer !");
                _winEr.ShowDialog();
            }
        }

        private void OnRmAction(object o) {

            if (Streamer != null) {
                ButtonPressedEvent.GetEvent().Handler += CloseRmView;
                _winRm = new Window_remove(_ans);
                _winRm.Name = "Remove";
                _winRm.ShowDialog();

                if (_winRm.ViewModel.Ans)
                    ListStreamers.Remove(Streamer);
            }
        }


        private void OnModAction(object obj) {

            if (Streamer != null) {
                ButtonPressedEvent.GetEvent().Handler += CloseModView;
                string nameTmp = Streamer.Name;

                _winMod = new Window_modify(Streamer);
                _winMod.Name = "Modify";
                try {
                    _winMod.ShowDialog();

                    if (!_winMod.ViewModel.Ans || string.IsNullOrWhiteSpace(_winMod.ViewModel.Streamer.Name) || string.IsNullOrEmpty(_winMod.ViewModel.Streamer.Name))
                        Streamer.Name = nameTmp;

                    NotifyPropertyChanged("Streamer");
                    NotifyPropertyChanged("ListStreamers");
                }

                catch (Exception) {
                    _winMod.Close();
                    _winEr = new Window_error("Unknown streamer !");
                    _winEr.ShowDialog();
                }
            }
        }

        private void OnSaveAction(object o) {
            Save();
        }
        private void OnQuitAction(object o) {
            Save();
            App.Current.Shutdown();
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
        private bool CanExecuteSave(object o) {
            return true;
        }
        private bool CanExecuteQuit(object o) {
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
        private void CloseLogView(object sender, EventArgs e) {
            _winLog.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseLogView;
        }
        #endregion
    }
}
