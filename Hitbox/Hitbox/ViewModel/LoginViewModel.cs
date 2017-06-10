using System;
using Hitbox.Library;
using Hitbox.Website;
using MySql.Data;
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
        private string server;
        private string database;
        private string uid;
        private string password;

        public Member Member {
            get { return _member; }
            set { _member = value; }
        }

        public LoginViewModel(Member member) {
            _member = member;
            RegCommand = new DelegateCommand(OnRegAction, CanExecuteReg);
            LogCommand = new DelegateCommand(OnLogAction, CanExecuteLog);

            server = "185.28.20.242";
            database = "u552241586_hit";
            uid = "u552241586_tvax";
            password = "motdepasse";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            _connection = new MySqlConnection(connectionString);

        }

        public List<Member> Select() {
            string query = "SELECT * FROM User";

            //Create a list to store the result
            List<Member> list = new List<Member>();

            if (OpenConnection()) {

                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read()) {
                    list.Add(new Member { Name = (string)dataReader["name"], Password = (string)dataReader["password"] });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
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

            //open connection
            if (OpenConnection()) {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, _connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
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