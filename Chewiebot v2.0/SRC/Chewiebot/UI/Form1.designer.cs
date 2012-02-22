using System.Windows.Forms;
using System.ComponentModel;

namespace Chewie
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( Form1 ) );
            this.commandBox = new System.Windows.Forms.TextBox ();
            this.setbtn = new System.Windows.Forms.Button ();
            this.button1 = new System.Windows.Forms.Button ();
            this.SuspendLayout ();
            // 
            // commandBox
            // 
            this.commandBox.BackColor = System.Drawing.Color.Black;
            this.commandBox.ForeColor = System.Drawing.Color.White;
            this.commandBox.Location = new System.Drawing.Point ( 12, 12 );
            this.commandBox.Multiline = true;
            this.commandBox.Name = "commandBox";
            this.commandBox.ReadOnly = true;
            this.commandBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commandBox.Size = new System.Drawing.Size ( 963, 341 );
            this.commandBox.TabIndex = 0;
            // 
            // setbtn
            // 
            this.setbtn.Location = new System.Drawing.Point ( 819, 359 );
            this.setbtn.Name = "setbtn";
            this.setbtn.Size = new System.Drawing.Size ( 75, 23 );
            this.setbtn.TabIndex = 1;
            this.setbtn.Text = "Settings";
            this.setbtn.UseVisualStyleBackColor = true;
            this.setbtn.Click += new System.EventHandler ( this.setbtn_Click );
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point ( 900, 359 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size ( 75, 23 );
            this.button1.TabIndex = 2;
            this.button1.Text = "About";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler ( this.button1_Click );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 987, 388 );
            this.Controls.Add ( this.button1 );
            this.Controls.Add ( this.setbtn );
            this.Controls.Add ( this.commandBox );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject ( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Chewiebot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler ( this.Form1_FormClosing );
            this.Load += new System.EventHandler ( this.Form1_Load );
            this.ResumeLayout ( false );
            this.PerformLayout ();

        }

        #endregion

        public System.Windows.Forms.TextBox commandBox;
      
       private void Form1_FormClosing(object sender, FormClosingEventArgs e)
       {
           System.Environment.Exit ( 0 );
        }
        public void SendtoChewie ( string myString )
        {
            this.Invoke ( ( MethodInvoker ) delegate
            {
                commandBox.AppendText ( myString );
                commandBox.AppendText ( "\n" );
            } );
        }

        private Button setbtn;
        private Button button1;
    }
}