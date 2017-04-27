namespace Hitbox.API {
    class User {
        public class RootObject {
            public string user_name { get; set; }
            public string user_cover { get; set; }
            public string user_status { get; set; }
            public string user_logo { get; set; }
            public string user_logo_small { get; set; }
            public bool user_is_broadcaster { get; set; }
            public string followers { get; set; }
            public string user_partner { get; set; }
            public string user_id { get; set; }
            public string is_live { get; set; }
            public string live_since { get; set; }
            public string twitter_account { get; set; }
            public string twitter_enabled { get; set; }
            public string user_beta_profile { get; set; }
        }
    }
}