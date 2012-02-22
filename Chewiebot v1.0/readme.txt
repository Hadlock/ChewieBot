For instructions and other details please check the official chat logger facepunch thread: http://www.facepunch.com/showthread.php?t=840880

Chat Log is licensed under the MIT license, check license.txt for more details


---------

ChewieBot V1.0
By Hadlock

Licenced under the WTFPL http://sam.zoy.org/wtfpl/


for the au3 files, they will need to be compiled using AutoIt (http://www.autoitscript.com/site/autoit/)

from my blog here http://nearlydeaf.com/?p=933

First off, I’ll admit, this is a kludge built upon at least two dirty hacks. But it works!

We were running in to problems with getting preferred users in to the video game server when full. Battlefield and the Frostbite engine supports a sort of “VIP list”, but it breaks if you have more than 500 users on the list. It was suggested that we do what our predecssors did, which was have an IRC bot handle kicking “pubbies”, or people not on the VIP list. While IRC is still the superior chat protocol, many more people have Steam installed, and you can invoke the steam overlay from in game. You also can’t send server-filling announcements to all your friends via IRC.

The problem is, even though Steam is the superior chat platform for online gaming, there is no easy way to access the chat logs, since steam doesn’t support chat logging. Lucikly someone by the name of Voided struck up the Open Steamworks Project. Out of it came the Steam Chat Logger, which only a week ago (beta 6) offered group chat logging support, saving in an easy-to-read plain text format. From here I got to work.

Ingredients for a Steam Chat Bot (alpha quality)


1. Virtual Box
2. Steam Chat Logger (beta 6 and above)
3. Powerful scripting language (I went with Ruby)
4. GUI Automation Software (AutoIt)

The whole instance runs on an instance of Virtual Box. This allows you to normally use your steam account, while having a dedicated steam account to hang out in your specified group chat room. The whole vbox instance running the bot uses about 0.5% cpu on my HTPC (C2D 2.6ghz)

And that’s about it. Steam chat logger is your listen device. There is a ruby script that runs in a loop, parsing the chat logger text, looking for a specific keyword, in this case ” !command” (no quotation marks). When it finds !command, it will run any number of preset commands. In our case, it runs a PHP script that will kick a pubbie from the game server to make room for a VIP, along with a customized kick message. The ruby script can also call python scripts and run local executable (.exe) files.

The speaking portion of the bot is limited to however many canned responses you need to give it. I used AutoIt to create scripts to watch for a particular window (in this case, hard-coded to the group chat room window) to become active, and then type out a string of text, interrupted by carriage returns and wait/sleep commands. Once you sort out your AutoIt scripts, you can carve them in stone and have it compile them as executable files, rather than counting on the system to have an interpreter installed. This way the ruby/python script can simply call the correct response (.exe) as needed.

If I did this over again, I would write the script in Python. Dice has some example scripts for expressing RCON commands to the server in Python. Instead of calling PHP scripts that then call Python scripts, the chat bot could call everything directly.

Bugs: Windows File Lock is a bitch. I currently have my ruby script set to sleep for 15 seconds after each loop. The reason is that while the ruby script is processing the chat log, the chat log app can’t write to it. If you issue a command while ruby is processing the file, it won’t get written to, and won’t be processed during the next loop. Setting the timer to 15 seconds is an uneasy middle ground between acceptable latency and dropped requests (about 6%).