using System;
using Hitbox.Library;
using Hitbox.Website;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Hitbox.ViewModel {
    public class LoginViewModel {
        public DelegateCommand RegCommand { get; set; }
        public DelegateCommand LogCommand { get; set; }

        private List<Member> _sqlContent;

        private MySqlConnection _connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        private Member _member;

        public LoginViewModel(Member member) {
            _member = member;
            RegCommand = new DelegateCommand(OnRegAction, CanExecuteReg);
            LogCommand = new DelegateCommand(OnLogAction, CanExecuteLog);

            server = "mysql.hostinger.fr";
            database = "u552241586_hit";
            uid = "u552241586_tvax";
            password = "motdepasse";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            _connection = new MySqlConnection(connectionString);

            if (!OpenConnection()) {
                //throw catch demerde toi
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
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
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
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<Member> Select() {
            string query = "SELECT * FROM User";

            //Create a list to store the result
            List<Member> list = new List<Member>();


            //Open connection
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

        #region Actions
        private void OnLogAction(object obj) {
            _sqlContent = Select();
            if (_sqlContent.Any(m => m.Name == _member.Name && m.Password == _member.Password)) {

            }
        }
        //else //error name unknown
  
    private void OnRegAction(object obj) {
        throw new NotImplementedException();
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