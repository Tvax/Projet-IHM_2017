using System.Collections.Generic;

namespace Hitbox.API {
    public class Request {
        public string @this { get; set; }
    }

    public class Follower {
        public string followers { get; set; }
        public string user_name { get; set; }
        public string user_id { get; set; }
        public string user_logo { get; set; }
        public string user_logo_small { get; set; }
        public string follow_id { get; set; }
        public string follower_user_id { get; set; }
        public string follower_notify { get; set; }
        public string date_added { get; set; }
    }

    public class RootObject {
        public Request request { get; set; }
        public List<Follower> followers { get; set; }
        public string max_results { get; set; }
    }
}
