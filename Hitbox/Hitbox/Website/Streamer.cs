using Hitbox.API;
using Hitbox.Library;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net;
using static Hitbox.API.Request;
using System.Windows.Media.Imaging;

namespace Hitbox.Website {
    public class Streamer : NotifyPropertyChangedBase {
        private string _name;
        public string _viewers;
        private string _followers;
        private string _live;
        private string _subActivated;
        private BitmapImage _profilePic;
        private ObservableCollection<BitmapImage> _listProfilePicture = new ObservableCollection<BitmapImage>();
        private Window_error _winErr;
        private string _json;

        private WebClient _webClient;
        private string _url;
        private bool _userExists;

        public ObservableCollection<BitmapImage> ListProfilePicture {
            get { return _listProfilePicture; }
            set {
                _listProfilePicture = value;
                NotifyPropertyChanged("ListProfilePicture");
                NotifyPropertyChanged("BitmapImage");
            }
        }

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        public string Viewers {
            get { return _viewers; }
            set { _viewers = value; }
        }

        public string Followers {
            get { return _followers; }
            set { _followers = value; }
        }

        public string Live {
            get { return _live; }
            set { _live = value; }
        }

        public string SubActivated {
            get { return _subActivated; }
            set { _subActivated = value; }
        }

        public BitmapImage ProfilePic {
            get { return _profilePic; }
            set { _profilePic = value; }
        }

        public bool UserExists {
            get { return _userExists; }
            set { _userExists = value; }
        }

        public Streamer() {
            _webClient = new WebClient();    
        }

        public void LoadStreamerInfo() {
            getUser();
            if (UserExists) {
                getViews();
                GetLastFollowers();
            }
        }

        private void getUser() {
            _url = "https://api.hitbox.tv/user/" + _name;

            string json = _webClient.DownloadString(_url);
            User.RootObject user = new User.RootObject();
            try {
                user = JsonConvert.DeserializeObject<User.RootObject>(json);
            }
            catch {
                _winErr = new Window_error("Unknown streamer !");
                _winErr.ShowDialog();
                UserExists = false;
                return;
            }
            UserExists = true;
            /*
            if (string.IsNullOrEmpty(user.user_name)) {
                throw new Exception(e);//open error window
            }
            */
            _name = user.user_name;
            if (String.IsNullOrEmpty(user.followers))
                _followers = "0";
            else
                _followers = user.followers;
            _profilePic = new BitmapImage(new Uri("https://edge.sf.hitbox.tv" + user.user_logo));

            if (user.is_live == "1")
                _live = "On";
            else
                _live = "Off";


            if (user.user_partner == "1")
                _subActivated = "On";
            else
                _subActivated = "Off";
        }

        private void getViews() {
            _url = "https://api.hitbox.tv/media/views/" + _name;
            string json = _webClient.DownloadString(_url);
            Views.RootObject views = JsonConvert.DeserializeObject<Views.RootObject>(json);

            if (views.total_live_views == "false")
                _viewers = "0";
            else
             _viewers = views.total_live_views;
          
        }

        public void GetLastFollowers() {
            ListProfilePicture.Clear();
            _url = "https://api.hitbox.tv/followers/user/" + _name + "?limit=50";
            //mettre une image de base lorsque ya aps de followers avec un try catch ou check si error 404 or not
            try {
                _json = _webClient.DownloadString(_url);
            }
            catch {
                //ListProfilePicture.Add(new BitmapImage(new Uri("https://i.stack.imgur.com/ymxcL.png")));
                return;
            }
            RootObject lastFollowers = JsonConvert.DeserializeObject<Request.RootObject>(_json);

            ObservableCollection<Follower> lastF = new ObservableCollection<Follower>(lastFollowers.followers);
            foreach (Follower f in lastF) {
                ListProfilePicture.Add(new BitmapImage(new Uri("https://edge.sf.hitbox.tv" + f.user_logo_small)));
            }
        }

        public override string ToString() {
            return string.Format(_name);
        }
    }

}