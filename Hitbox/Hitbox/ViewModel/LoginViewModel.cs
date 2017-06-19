using System;
using Hitbox.Library;
using Hitbox.Website;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using Hitbox.Events;

namespace Hitbox.ViewModel {
    public class LoginViewModel {
        public DelegateCommand RegCommand { get; set; }
        public DelegateCommand LogCommand { get; set; }

        private Window_error _winEr;

        private List<Member> _sqlContent;
        private Member _member;

        private MySqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;
        private string _password;

        public Member Member {
            get { return _member; }
            set { _member = value; }
        }

        public LoginViewModel(Member member) {
            _member = member;
            RegCommand = new DelegateCommand(OnRegAction, CanExecuteReg);
            LogCommand = new DelegateCommand(OnLogAction, CanExecuteLog);

            _server = "185.28.20.242";
            _database = "u552241586_hit";
            _uid = "u552241586_tvax";
            _password = "motdepasse";
            string connectionString;
            connectionString = "SERVER=" + _server + ";" + "DATABASE=" +
            _database + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";
            _connection = new MySqlConnection(connectionString);

        }

        public List<Member> Select() {
            string query = "SELECT * FROM User";

            List<Member> list = new List<Member>();

            if (OpenConnection()) {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read()) {
                    list.Add(new Member { Name = (string)dataReader["name"], Password = (string)dataReader["password"] });
                }

                dataReader.Close();
                CloseConnection();

                return list;
            }
            else {
                return list;
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
                        break;

                    case 1045:
                        _winEr = new Window_error("Invalid username/password, please try again");
                        _winEr.ShowDialog();   
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
                
                return false;
            }
        }

        #region Actions
        private void OnLogAction(object obj) {
            _sqlContent = Select();

            if (_sqlContent.Any(m => m.Name == _member.Name && m.Password == _member.Password)) {
                ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
            }
            else {
                _winEr = new Window_error("Invalid login or password");
                _winEr.ShowDialog();
            }
        }


        public void Insert() {
            string query = "INSERT INTO User (Name, Password) VALUES('" + _member.Name + "','" + _member.Password + "')";

            if (OpenConnection()) {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                if(string.IsNullOrEmpty(_member.Name) || string.IsNullOrWhiteSpace(_member.Name) || string.IsNullOrEmpty(_member.Password) || string.IsNullOrWhiteSpace(_member.Password)) {
                    _winEr = new Window_error("Invalid login or password");
                    _winEr.ShowDialog();
                    return;
                }
                try {
                    cmd.ExecuteNonQuery();
                }
                catch {
                    CloseConnection();
                    _winEr = new Window_error("Username already taken !");
                    _winEr.ShowDialog();
                }
                CloseConnection();
            }
        }

        private void OnRegAction(object obj) {
            Insert();
        }
        #endregion

        #region CanExecute
        private bool CanExecuteLog(object obj) {
            return true;
        }

        private bool CanExecuteReg(object obj) {
            return true;
        }
        #endregion
    }
}