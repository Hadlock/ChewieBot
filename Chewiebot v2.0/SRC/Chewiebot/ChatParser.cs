namespace Chewie
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Steam4NET;
    using System.Runtime.InteropServices;
    using System.Net;
    using System.IO;

    // Handling messages for group chat involves using a Virtual Table to call
    // the functions.
    [UnmanagedFunctionPointer ( CallingConvention.ThisCall )]
    delegate Int32 NativeGetChatRoomEntry ( IntPtr thisobj, UInt64 steamIDchat, Int32 iChatID, ref UInt64 steamIDuser, byte[] pvData, Int32 cubData, ref EChatEntryType peChatEntryType );

    [UnmanagedFunctionPointer ( CallingConvention.ThisCall )]
    delegate string NativeGetChatRoomName ( IntPtr thisobj, UInt64 steamIDchat );

    [UnmanagedFunctionPointer ( CallingConvention.ThisCall )]
    delegate bool NativeSendChatMsg ( IntPtr thisobj, UInt64 steamIDchat, EChatEntryType eChatEntryType, byte[] pvMsgBody, Int32 cubMsgBody );

    class ChatParser
    {

        IClientEngine clientEngine;
        IClientFriends clientFriends;
        ISteamClient008 steamClient;
        ISteamFriends002 steamFriends;

        int pipe;
        int user;

        NativeGetChatRoomEntry getChatMsg;
        NativeGetChatRoomName getChatName;
        NativeSendChatMsg sendChatMsg;

        DateTime sixtysec;

        //Callback<FriendChatMsg_t> chatCallback;
        Callback<ChatRoomMsg_t> chatRoomCallback;

        public ChatParser ()
        {
            //chatCallback = new Callback<FriendChatMsg_t> ( ChatMsg, FriendChatMsg_t.k_iCallback );
            chatRoomCallback = new Callback<ChatRoomMsg_t> ( ChatRoomMsg, ChatRoomMsg_t.k_iCallback );
        }

        public bool GetSteamClient ()
        {
            if ( !Steamworks.Load () )
                return false;

            steamClient = Steamworks.CreateInterface<ISteamClient008> ( "SteamClient008" );
            clientEngine = Steamworks.CreateInterface<IClientEngine> ( "CLIENTENGINE_INTERFACE_VERSION002" );

            // This is a one minute timer so the !commands can't be spammed in chat
            sixtysec = DateTime.Now;

            Program.parsetoChewie ( "Chewiebot started at: " + sixtysec.ToString() );
            if ( steamClient == null )
                return false;

            if ( clientEngine == null )
                return false;

            return true;
        }

        public bool GetPipe ()
        {
            if ( pipe != 0 )
            {
                steamClient.ReleaseSteamPipe ( pipe );
            }

            pipe = steamClient.CreateSteamPipe ();

            if ( pipe == 0 )
                return false;

            return true;
        }

        public bool GetUser ()
        {
            if ( user != 0 )
            {
                steamClient.ReleaseUser ( pipe, user );
            }

            user = steamClient.ConnectToGlobalUser ( pipe );

            if ( user == 0 )
                return false;

            return true;
        }

        public bool GetInterface ()
        {
            steamFriends = Steamworks.CastInterface<ISteamFriends002> ( steamClient.GetISteamFriends ( user, pipe, "SteamFriends002" ) );

            if ( steamFriends == null )
                return false;

            clientFriends = Steamworks.CastInterface<IClientFriends> ( clientEngine.GetIClientFriends ( user, pipe, "CLIENTFRIENDS_INTERFACE_VERSION001" ) );

            if ( clientFriends == null )
                return false;

            // Virtual Table
            VTable vTable = new VTable ( clientFriends.Interface );

            getChatMsg = vTable.GetFunc<NativeGetChatRoomEntry> ( 99 );
            getChatName = vTable.GetFunc<NativeGetChatRoomName> ( 117 );
            sendChatMsg = vTable.GetFunc<NativeSendChatMsg> ( 98 );

            CallbackDispatcher.SpawnDispatchThread ( pipe );

            return true;
        }


        int Clamp ( int value, int min, int max )
        {
            if ( value < min )
                return min;

            if ( value > max )
                return max;

            return value;
        }

        // The class called that handles Chat room messages
        LogVars chewieBrain = new LogVars ();

        // defines the arrays for the Chat room parsing
        public bool getLogSettings ()
        {
            chewieBrain.settingfile ();
            return true;
        }

        // Parse ChatRoom messages
        void ChatRoomMsg ( ChatRoomMsg_t chatMsg )
        {
            byte[] msgData = new byte[ 1024 * 4 ];
            EChatEntryType chatType = EChatEntryType.k_EChatEntryTypeInvalid;
            ulong chatter = 0;

            int len = getChatMsg ( clientFriends.Interface, chatMsg.m_ulSteamIDChat, ( int ) chatMsg.m_iChatID, ref chatter, msgData, msgData.Length, ref chatType );

            len = Clamp ( len, 1, msgData.Length );

            chewieBrain.IsGroupMsg = true;
            chewieBrain.ChatRoom = chatMsg.m_ulSteamIDChat;
            chewieBrain.ChatRoomName = getChatName ( clientFriends.Interface, chewieBrain.ChatRoom );

            chewieBrain.Sender = new CSteamID ( chatMsg.m_ulSteamIDUser );
            chewieBrain.SenderName = steamFriends.GetFriendPersonaName ( chewieBrain.Sender );

            chewieBrain.Reciever = chewieBrain.Sender;
            chewieBrain.RecieverName = chewieBrain.SenderName;

            chewieBrain.Message = Encoding.UTF8.GetString ( msgData, 0, len );
            chewieBrain.Message = chewieBrain.Message.Substring ( 0, chewieBrain.Message.Length - 1 );
            chewieBrain.MessageType = chatType;
            chewieBrain.MessageTime = DateTime.Now;

            string webURL = "";
            bool silent = false;

            string roomMessage = ( "[" + chewieBrain.MessageTime + "] From " + chewieBrain.SenderName + " in " + chewieBrain.ChatRoomName + ": " + chewieBrain.Message );
            Program.parsetoChewie ( roomMessage );

            for ( int i = 0; i < 128; ++i )
            {
                //Program.parsetoChewie ( "CMD: " + log.roomCMDGET[ cnt, comval ] );

                // Again, why bother with empty arrays?
                if ( chewieBrain.cmdsfromfile[ i, 0 ] == null )
                    break;

                if ( chewieBrain.cmdsfromfile[ i, 0 ].Equals ( chewieBrain.ChatRoomName ) && chewieBrain.cmdsfromfile[ i, 2 ].Equals ( chewieBrain.Message ) && chewieBrain.Message != "!commands" )
                {
                    webURL = chewieBrain.cmdsfromfile[ i, 4 ];
                    if ( chewieBrain.cmdsfromfile[ i, 1 ] == "Yes" )
                        silent = true;

                    handleCommands ( silent, i, webURL, chewieBrain.cmdsfromfile[ i, 2 ] );
                    break;
                }
                else if ( chewieBrain.Message == "!commands" )
                {
                    if ( DateTime.Now > sixtysec.AddSeconds ( 60 ) )
                    {
                        sixtysec = DateTime.Now;
                        handleCMDLIST ();
                        break;
                    }
                }

            }



        }


        void handleCMDLIST (  )
        {
            string strCMDLIST = "Commands are: ";

            for ( int i = 0; i < 128; ++i )
            {
                if ( chewieBrain.cmdsfromfile[ i, 0 ] == null )
                    break;
                if ( chewieBrain.cmdsfromfile[ i, 0 ].Equals ( chewieBrain.ChatRoomName ) && chewieBrain.cmdsfromfile[ i, 3] == "No" )
                {
                    strCMDLIST += chewieBrain.cmdsfromfile[ i, 2 ] + " ";
                }
                else
                {
                    continue;
                }

            }
            sendChatMsg ( clientFriends.Interface, chewieBrain.ChatRoom, chewieBrain.MessageType, System.Text.Encoding.UTF8.GetBytes ( strCMDLIST ), strCMDLIST.Length + 1 );
        }

        // handles commands if there is one to handle
        void handleCommands ( bool silent, int i, string webURL, string command )
        {
            string respondSTR = "";
            string answer = "Acknowledged, processing...";
            int count = 0;
            byte[] chewieBuf = new byte[ 8192 ];
            string htmlClear = @"<(.|\n)*?>";
            HttpWebRequest chewieWeb;
            HttpWebResponse chewieRes;
            Stream readChewie;


            if ( silent == false && chewieBrain.cmdsfromfile[ i, 3 ] == "No" )
                sendChatMsg ( clientFriends.Interface, chewieBrain.ChatRoom, chewieBrain.MessageType, System.Text.Encoding.UTF8.GetBytes ( answer ), answer.Length + 1 );

            //Sends an HTTP request to the specific file

            try
            {
                chewieWeb = ( ( HttpWebRequest ) WebRequest.Create ( webURL ) );
                chewieRes = ( ( HttpWebResponse ) chewieWeb.GetResponse () );
                readChewie = chewieRes.GetResponseStream ();
                do
                {
                    count = readChewie.Read ( chewieBuf, 0, chewieBuf.Length );

                    if ( count != 0 )
                    {
                        respondSTR = Encoding.ASCII.GetString ( chewieBuf, 0, count );
                    }
                }
                while ( count > 0 );

                try
                {



                    // clean up the string removing html parses

                    foreach ( Match match in Regex.Matches ( respondSTR, htmlClear ) )
                        respondSTR = Regex.Replace ( respondSTR, htmlClear, "" );

                    if ( silent == true )
                    {
                        if ( chewieBrain.cmdsfromfile[ i, 3 ] == "Yes" )
                        {
                            Program.parsetoChewie ( "Command " + chewieBrain.cmdsfromfile[ i, 2 ] + " completed silently." );
                        }
                        else
                            sendChatMsg ( clientFriends.Interface, chewieBrain.ChatRoom, chewieBrain.MessageType, System.Text.Encoding.UTF8.GetBytes ( respondSTR ), respondSTR.Length + 1 );
                    }
                    else if ( silent == false )
                    {
                        if ( chewieBrain.cmdsfromfile[ i, 3 ] == "No" )
                        {
                            sendChatMsg ( clientFriends.Interface, chewieBrain.ChatRoom, chewieBrain.MessageType, System.Text.Encoding.UTF8.GetBytes ( respondSTR ), respondSTR.Length + 1 );
                        }
                        else
                            Program.parsetoChewie ( "Command " + chewieBrain.cmdsfromfile[ i, 2 ] + " completed silently." );
                    }
                }
                catch ( Exception )
                {
                }
            }
            catch ( Exception  )
            {
                Program.parsetoChewie ( "Command " + command + " has an invalid url.  Check your settings!" );
            }

        }

       
        // Uncomment this if you want to see private messages in the window
        /*void ChatMsg ( FriendChatMsg_t chatMsg )
        {
            byte[] msgData = new byte[ 1024 * 4 ];
            EChatEntryType type = EChatEntryType.k_EChatEntryTypeChatMsg;

            CSteamID reciever = new CSteamID ( chatMsg.m_ulReceiver );

            int msgLength = steamFriends.GetChatMessage ( chatMsg.m_ulReceiver, ( int ) chatMsg.m_iChatID, msgData, msgData.Length, ref type );

            if ( type != EChatEntryType.k_EChatEntryTypeChatMsg )
                return;

            msgLength = Clamp ( msgLength, 1, msgData.Length );

            LogVars log = new LogVars ();

            log.Sender = new CSteamID ( chatMsg.m_ulSender );
            log.SenderName = steamFriends.GetFriendPersonaName ( log.Sender );

            log.Reciever = reciever;
            log.RecieverName = steamFriends.GetFriendPersonaName ( log.Reciever );

            log.Message = Encoding.UTF8.GetString ( msgData, 0, msgLength );
            log.Message = log.Message.Substring ( 0, log.Message.Length - 1 );
            log.MessageType = type;
            
            string privMessage = ( log.SenderName + ": " + log.Message );
            Program.parsetoChewie ( privMessage );

   
        }*/
    }
}
