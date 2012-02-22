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
using System.IO;
using System.Collections;
using SteamKit2;


namespace ChewieBot_SteamRE
{

    /* TODO: Um, clean this up to use an XML as opposed to a txt file
     * 
     */ 

    class cmdList
    {
        public SteamID chatRoom;
        public string Command;
        public string url;

        // Array Lists for parsing the setting txt file.
        public string[,] cmdsfromfile = new string[128, 5];

        // Get file settings
        public void settingfile()
        {
            string fileloc = Environment.CurrentDirectory + "\\settings.txt";
            if (File.Exists(fileloc))
            {
                int x = 0;
                int y = 0;
                StreamReader fileRd = new StreamReader(fileloc);
                while (!(fileRd.EndOfStream))
                {
                    string[] splitStream;
                    splitStream = fileRd.ReadLine().Split(',');
                    foreach (string part in splitStream)
                    {
                        cmdsfromfile[x, y] = splitStream[y];
                        ++y;
                    }
                    ++x;
                    y = 0;
                }
                fileRd.Close();
            }
        }
    }
}
