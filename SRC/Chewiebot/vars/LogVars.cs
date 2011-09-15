
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
        public ArrayList roomSettingsName = new ArrayList ();
        public ArrayList roomSettingsCMD = new ArrayList ();
        public ArrayList roomSettingsGET = new ArrayList ();
        public string[ , ] roomCMDGET = new string[ 64, 129 ];

        // Get file settings
        public void settingfile ()
        {
            string fileloc = Environment.CurrentDirectory + "\\settings.txt";
            if ( File.Exists ( fileloc ) )
            {
                StreamReader fileRd = new StreamReader ( fileloc );
                int roomCNT = 0;
                int GETCNT = 1;

                string nameVar = "name=";
                string cmdVar = "cmd=";
                string getVar = "url=";

                ArrayList fileContents = new ArrayList ();
                
                // store settings in an arraylist so we can verify the contents
                while ( !(fileRd.EndOfStream ) )
                {
                    fileContents.Add (fileRd.ReadLine());
                }
                
                // After we store the file in our array, we can free up those resources
                fileRd.Close ();

                // Here's where the actual contents are stored in the multi-dimensional array
                // It is appended like so:
                // roomCMDGET [ 0, 0 ] = room name
                // [ 0, 1 ] = command
                // [ 0, 2 ] = GET
                // [ 0, 3 ] = command
                // [ 0, 4 ] = GET
                // etc.

                for ( int i = 0; i < fileContents.Count; ++i )
                {
                    if ( ( fileContents[ i ].ToString () ).StartsWith ( nameVar ) )
                    {
                        roomCMDGET[ roomCNT, 0 ] = fileContents[ i ].ToString ().Remove ( 0, 5 );
                        ++roomCNT;
                    }

                    if ( ( fileContents[ i ].ToString () ).StartsWith ( cmdVar ) )
                    {
                        roomCMDGET[ roomCNT -1, GETCNT ] = fileContents[ i ].ToString ().Remove ( 0, 4 );
                        ++GETCNT;
                    }

                    if ( ( fileContents[ i ].ToString () ).StartsWith ( getVar ) )
                    {
                        roomCMDGET[ roomCNT -1, GETCNT ] = fileContents[ i ].ToString ().Remove ( 0, 4 );
                        ++GETCNT;
                    }

                }
            }
        }
    }
}
