///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// Chewiebot 3.0: A steam-chat integrated bot used to kick players from      //
// Bad Company 2 / Battlefield 3 servers                                     //
// This bot's code is based on Chat Logger by VoiDeD and was written         //
// by Cessna                                                                 //
// Under the WTFPL: http://sam.zoy.org/wtfpl/                                //
//                                                                           //
// Special thanks to VoiDeD and his team for the API at:                     //
// http://opensteamworks.org, and the SteamRE team at                        //
// https://bitbucket.org/VoiDeD/steamre/wiki/Home                            //
///////////////////////////////////////////////////////////////////////////////

/* This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://sam.zoy.org/wtfpl/COPYING for more details. */

Version 3.0.1 2/18/2012
Written by Cessna/Hadlock/Wolffenstein

Included PHP-based RCON scripts for Battlefield 3 PC servers (player Kick/Stats/Status)
Included PHP-based random kick reason system
Included Ruby-based Chewiebot v1.0 source

Version 3.0 2/17/2012
Written by Cessna

Complete rewrite in visual Studio of Chewiebot 2.0 from scratch. Open Steam Works was depreciated and this has been written using SteamRE API.
New version/rewrite of ChewieBot written to use the new SteamRE API.  
Backward compatibility with the old ChewieBot settings.txt. 
Newer versiona of ChewieBot can probably be found here: https://github.com/cessna/ChewieBot_SteamRE


Version 2.5 11/9/2011:
Written by Mekkanare
The new settings.txt is parsed in the following order:

Room Name,Silent Command? Yes/No,Command Name,Private Command? Yes/No, URL

An example is provided in the settings.txt file.

A silent command will appear on the !commands list, but will only show its results.
A private command will not appear on the !commands list, and will not echo its results in chat.
 
Version 2.0 - 9/13/2011
Written by Mekkanare

ChewieBot is a Steam Group Chat-based bot that will monitor one or multiple steam group chats for a specific string or strings and then call a specific internet URL such as Dice's command.py stored on a webhost to kick players or transmit say/yell messages to the server. Chewiebot can echo back the php response in to chat. This makes him a fully functional listen and send Steam chat bot. Chewiebot is the second half of the WookWook platform and provides a front end user interface to the web back end. More info on WookWook can be had here: https://github.com/Hadlock/WookWook

ChewieBot relies heavily on the Open Steam Works (OSW) API. See here http://opensteamworks.org 

From time to time Valve changes the names of functions and breaks group chat logging due to the OSW API. This has happened about 4 times in the last six months, but is a quick fix generally.

Version 1.0 - 5/1/2011
Written by Hadlock

ChewieBot v1.0 is a hodge-podge of scripting and various programs loosely tied together. Other than the end user functionality, they are completley unrelated. 1.0 source (including ruby and autoit scripts) are now included (2/18/2012) - Hadlock.

