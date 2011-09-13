///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// Chewiebot: A steam-chat integrated bot used to kick players from          //
// Bad Company 2 / Battlefield 3 servers                                     //
// This bot's code is based on Chat Logger by VoiDeD and was written         //
// by mekkanare [ mekkanare ( at ) mail ( dot ) ru ]                         //
// Under the WTFPL: http://sam.zoy.org/wtfpl/                                //
//                                                                           //
// Special thanks to VoiDeD and his team for the API at:                     //
// http://opensteamworks.org                                                 //
//                                                                           //
///////////////////////////////////////////////////////////////////////////////


namespace Chewie
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Steam4NET;

    static class Program
    {

        static ChatParser chatparser;
        static Form1 Chewiewin;
        //static bool isRunning = true;
        public static void parsetoChewie ( string myString )
        {
            Chewiewin.SendtoChewie ( myString );
        }

        [STAThread]
        static void Main ()
        {

                bool firstProc;
                Mutex ChewieMutex = new Mutex
                (
                true,
                "Chewiebot_86374h91h",
                out firstProc
                );

                //if ( !firstProc )
                //  return;

                chatparser = new ChatParser ();
                Application.SetCompatibleTextRenderingDefault ( false );
                Application.EnableVisualStyles ();
                Chewiewin = new Form1 ();
                Chewiewin.Show ();

                bool waited = false;

                Chewiewin.SendtoChewie ( "Loading ChewieBot - Press \"X\" to close..." );

                if ( !chatparser.GetSteamClient () )
                {
                    int i = 0;
                    Chewiewin.SendtoChewie ( "Unable get SteamClient interface!  Retrying." );
                    while ( !chatparser.GetSteamClient() )
                    {
                        chatparser.GetSteamClient ();
                        ++i;
                        if ( i == 10 )
                            MessageBox.Show ( "Error! Unable to find Steam!  Check that it's installed and is updated!" );
                    }
                    return;
                }


                if ( !chatparser.GetPipe () )
                {
                    Chewiewin.SendtoChewie ( "Steam is currently not running.. Waiting for it to startup." );
                    waited = true;

                    while ( !chatparser.GetPipe () )
                    {
                        Application.DoEvents ();
                        Thread.Sleep ( 100 );
                    }
                    
                    // get the pipe again just in case
                    if ( !chatparser.GetPipe () )
                    {
                        Chewiewin.SendtoChewie ( "Error getting steam pipe after steam startup!" );
                        return;
                    }
                }
                else
                {
                    Chewiewin.SendtoChewie ( "Got Steam Pipe! Continuing..." );
                }

                while ( !chatparser.GetUser () )
                {
                    Application.DoEvents ();

                    Thread.Sleep ( 100 );
                }

                // wait for steam to full start itself
                if ( waited )
                    Thread.Sleep ( 100 );

                // get the user again
                if ( !chatparser.GetUser () )
                {
                    Chewiewin.SendtoChewie ( "Error getting steam user after steam startup!" );
                    return;
                }
                else
                {
                    Chewiewin.SendtoChewie ( "Got Steam User! Continuing..." );
                }


                if ( !chatparser.GetInterface () )
                {
                    Chewiewin.SendtoChewie ( "Unable to get SteamFriends interface!" );
                    return;
                }
                else
                {
                    Chewiewin.SendtoChewie ( "Got SteamFriends interface!  Waiting for messages..." );
                }
                while ( firstProc )
                {
                    
                    Application.DoEvents ();
                    Thread.Sleep ( 10 );
                }
                GC.KeepAlive ( ChewieMutex );
          
            //static void endProg ()
            // {
            //   Environment.Exit ( 0 );
            //}     
        }
    }
}
