#$answer = MsgBox(4, "Run...", "Wanna run this script?")

#If $answer = 7 Then
#	MsgBox(0, "Exit...", "Ok, you have chosen to exit the script, ByeBye!")
#	Exit
#EndIf

#Run("notepad.exe")

WinWaitActive("Battlefield Goons - Group Chat")

#Send("Chewie Bot is down for maintenance. Please stand by..." & @CR)
#Send("Today's time/date is " & @HOUR & ":" & @MIN &  ":" & @SEC & "CST - " & @MDAY & "/" & @MON & "/" & @YEAR & @CR)
Send("Message ends." & @CR)

#Send("- --------- Help Menu -------- -" & @CR & "Current Commands. All commands are preceeded by a {!}" & @CR & "lolocaust - kicks a pubbie" & @CR)

#Sleep(5000)

#Send("help - accesses this menu" & @CR & "page - pages an admin" & @CR & "godmode - ?????" & @CR & "- - ----------------------------- - -" & @CR)

#WinClose("Untitled - Notepad")

#WinWaitActivate("Notepad")

#Sleep(500)
#Send("{ENTER}")
#Sleep(1000)

#WinWaitClose("Untitled - Notepad")

#MsgBox(0, "Finished", "Script finished his work")

Exit