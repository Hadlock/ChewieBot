using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chewie
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent ();
        }

        private void Form1_Load ( object sender, EventArgs e )
        {

        }

        private void setbtn_Click ( object sender, EventArgs e )
        {
            settingForm settingForm = new settingForm ();
            settingForm.loadSettings ();
            settingForm.ShowDialog ();
        }

        private void button1_Click ( object sender, EventArgs e )
        {
            MessageBox.Show (  "Original version by Hadlock: http://nearlydeaf.com"
                            + "\nCurrent version maintained by mekkanare ( @ mail.ru )" + "\nUsing OpenSteamWorks: http://opensteamworks.org/" + 
                            "\nHosted at: http://github.com/Hadlock/ChewieBot", "Chewiebot 2.0" );
        }
    }
}
