using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Chewie
{
    public partial class settingForm : Form
    {
        public settingForm ()
        {
            InitializeComponent ();
        }

        private void label1_Click ( object sender, EventArgs e )
        {

        }

        private void cmdFileView_SelectedIndexChanged ( object sender, EventArgs e )
        {

        }
        private void removeToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( cmdFileView.SelectedItems.Count == 0 )
                return;
            cmdFileView.Items.Remove ( cmdFileView.SelectedItems[ 0 ] );
        }
        private void settingForm_FormClosing ( object sender, FormClosingEventArgs e )
        {
            savesettings ();
            Program.parsetoChewie ( "Reloading settings..." );
            if ( !( Program.chatparser.getLogSettings () ) )
            {
                Program.parsetoChewie ( "Error reloading settings!" );
            }
            else
                Program.parsetoChewie ( "Settings reloaded." );
        }

        private void addCMDButton_Click ( object sender, EventArgs e )
        {
            ListViewItem item = new ListViewItem ( roomNameBox.Text );
            
            if (silentRoomCheck.Checked == true )
            {
                item.SubItems.Add ( "Yes" );
            }
            else
                item.SubItems.Add ( "No" );

            item.SubItems.Add ( commandNameBox.Text );

            if ( privateCMDCheck.Checked == true )
            {
                item.SubItems.Add ( "Yes" );
            }
            else
                item.SubItems.Add ( "No" );

            item.SubItems.Add ( urlBox.Text );

            cmdFileView.Items.Add ( item );

            silentRoomCheck.Checked = false;
            commandNameBox.Text = "";
            privateCMDCheck.Checked = false;
            urlBox.Text = "";
        }

        private void toolTip1_Popup ( object sender, PopupEventArgs e )
        {

        }

        private void settingForm_Load ( object sender, EventArgs e )
        {

        }
    }
}
