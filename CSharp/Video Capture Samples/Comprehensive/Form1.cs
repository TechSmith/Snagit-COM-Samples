//---------------------------------------------------------------------------
// This is a simple C# sample application to demonstrate the use of the Snagit
// COM interface for recording video. It covers every option for recording
// video that is exposed to COM by Snagit.
// Summary:
//   * Instantiating the Snagit video capture class
//   * Handling recorder events
//   * Setting all input/output/recording options
//   * Demonstrates how you can use your own recording UI
//     - Getting recording devices
//     - Getting user-selected region to record
//     - Catching volume level events
//     - Handling recorder states (start/stop pause/resume)
//     - Catching recorder error events
//     - Getting recording stats
//     - Disabling MP4 Moov atom optimization (if not streaming video from the net)
//
// Note: This sample requires Snagit 11.1.0 or later.
// 
// Support e-mail: support@techsmith.zendesk.com
// This software is provided under the MIT License (http://opensource.org/licenses/MIT)
// Copyright (c) 2014 TechSmith Corporation
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sub-license, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
//    The above copyright notice and this permission notice shall be included in all copies
//    or substantial portions of the Software.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//    PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//---------------------------------------------------------------------------
using System;
using System.Threading;
using System.Windows.Forms;
using SNAGITLib;
using VideoSample;


namespace VideoSample
{
	public class Form1 : System.Windows.Forms.Form
   {
      #region Class private members
      // Declare the SnagIt VideoCaptureClass object.  
      // NOTE: First you must add a reference to 
      // the SNAGIT 1.0 Type Library in order for
      // this line to compile. Please be sure to
      // set the Embed Interop Types to False in
      // the SNAGITLib reference properties.
      private SNAGITLib.VideoCaptureClass snagVideo;

      private System.Windows.Forms.GroupBox OptionsGroupBox;
      private System.Windows.Forms.CheckBox ChkPreviewWindow;
      private CheckBox IncludeCursor;
      private ComboBox CaptureType;
      private Button RecordButton;
      private ComboBox WindowInputSelection;
      private TextBox XPos;
      private TextBox YPos;
      private TextBox WinHandle;
      private Label WindowPosLabel;
      private Label xLabel;
      private Label WinHandleLabel;
      private GroupBox SelectionGroupBox;
      private CheckBox DisableMoovOptimization;
      private CheckBox ShowSelectionMagnifier;
      private GroupBox groupBox1;
      private TabControl outputTabControl;
      private TabPage tabFile;
      private TabPage tabFTP;
      private ComboBox FileNamingSelector;
      private TextBox FixedFilename;
      private Label FilenameLabel;
      private Label label1;
      private ComboBox OutputSelector;
      private TabPage tabNone;
      private Label label2;
      private GroupBox groupBox2;
      private Button BrowseOutputFolderButton;
      private TextBox FileOutputFolderPath;
      private RadioButton SpecifyFolderRButton;
      private RadioButton LastUsedFolderRButton;
      private GroupBox groupBox5;
      private GroupBox groupBox4;
      private TextBox FTPProxyServerEdit;
      private Label label8;
      private CheckBox FTPProxyCheckbox;
      private GroupBox groupBox3;
      private TextBox FTPPassword;
      private TextBox FTPUsername;
      private Label label7;
      private Label label6;
      private CheckBox FTPAuthenticationCheckbox;
      private CheckBox FTPProgressCheckbox;
      private TextBox RemoteFTPPathEdit;
      private Label label5;
      private CheckBox PassiveFTPCheckbox;
      private TextBox TFPServerPortEdit;
      private Label label4;
      private TextBox FTPServerEdit;
      private Label label3;
      private TextBox FileDigitsEdit;
      private Label NumberOfDigitsLabel;
      private Label label9;
      private Label label10;
      private TextBox FTPNumberOfDigitsEdit;
      private Label FTPNumberOfDigits;
      private TextBox FTPRemoteFilename;
      private ComboBox FTPFileNamingSelector;
      private CheckBox FTPTempReplaceCheckbox;
      private TextBox RegionHeight;
      private TextBox RegionWidth;
      private Label HeightLabel;
      private Label WidthLabel;
      private Label StartYLabel;
      private Label StartXLabel;
      private CheckBox UseStartPos;
      private TextBox StartYPos;
      private TextBox StartXPos;
      private Label PointYLabel;
      private Label PointXLabel;      
      private CheckBox chkBxHideSnagitRecorderUI;
      private ComboBox AudioDevice;
      private Label label13;
      private TextBox VolumeLevelMeter;
      private Label label11;
      private Label label12;
      private TextBox RecorderErrorTextBox;
      private Button bttnToggle;
      private Button bttnStop;
      private Button bttnShowInfo;
      private CheckBox chkMuteMic;
      private CheckBox chkMuteSystemAudio;
      private CheckBox chkShowRegion;
      private GroupBox groupBox6;
      private GroupBox groupBox7;

		private readonly System.ComponentModel.Container _components = null;
      #endregion

      public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         // Create a new SnagIt Video Capture object.
         // NOTE: First you must add a reference to 
         // the SNAGIT 1.0 Type Library
         snagVideo = new SNAGITLib.VideoCaptureClass();
         SetDefaultOptions();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (_components != null) 
				{
					_components.Dispose();
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
         this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
         this.ShowSelectionMagnifier = new System.Windows.Forms.CheckBox();
         this.ChkPreviewWindow = new System.Windows.Forms.CheckBox();
         this.IncludeCursor = new System.Windows.Forms.CheckBox();
         this.DisableMoovOptimization = new System.Windows.Forms.CheckBox();
         this.chkShowRegion = new System.Windows.Forms.CheckBox();
         this.chkBxHideSnagitRecorderUI = new System.Windows.Forms.CheckBox();
         this.CaptureType = new System.Windows.Forms.ComboBox();
         this.RecordButton = new System.Windows.Forms.Button();
         this.WindowInputSelection = new System.Windows.Forms.ComboBox();
         this.XPos = new System.Windows.Forms.TextBox();
         this.YPos = new System.Windows.Forms.TextBox();
         this.WinHandle = new System.Windows.Forms.TextBox();
         this.WindowPosLabel = new System.Windows.Forms.Label();
         this.xLabel = new System.Windows.Forms.Label();
         this.WinHandleLabel = new System.Windows.Forms.Label();
         this.SelectionGroupBox = new System.Windows.Forms.GroupBox();
         this.AudioDevice = new System.Windows.Forms.ComboBox();
         this.label13 = new System.Windows.Forms.Label();
         this.PointYLabel = new System.Windows.Forms.Label();
         this.PointXLabel = new System.Windows.Forms.Label();
         this.StartYLabel = new System.Windows.Forms.Label();
         this.StartXLabel = new System.Windows.Forms.Label();
         this.UseStartPos = new System.Windows.Forms.CheckBox();
         this.StartYPos = new System.Windows.Forms.TextBox();
         this.StartXPos = new System.Windows.Forms.TextBox();
         this.HeightLabel = new System.Windows.Forms.Label();
         this.WidthLabel = new System.Windows.Forms.Label();
         this.RegionHeight = new System.Windows.Forms.TextBox();
         this.RegionWidth = new System.Windows.Forms.TextBox();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.OutputSelector = new System.Windows.Forms.ComboBox();
         this.outputTabControl = new System.Windows.Forms.TabControl();
         this.tabNone = new System.Windows.Forms.TabPage();
         this.label2 = new System.Windows.Forms.Label();
         this.tabFile = new System.Windows.Forms.TabPage();
         this.FileDigitsEdit = new System.Windows.Forms.TextBox();
         this.NumberOfDigitsLabel = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.BrowseOutputFolderButton = new System.Windows.Forms.Button();
         this.FileOutputFolderPath = new System.Windows.Forms.TextBox();
         this.SpecifyFolderRButton = new System.Windows.Forms.RadioButton();
         this.LastUsedFolderRButton = new System.Windows.Forms.RadioButton();
         this.FilenameLabel = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.FixedFilename = new System.Windows.Forms.TextBox();
         this.FileNamingSelector = new System.Windows.Forms.ComboBox();
         this.tabFTP = new System.Windows.Forms.TabPage();
         this.groupBox5 = new System.Windows.Forms.GroupBox();
         this.FTPTempReplaceCheckbox = new System.Windows.Forms.CheckBox();
         this.label9 = new System.Windows.Forms.Label();
         this.label10 = new System.Windows.Forms.Label();
         this.FTPNumberOfDigitsEdit = new System.Windows.Forms.TextBox();
         this.FTPNumberOfDigits = new System.Windows.Forms.Label();
         this.FTPRemoteFilename = new System.Windows.Forms.TextBox();
         this.FTPFileNamingSelector = new System.Windows.Forms.ComboBox();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.FTPProxyServerEdit = new System.Windows.Forms.TextBox();
         this.label8 = new System.Windows.Forms.Label();
         this.FTPProxyCheckbox = new System.Windows.Forms.CheckBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.FTPPassword = new System.Windows.Forms.TextBox();
         this.FTPUsername = new System.Windows.Forms.TextBox();
         this.label7 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.FTPAuthenticationCheckbox = new System.Windows.Forms.CheckBox();
         this.FTPProgressCheckbox = new System.Windows.Forms.CheckBox();
         this.RemoteFTPPathEdit = new System.Windows.Forms.TextBox();
         this.label5 = new System.Windows.Forms.Label();
         this.PassiveFTPCheckbox = new System.Windows.Forms.CheckBox();
         this.TFPServerPortEdit = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.FTPServerEdit = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.VolumeLevelMeter = new System.Windows.Forms.TextBox();
         this.label11 = new System.Windows.Forms.Label();
         this.label12 = new System.Windows.Forms.Label();
         this.RecorderErrorTextBox = new System.Windows.Forms.TextBox();
         this.bttnToggle = new System.Windows.Forms.Button();
         this.bttnStop = new System.Windows.Forms.Button();
         this.bttnShowInfo = new System.Windows.Forms.Button();
         this.chkMuteMic = new System.Windows.Forms.CheckBox();
         this.chkMuteSystemAudio = new System.Windows.Forms.CheckBox();
         this.groupBox6 = new System.Windows.Forms.GroupBox();
         this.groupBox7 = new System.Windows.Forms.GroupBox();
         this.OptionsGroupBox.SuspendLayout();
         this.SelectionGroupBox.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.outputTabControl.SuspendLayout();
         this.tabNone.SuspendLayout();
         this.tabFile.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.tabFTP.SuspendLayout();
         this.groupBox5.SuspendLayout();
         this.groupBox4.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.groupBox6.SuspendLayout();
         this.groupBox7.SuspendLayout();
         this.SuspendLayout();
         // 
         // OptionsGroupBox
         // 
         this.OptionsGroupBox.Controls.Add(this.ShowSelectionMagnifier);
         this.OptionsGroupBox.Controls.Add(this.ChkPreviewWindow);
         this.OptionsGroupBox.Controls.Add(this.IncludeCursor);
         this.OptionsGroupBox.Location = new System.Drawing.Point(354, 16);
         this.OptionsGroupBox.Name = "OptionsGroupBox";
         this.OptionsGroupBox.Size = new System.Drawing.Size(195, 101);
         this.OptionsGroupBox.TabIndex = 6;
         this.OptionsGroupBox.TabStop = false;
         this.OptionsGroupBox.Text = "Basic Options";
         // 
         // ShowSelectionMagnifier
         // 
         this.ShowSelectionMagnifier.Location = new System.Drawing.Point(13, 40);
         this.ShowSelectionMagnifier.Name = "ShowSelectionMagnifier";
         this.ShowSelectionMagnifier.Size = new System.Drawing.Size(156, 24);
         this.ShowSelectionMagnifier.TabIndex = 4;
         this.ShowSelectionMagnifier.Text = "Show Selection Magnifier";
         // 
         // ChkPreviewWindow
         // 
         this.ChkPreviewWindow.Checked = true;
         this.ChkPreviewWindow.CheckState = System.Windows.Forms.CheckState.Checked;
         this.ChkPreviewWindow.Location = new System.Drawing.Point(13, 61);
         this.ChkPreviewWindow.Name = "ChkPreviewWindow";
         this.ChkPreviewWindow.Size = new System.Drawing.Size(114, 24);
         this.ChkPreviewWindow.TabIndex = 2;
         this.ChkPreviewWindow.Text = "Preview Window";
         // 
         // IncludeCursor
         // 
         this.IncludeCursor.Checked = true;
         this.IncludeCursor.CheckState = System.Windows.Forms.CheckState.Checked;
         this.IncludeCursor.Location = new System.Drawing.Point(13, 20);
         this.IncludeCursor.Name = "IncludeCursor";
         this.IncludeCursor.Size = new System.Drawing.Size(104, 24);
         this.IncludeCursor.TabIndex = 1;
         this.IncludeCursor.Text = "Include Cursor";
         // 
         // DisableMoovOptimization
         // 
         this.DisableMoovOptimization.Location = new System.Drawing.Point(13, 57);
         this.DisableMoovOptimization.Name = "DisableMoovOptimization";
         this.DisableMoovOptimization.Size = new System.Drawing.Size(172, 24);
         this.DisableMoovOptimization.TabIndex = 3;
         this.DisableMoovOptimization.Text = "Disable Moov Optimization";
         // 
         // chkShowRegion
         // 
         this.chkShowRegion.AutoSize = true;
         this.chkShowRegion.Location = new System.Drawing.Point(13, 39);
         this.chkShowRegion.Name = "chkShowRegion";
         this.chkShowRegion.Size = new System.Drawing.Size(157, 17);
         this.chkShowRegion.TabIndex = 6;
         this.chkShowRegion.Text = "Show Selection Dimensions";
         this.chkShowRegion.UseVisualStyleBackColor = true;
         // 
         // chkBxHideSnagitRecorderUI
         // 
         this.chkBxHideSnagitRecorderUI.Location = new System.Drawing.Point(13, 16);
         this.chkBxHideSnagitRecorderUI.Name = "chkBxHideSnagitRecorderUI";
         this.chkBxHideSnagitRecorderUI.Size = new System.Drawing.Size(172, 24);
         this.chkBxHideSnagitRecorderUI.TabIndex = 5;
         this.chkBxHideSnagitRecorderUI.Text = "Hide Snagit Recorder UI";
         this.chkBxHideSnagitRecorderUI.CheckedChanged += new System.EventHandler(this.chkBxHideSnagitRecorderUI_CheckedChanged);
         // 
         // CaptureType
         // 
         this.CaptureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.CaptureType.FormattingEnabled = true;
         this.CaptureType.Items.AddRange(new object[] {
            "All-in-One",
            "Region",
            "Fixed Region",
            "Window"});
         this.CaptureType.Location = new System.Drawing.Point(6, 19);
         this.CaptureType.Name = "CaptureType";
         this.CaptureType.Size = new System.Drawing.Size(122, 21);
         this.CaptureType.TabIndex = 8;
         this.CaptureType.SelectedIndexChanged += new System.EventHandler(this.CaptureTypeSelectedIndexChanged);
         // 
         // RecordButton
         // 
         this.RecordButton.Enabled = false;
         this.RecordButton.Location = new System.Drawing.Point(202, 607);
         this.RecordButton.Name = "RecordButton";
         this.RecordButton.Size = new System.Drawing.Size(129, 36);
         this.RecordButton.TabIndex = 9;
         this.RecordButton.Text = "Record using SnagitUI";
         this.RecordButton.UseVisualStyleBackColor = true;
         this.RecordButton.Click += new System.EventHandler(this.Capture_Click);
         // 
         // WindowInputSelection
         // 
         this.WindowInputSelection.FormattingEnabled = true;
         this.WindowInputSelection.Items.AddRange(new object[] {
            "Interactive",
            "Active Window",
            "Window Handle",
            "Point on the Desktop"});
         this.WindowInputSelection.Location = new System.Drawing.Point(6, 67);
         this.WindowInputSelection.Name = "WindowInputSelection";
         this.WindowInputSelection.Size = new System.Drawing.Size(135, 21);
         this.WindowInputSelection.TabIndex = 10;
         this.WindowInputSelection.Text = "Input Selection Type";
         this.WindowInputSelection.Visible = false;
         this.WindowInputSelection.SelectedIndexChanged += new System.EventHandler(this.InputSelectionSelectedIndexChanged);
         // 
         // XPos
         // 
         this.XPos.Location = new System.Drawing.Point(24, 120);
         this.XPos.MaxLength = 5;
         this.XPos.Name = "XPos";
         this.XPos.Size = new System.Drawing.Size(41, 20);
         this.XPos.TabIndex = 11;
         this.XPos.Text = "0";
         this.XPos.Visible = false;
         this.XPos.WordWrap = false;
         // 
         // YPos
         // 
         this.YPos.Location = new System.Drawing.Point(105, 120);
         this.YPos.MaxLength = 5;
         this.YPos.Name = "YPos";
         this.YPos.Size = new System.Drawing.Size(41, 20);
         this.YPos.TabIndex = 12;
         this.YPos.Text = "0";
         this.YPos.Visible = false;
         this.YPos.WordWrap = false;
         // 
         // WinHandle
         // 
         this.WinHandle.Location = new System.Drawing.Point(53, 107);
         this.WinHandle.MaxLength = 16;
         this.WinHandle.Name = "WinHandle";
         this.WinHandle.Size = new System.Drawing.Size(77, 20);
         this.WinHandle.TabIndex = 13;
         this.WinHandle.Text = "0x0";
         this.WinHandle.Visible = false;
         this.WinHandle.WordWrap = false;
         // 
         // WindowPosLabel
         // 
         this.WindowPosLabel.AutoSize = true;
         this.WindowPosLabel.Location = new System.Drawing.Point(6, 100);
         this.WindowPosLabel.Name = "WindowPosLabel";
         this.WindowPosLabel.Size = new System.Drawing.Size(86, 13);
         this.WindowPosLabel.TabIndex = 14;
         this.WindowPosLabel.Text = "Window Position";
         this.WindowPosLabel.Visible = false;
         // 
         // xLabel
         // 
         this.xLabel.AutoSize = true;
         this.xLabel.Location = new System.Drawing.Point(71, 120);
         this.xLabel.Name = "xLabel";
         this.xLabel.Size = new System.Drawing.Size(12, 13);
         this.xLabel.TabIndex = 15;
         this.xLabel.Text = "x";
         this.xLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.xLabel.Visible = false;
         // 
         // WinHandleLabel
         // 
         this.WinHandleLabel.AutoSize = true;
         this.WinHandleLabel.Location = new System.Drawing.Point(6, 110);
         this.WinHandleLabel.Name = "WinHandleLabel";
         this.WinHandleLabel.Size = new System.Drawing.Size(41, 13);
         this.WinHandleLabel.TabIndex = 16;
         this.WinHandleLabel.Text = "Handle";
         this.WinHandleLabel.Visible = false;
         // 
         // SelectionGroupBox
         // 
         this.SelectionGroupBox.Controls.Add(this.AudioDevice);
         this.SelectionGroupBox.Controls.Add(this.label13);
         this.SelectionGroupBox.Controls.Add(this.PointYLabel);
         this.SelectionGroupBox.Controls.Add(this.PointXLabel);
         this.SelectionGroupBox.Controls.Add(this.StartYLabel);
         this.SelectionGroupBox.Controls.Add(this.StartXLabel);
         this.SelectionGroupBox.Controls.Add(this.UseStartPos);
         this.SelectionGroupBox.Controls.Add(this.StartYPos);
         this.SelectionGroupBox.Controls.Add(this.StartXPos);
         this.SelectionGroupBox.Controls.Add(this.HeightLabel);
         this.SelectionGroupBox.Controls.Add(this.WidthLabel);
         this.SelectionGroupBox.Controls.Add(this.RegionHeight);
         this.SelectionGroupBox.Controls.Add(this.xLabel);
         this.SelectionGroupBox.Controls.Add(this.RegionWidth);
         this.SelectionGroupBox.Controls.Add(this.WinHandleLabel);
         this.SelectionGroupBox.Controls.Add(this.WindowPosLabel);
         this.SelectionGroupBox.Controls.Add(this.WinHandle);
         this.SelectionGroupBox.Controls.Add(this.YPos);
         this.SelectionGroupBox.Controls.Add(this.XPos);
         this.SelectionGroupBox.Controls.Add(this.WindowInputSelection);
         this.SelectionGroupBox.Controls.Add(this.CaptureType);
         this.SelectionGroupBox.Location = new System.Drawing.Point(7, 16);
         this.SelectionGroupBox.Name = "SelectionGroupBox";
         this.SelectionGroupBox.Size = new System.Drawing.Size(341, 194);
         this.SelectionGroupBox.TabIndex = 17;
         this.SelectionGroupBox.TabStop = false;
         this.SelectionGroupBox.Text = "Input Options";
         // 
         // AudioDevice
         // 
         this.AudioDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.AudioDevice.FormattingEnabled = true;
         this.AudioDevice.Location = new System.Drawing.Point(80, 152);
         this.AudioDevice.Name = "AudioDevice";
         this.AudioDevice.Size = new System.Drawing.Size(245, 21);
         this.AudioDevice.TabIndex = 32;
         // 
         // label13
         // 
         this.label13.AutoSize = true;
         this.label13.Location = new System.Drawing.Point(3, 155);
         this.label13.Name = "label13";
         this.label13.Size = new System.Drawing.Size(71, 13);
         this.label13.TabIndex = 31;
         this.label13.Text = "Audio Device";
         // 
         // PointYLabel
         // 
         this.PointYLabel.AutoSize = true;
         this.PointYLabel.Location = new System.Drawing.Point(91, 123);
         this.PointYLabel.Name = "PointYLabel";
         this.PointYLabel.Size = new System.Drawing.Size(14, 13);
         this.PointYLabel.TabIndex = 30;
         this.PointYLabel.Text = "Y";
         this.PointYLabel.Visible = false;
         // 
         // PointXLabel
         // 
         this.PointXLabel.AutoSize = true;
         this.PointXLabel.Location = new System.Drawing.Point(10, 123);
         this.PointXLabel.Name = "PointXLabel";
         this.PointXLabel.Size = new System.Drawing.Size(14, 13);
         this.PointXLabel.TabIndex = 29;
         this.PointXLabel.Text = "X";
         this.PointXLabel.Visible = false;
         // 
         // StartYLabel
         // 
         this.StartYLabel.AutoSize = true;
         this.StartYLabel.Location = new System.Drawing.Point(192, 103);
         this.StartYLabel.Name = "StartYLabel";
         this.StartYLabel.Size = new System.Drawing.Size(36, 13);
         this.StartYLabel.TabIndex = 28;
         this.StartYLabel.Text = "StartY";
         this.StartYLabel.Visible = false;
         // 
         // StartXLabel
         // 
         this.StartXLabel.AutoSize = true;
         this.StartXLabel.Location = new System.Drawing.Point(92, 103);
         this.StartXLabel.Name = "StartXLabel";
         this.StartXLabel.Size = new System.Drawing.Size(36, 13);
         this.StartXLabel.TabIndex = 27;
         this.StartXLabel.Text = "StartX";
         this.StartXLabel.Visible = false;
         // 
         // UseStartPos
         // 
         this.UseStartPos.Location = new System.Drawing.Point(51, 77);
         this.UseStartPos.Name = "UseStartPos";
         this.UseStartPos.Size = new System.Drawing.Size(124, 24);
         this.UseStartPos.TabIndex = 26;
         this.UseStartPos.Text = "Use starting offset";
         this.UseStartPos.Visible = false;
         this.UseStartPos.CheckedChanged += new System.EventHandler(this.UseStartPos_CheckedChanged);
         // 
         // StartYPos
         // 
         this.StartYPos.Enabled = false;
         this.StartYPos.Location = new System.Drawing.Point(234, 100);
         this.StartYPos.MaxLength = 5;
         this.StartYPos.Name = "StartYPos";
         this.StartYPos.Size = new System.Drawing.Size(41, 20);
         this.StartYPos.TabIndex = 25;
         this.StartYPos.Text = "0";
         this.StartYPos.Visible = false;
         this.StartYPos.WordWrap = false;
         // 
         // StartXPos
         // 
         this.StartXPos.Enabled = false;
         this.StartXPos.Location = new System.Drawing.Point(134, 100);
         this.StartXPos.MaxLength = 5;
         this.StartXPos.Name = "StartXPos";
         this.StartXPos.Size = new System.Drawing.Size(41, 20);
         this.StartXPos.TabIndex = 24;
         this.StartXPos.Text = "0";
         this.StartXPos.Visible = false;
         this.StartXPos.WordWrap = false;
         // 
         // HeightLabel
         // 
         this.HeightLabel.AutoSize = true;
         this.HeightLabel.Location = new System.Drawing.Point(113, 49);
         this.HeightLabel.Name = "HeightLabel";
         this.HeightLabel.Size = new System.Drawing.Size(38, 13);
         this.HeightLabel.TabIndex = 22;
         this.HeightLabel.Text = "Height";
         this.HeightLabel.Visible = false;
         // 
         // WidthLabel
         // 
         this.WidthLabel.AutoSize = true;
         this.WidthLabel.Location = new System.Drawing.Point(10, 49);
         this.WidthLabel.Name = "WidthLabel";
         this.WidthLabel.Size = new System.Drawing.Size(35, 13);
         this.WidthLabel.TabIndex = 23;
         this.WidthLabel.Text = "Width";
         this.WidthLabel.Visible = false;
         // 
         // RegionHeight
         // 
         this.RegionHeight.Location = new System.Drawing.Point(155, 46);
         this.RegionHeight.MaxLength = 5;
         this.RegionHeight.Name = "RegionHeight";
         this.RegionHeight.Size = new System.Drawing.Size(41, 20);
         this.RegionHeight.TabIndex = 20;
         this.RegionHeight.Text = "0";
         this.RegionHeight.Visible = false;
         this.RegionHeight.WordWrap = false;
         // 
         // RegionWidth
         // 
         this.RegionWidth.Location = new System.Drawing.Point(51, 46);
         this.RegionWidth.MaxLength = 5;
         this.RegionWidth.Name = "RegionWidth";
         this.RegionWidth.Size = new System.Drawing.Size(41, 20);
         this.RegionWidth.TabIndex = 21;
         this.RegionWidth.Text = "0";
         this.RegionWidth.Visible = false;
         this.RegionWidth.WordWrap = false;
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.OutputSelector);
         this.groupBox1.Controls.Add(this.outputTabControl);
         this.groupBox1.Location = new System.Drawing.Point(7, 233);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(542, 368);
         this.groupBox1.TabIndex = 19;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Output Options";
         // 
         // OutputSelector
         // 
         this.OutputSelector.FormattingEnabled = true;
         this.OutputSelector.Items.AddRange(new object[] {
            "None",
            "File",
            "FTP"});
         this.OutputSelector.Location = new System.Drawing.Point(6, 29);
         this.OutputSelector.Name = "OutputSelector";
         this.OutputSelector.Size = new System.Drawing.Size(70, 21);
         this.OutputSelector.TabIndex = 1;
         this.OutputSelector.Text = "None";
         this.OutputSelector.SelectedIndexChanged += new System.EventHandler(this.OutputSelectedIndexChanged);
         // 
         // outputTabControl
         // 
         this.outputTabControl.Controls.Add(this.tabNone);
         this.outputTabControl.Controls.Add(this.tabFile);
         this.outputTabControl.Controls.Add(this.tabFTP);
         this.outputTabControl.Location = new System.Drawing.Point(9, 56);
         this.outputTabControl.Name = "outputTabControl";
         this.outputTabControl.SelectedIndex = 0;
         this.outputTabControl.Size = new System.Drawing.Size(527, 297);
         this.outputTabControl.TabIndex = 0;
         this.outputTabControl.SelectedIndexChanged += new System.EventHandler(this.OutputTabSelectionChanged);
         // 
         // tabNone
         // 
         this.tabNone.Controls.Add(this.label2);
         this.tabNone.Location = new System.Drawing.Point(4, 22);
         this.tabNone.Name = "tabNone";
         this.tabNone.Padding = new System.Windows.Forms.Padding(3);
         this.tabNone.Size = new System.Drawing.Size(519, 271);
         this.tabNone.TabIndex = 3;
         this.tabNone.Text = "None";
         this.tabNone.UseVisualStyleBackColor = true;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(7, 19);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(85, 13);
         this.label2.TabIndex = 0;
         this.label2.Text = "Preview in editor";
         // 
         // tabFile
         // 
         this.tabFile.Controls.Add(this.FileDigitsEdit);
         this.tabFile.Controls.Add(this.NumberOfDigitsLabel);
         this.tabFile.Controls.Add(this.groupBox2);
         this.tabFile.Controls.Add(this.FilenameLabel);
         this.tabFile.Controls.Add(this.label1);
         this.tabFile.Controls.Add(this.FixedFilename);
         this.tabFile.Controls.Add(this.FileNamingSelector);
         this.tabFile.Location = new System.Drawing.Point(4, 22);
         this.tabFile.Name = "tabFile";
         this.tabFile.Padding = new System.Windows.Forms.Padding(3);
         this.tabFile.Size = new System.Drawing.Size(519, 271);
         this.tabFile.TabIndex = 0;
         this.tabFile.Text = "File";
         this.tabFile.UseVisualStyleBackColor = true;
         // 
         // FileDigitsEdit
         // 
         this.FileDigitsEdit.Location = new System.Drawing.Point(362, 44);
         this.FileDigitsEdit.MaxLength = 8;
         this.FileDigitsEdit.Name = "FileDigitsEdit";
         this.FileDigitsEdit.Size = new System.Drawing.Size(59, 20);
         this.FileDigitsEdit.TabIndex = 6;
         this.FileDigitsEdit.Text = "0";
         this.FileDigitsEdit.WordWrap = false;
         // 
         // NumberOfDigitsLabel
         // 
         this.NumberOfDigitsLabel.AutoSize = true;
         this.NumberOfDigitsLabel.Location = new System.Drawing.Point(359, 21);
         this.NumberOfDigitsLabel.Name = "NumberOfDigitsLabel";
         this.NumberOfDigitsLabel.Size = new System.Drawing.Size(86, 13);
         this.NumberOfDigitsLabel.TabIndex = 5;
         this.NumberOfDigitsLabel.Text = "Number of digits:";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.BrowseOutputFolderButton);
         this.groupBox2.Controls.Add(this.FileOutputFolderPath);
         this.groupBox2.Controls.Add(this.SpecifyFolderRButton);
         this.groupBox2.Controls.Add(this.LastUsedFolderRButton);
         this.groupBox2.Location = new System.Drawing.Point(6, 84);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(338, 106);
         this.groupBox2.TabIndex = 4;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Folder";
         // 
         // BrowseOutputFolderButton
         // 
         this.BrowseOutputFolderButton.Enabled = false;
         this.BrowseOutputFolderButton.Location = new System.Drawing.Point(29, 62);
         this.BrowseOutputFolderButton.Name = "BrowseOutputFolderButton";
         this.BrowseOutputFolderButton.Size = new System.Drawing.Size(10, 23);
         this.BrowseOutputFolderButton.TabIndex = 3;
         this.BrowseOutputFolderButton.UseVisualStyleBackColor = true;
         this.BrowseOutputFolderButton.Click += new System.EventHandler(this.BrowseOutputFolderButton_Click);
         // 
         // FileOutputFolderPath
         // 
         this.FileOutputFolderPath.Enabled = false;
         this.FileOutputFolderPath.Location = new System.Drawing.Point(45, 65);
         this.FileOutputFolderPath.Name = "FileOutputFolderPath";
         this.FileOutputFolderPath.Size = new System.Drawing.Size(287, 20);
         this.FileOutputFolderPath.TabIndex = 2;
         // 
         // SpecifyFolderRButton
         // 
         this.SpecifyFolderRButton.AutoSize = true;
         this.SpecifyFolderRButton.Location = new System.Drawing.Point(6, 42);
         this.SpecifyFolderRButton.Name = "SpecifyFolderRButton";
         this.SpecifyFolderRButton.Size = new System.Drawing.Size(92, 17);
         this.SpecifyFolderRButton.TabIndex = 1;
         this.SpecifyFolderRButton.Text = "Use this folder";
         this.SpecifyFolderRButton.UseVisualStyleBackColor = true;
         // 
         // LastUsedFolderRButton
         // 
         this.LastUsedFolderRButton.AutoSize = true;
         this.LastUsedFolderRButton.Checked = true;
         this.LastUsedFolderRButton.Location = new System.Drawing.Point(6, 19);
         this.LastUsedFolderRButton.Name = "LastUsedFolderRButton";
         this.LastUsedFolderRButton.Size = new System.Drawing.Size(118, 17);
         this.LastUsedFolderRButton.TabIndex = 0;
         this.LastUsedFolderRButton.TabStop = true;
         this.LastUsedFolderRButton.Text = "Use last used folder";
         this.LastUsedFolderRButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.LastUsedFolderRButton.UseVisualStyleBackColor = true;
         this.LastUsedFolderRButton.CheckedChanged += new System.EventHandler(this.FileFolderRadioChanged);
         // 
         // FilenameLabel
         // 
         this.FilenameLabel.AutoSize = true;
         this.FilenameLabel.Location = new System.Drawing.Point(106, 22);
         this.FilenameLabel.Name = "FilenameLabel";
         this.FilenameLabel.Size = new System.Drawing.Size(166, 13);
         this.FilenameLabel.TabIndex = 3;
         this.FilenameLabel.Text = "File name(no extensions) or prefix:";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(6, 22);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(84, 13);
         this.label1.TabIndex = 2;
         this.label1.Text = "Naming method:";
         // 
         // FixedFilename
         // 
         this.FixedFilename.Location = new System.Drawing.Point(105, 44);
         this.FixedFilename.Name = "FixedFilename";
         this.FixedFilename.Size = new System.Drawing.Size(243, 20);
         this.FixedFilename.TabIndex = 1;
         this.FixedFilename.Visible = false;
         // 
         // FileNamingSelector
         // 
         this.FileNamingSelector.FormattingEnabled = true;
         this.FileNamingSelector.Items.AddRange(new object[] {
            "Auto",
            "Fixed",
            "Prompt"});
         this.FileNamingSelector.Location = new System.Drawing.Point(7, 44);
         this.FileNamingSelector.Name = "FileNamingSelector";
         this.FileNamingSelector.Size = new System.Drawing.Size(80, 21);
         this.FileNamingSelector.TabIndex = 0;
         this.FileNamingSelector.Text = "Prompt";
         this.FileNamingSelector.SelectedIndexChanged += new System.EventHandler(this.FileNamingSelection_SelectedIndexChanged);
         // 
         // tabFTP
         // 
         this.tabFTP.Controls.Add(this.groupBox5);
         this.tabFTP.Controls.Add(this.groupBox4);
         this.tabFTP.Controls.Add(this.groupBox3);
         this.tabFTP.Controls.Add(this.FTPProgressCheckbox);
         this.tabFTP.Controls.Add(this.RemoteFTPPathEdit);
         this.tabFTP.Controls.Add(this.label5);
         this.tabFTP.Controls.Add(this.PassiveFTPCheckbox);
         this.tabFTP.Controls.Add(this.TFPServerPortEdit);
         this.tabFTP.Controls.Add(this.label4);
         this.tabFTP.Controls.Add(this.FTPServerEdit);
         this.tabFTP.Controls.Add(this.label3);
         this.tabFTP.Location = new System.Drawing.Point(4, 22);
         this.tabFTP.Name = "tabFTP";
         this.tabFTP.Padding = new System.Windows.Forms.Padding(3);
         this.tabFTP.Size = new System.Drawing.Size(519, 271);
         this.tabFTP.TabIndex = 1;
         this.tabFTP.Text = "FTP";
         this.tabFTP.UseVisualStyleBackColor = true;
         // 
         // groupBox5
         // 
         this.groupBox5.Controls.Add(this.FTPTempReplaceCheckbox);
         this.groupBox5.Controls.Add(this.label9);
         this.groupBox5.Controls.Add(this.label10);
         this.groupBox5.Controls.Add(this.FTPNumberOfDigitsEdit);
         this.groupBox5.Controls.Add(this.FTPNumberOfDigits);
         this.groupBox5.Controls.Add(this.FTPRemoteFilename);
         this.groupBox5.Controls.Add(this.FTPFileNamingSelector);
         this.groupBox5.Location = new System.Drawing.Point(290, 80);
         this.groupBox5.Name = "groupBox5";
         this.groupBox5.Size = new System.Drawing.Size(226, 165);
         this.groupBox5.TabIndex = 10;
         this.groupBox5.TabStop = false;
         this.groupBox5.Text = "Remote file";
         // 
         // FTPTempReplaceCheckbox
         // 
         this.FTPTempReplaceCheckbox.AutoSize = true;
         this.FTPTempReplaceCheckbox.Checked = true;
         this.FTPTempReplaceCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.FTPTempReplaceCheckbox.Location = new System.Drawing.Point(17, 74);
         this.FTPTempReplaceCheckbox.Name = "FTPTempReplaceCheckbox";
         this.FTPTempReplaceCheckbox.Size = new System.Drawing.Size(163, 17);
         this.FTPTempReplaceCheckbox.TabIndex = 11;
         this.FTPTempReplaceCheckbox.Text = "Use temp file replace method";
         this.FTPTempReplaceCheckbox.UseVisualStyleBackColor = true;
         // 
         // label9
         // 
         this.label9.AutoSize = true;
         this.label9.Location = new System.Drawing.Point(14, 24);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(84, 13);
         this.label9.TabIndex = 10;
         this.label9.Text = "Naming method:";
         // 
         // label10
         // 
         this.label10.AutoSize = true;
         this.label10.Location = new System.Drawing.Point(5, 108);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(166, 13);
         this.label10.TabIndex = 9;
         this.label10.Text = "File name(no extensions) or prefix:";
         // 
         // FTPNumberOfDigitsEdit
         // 
         this.FTPNumberOfDigitsEdit.Enabled = false;
         this.FTPNumberOfDigitsEdit.Location = new System.Drawing.Point(130, 47);
         this.FTPNumberOfDigitsEdit.MaxLength = 8;
         this.FTPNumberOfDigitsEdit.Name = "FTPNumberOfDigitsEdit";
         this.FTPNumberOfDigitsEdit.Size = new System.Drawing.Size(59, 20);
         this.FTPNumberOfDigitsEdit.TabIndex = 8;
         this.FTPNumberOfDigitsEdit.Text = "0";
         this.FTPNumberOfDigitsEdit.WordWrap = false;
         // 
         // FTPNumberOfDigits
         // 
         this.FTPNumberOfDigits.AutoSize = true;
         this.FTPNumberOfDigits.Location = new System.Drawing.Point(127, 24);
         this.FTPNumberOfDigits.Name = "FTPNumberOfDigits";
         this.FTPNumberOfDigits.Size = new System.Drawing.Size(83, 13);
         this.FTPNumberOfDigits.TabIndex = 7;
         this.FTPNumberOfDigits.Text = "Sequence Limit:";
         // 
         // FTPRemoteFilename
         // 
         this.FTPRemoteFilename.Location = new System.Drawing.Point(5, 132);
         this.FTPRemoteFilename.Name = "FTPRemoteFilename";
         this.FTPRemoteFilename.Size = new System.Drawing.Size(211, 20);
         this.FTPRemoteFilename.TabIndex = 4;
         this.FTPRemoteFilename.Text = "SNAG";
         this.FTPRemoteFilename.WordWrap = false;
         // 
         // FTPFileNamingSelector
         // 
         this.FTPFileNamingSelector.FormattingEnabled = true;
         this.FTPFileNamingSelector.Items.AddRange(new object[] {
            "Fixed",
            "Auto"});
         this.FTPFileNamingSelector.Location = new System.Drawing.Point(17, 46);
         this.FTPFileNamingSelector.Name = "FTPFileNamingSelector";
         this.FTPFileNamingSelector.Size = new System.Drawing.Size(80, 21);
         this.FTPFileNamingSelector.TabIndex = 3;
         this.FTPFileNamingSelector.Text = "Fixed";
         this.FTPFileNamingSelector.SelectedIndexChanged += new System.EventHandler(this.FTPFileNamingSelector_SelectedIndexChanged);
         // 
         // groupBox4
         // 
         this.groupBox4.Controls.Add(this.FTPProxyServerEdit);
         this.groupBox4.Controls.Add(this.label8);
         this.groupBox4.Controls.Add(this.FTPProxyCheckbox);
         this.groupBox4.Location = new System.Drawing.Point(7, 154);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(277, 91);
         this.groupBox4.TabIndex = 9;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "Proxy";
         // 
         // FTPProxyServerEdit
         // 
         this.FTPProxyServerEdit.Enabled = false;
         this.FTPProxyServerEdit.Location = new System.Drawing.Point(7, 60);
         this.FTPProxyServerEdit.Name = "FTPProxyServerEdit";
         this.FTPProxyServerEdit.Size = new System.Drawing.Size(264, 20);
         this.FTPProxyServerEdit.TabIndex = 2;
         this.FTPProxyServerEdit.WordWrap = false;
         // 
         // label8
         // 
         this.label8.AutoSize = true;
         this.label8.Location = new System.Drawing.Point(6, 43);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(83, 13);
         this.label8.TabIndex = 1;
         this.label8.Text = "Server and port:";
         // 
         // FTPProxyCheckbox
         // 
         this.FTPProxyCheckbox.AutoSize = true;
         this.FTPProxyCheckbox.Location = new System.Drawing.Point(6, 19);
         this.FTPProxyCheckbox.Name = "FTPProxyCheckbox";
         this.FTPProxyCheckbox.Size = new System.Drawing.Size(76, 17);
         this.FTPProxyCheckbox.TabIndex = 0;
         this.FTPProxyCheckbox.Text = "Use  proxy";
         this.FTPProxyCheckbox.UseVisualStyleBackColor = true;
         this.FTPProxyCheckbox.CheckedChanged += new System.EventHandler(this.FTPProxyCheckbox_CheckedChanged);
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.FTPPassword);
         this.groupBox3.Controls.Add(this.FTPUsername);
         this.groupBox3.Controls.Add(this.label7);
         this.groupBox3.Controls.Add(this.label6);
         this.groupBox3.Controls.Add(this.FTPAuthenticationCheckbox);
         this.groupBox3.Location = new System.Drawing.Point(7, 60);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(277, 87);
         this.groupBox3.TabIndex = 8;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Authentication";
         // 
         // FTPPassword
         // 
         this.FTPPassword.Enabled = false;
         this.FTPPassword.Location = new System.Drawing.Point(70, 63);
         this.FTPPassword.Name = "FTPPassword";
         this.FTPPassword.PasswordChar = '*';
         this.FTPPassword.Size = new System.Drawing.Size(201, 20);
         this.FTPPassword.TabIndex = 4;
         this.FTPPassword.WordWrap = false;
         // 
         // FTPUsername
         // 
         this.FTPUsername.Enabled = false;
         this.FTPUsername.Location = new System.Drawing.Point(72, 36);
         this.FTPUsername.Name = "FTPUsername";
         this.FTPUsername.Size = new System.Drawing.Size(199, 20);
         this.FTPUsername.TabIndex = 3;
         this.FTPUsername.WordWrap = false;
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(7, 61);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(56, 13);
         this.label7.TabIndex = 2;
         this.label7.Text = "Password:";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(7, 44);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(58, 13);
         this.label6.TabIndex = 1;
         this.label6.Text = "Username:";
         // 
         // FTPAuthenticationCheckbox
         // 
         this.FTPAuthenticationCheckbox.AutoSize = true;
         this.FTPAuthenticationCheckbox.Location = new System.Drawing.Point(7, 20);
         this.FTPAuthenticationCheckbox.Name = "FTPAuthenticationCheckbox";
         this.FTPAuthenticationCheckbox.Size = new System.Drawing.Size(69, 17);
         this.FTPAuthenticationCheckbox.TabIndex = 0;
         this.FTPAuthenticationCheckbox.Text = "Required";
         this.FTPAuthenticationCheckbox.UseVisualStyleBackColor = true;
         this.FTPAuthenticationCheckbox.CheckedChanged += new System.EventHandler(this.FTPAuthenticationCheckbox_CheckedChanged);
         // 
         // FTPProgressCheckbox
         // 
         this.FTPProgressCheckbox.AutoSize = true;
         this.FTPProgressCheckbox.Location = new System.Drawing.Point(415, 57);
         this.FTPProgressCheckbox.Name = "FTPProgressCheckbox";
         this.FTPProgressCheckbox.Size = new System.Drawing.Size(96, 17);
         this.FTPProgressCheckbox.TabIndex = 7;
         this.FTPProgressCheckbox.Text = "Show progress";
         this.FTPProgressCheckbox.UseVisualStyleBackColor = true;
         // 
         // RemoteFTPPathEdit
         // 
         this.RemoteFTPPathEdit.Location = new System.Drawing.Point(73, 34);
         this.RemoteFTPPathEdit.Name = "RemoteFTPPathEdit";
         this.RemoteFTPPathEdit.Size = new System.Drawing.Size(314, 20);
         this.RemoteFTPPathEdit.TabIndex = 6;
         this.RemoteFTPPathEdit.WordWrap = false;
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(4, 33);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(71, 13);
         this.label5.TabIndex = 5;
         this.label5.Text = "Remote path:";
         // 
         // PassiveFTPCheckbox
         // 
         this.PassiveFTPCheckbox.AutoSize = true;
         this.PassiveFTPCheckbox.Location = new System.Drawing.Point(415, 33);
         this.PassiveFTPCheckbox.Name = "PassiveFTPCheckbox";
         this.PassiveFTPCheckbox.Size = new System.Drawing.Size(107, 17);
         this.PassiveFTPCheckbox.TabIndex = 4;
         this.PassiveFTPCheckbox.Text = "Use passive FTP";
         this.PassiveFTPCheckbox.UseVisualStyleBackColor = true;
         // 
         // TFPServerPortEdit
         // 
         this.TFPServerPortEdit.Location = new System.Drawing.Point(448, 7);
         this.TFPServerPortEdit.Name = "TFPServerPortEdit";
         this.TFPServerPortEdit.Size = new System.Drawing.Size(65, 20);
         this.TFPServerPortEdit.TabIndex = 3;
         this.TFPServerPortEdit.Text = "21";
         this.TFPServerPortEdit.WordWrap = false;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(412, 7);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(29, 13);
         this.label4.TabIndex = 2;
         this.label4.Text = "Port:";
         // 
         // FTPServerEdit
         // 
         this.FTPServerEdit.Location = new System.Drawing.Point(73, 7);
         this.FTPServerEdit.Name = "FTPServerEdit";
         this.FTPServerEdit.Size = new System.Drawing.Size(314, 20);
         this.FTPServerEdit.TabIndex = 1;
         this.FTPServerEdit.WordWrap = false;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(4, 7);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(62, 13);
         this.label3.TabIndex = 0;
         this.label3.Text = "FTP server:";
         // 
         // VolumeLevelMeter
         // 
         this.VolumeLevelMeter.Location = new System.Drawing.Point(105, 43);
         this.VolumeLevelMeter.MaxLength = 4;
         this.VolumeLevelMeter.Name = "VolumeLevelMeter";
         this.VolumeLevelMeter.ReadOnly = true;
         this.VolumeLevelMeter.Size = new System.Drawing.Size(30, 20);
         this.VolumeLevelMeter.TabIndex = 20;
         this.VolumeLevelMeter.TabStop = false;
         this.VolumeLevelMeter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.VolumeLevelMeter.WordWrap = false;
         // 
         // label11
         // 
         this.label11.AutoSize = true;
         this.label11.Location = new System.Drawing.Point(13, 47);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(85, 13);
         this.label11.TabIndex = 21;
         this.label11.Text = "Volume Level(%)";
         // 
         // label12
         // 
         this.label12.AutoSize = true;
         this.label12.Location = new System.Drawing.Point(13, 73);
         this.label12.Name = "label12";
         this.label12.Size = new System.Drawing.Size(79, 13);
         this.label12.TabIndex = 23;
         this.label12.Text = "Recorder Error:";
         // 
         // RecorderErrorTextBox
         // 
         this.RecorderErrorTextBox.Location = new System.Drawing.Point(105, 69);
         this.RecorderErrorTextBox.MaxLength = 20;
         this.RecorderErrorTextBox.Name = "RecorderErrorTextBox";
         this.RecorderErrorTextBox.ReadOnly = true;
         this.RecorderErrorTextBox.Size = new System.Drawing.Size(75, 20);
         this.RecorderErrorTextBox.TabIndex = 22;
         this.RecorderErrorTextBox.TabStop = false;
         this.RecorderErrorTextBox.Text = "None";
         this.RecorderErrorTextBox.WordWrap = false;
         // 
         // bttnToggle
         // 
         this.bttnToggle.Enabled = false;
         this.bttnToggle.Location = new System.Drawing.Point(172, 20);
         this.bttnToggle.Name = "bttnToggle";
         this.bttnToggle.Size = new System.Drawing.Size(75, 23);
         this.bttnToggle.TabIndex = 24;
         this.bttnToggle.Text = "Start";
         this.bttnToggle.UseVisualStyleBackColor = true;
         this.bttnToggle.Click += new System.EventHandler(this.bttnToggle_Click);
         // 
         // bttnStop
         // 
         this.bttnStop.Enabled = false;
         this.bttnStop.Location = new System.Drawing.Point(266, 20);
         this.bttnStop.Name = "bttnStop";
         this.bttnStop.Size = new System.Drawing.Size(75, 23);
         this.bttnStop.TabIndex = 27;
         this.bttnStop.Text = "Stop";
         this.bttnStop.UseVisualStyleBackColor = true;
         this.bttnStop.Click += new System.EventHandler(this.bttnStop_Click);
         // 
         // bttnShowInfo
         // 
         this.bttnShowInfo.Enabled = false;
         this.bttnShowInfo.Location = new System.Drawing.Point(417, 20);
         this.bttnShowInfo.Name = "bttnShowInfo";
         this.bttnShowInfo.Size = new System.Drawing.Size(75, 23);
         this.bttnShowInfo.TabIndex = 30;
         this.bttnShowInfo.Text = "Video Info";
         this.bttnShowInfo.UseVisualStyleBackColor = true;
         this.bttnShowInfo.Click += new System.EventHandler(this.bttnShowInfo_Click);
         // 
         // chkMuteMic
         // 
         this.chkMuteMic.AutoSize = true;
         this.chkMuteMic.Location = new System.Drawing.Point(417, 69);
         this.chkMuteMic.Name = "chkMuteMic";
         this.chkMuteMic.Size = new System.Drawing.Size(70, 17);
         this.chkMuteMic.TabIndex = 33;
         this.chkMuteMic.Text = "Mute Mic";
         this.chkMuteMic.UseVisualStyleBackColor = true;
         this.chkMuteMic.CheckedChanged += new System.EventHandler(this.chkMuteMic_CheckedChanged);
         // 
         // chkMuteSystemAudio
         // 
         this.chkMuteSystemAudio.AutoSize = true;
         this.chkMuteSystemAudio.Location = new System.Drawing.Point(266, 69);
         this.chkMuteSystemAudio.Name = "chkMuteSystemAudio";
         this.chkMuteSystemAudio.Size = new System.Drawing.Size(117, 17);
         this.chkMuteSystemAudio.TabIndex = 34;
         this.chkMuteSystemAudio.Text = "Mute System Audio";
         this.chkMuteSystemAudio.UseVisualStyleBackColor = true;
         this.chkMuteSystemAudio.CheckedChanged += new System.EventHandler(this.chkMuteSystemAudio_CheckedChanged);
         // 
         // groupBox6
         // 
         this.groupBox6.Controls.Add(this.chkShowRegion);
         this.groupBox6.Controls.Add(this.DisableMoovOptimization);
         this.groupBox6.Controls.Add(this.chkBxHideSnagitRecorderUI);
         this.groupBox6.Location = new System.Drawing.Point(354, 136);
         this.groupBox6.Name = "groupBox6";
         this.groupBox6.Size = new System.Drawing.Size(195, 91);
         this.groupBox6.TabIndex = 35;
         this.groupBox6.TabStop = false;
         this.groupBox6.Text = "Options to use your own UI";
         // 
         // groupBox7
         // 
         this.groupBox7.Controls.Add(this.chkMuteSystemAudio);
         this.groupBox7.Controls.Add(this.chkMuteMic);
         this.groupBox7.Controls.Add(this.bttnShowInfo);
         this.groupBox7.Controls.Add(this.bttnStop);
         this.groupBox7.Controls.Add(this.bttnToggle);
         this.groupBox7.Controls.Add(this.label12);
         this.groupBox7.Controls.Add(this.RecorderErrorTextBox);
         this.groupBox7.Controls.Add(this.label11);
         this.groupBox7.Controls.Add(this.VolumeLevelMeter);
         this.groupBox7.Location = new System.Drawing.Point(7, 665);
         this.groupBox7.Name = "groupBox7";
         this.groupBox7.Size = new System.Drawing.Size(538, 101);
         this.groupBox7.TabIndex = 36;
         this.groupBox7.TabStop = false;
         this.groupBox7.Text = "Example of not using Snagit recording UI";
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(561, 778);
         this.Controls.Add(this.groupBox7);
         this.Controls.Add(this.groupBox6);
         this.Controls.Add(this.RecordButton);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.SelectionGroupBox);
         this.Controls.Add(this.OptionsGroupBox);
         this.Name = "Form1";
         this.Text = "Snagit COM Vide Recording";
         this.OptionsGroupBox.ResumeLayout(false);
         this.SelectionGroupBox.ResumeLayout(false);
         this.SelectionGroupBox.PerformLayout();
         this.groupBox1.ResumeLayout(false);
         this.outputTabControl.ResumeLayout(false);
         this.tabNone.ResumeLayout(false);
         this.tabNone.PerformLayout();
         this.tabFile.ResumeLayout(false);
         this.tabFile.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.tabFTP.ResumeLayout(false);
         this.tabFTP.PerformLayout();
         this.groupBox5.ResumeLayout(false);
         this.groupBox5.PerformLayout();
         this.groupBox4.ResumeLayout(false);
         this.groupBox4.PerformLayout();
         this.groupBox3.ResumeLayout(false);
         this.groupBox3.PerformLayout();
         this.groupBox6.ResumeLayout(false);
         this.groupBox6.PerformLayout();
         this.groupBox7.ResumeLayout(false);
         this.groupBox7.PerformLayout();
         this.ResumeLayout(false);

      }
		#endregion

      #region The main entry point for the application
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
      #endregion

      #region Event Handlers Defined
      //For more information about the Snagit recorder event handlers please
      //see the Snagit COM documentation under _IRecorderEvents Methods.
      
      //Example of handling volume level events
      public delegate void UpdateVolumeLevelTextCallback( string text );
      private void UpdateVolumeLevel( string text )
      {
         VolumeLevelMeter.Text = text; //volume level as a percentage
      }

      //Event handler for the OnAudioVolumeLevel recorder event
      public void RecorderVolumeLevelEventListener( int percent )
      {
         VolumeLevelMeter.Invoke( new UpdateVolumeLevelTextCallback( this.UpdateVolumeLevel ), new object[]
                                                                          {
                                                                             percent.ToString()
                                                                          });
      }

      //Example of how to be notified of the recording region selected by the user
      private void UpdateRecordingRegion( int startX, int startY, int width, int height )
      {
         if ( chkShowRegion.Checked )
         {
            String msg = "Selected Recording Region " + Environment.NewLine;
            msg += "   startX: " + startX + Environment.NewLine;
            msg += "   startY: " + startY + Environment.NewLine;
            msg += "    width: " + width + Environment.NewLine;
            msg += "   height: " + height;
            MessageBox.Show(msg);
         }
      }

      //Event handler for the OnSelectedRecordingRegion recorder event
      public void RecordingRegionUpdateEventListener( int startX, int startY, int width, int height )
      {
         var ShowRecordingRegionThread = new Thread( new ThreadStart( () => UpdateRecordingRegion( startX, startY, width, height ) ) );
         ShowRecordingRegionThread.Start();
      }

      //Example of catching recorder errors
      public delegate void UpdateRecorderErrorTextCallback( string errorText );
      private void UpdateRecorderErrorText( string errorText )
      {
         RecorderErrorTextBox.Text = errorText;
      }

      //Event handler for the OnRecorderError recorder event
      public void RecorderErrorTextEventListener( SNAGITLib.snagRecorderError errorNum )
      {
         string strError = errorNum == 0 ? "None" : errorNum.ToString();

         //There are two categories of errors: 1)From the recorder  2)From the encoder
         if (errorNum == snagRecorderError.srErrEncoderThrownCode || errorNum == snagRecorderError.srErrRecorderThrownCode)
         {
            bool bRecorderCode = errorNum == snagRecorderError.srErrRecorderThrownCode; //Determine if originating from the recorder
            uint uCode = bRecorderCode ? snagVideo.RecorderErrorCode : snagVideo.EncoderErrorCode;
            string strErrMsg = (bRecorderCode ? "Recorder" : "Encoder") + " error code";
            ShowRecorderErrorMessage( strErrMsg, uCode.ToString() );
         } 

         RecorderErrorTextBox.Invoke( new UpdateRecorderErrorTextCallback( this.UpdateRecorderErrorText ), new object[]
                                                                          {
                                                                             strError
                                                                          } );
      }

      //Example of handling the various recorder states
      public delegate void UpdateStopButtonCallback( bool bEnabled );
      private void UpdateStopButton( bool bEnabled )
      {
         bttnStop.Enabled = bEnabled;
      }

      public delegate void UpdateStartButtonCallback( String newLabel );
      private void UpdateStartButton( String newLabel )
      {
         bttnToggle.Text = newLabel;
      }

      //Event handler for the OnRecorderStateChange recorder event
      public void RecorderStateEventListener( snagRecorderState newState )
      {
         String StartLabel = "Start";
         bool bEnableStop = false;

         switch ( newState )
         {
            case snagRecorderState.srStateRecording:
            {
               StartLabel = "[Pause]";
               bEnableStop = true;
               break;
            }

            case snagRecorderState.srStatePausing:
            {
               StartLabel = "[Resume]";
               bEnableStop = true;
               break;
            }

            case snagRecorderState.srStateCounting:
            {
               bEnableStop = true;
               break;
            }

            case snagRecorderState.srReselecting:
            case snagRecorderState.srStateStopping:
            case snagRecorderState.srStateInitialized:
            default:
            {
               StartLabel = "Start";
               bEnableStop = false;
               break;
            }
         }
         bttnToggle.Invoke( new UpdateStartButtonCallback( this.UpdateStartButton ), new object[] { StartLabel } );
         bttnStop.Invoke( new UpdateStopButtonCallback( this.UpdateStopButton ), new object[] { bEnableStop } );
      }

      //Example of handling audio device muting
      public delegate void UpdateMuteMicCallback( bool bMuted );
      private void UpdateMicMuteButton( bool bMuted )
      {
         chkMuteMic.Checked = bMuted;
      }

      //Event handler for the OnMuteDeviceChanged recorder event
      public void RecorderMuteMicEventListener( bool bMuted )
      {
         chkMuteMic.Invoke( new UpdateMuteMicCallback( this.UpdateMicMuteButton ), new object[] { bMuted } );
      }

      //Example of handling system audio muting
      public delegate void UpdateMuteSystemAudioCallback( bool bMuted );
      private void UpdateSystemAudioMuteButton( bool bMuted )
      {
         chkMuteSystemAudio.Checked = bMuted;
      }

      //Event handler for the OnMuteSystemAudioChanged recorder event
      public void RecorderMuteSystemAudioEventListener( bool bMuted )
      {
         chkMuteSystemAudio.Invoke( new UpdateMuteSystemAudioCallback( this.UpdateSystemAudioMuteButton ), new object[] { bMuted } );
      }

      #region Hook/Unhook Snagit Event Handlers
      private void HookupRecorderEvents()
      {
         //Hook up event handlers to the Snagit _IRecorderEvents
         snagVideo.OnAudioVolumeLevel += RecorderVolumeLevelEventListener;             //Volume level percent(integer)
         snagVideo.OnRecorderError += RecorderErrorTextEventListener;                  //Recorder error(enum snagRecorderError)
         snagVideo.OnRecorderStateChange += RecorderStateEventListener;                //State changes in the recorder(enum snagRecorderState)
         snagVideo.OnMuteDeviceChanged += RecorderMuteMicEventListener;                //Occurs when using the Snagit recording UI and the user clicks the mute device control(boolean)
         snagVideo.OnMuteSystemAudioChanged += RecorderMuteSystemAudioEventListener;   //Occurs when using the Snagit recording UI and the user clicks the mute system audio control(boolean)
         snagVideo.OnSelectedRecordingRegion += RecordingRegionUpdateEventListener;    //Occurs when using the Snagit selection UI (xOffset,yOffset, width and height as type long)
      }

      private void UnhookRecorderEvents()
      {
         //Remove event handlers when finished
         snagVideo.OnAudioVolumeLevel -= RecorderVolumeLevelEventListener;
         snagVideo.OnRecorderError -= RecorderErrorTextEventListener;
         snagVideo.OnRecorderStateChange -= RecorderStateEventListener;
         snagVideo.OnMuteDeviceChanged -= RecorderMuteMicEventListener;
         snagVideo.OnMuteSystemAudioChanged -= RecorderMuteSystemAudioEventListener;
         snagVideo.OnSelectedRecordingRegion -= RecordingRegionUpdateEventListener;
      }
      #endregion
      #endregion

      #region Set Defaults
      private void SetDefaultOptions()
      {
         // Get a list of audio recording devices on the system
         Array devicenames, deviceids;
         snagVideo.AudioDevices( out devicenames, out deviceids );
         for ( int i = 0; i <= devicenames.GetUpperBound( 0 ); i++ )
         {
            // retrieve the audio devices
            AudioDevice.Items.Add( new AudioDevice( (int) deviceids.GetValue( i ), devicenames.GetValue( i ).ToString() ) );
         }

         // Default to the first audio recording device
         if ( AudioDevice.Items.Count > 0 )
         {
            AudioDevice.SelectedIndex = 0;
         }

         //Default to specify output location
         SpecifyFolderRButton.Checked = true;
         LastUsedFolderRButton.Checked = false;

         //Default the output destination folder to the desktop
         FileOutputFolderPath.Text = Environment.GetFolderPath( Environment.SpecialFolder.Desktop );
      }
      #endregion

      #region Selection Input UI Options
      private void ShowWindowSelectionOptions(bool bShow)
      {
         //Hide most of the fields
         PointXLabel.Hide();
         PointYLabel.Hide();
         WidthLabel.Hide();
         HeightLabel.Hide();
         RegionHeight.Hide();
         RegionWidth.Hide();
         UseStartPos.Hide();
         StartXPos.Hide();
         StartYPos.Hide();
         StartXLabel.Hide();
         StartYLabel.Hide();
         XPos.Hide();
         YPos.Hide();
         WinHandle.Hide();
         WindowPosLabel.Hide();
         xLabel.Hide();
         WinHandleLabel.Hide();

         //Show/hide the window selection options
         if (bShow)
            WindowInputSelection.Show();
         else
            WindowInputSelection.Hide();
      }
      private void CaptureTypeSelectedIndexChanged(object sender, EventArgs e)
      {
         switch (CaptureType.Text)
         {
            case "Region":
               {
                  ShowWindowSelectionOptions(false);
                  snagVideo.Input = SNAGITLib.snagVideoInput.sviRegion;
                  break;
               }
            case "Fixed Region":
               {
                  ShowWindowSelectionOptions(false);
                  snagVideo.Input = SNAGITLib.snagVideoInput.sviRegion;
                  WidthLabel.Show();
                  HeightLabel.Show();
                  RegionHeight.Show();
                  RegionWidth.Show();
                  UseStartPos.Show();
                  StartXPos.Show();
                  StartYPos.Show();
                  StartXLabel.Show();
                  StartYLabel.Show();

                  //Default the height/width
                  RegionHeight.Text = "240";
                  RegionWidth.Text = "320";
                  break;
               }
            case "Window":
               {
                  ShowWindowSelectionOptions(true);
                  snagVideo.Input = SNAGITLib.snagVideoInput.sviWindow;
                  break;
               }
            default: //All-in-One
               {
                  ShowWindowSelectionOptions(false);
                  snagVideo.Input = SNAGITLib.snagVideoInput.sviCapture;
                  break;
               }
         }

         EnableRecorderButtons();
      }

      private void InputSelectionSelectedIndexChanged(object sender, EventArgs e)
      {
         switch (WindowInputSelection.Text)
         {
            case "Window Handle":
               {
                  XPos.Hide();
                  YPos.Hide();
                  PointXLabel.Hide();
                  PointYLabel.Hide();
                  WindowPosLabel.Hide();
                  xLabel.Hide();
                  WinHandle.Show();
                  WinHandleLabel.Show();
                  break;
               }
            case "Point on the Desktop":
               {
                  WinHandle.Hide();
                  WinHandleLabel.Hide();
                  XPos.Show();
                  YPos.Show();
                  WindowPosLabel.Show();
                  PointXLabel.Show();
                  PointYLabel.Show();
                  xLabel.Show();
                  break;
               }
            default:
               {
                  XPos.Hide();
                  YPos.Hide();
                  PointXLabel.Hide();
                  PointYLabel.Hide();
                  WinHandle.Hide();
                  WindowPosLabel.Hide();
                  xLabel.Hide();
                  WinHandleLabel.Hide();
                  break;
               }
         }

         EnableRecorderButtons();
      }

      private void UseStartPos_CheckedChanged(object sender, EventArgs e)
      {
         StartXPos.Enabled = UseStartPos.Checked;
         StartYPos.Enabled = UseStartPos.Checked;
      }

      private void chkBxHideSnagitRecorderUI_CheckedChanged( object sender, EventArgs e )
      {
         EnableRecorderButtons();
      }

      #endregion

	   #region Output UI
      private void OutputSelectedIndexChanged(object sender, EventArgs e)
      {
         String selection = OutputSelector.Text;

         switch(selection)
         {
            case "File":
            {
               outputTabControl.SelectTab(tabFile);
               snagVideo.Output = SNAGITLib.snagVideoOutput.svoFile;
               break;
            }
            case "FTP":
            {
               outputTabControl.SelectTab(tabFTP);
               snagVideo.Output = SNAGITLib.snagVideoOutput.svoFTP;
               break;
            }
            default: //None - preview in editor
            {
               outputTabControl.SelectTab(tabNone);
               snagVideo.Output = SNAGITLib.snagVideoOutput.svoNone;
               break;
            }
         }
      }
      private void FileNamingSelection_SelectedIndexChanged(object sender, EventArgs e)
      {
         String selection = FileNamingSelector.Text;

         switch (selection)
         {
            case "Fixed":
            {
               NumberOfDigitsLabel.Hide();
               FileDigitsEdit.Hide();
               FilenameLabel.Show();
               FixedFilename.Show();
               SpecifyFolderRButton.Checked = true;
               LastUsedFolderRButton.Enabled = false;
               snagVideo.OutputVideoFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmFixed;
               break;
            }
            case "Prompt":
            {
               NumberOfDigitsLabel.Hide();
               FileDigitsEdit.Hide();
               FilenameLabel.Hide();
               FixedFilename.Hide();
               LastUsedFolderRButton.Enabled = true;
               snagVideo.OutputVideoFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmPrompt;
               break;
            }
            default: //Auto file naming
            {
               NumberOfDigitsLabel.Show();
               FileDigitsEdit.Show();
               FilenameLabel.Show();
               FixedFilename.Show();
               SpecifyFolderRButton.Checked = true;
               LastUsedFolderRButton.Enabled = false;
               snagVideo.OutputVideoFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmAuto;

               //Default the filename prefix if it is empty
               String strPreFix = FixedFilename.Text;
               if (strPreFix.Length == 0)
               {
                  FixedFilename.Text = "SNAG";
               }
               break;
            }
         }//end switch

         //Default the destination folder if it is empty
         String strFolder = FileOutputFolderPath.Text;
         if (strFolder.Length == 0 && !LastUsedFolderRButton.Checked)
         {
            strFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileOutputFolderPath.Text = strFolder;
         }

      }
      private void FileFolderRadioChanged(object sender, EventArgs e)
      {
         if (LastUsedFolderRButton.Checked)
         {
            BrowseOutputFolderButton.Enabled = false;
            FileOutputFolderPath.Enabled = false;
         }
         else
         {
            BrowseOutputFolderButton.Enabled = true;
            FileOutputFolderPath.Enabled = true;
         }
      }
      private void OutputTabSelectionChanged(object sender, EventArgs e)
      {
         OutputSelector.SelectedItem = outputTabControl.SelectedTab.Text;
      }
      private void FTPProxyCheckbox_CheckedChanged(object sender, EventArgs e)
      {
         FTPProxyServerEdit.Enabled = FTPProxyCheckbox.Checked;
      }
      private void FTPAuthenticationCheckbox_CheckedChanged(object sender, EventArgs e)
      {
         FTPUsername.Enabled = FTPAuthenticationCheckbox.Checked;
         FTPPassword.Enabled = FTPAuthenticationCheckbox.Checked;
      }
      private void FTPFileNamingSelector_SelectedIndexChanged(object sender, EventArgs e)
      {
         FTPNumberOfDigitsEdit.Enabled = (FTPFileNamingSelector.Text == "Auto");
      }
      private void BrowseOutputFolderButton_Click(object sender, EventArgs e)
      {
         FileOutputFolderPath.Text = BrowseOutputFolder();
      }
      #endregion

      #region UI input validation
      //Returns false if input validation fails.
      private bool SetInputOptions()
      {
         //Selection Input options
         if ( snagVideo.Input == snagVideoInput.sviRegion && RegionHeight.Visible )
         {
            //Fixed Region
            snagVideo.InputRegionOptions.SelectionMethod = snagRegionSelectionMethod.srsmFixed;
            snagVideo.InputRegionOptions.Height = Convert.ToInt16( RegionHeight.Text );
            snagVideo.InputRegionOptions.Width = Convert.ToInt16( RegionWidth.Text );
            snagVideo.InputRegionOptions.StartX = 0;
            snagVideo.InputRegionOptions.StartY = 0;
            snagVideo.InputRegionOptions.UseStartPosition = UseStartPos.Checked;

            if ( UseStartPos.Checked )
            {
               snagVideo.InputRegionOptions.StartX = Convert.ToInt16( StartXPos.Text );
               snagVideo.InputRegionOptions.StartY = Convert.ToInt16( StartYPos.Text );
            }
         } 
         else if ( snagVideo.Input == snagVideoInput.sviWindow && XPos.Visible )
         {
            //Window based on position
            snagVideo.InputWindowOptions.XPos = Convert.ToInt16( XPos.Text );
            snagVideo.InputWindowOptions.YPos = Convert.ToInt16( YPos.Text );
            snagVideo.InputWindowOptions.SelectionMethod = snagWindowSelectionMethod.swsmPoint;
         } 
         else if ( snagVideo.Input == snagVideoInput.sviWindow && WinHandle.Visible )
         {
            //Window base on a handle
            Int32 nHwnd = 0;
            try
            {
               nHwnd = Convert.ToInt32( WinHandle.Text );
            } 
            catch
            {
               //Must be in hexadecimal...
               try
               {
                  String strHandle = WinHandle.Text.ToLower();
                  if ( strHandle.IndexOf( 'x' ) > 0 )
                  {//Strip off the 0X prefix if it is present
                     strHandle = strHandle.Substring( strHandle.IndexOf( 'x' ) + 1 );
                  }
                  nHwnd = Int32.Parse( strHandle, System.Globalization.NumberStyles.HexNumber );
               } 
               catch ( Exception e )
               {
                  String strErr = "Error setting input options.\nException: " + e.Message;
                  MessageBox.Show( strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                  return false;
               }
            }

            snagVideo.InputWindowOptions.Handle = Convert.ToInt32( nHwnd );
            snagVideo.InputWindowOptions.SelectionMethod = snagWindowSelectionMethod.swsmHandle;
         }

         //Misc options
         snagVideo.UseMagnifierWindow = ShowSelectionMagnifier.Checked;
         snagVideo.IncludeCursor = IncludeCursor.Checked;
         snagVideo.EnablePreviewWindow = ChkPreviewWindow.Checked;
         snagVideo.HideRecordingUI = chkBxHideSnagitRecorderUI.Checked;
         if ( AudioDevice.SelectedItem != null )
         {
            snagVideo.AudioDevice = ((AudioDevice)AudioDevice.SelectedItem).AudioId;
         }

         //Perform Moov atom optimization on the MP4 file?
         //Note: The optimization is done by default and you do not even need
         //      to specify the format unless you want to disable the option.
         SNAGITLib.MP4FormatClass mp4Format = new SNAGITLib.MP4FormatClass();
         mp4Format.DisableMOOVAtomOptimization = DisableMoovOptimization.Checked;
         snagVideo.VideoFormat = mp4Format;

         return true; //succeeded
      }
      
      //Returns false if input validation fails.
      private bool SetFileNamingOptions()
      {
         if ( OutputSelector.Text != "File" )
         {
            return true;
         }

         //Validate input
         if ( FileOutputFolderPath.TextLength < 3 ||
            ( FileNamingSelector.Text == "Fixed" && FixedFilename.TextLength < 1 ) ||
            ( FileNamingSelector.Text == "Auto" && FileDigitsEdit.TextLength < 1 ) )
         {
            return false;
         }

         //Set output filename options
         snagVideo.OutputVideoFile.Directory = FileOutputFolderPath.Text;
         snagVideo.OutputVideoFile.Filename = FixedFilename.Text;
         snagVideo.OutputVideoFile.AutoFilePrefix = FixedFilename.Text;
         snagVideo.OutputVideoFile.AutoNumPrefixDigits = Convert.ToInt16( FileDigitsEdit.Text );

         return true;
      }

      //Returns false if input validation fails.
      private bool SetFTPOptions()
      {
         if ( OutputSelector.Text != "FTP" )
         {
            return true;
         }

         //Validate input:
         if ( FTPServerEdit.TextLength < 1 ||
            ( FTPAuthenticationCheckbox.Checked && ( FTPUsername.TextLength < 1 || FTPPassword.TextLength < 1 ) ) ||
            ( FTPProxyCheckbox.Checked && FTPProxyServerEdit.TextLength < 1 ) )
         {
            return false;
         }

         //FTP Output Options:  
         snagVideo.OutputFTPOptions.EnableProgressDialog = FTPProgressCheckbox.Checked;
         snagVideo.OutputFTPOptions.UsePassiveFTP = PassiveFTPCheckbox.Checked;
         snagVideo.OutputFTPOptions.Port = ( TFPServerPortEdit.Text.Length > 0 ) ? Convert.ToInt16( TFPServerPortEdit.Text ) : 21;
         snagVideo.OutputFTPOptions.RemotePath = RemoteFTPPathEdit.Text;
         snagVideo.OutputFTPOptions.Server = FTPServerEdit.Text;

         //Remote authentication
         snagVideo.OutputFTPOptions.ServerRequiresAuthentication = FTPAuthenticationCheckbox.Checked;
         if ( FTPAuthenticationCheckbox.Checked )
         {
            snagVideo.OutputFTPOptions.UserName = FTPUsername.Text;
            snagVideo.OutputFTPOptions.Password = FTPPassword.Text;
         }

         //Remote file naming options
         snagVideo.OutputFTPOptions.UseAutomaticFileNaming = ( FTPFileNamingSelector.Text == "Auto" );
         snagVideo.OutputFTPOptions.SequenceLimit = Convert.ToInt16( FTPNumberOfDigitsEdit.Text );
         snagVideo.OutputFTPOptions.UseTempFileReplace = FTPTempReplaceCheckbox.Checked;
         snagVideo.OutputFTPOptions.Filename = FTPRemoteFilename.Text;

         //Proxy server settings
         snagVideo.OutputFTPOptions.UseProxyServer = FTPProxyCheckbox.Checked;
         if ( FTPProxyCheckbox.Checked )
         {
            snagVideo.OutputFTPOptions.ProxyServer = FTPProxyServerEdit.Text;
         }

         return true;
      }
      #endregion

      #region Utility Methods
      private String BrowseOutputFolder()
      {
         String folderPath = "";
         FolderBrowserDialog browseFoldersDialog = new FolderBrowserDialog();
         browseFoldersDialog.Description = "Select a folder";
         
         if (browseFoldersDialog.ShowDialog() == DialogResult.OK)
         {
            folderPath = browseFoldersDialog.SelectedPath;
         }

         return folderPath;
      }

      private void EnableRecorderButtons()
      {
         if ( CaptureType.Text == "Select Capture Type" )
         {
            //Do not enable any buttons yet.
            return;
         }

         RecordButton.Enabled = !chkBxHideSnagitRecorderUI.Checked;
         bttnToggle.Enabled = chkBxHideSnagitRecorderUI.Checked;
         bttnStop.Enabled = false;
     }

      private void ShowRecorderErrorMessage( String strMsg, String strError )
      {
         String errMsg = strMsg;
         if ( strError.Length > 0 )
         {
            errMsg += ":" + Environment.NewLine + strError;
         }
         MessageBox.Show( errMsg );
      }

	   #endregion

      #region Recorder Controls
      //Simply set some recording options, start the recording and use the Snagit recording UI.
      private void Capture_Click( object sender, EventArgs e )
      {
         if ( !SetInputOptions() )
         {
            return;
         }
         if (!SetFileNamingOptions())
         {
            ShowRecorderErrorMessage("Invalid file output options!", "");
            return;
         }
         if (!SetFTPOptions())
         {
            ShowRecorderErrorMessage("Invalid FTP output options!", "");
            return;
         }

         // Try to initiate the capture, catch any errors and display an 
         // appropriate error message.  Here, SnagIt expiration error is shown
         // as an example. See the Snagit COM documentation for the rest of the
         // error types in the snagError enum.
         try
         {
            snagVideo.Capture(); //Starts the Snagit selection UI and then displays the recording UI
            bttnShowInfo.Enabled = true;
         } 
         catch
         {
            if ( snagVideo.LastError == SNAGITLib.snagError.serrSnagItExpired )
            {
               ShowRecorderErrorMessage( "Unable to capture", "SnagIt evaluation has expired" );
            }
         }
      }

      //Example for using your own recorder controls. Here we use the same button
      //to start the capture and to pause/resume recording
      private void bttnToggle_Click( object sender, EventArgs e )
      {
         //Only initialize when first starting the recording.
         if ( bttnToggle.Text == "Start" )
         {
            UnhookRecorderEvents();

            if ( !SetInputOptions() )
            {
               return;
            }
            if (!SetFileNamingOptions())
            {
               ShowRecorderErrorMessage("Invalid file output options!", "");
               return;
            }
            if (!SetFTPOptions())
            {
               ShowRecorderErrorMessage("Invalid FTP output options!", "");
               return;
            }

            HookupRecorderEvents();
            bttnShowInfo.Enabled = true;
         }

         //Call the recorder actions on a separate thread so as not to lock up the UI
         var StartThread = new Thread( new ThreadStart( () => StartThreadWorker() ) );
         StartThread.Start();
      }
      private void StartThreadWorker()
      {
         try
         {
            snagVideo.Start(); //Tells Snagit to start recording now. Recording region must already be set by input options.
         } 
         catch
         {
            ShowRecorderErrorMessage( "Error starting recording", snagVideo.LastRecorderError.ToString() );
         }
      }

	   private void bttnStop_Click( object sender, EventArgs e )
      {
         //Reset the button label
         bttnToggle.Text = "Start";

         bttnShowInfo.Enabled = false;

         //Call the recorder actions on a separate thread so as not to lock up the UI
         var StopThread = new Thread( new ThreadStart( () => StopThreadWorker() ) );
         StopThread.Start();
      }
      private void StopThreadWorker()
      {
         try
         {
            snagVideo.Stop();
            UnhookRecorderEvents();
         } 
         catch
         {
            ShowRecorderErrorMessage( "Error stopping recording", snagVideo.LastRecorderError.ToString() );
         }
      }

      private void chkMuteMic_CheckedChanged( object sender, EventArgs e )
      {
         //Call the recorder actions on a separate thread so as not to lock up the UI
         var MuteMicThread = new Thread( new ThreadStart( () => MuteMicThreadWorker() ) );
         MuteMicThread.Start();
      }
      private void MuteMicThreadWorker()
      {
         try
         {
            snagVideo.MuteMic = chkMuteMic.Checked;
         } 
         catch
         {
            ShowRecorderErrorMessage( "Error muting mic", snagVideo.LastRecorderError.ToString() );
         }
      }

      private void chkMuteSystemAudio_CheckedChanged( object sender, EventArgs e )
      {
         //Call the recorder actions on a separate thread so as not to lock up the UI
         var MuteSystemAudioThread = new Thread( new ThreadStart( () => MuteSystemAudioThreadWorker() ) );
         MuteSystemAudioThread.Start();
      }
      private void MuteSystemAudioThreadWorker()
      {
         try
         {
            snagVideo.MuteSystemAudio = chkMuteSystemAudio.Checked;
         } 
         catch
         {
            ShowRecorderErrorMessage( "Error muting system audio", snagVideo.LastRecorderError.ToString() );
         }
      }

      //Example of how to get recording stats
      private void bttnShowInfo_Click( object sender, EventArgs e )
      {
         //Call the recorder actions on a separate thread so as not to lock up the UI
         var ShowRecordingInfoThread = new Thread( new ThreadStart( () => ShowRecordingInfoThreadWorker() ) );
         ShowRecordingInfoThread.Start();
      }
      private void ShowRecordingInfoThreadWorker()
      {
         float frameRate = snagVideo.AverageFrameRate;
         uint duration = snagVideo.RecordingDuration;
         uint totalFrames = snagVideo.FrameCount;
         String strMsg = "Average Framerate: " + frameRate.ToString() + Environment.NewLine;
         strMsg += "         Duration: " + duration.ToString() + " ms" + Environment.NewLine;
         strMsg += "     Total Frames: " + totalFrames.ToString() + Environment.NewLine;
         strMsg += "  Audio Mic Muted: " + snagVideo.MuteMic + Environment.NewLine;
         strMsg += "  Sys Audio Muted: " + snagVideo.MuteSystemAudio;
         MessageBox.Show( strMsg );
      }
      #endregion

   }
}
