using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;


namespace Chewie
{
    partial class settingForm
    {
 
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container ();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( settingForm ) );
            this.label1 = new System.Windows.Forms.Label ();
            this.roomNameBox = new System.Windows.Forms.TextBox ();
            this.silentRoomCheck = new System.Windows.Forms.CheckBox ();
            this.label2 = new System.Windows.Forms.Label ();
            this.commandNameBox = new System.Windows.Forms.TextBox ();
            this.privateCMDCheck = new System.Windows.Forms.CheckBox ();
            this.label3 = new System.Windows.Forms.Label ();
            this.urlBox = new System.Windows.Forms.TextBox ();
            this.addCMDButton = new System.Windows.Forms.Button ();
            this.cmdFileView = new System.Windows.Forms.ListView ();
            this.Room = ( ( System.Windows.Forms.ColumnHeader ) ( new System.Windows.Forms.ColumnHeader () ) );
            this.Silent = ( ( System.Windows.Forms.ColumnHeader ) ( new System.Windows.Forms.ColumnHeader () ) );
            this.Command = ( ( System.Windows.Forms.ColumnHeader ) ( new System.Windows.Forms.ColumnHeader () ) );
            this.PrivateCmd = ( ( System.Windows.Forms.ColumnHeader ) ( new System.Windows.Forms.ColumnHeader () ) );
            this.URL = ( ( System.Windows.Forms.ColumnHeader ) ( new System.Windows.Forms.ColumnHeader () ) );
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip ( this.components );
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolTip1 = new System.Windows.Forms.ToolTip ( this.components );
            this.contextMenuStrip1.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point ( 13, 13 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 66, 13 );
            this.label1.TabIndex = 0;
            this.label1.Text = "Room Name";
            this.label1.Click += new System.EventHandler ( this.label1_Click );
            // 
            // roomNameBox
            // 
            this.roomNameBox.Location = new System.Drawing.Point ( 16, 30 );
            this.roomNameBox.Name = "roomNameBox";
            this.roomNameBox.Size = new System.Drawing.Size ( 100, 20 );
            this.roomNameBox.TabIndex = 1;
            this.toolTip1.SetToolTip ( this.roomNameBox, "The name of the room for the command" );
            // 
            // silentRoomCheck
            // 
            this.silentRoomCheck.AutoSize = true;
            this.silentRoomCheck.Location = new System.Drawing.Point ( 126, 30 );
            this.silentRoomCheck.Name = "silentRoomCheck";
            this.silentRoomCheck.Size = new System.Drawing.Size ( 76, 17 );
            this.silentRoomCheck.TabIndex = 3;
            this.silentRoomCheck.Text = "Silent Cmd";
            this.toolTip1.SetToolTip ( this.silentRoomCheck, "Silent commands won\'t let the user know they triggered." );
            this.silentRoomCheck.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point ( 215, 13 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 85, 13 );
            this.label2.TabIndex = 4;
            this.label2.Text = "Command Name";
            // 
            // commandNameBox
            // 
            this.commandNameBox.Location = new System.Drawing.Point ( 218, 30 );
            this.commandNameBox.Name = "commandNameBox";
            this.commandNameBox.Size = new System.Drawing.Size ( 100, 20 );
            this.commandNameBox.TabIndex = 5;
            this.toolTip1.SetToolTip ( this.commandNameBox, "Prepend the command name with !" );
            // 
            // privateCMDCheck
            // 
            this.privateCMDCheck.AutoSize = true;
            this.privateCMDCheck.Location = new System.Drawing.Point ( 325, 30 );
            this.privateCMDCheck.Name = "privateCMDCheck";
            this.privateCMDCheck.Size = new System.Drawing.Size ( 109, 17 );
            this.privateCMDCheck.TabIndex = 6;
            this.privateCMDCheck.Text = "Private Command";
            this.toolTip1.SetToolTip ( this.privateCMDCheck, "Private commands won\'t appear in chat at all." );
            this.privateCMDCheck.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point ( 434, 13 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size ( 29, 13 );
            this.label3.TabIndex = 7;
            this.label3.Text = "URL";
            // 
            // urlBox
            // 
            this.urlBox.Location = new System.Drawing.Point ( 434, 30 );
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size ( 450, 20 );
            this.urlBox.TabIndex = 8;
            this.toolTip1.SetToolTip ( this.urlBox, "The remote URL to be activated" );
            // 
            // addCMDButton
            // 
            this.addCMDButton.Location = new System.Drawing.Point ( 890, 28 );
            this.addCMDButton.Name = "addCMDButton";
            this.addCMDButton.Size = new System.Drawing.Size ( 75, 23 );
            this.addCMDButton.TabIndex = 9;
            this.addCMDButton.Text = "Add";
            this.addCMDButton.UseVisualStyleBackColor = true;
            this.addCMDButton.Click += new System.EventHandler ( this.addCMDButton_Click );
            // 
            // cmdFileView
            // 
            this.cmdFileView.Columns.AddRange ( new System.Windows.Forms.ColumnHeader[] {
            this.Room,
            this.Silent,
            this.Command,
            this.PrivateCmd,
            this.URL} );
            this.cmdFileView.ContextMenuStrip = this.contextMenuStrip1;
            this.cmdFileView.FullRowSelect = true;
            this.cmdFileView.GridLines = true;
            this.cmdFileView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.cmdFileView.Location = new System.Drawing.Point ( 16, 57 );
            this.cmdFileView.MultiSelect = false;
            this.cmdFileView.Name = "cmdFileView";
            this.cmdFileView.Size = new System.Drawing.Size ( 949, 309 );
            this.cmdFileView.TabIndex = 10;
            this.cmdFileView.UseCompatibleStateImageBehavior = false;
            this.cmdFileView.View = System.Windows.Forms.View.Details;
            this.cmdFileView.SelectedIndexChanged += new System.EventHandler ( this.cmdFileView_SelectedIndexChanged );
            // 
            // Room
            // 
            this.Room.Text = "Room";
            this.Room.Width = 150;
            // 
            // Silent
            // 
            this.Silent.Text = "Silent?";
            // 
            // Command
            // 
            this.Command.Text = "Command";
            this.Command.Width = 150;
            // 
            // PrivateCmd
            // 
            this.PrivateCmd.Text = "Private?";
            // 
            // URL
            // 
            this.URL.Text = "URL";
            this.URL.Width = 525;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem} );
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size ( 153, 48 );
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler ( this.removeToolStripMenuItem_Click );
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler ( this.toolTip1_Popup );
            // 
            // settingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 977, 378 );
            this.Controls.Add ( this.cmdFileView );
            this.Controls.Add ( this.addCMDButton );
            this.Controls.Add ( this.urlBox );
            this.Controls.Add ( this.label3 );
            this.Controls.Add ( this.privateCMDCheck );
            this.Controls.Add ( this.commandNameBox );
            this.Controls.Add ( this.label2 );
            this.Controls.Add ( this.silentRoomCheck );
            this.Controls.Add ( this.roomNameBox );
            this.Controls.Add ( this.label1 );
            this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject ( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "settingForm";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler ( this.settingForm_FormClosing );
            this.Load += new System.EventHandler ( this.settingForm_Load );
            this.contextMenuStrip1.ResumeLayout ( false );
            this.ResumeLayout ( false );
            this.PerformLayout ();

        }

        #endregion



        private Label label1;
        private TextBox roomNameBox;
        private CheckBox silentRoomCheck;
        private Label label2;
        private TextBox commandNameBox;
        private CheckBox privateCMDCheck;
        private Label label3;
        private TextBox urlBox;
        private Button addCMDButton;
        private ListView cmdFileView;
        private ColumnHeader Room;
        private ColumnHeader Silent;
        private ColumnHeader Command;
        private ColumnHeader PrivateCmd;
        private ColumnHeader URL;


        public void loadSettings ()
        {
            

            if ( File.Exists ( fileloc ) )
            {
                cmdFileView.BeginUpdate ();

                StreamReader filerd = new StreamReader ( fileloc );
                

                while ( !( filerd.EndOfStream ) )
                {
                    string line = filerd.ReadLine ();
                    string[] content = line.Split ( ',' );
                    ListViewItem item = new ListViewItem ( content[ 0 ].ToString () );
                    item.SubItems.Add ( content[ 1 ] );
                    item.SubItems.Add ( content[ 2 ] );
                    item.SubItems.Add ( content[ 3 ] );
                    item.SubItems.Add ( content[ 4 ] );
                    cmdFileView.Items.Add ( item );
                }
                filerd.Close ();
                cmdFileView.EndUpdate ();
            }
        }

        private void savesettings ()
        {
            StreamWriter filewrite = new StreamWriter ( fileloc );
            filewrite.AutoFlush = true;

            foreach ( ListViewItem item in cmdFileView.Items )
            {
                filewrite.Write ( item.Text );
                for ( int i = 1; i < 5; ++i )
                {
                    filewrite.Write ( "," + item.SubItems[ i ].Text );
                }
                filewrite.WriteLine ();
            }
            filewrite.Close ();
        }

        private string fileloc = Environment.CurrentDirectory + "\\settings.txt";
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolTip toolTip1;
    }
}