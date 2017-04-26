using Hitbox.Library;
using Hitbox.Website;
using System.Collections.ObjectModel;

//TODO add banner in background?

namespace Hitbox.ViewModel {
    class MainViewModel : NotifyPropertyChangedBase {

        private ObservableCollection<Streamer> _listStreamers;
        private Streamer _streamer;

        public ObservableCollection<Streamer> ListStreamers {
            get { return _listStreamers; }
            set { _listStreamers = value; }
        }
        public Streamer Streamer {
            get { return _streamer; }
            set { _streamer = value;
                NotifyPropertyChanged("Streamer");
                NotifyPropertyChanged("ListStreamers");
            }
        }

        public MainViewModel() {
            ListStreamers = new ObservableCollection<Streamer>();
            Streamer = new Streamer();
            ListStreamers.Add(Streamer);
        }
    }
}
