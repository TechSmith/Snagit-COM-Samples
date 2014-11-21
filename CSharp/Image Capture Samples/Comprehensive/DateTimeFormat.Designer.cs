namespace ImageCaptureSample
{
   partial class DateTimeFormatOptions
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose( bool disposing )
      {
         if ( disposing && ( components != null ) )
         {
            components.Dispose();
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
         this.OkBttn = new System.Windows.Forms.Button();
         this.CancelBttn = new System.Windows.Forms.Button();
         this.DateTimeOrderSelector = new System.Windows.Forms.ComboBox();
         this.WindowsTimeFormatChkBx = new System.Windows.Forms.CheckBox();
         this.WindowsDateFormatChkBx = new System.Windows.Forms.CheckBox();
         this.CustomDateTimeFormatChkBx = new System.Windows.Forms.CheckBox();
         this.CustomDateFormatTxtBx = new System.Windows.Forms.TextBox();
         this.CustomTimeFormatTxtBx = new System.Windows.Forms.TextBox();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.label5 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // OkBttn
         // 
         this.OkBttn.Location = new System.Drawing.Point(23, 282);
         this.OkBttn.Name = "OkBttn";
         this.OkBttn.Size = new System.Drawing.Size(75, 23);
         this.OkBttn.TabIndex = 56;
         this.OkBttn.Text = "Ok";
         this.OkBttn.UseVisualStyleBackColor = true;
         this.OkBttn.Click += new System.EventHandler(this.OkBttn_Click);
         // 
         // CancelBttn
         // 
         this.CancelBttn.Location = new System.Drawing.Point(183, 282);
         this.CancelBttn.Name = "CancelBttn";
         this.CancelBttn.Size = new System.Drawing.Size(75, 23);
         this.CancelBttn.TabIndex = 57;
         this.CancelBttn.Text = "Cancel";
         this.CancelBttn.UseVisualStyleBackColor = true;
         this.CancelBttn.Click += new System.EventHandler(this.CancelBttn_Click);
         // 
         // DateTimeOrderSelector
         // 
         this.DateTimeOrderSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.DateTimeOrderSelector.FormattingEnabled = true;
         this.DateTimeOrderSelector.Items.AddRange(new object[] {
            "Date only",
            "Time only",
            "Time then date",
            "Date then time"});
         this.DateTimeOrderSelector.Location = new System.Drawing.Point(12, 12);
         this.DateTimeOrderSelector.Name = "DateTimeOrderSelector";
         this.DateTimeOrderSelector.Size = new System.Drawing.Size(96, 21);
         this.DateTimeOrderSelector.TabIndex = 60;
         this.DateTimeOrderSelector.SelectedIndexChanged += new System.EventHandler(this.DateTimeOrderSelector_SelectedIndexChanged);
         // 
         // WindowsTimeFormatChkBx
         // 
         this.WindowsTimeFormatChkBx.AutoSize = true;
         this.WindowsTimeFormatChkBx.Enabled = false;
         this.WindowsTimeFormatChkBx.Location = new System.Drawing.Point(126, 12);
         this.WindowsTimeFormatChkBx.Name = "WindowsTimeFormatChkBx";
         this.WindowsTimeFormatChkBx.Size = new System.Drawing.Size(143, 17);
         this.WindowsTimeFormatChkBx.TabIndex = 61;
         this.WindowsTimeFormatChkBx.Text = "Use windows time format";
         this.WindowsTimeFormatChkBx.UseVisualStyleBackColor = true;
         // 
         // WindowsDateFormatChkBx
         // 
         this.WindowsDateFormatChkBx.AutoSize = true;
         this.WindowsDateFormatChkBx.Enabled = false;
         this.WindowsDateFormatChkBx.Location = new System.Drawing.Point(126, 35);
         this.WindowsDateFormatChkBx.Name = "WindowsDateFormatChkBx";
         this.WindowsDateFormatChkBx.Size = new System.Drawing.Size(145, 17);
         this.WindowsDateFormatChkBx.TabIndex = 62;
         this.WindowsDateFormatChkBx.Text = "Use windows date format";
         this.WindowsDateFormatChkBx.UseVisualStyleBackColor = true;
         // 
         // CustomDateTimeFormatChkBx
         // 
         this.CustomDateTimeFormatChkBx.AutoSize = true;
         this.CustomDateTimeFormatChkBx.Location = new System.Drawing.Point(9, 26);
         this.CustomDateTimeFormatChkBx.Name = "CustomDateTimeFormatChkBx";
         this.CustomDateTimeFormatChkBx.Size = new System.Drawing.Size(114, 17);
         this.CustomDateTimeFormatChkBx.TabIndex = 63;
         this.CustomDateTimeFormatChkBx.Text = "Use custom format";
         this.CustomDateTimeFormatChkBx.UseVisualStyleBackColor = true;
         this.CustomDateTimeFormatChkBx.CheckedChanged += new System.EventHandler(this.CustomDateTimeFormatChkBx_CheckedChanged);
         // 
         // CustomDateFormatTxtBx
         // 
         this.CustomDateFormatTxtBx.Location = new System.Drawing.Point(58, 55);
         this.CustomDateFormatTxtBx.MaxLength = 128;
         this.CustomDateFormatTxtBx.Name = "CustomDateFormatTxtBx";
         this.CustomDateFormatTxtBx.Size = new System.Drawing.Size(164, 20);
         this.CustomDateFormatTxtBx.TabIndex = 64;
         this.CustomDateFormatTxtBx.TabStop = false;
         this.CustomDateFormatTxtBx.WordWrap = false;
         // 
         // CustomTimeFormatTxtBx
         // 
         this.CustomTimeFormatTxtBx.Location = new System.Drawing.Point(58, 135);
         this.CustomTimeFormatTxtBx.MaxLength = 128;
         this.CustomTimeFormatTxtBx.Name = "CustomTimeFormatTxtBx";
         this.CustomTimeFormatTxtBx.Size = new System.Drawing.Size(164, 20);
         this.CustomTimeFormatTxtBx.TabIndex = 65;
         this.CustomTimeFormatTxtBx.TabStop = false;
         this.CustomTimeFormatTxtBx.WordWrap = false;
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.label5);
         this.groupBox1.Controls.Add(this.label6);
         this.groupBox1.Controls.Add(this.label4);
         this.groupBox1.Controls.Add(this.label3);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Controls.Add(this.CustomTimeFormatTxtBx);
         this.groupBox1.Controls.Add(this.CustomDateFormatTxtBx);
         this.groupBox1.Controls.Add(this.CustomDateTimeFormatChkBx);
         this.groupBox1.Location = new System.Drawing.Point(14, 76);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(255, 187);
         this.groupBox1.TabIndex = 66;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Custom Date/Time";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(94, 171);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(79, 13);
         this.label5.TabIndex = 71;
         this.label5.Text = "Use: h:mm:ss tt";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(57, 158);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(129, 13);
         this.label6.TabIndex = 70;
         this.label6.Text = "Example for: 11:43:53 AM";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(94, 91);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(77, 13);
         this.label4.TabIndex = 69;
         this.label4.Text = "Use: M/d/yyyy";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(57, 78);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(126, 13);
         this.label3.TabIndex = 68;
         this.label3.Text = "Example for: 11/12/2014";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(22, 138);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(30, 13);
         this.label2.TabIndex = 67;
         this.label2.Text = "Time";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(22, 58);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(30, 13);
         this.label1.TabIndex = 66;
         this.label1.Text = "Date";
         // 
         // DateTimeFormatOptions
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(281, 327);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.WindowsDateFormatChkBx);
         this.Controls.Add(this.WindowsTimeFormatChkBx);
         this.Controls.Add(this.DateTimeOrderSelector);
         this.Controls.Add(this.CancelBttn);
         this.Controls.Add(this.OkBttn);
         this.Name = "DateTimeFormatOptions";
         this.Text = "DateTimeFormatOptions";
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button OkBttn;
      private System.Windows.Forms.Button CancelBttn;
      private System.Windows.Forms.ComboBox DateTimeOrderSelector;
      private System.Windows.Forms.CheckBox WindowsTimeFormatChkBx;
      private System.Windows.Forms.CheckBox WindowsDateFormatChkBx;
      private System.Windows.Forms.CheckBox CustomDateTimeFormatChkBx;
      private System.Windows.Forms.TextBox CustomDateFormatTxtBx;
      private System.Windows.Forms.TextBox CustomTimeFormatTxtBx;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
   }
}