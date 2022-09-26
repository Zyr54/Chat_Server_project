using System.ComponentModel.DataAnnotations.Schema;

namespace Chat{

    public class User{

        public int _userID { get; private set; }
        public String _username { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public User(){
            this._username = "";
        }
        public User(String username){
            this._username = username;
            this.Messages = new HashSet<Message>();
        }

    }
}