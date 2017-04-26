using Hitbox.Library;
using System;
using System.Collections.ObjectModel;

namespace Hitbox.Website {
    class Streamer : NotifyPropertyChangedBase {
        private String _name;
        private int _followers;
        private Boolean _subActivated;
        private int _subCount;
        private ObservableCollection<Streamer> _listeStreamers;
        private Streamer _streamer;

        public String Name { get; set; }
        public int Followers { get; set; }
        public Boolean SubActivated { get; set; }
        public int SubCount { get; set; }
        public ObservableCollection<Streamer> ListeStreamers { get; set; }
        public Streamer Streamers {
            get { return _streamer; }
            set {
                _streamer = value;
                NotifyPropertyChanged("Streamer");
                NotifyPropertyChanged("ListeStreamers");
            }
        }


        private Boolean checkUsernameExists() {
            //call api
            //if error return false

            return true;
        }



    }
    
}
