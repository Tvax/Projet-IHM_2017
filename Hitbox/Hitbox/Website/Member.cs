namespace Hitbox.Website {
    public class Member {

        private string _name;
        private string _pswd;

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        public string Password {
            get { return _pswd; }
            set { _pswd = value; }
        }
    }
}
