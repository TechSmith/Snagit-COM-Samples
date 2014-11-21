//---------------------------------------------------------------------------
// Filename: Form1.cs - Contains the code for the form for the SnagIt COM
//           server C# simple sample.
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Simple
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      // Declare the SnagIt ImageCapture object.  NOTE: You must add a 
      // reference to the project in order for this line to compile.  
      private SNAGITLib.ImageCapture SnagImg;

      private System.Windows.Forms.Label Label1;
      private System.Windows.Forms.GroupBox GroupBox2;
      private System.Windows.Forms.Button BtnRegion;
      private System.Windows.Forms.Button BtnWindow;
      private System.Windows.Forms.GroupBox GroupBox1;
      private System.Windows.Forms.CheckBox ChkPreviewWindow;
      private System.Windows.Forms.CheckBox ChkIncludeCursor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         // Create a new SnagIt ImageCapture object.
         SnagImg = new SNAGITLib.ImageCaptureClass();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.Label1 = new System.Windows.Forms.Label();
         this.GroupBox2 = new System.Windows.Forms.GroupBox();
         this.BtnRegion = new System.Windows.Forms.Button();
         this.BtnWindow = new System.Windows.Forms.Button();
         this.GroupBox1 = new System.Windows.Forms.GroupBox();
         this.ChkPreviewWindow = new System.Windows.Forms.CheckBox();
         this.ChkIncludeCursor = new System.Windows.Forms.CheckBox();
         this.GroupBox2.SuspendLayout();
         this.GroupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // Label1
         // 
         this.Label1.Location = new System.Drawing.Point(16, 104);
         this.Label1.Name = "Label1";
         this.Label1.Size = new System.Drawing.Size(264, 40);
         this.Label1.TabIndex = 8;
         this.Label1.Text = "For simplicity, we will always output to a file, the name of which will be suppli" +
            "ed by the user when prompted.";
         // 
         // GroupBox2
         // 
         this.GroupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.BtnRegion,
                                                                                this.BtnWindow});
         this.GroupBox2.Location = new System.Drawing.Point(8, 16);
         this.GroupBox2.Name = "GroupBox2";
         this.GroupBox2.Size = new System.Drawing.Size(128, 80);
         this.GroupBox2.TabIndex = 7;
         this.GroupBox2.TabStop = false;
         this.GroupBox2.Text = "Capture Type";
         // 
         // BtnRegion
         // 
         this.BtnRegion.Location = new System.Drawing.Point(16, 48);
         this.BtnRegion.Name = "BtnRegion";
         this.BtnRegion.Size = new System.Drawing.Size(96, 23);
         this.BtnRegion.TabIndex = 1;
         this.BtnRegion.Text = "Region Capture";
         this.BtnRegion.Click += new System.EventHandler(this.BtnRegion_Click);
         // 
         // BtnWindow
         // 
         this.BtnWindow.Location = new System.Drawing.Point(16, 16);
         this.BtnWindow.Name = "BtnWindow";
         this.BtnWindow.Size = new System.Drawing.Size(96, 24);
         this.BtnWindow.TabIndex = 0;
         this.BtnWindow.Text = "Window Capture";
         this.BtnWindow.Click += new System.EventHandler(this.BtnWindow_Click);
         // 
         // GroupBox1
         // 
         this.GroupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.ChkPreviewWindow,
                                                                                this.ChkIncludeCursor});
         this.GroupBox1.Location = new System.Drawing.Point(144, 16);
         this.GroupBox1.Name = "GroupBox1";
         this.GroupBox1.Size = new System.Drawing.Size(136, 80);
         this.GroupBox1.TabIndex = 6;
         this.GroupBox1.TabStop = false;
         this.GroupBox1.Text = "Options";
         // 
         // ChkPreviewWindow
         // 
         this.ChkPreviewWindow.Location = new System.Drawing.Point(16, 48);
         this.ChkPreviewWindow.Name = "ChkPreviewWindow";
         this.ChkPreviewWindow.Size = new System.Drawing.Size(112, 24);
         this.ChkPreviewWindow.TabIndex = 2;
         this.ChkPreviewWindow.Text = "Preview Window";
         // 
         // ChkIncludeCursor
         // 
         this.ChkIncludeCursor.Location = new System.Drawing.Point(16, 24);
         this.ChkIncludeCursor.Name = "ChkIncludeCursor";
         this.ChkIncludeCursor.TabIndex = 1;
         this.ChkIncludeCursor.Text = "Include Cursor";
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(292, 158);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.Label1,
                                                                      this.GroupBox2,
                                                                      this.GroupBox1});
         this.Name = "Form1";
         this.Text = "SnagIt COM Example";
         this.GroupBox2.ResumeLayout(false);
         this.GroupBox1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

      private void BtnRegion_Click(object sender, System.EventArgs e)
      {
         // Choose an input and an output:
         SnagImg.Input = SNAGITLib.snagImageInput.siiRegion;
         SnagImg.Output = SNAGITLib.snagImageOutput.sioFile;

         // Prompting for the file name is the default, but it cannot hurt to set this explicitly
         SnagImg.OutputImageFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmPrompt;

         // Show Preview Window?
         SnagImg.EnablePreviewWindow = ChkPreviewWindow.Checked;

         // Include cursor if set
         SnagImg.IncludeCursor = ChkIncludeCursor.Checked;

         // Try to initiate the capture.. catch any errors and display an 
         // appropriate error mesasge.  Here, SnagIt expiration error is shown
         // as an example.
         try
         {
            SnagImg.Capture();
         }
         catch ( Exception exception )
         {
            if ( SnagImg.LastError == SNAGITLib.snagError.serrSnagItExpired )
            {
               MessageBox.Show( "Unable to capture: SnagIt evaluation has expired" );
            }
         }
      }

      private void BtnWindow_Click(object sender, System.EventArgs e)
      {
         // Choose an input and an output:
         SnagImg.Input = SNAGITLib.snagImageInput.siiWindow;
         SnagImg.Output = SNAGITLib.snagImageOutput.sioFile;

         // Prompting for the file name is the default, but it cannot hurt to 
         // set this explicitly
         SnagImg.OutputImageFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmPrompt;

         // Show Preview Window?
         SnagImg.EnablePreviewWindow = ChkPreviewWindow.Checked;

         // Include cursor if set
         SnagImg.IncludeCursor = ChkIncludeCursor.Checked;
      
         // Try to initiate the capture.. catch any errors and display an 
         // appropriate error mesasge.  Here, SnagIt expiration error is shown
         // as an example.
         try
         {
            SnagImg.Capture();
         }
         catch ( Exception exception )
         {
            if ( SnagImg.LastError == SNAGITLib.snagError.serrSnagItExpired )
            {
               MessageBox.Show( "Unable to capture: SnagIt evaluation has expired" );
            }
         }
      }
	}
}
