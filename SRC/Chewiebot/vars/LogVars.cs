
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
        public string[ , ] roomNAMECMD = new string[ 64, 64 ];
        public string[ , ] roomCMDGET = new string[ 64, 64 ];

        // Get file settings
        public void settingfile ()
        {
            string fileloc = Environment.CurrentDirectory + "\\settings.txt";
            if ( File.Exists ( fileloc ) )
            {
                StreamReader fileRd = new StreamReader ( fileloc );
                int roomCNT = 0;
                int GETCNT = 1;
                int sortNAMECMD = 1;

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

              // NAMECMD is as follows:
              // 0, 0 | Room Name
              // 0, n | Commands
              // CMDGET is:
              // 0, 0 | CMD
              // 0, 1 | GET
              // 0, 2 | CMD
              // 0, 3 | GET
              // 1, 0 | Room 2, CMD

                for ( int i = 0; i < fileContents.Count; ++i )
                {
                    if ( ( fileContents[ i ].ToString () ).StartsWith ( nameVar ) )
                    {
                        roomNAMECMD[ roomCNT, 0 ] = fileContents[ i ].ToString ().Remove ( 0, 5 );
                        sortNAMECMD = 1;
                        GETCNT = 1;
                        ++roomCNT;
                    }

                    if ( ( fileContents[ i ].ToString () ).StartsWith ( cmdVar ) )
                    {
                        if ( GETCNT == 1 )
                        {
                            roomNAMECMD[ roomCNT - 1, GETCNT ] = fileContents[ i ].ToString ().Remove ( 0, 4 );
                            roomCMDGET[ roomCNT - 1, GETCNT - 1 ] = roomNAMECMD[ roomCNT - 1, GETCNT ];
                        }
                        else if ( !( GETCNT % 2 == 0 ) && !( GETCNT == 1 ) )
                        {
                            ++sortNAMECMD;
                            roomNAMECMD[ roomCNT - 1, sortNAMECMD ] = fileContents[ i ].ToString ().Remove ( 0, 4 );
                            roomCMDGET[ roomCNT - 1, GETCNT - 1 ] = fileContents[ i ].ToString ().Remove ( 0, 4 );// roomNAMECMD[ roomCNT - 1, GETCNT - 1 ];
                        }
                        
                        ++GETCNT;
                    }

                    if ( ( fileContents[ i ].ToString () ).StartsWith ( getVar ) )
                    {
                        if ( GETCNT % 2 == 0 )
                        roomCMDGET [ roomCNT -1, GETCNT -1] = fileContents[ i ].ToString ().Remove ( 0, 4 );
                        ++GETCNT;
                    }

                }
            }
        }
    }
}
