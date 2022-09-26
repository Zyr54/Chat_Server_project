using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Chat{

    public class Message{

        public int _messageID { get; private set; }
        public String _messageCore { get; set; }
        [ForeignKey("User")]
        public int _userIDFK { get; set; }
        public virtual User User { get; set; }
        
        public Message(){
            this._messageCore = "";
        }

        public Message(User user, String messageText){
            this._messageCore = messageText;
            this.User = user;
        }

    }

}