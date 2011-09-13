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
            this.textBox1 = new System.Windows.Forms.TextBox ();
            this.SuspendLayout ();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point ( 12, 12 );
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size ( 963, 341 );
            this.textBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 987, 365 );
            this.Controls.Add ( this.textBox1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Chewiebot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler ( this.Form1_FormClosing );
            this.Load += new System.EventHandler ( this.Form1_Load );
            this.ResumeLayout ( false );
            this.PerformLayout ();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox1;
      
       private void Form1_FormClosing(object sender, FormClosingEventArgs e)
       {
           System.Environment.Exit ( 0 );
        }
        public void SendtoChewie ( string myString )
        {
            this.Invoke ( ( MethodInvoker ) delegate
            {
                textBox1.AppendText ( myString );
                textBox1.AppendText ( "\n" );
            } );
        }
    }
}