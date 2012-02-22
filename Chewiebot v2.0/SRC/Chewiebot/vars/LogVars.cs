
namespace Chewie
{
    using System;
    using System.IO;
    using Steam4NET;
    using System.Collections;
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

        // Array Lists for parsing the setting txt file.
        public string[ , ] cmdsfromfile = new string[ 128, 5 ];

        // Get file settings
        public void settingfile ()
        {
            string fileloc = Environment.CurrentDirectory + "\\settings.txt";
            if ( File.Exists ( fileloc ) )
            {
                int x = 0;
                int y = 0;
                StreamReader fileRd = new StreamReader ( fileloc );
                while ( !( fileRd.EndOfStream ) )
                {
                    string[] splitStream;
                    splitStream = fileRd.ReadLine ().Split ( ',' );
                    foreach ( string part in splitStream )
                    {
                        cmdsfromfile[ x, y ] = splitStream[ y ];
                        ++y;
                    }
                    ++x;
                    y = 0;
                }
                fileRd.Close ();
            }
        }
    }
}
