﻿namespace Chewie
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Steam4NET;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System.Globalization;
    //using System.Threading;
    //using System.ComponentModel;
    //using System.Windows.Forms;
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


       // Parse ChatRoom messages
        void ChatRoomMsg ( ChatRoomMsg_t chatMsg )
        {
            byte[] msgData = new byte[ 1024 * 4 ];
            EChatEntryType chatType = EChatEntryType.k_EChatEntryTypeInvalid;
            ulong chatter = 0;

            int len = getChatMsg ( clientFriends.Interface, chatMsg.m_ulSteamIDChat, ( int ) chatMsg.m_iChatID, ref chatter, msgData, msgData.Length, ref chatType );

            len = Clamp ( len, 1, msgData.Length );

            LogVars log = new LogVars ();

            log.IsGroupMsg = true;
            log.ChatRoom = chatMsg.m_ulSteamIDChat;
            log.ChatRoomName = getChatName ( clientFriends.Interface, log.ChatRoom );

            log.Sender = new CSteamID ( chatMsg.m_ulSteamIDUser );
            log.SenderName = steamFriends.GetFriendPersonaName ( log.Sender );

            log.Reciever = log.Sender;
            log.RecieverName = log.SenderName;

            log.Message = Encoding.UTF8.GetString ( msgData, 0, len );
            log.Message = log.Message.Substring ( 0, log.Message.Length - 1 );
            log.MessageType = chatType;
            log.MessageTime = DateTime.Now;


           // if ( log.ChatRoomName == "Battlfield Goons" )
           // {


                if ( log.Message == "!lolocaust" )
                {
                    // If somebody says the appropriate message in chat room, run this.

                    string testStr = "Acknowledged. Processing...";
                    sendChatMsg ( clientFriends.Interface, log.ChatRoom, chatType, System.Text.Encoding.UTF8.GetBytes ( testStr ), testStr.Length + 1 );

                    //ProcessStartInfo kickScript = new ProcessStartInfo ( "python", "kick.py" );
                    //kickScript.UseShellExecute = false;
                    //kickScript.CreateNoWindow = true;

                    //Sends an HTTP request to the specific file
                    byte[] chewieBuf = new byte[ 8192 ];
                    HttpWebRequest chewieWeb = ( ( HttpWebRequest ) WebRequest.Create ( "http://path/to/lolocaust.php " ) );
                    HttpWebResponse chewieRes = ( ( HttpWebResponse ) chewieWeb.GetResponse () );

                    Stream readChewie = chewieRes.GetResponseStream ();

                    int count = 0;

                    do
                    {
                        count = readChewie.Read ( chewieBuf, 0, chewieBuf.Length );

                        if ( count != 0 )
                        {
                            testStr = Encoding.ASCII.GetString ( chewieBuf, 0, count );
                        }
                    }
                    while ( count > 0 );

                    // clean up the string removing html parses
                    string[] htmlClear = { "<br />", "<strong>", "</strong>" };

                    for ( int i = 0; i < 3; ++i )
                    {
                        testStr = testStr.Replace ( htmlClear[ i ], "" );
                    }


                    //Process startApp = Process.Start ( kickScript );
                    // while ( !startApp.HasExited )
                    //    startApp.WaitForExit ( 10 ); // Give the process time to exit.

                    //testStr = "A pubbie has been successfully kicked at [" + log.MessageTime + "]";
                    //if ( startApp.HasExited )

                    sendChatMsg ( clientFriends.Interface, log.ChatRoom, chatType, System.Text.Encoding.UTF8.GetBytes ( testStr ), testStr.Length + 1 );
                }
           // }

            string roomMessage = ( "[" +log.MessageTime + "] From " + log.SenderName + " in " + log.ChatRoomName + ": " + log.Message);
            Program.parsetoChewie ( roomMessage );
        }

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