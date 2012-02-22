//////////////////////////////////////////////////////////////////////////////////
//                                                                              //
// Please read LICENSE.TXT for License Information                              //
//                                                                              //
// ChewieBot: A Steam Chat Bot used to kick players or run other commands       //
// via PHP scripts                                                              //
//                                                                              //
// Thanks to VoiDeD and SteamRE https://bitbucket.org/VoiDeD/steamre/overview   //
// for the Steam Client API                                                     //
//                                                                              //
// Written by c355n4 nhrules (at) hotmail.com                                   // 
//                                                                              // 
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamKit2;
using System.Configuration;

namespace ChewieBot_SteamRE
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Add check for instance already running

            //login info
            string userID = ConfigurationManager.AppSettings["userID"];
            string userPW = ConfigurationManager.AppSettings["userPW"];
            string botName = ConfigurationManager.AppSettings["botName"];

            List<SteamID> chatRooms = new List<SteamID>();

            ulong chatSteamID;
            if (ulong.TryParse(ConfigurationManager.AppSettings["chatSteamID"], out chatSteamID))
            {
                //List of Chats to join
                // TODO: multiple chatrooms
                chatRooms.Add(chatSteamID);
            }
            else
            {
                Console.WriteLine("Exit Code 10: Failed to Load SteamID");
                Environment.Exit(10);
            }

            //Initiliaze Steam Client
            SteamClient steamClient = new SteamClient();
            SteamUser steamUsr = steamClient.GetHandler<SteamUser>();
            SteamFriends steamFrds = steamClient.GetHandler<SteamFriends>();

            //Connect to Steam Network
            steamClient.Connect();

            while (true)
            {
                // start listening
                CallbackMsg msg = steamClient.WaitForCallback(true); 

                msg.Handle<SteamClient.ConnectedCallback>(callback =>
                    {
                        if (callback.Result != EResult.OK)
                        {
                            Console.WriteLine("Exit Code 1: Failed to Connect to Steam Network - " + callback.Result.ToString());
                            Environment.Exit(1);
                            // connect fail
                        }
                        else
                        {
                            Console.WriteLine("Connected to Steam Network");

                            steamUsr.LogOn(new SteamUser.LogOnDetails
                            {
                                Username = userID,
                                Password = userPW,
                            });
                        }
                    });

                msg.Handle<SteamUser.LoggedOnCallback>(callback =>
                    {
                        if (callback.Result != EResult.OK)
                        {
                            Console.WriteLine("Exit Code 2: Logon Failure - " + callback.Result.ToString());
                            //Environment.Exit(2);
                            // logon fail
                        }
                        else
                        {
                            Console.WriteLine("Logged into Steam Network");

                            // Join Chat
                            // if you set PersonaState without setting PersonaName it goes to [unknown] and GetPersonaName doesn't work?
                            //steamFrds.SetPersonaName(steamFrds.GetPersonaName());
                            steamFrds.SetPersonaName(botName);
                            steamFrds.SetPersonaState(EPersonaState.Online);

                            foreach (SteamID steamID in chatRooms)
                            {
                                steamFrds.JoinChat(steamID);
                            }
                        }
                    });

                msg.Handle<SteamFriends.ChatEnterCallback>(callback =>
                    {
                        if (callback.EnterResponse != EChatRoomEnterResponse.Success)
                        {
                            Console.WriteLine("Exit Code 3: Failure to Enter Chat" + callback.EnterResponse.ToString());
                            //Environment.Exit(3);
                            // join chat fail
                        }
                        else
                        {
                            Console.WriteLine("Joined Chat");
                            foreach (SteamID steamID in chatRooms)
                            {
                                steamFrds.SendChatRoomMessage(steamID, EChatEntryType.ChatMsg, "Now listening");
                            }
                        }
                    });

                msg.Handle<SteamFriends.ChatMsgCallback>(callback =>
                    {
                        // TODO: Handle this in BotCommandHandler.cs

                        BotCommandHandler cmd = new BotCommandHandler();

                        foreach (SteamID steamID in chatRooms)
                        {
                            if (cmd.isCommand(callback.Message))
                            {

                                steamFrds.SendChatRoomMessage(steamID, EChatEntryType.ChatMsg, "Acknowledged. Processing...");

                                string retMsg = cmd.CommandHandler(callback.Message);
                                if (!string.IsNullOrEmpty(retMsg))
                                {
                                    steamFrds.SendChatRoomMessage(steamID, EChatEntryType.ChatMsg, cmd.CommandHandler(callback.Message));
                                }
                            }   
                        }  
                    });
               
            }
        }
    }
}
