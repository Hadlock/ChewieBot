
namespace Chewie
{
    using System;
    using Steam4NET;
    class LogVars {

        public bool IsGroupMsg;

        public CSteamID ChatRoom;
        public string ChatRoomName;

        public CSteamID Sender;
        public string SenderName;

        public CSteamID Reciever;
        public string RecieverName;

        public string Message;

        public DateTime MessageTime;

        public EChatEntryType MessageType;
    }
}
