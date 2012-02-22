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
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace ChewieBot_SteamRE
{
    public class BotCommandHandler
    {
        public BotCommandHandler()
        {
        }

        /// <summary>
        /// Returns back status message that can be passed back to chat
        /// </summary>
        /// <param name="message">line from chat</param>
        /// <returns></returns>
        public string CommandHandler(string message)
        {
            /* TODO: Change this to handle command lookups cleaner.  Switch to XML file or Web Service.  
             * Having to handle commands like this due to backwards compatibility with a txt file
             */ 
            string returnMessage = string.Empty;

            cmdList cmds = new cmdList();
            cmds.settingfile();

            if (message == "!commands")
            {
                for (int i = 0; i < 128; ++i)
                {
                    if (cmds.cmdsfromfile[i, 0] == null)
                        break;

                    returnMessage += cmds.cmdsfromfile[i, 2] + " ";

                }
            }
            else
            {
                for (int i = 0; i < 128; ++i)
                {
                    if (cmds.cmdsfromfile[i, 0] == null)
                        break;
                    if (cmds.cmdsfromfile[i, 2].Equals(message))
                    {
                        string webURL = cmds.cmdsfromfile[i, 4];
                        int count = 0;
                        byte[] chewieBuf = new byte[8192];
                        string htmlClear = @"<(.|\n)*?>";

                        WebRequest request;
                        WebResponse response;
                        Stream stream;

                        try
                        {
                            request = WebRequest.Create(webURL);
                            
                            response = request.GetResponse();
                            stream = response.GetResponseStream();

                            do
                            {
                                count = stream.Read(chewieBuf, 0, chewieBuf.Length);

                                if (count != 0)
                                {
                                    returnMessage = Encoding.ASCII.GetString(chewieBuf, 0, count);
                                }
                            }
                            while (count > 0);
                            try
                            {
                                // clean up the string removing html parses

                                foreach (Match match in Regex.Matches(returnMessage, htmlClear))
                                    returnMessage = Regex.Replace(returnMessage, htmlClear, "");
                            }
                            catch (Exception)
                            {
                            }
                        }
                        catch (Exception)
                        {
                            // TODO: Timeouts?
                            //Console.WriteLine(ex.ToString());
                            //returnMessage = "Command Failed";
                            //Console.WriteLine("Command " + message + " has an invalid url.");
                        }
                    }
                }
            }

            return returnMessage;
        }

        /// <summary>
        /// Checks to see if the message is a command
        /// </summary>
        /// <param name="message">line from chat</param>
        /// <returns></returns>
        public bool isCommand(string message)
        {
            bool retVal = false;

            cmdList cmds = new cmdList();
            cmds.settingfile();

            if (message == "!commands")
            {
                retVal = true;
            }

            for (int i = 0; i < 128; ++i)
            {
                if (cmds.cmdsfromfile[i, 0] == null)
                    break;
                if (cmds.cmdsfromfile[i, 2].Equals(message))
                {
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}
