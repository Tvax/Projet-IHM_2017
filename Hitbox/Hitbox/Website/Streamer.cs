using Hitbox.Library;
using System;
using System.Collections.ObjectModel;

namespace Hitbox.Website {
    class Streamer : NotifyPropertyChangedBase {
        private String _name;
        public int _viewers;
        private int _followers;
        private Boolean _subActivated;
        private int _subCount;
        private ObservableCollection<Emote> _listEmotes;

        public String Name { get; set; }
        public int Viewers { get; set; }
        public int Followers { get; set; }
        public Boolean SubActivated { get; set; }
        public int SubCount { get; set; }
        public ObservableCollection<Emote> ListEmotes { get; set; }
        //displayed by creating an copy in MainView??
        //


        public Streamer() {
            defaultStreamer();

            if (!checkUsernameExists())
                throw new Exception("Username Unknown");//open error window
            
            getStreamerEmotes();
        }

        private void getStreamerEmotes() {
            //_listEmotes = images
        }

        private void defaultStreamer() {
            this._name = "Name";
            this._viewers = 0;
            this._followers = 0;
            this._subActivated = false;
            this._subCount = 0;
        }

        private Boolean checkUsernameExists() {
            //call api
            //if error return false

            return true;
        }

        public override string ToString() {
            return string.Format(_name);
        }
    }

}
