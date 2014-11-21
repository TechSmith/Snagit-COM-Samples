namespace SimpleImageCapture
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
         this.CaptureType = new System.Windows.Forms.ComboBox();
         this.MagnifierChkBx = new System.Windows.Forms.CheckBox();
         this.PreviewChkBx = new System.Windows.Forms.CheckBox();
         this.CursorChkBx = new System.Windows.Forms.CheckBox();
         this.MiscOptsGroupBox = new System.Windows.Forms.GroupBox();
         this.txtBoxErrors = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.CaptureBttn = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.MiscOptsGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // CaptureType
         // 
         this.CaptureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.CaptureType.FormattingEnabled = true;
         this.CaptureType.Items.AddRange(new object[] {
            "Desktop",
            "Window",
            "Region"});
         this.CaptureType.Location = new System.Drawing.Point(12, 23);
         this.CaptureType.Name = "CaptureType";
         this.CaptureType.Size = new System.Drawing.Size(121, 21);
         this.CaptureType.TabIndex = 1;
         this.CaptureType.SelectedIndexChanged += new System.EventHandler(this.CaptureType_SelectedIndexChanged);
         // 
         // MagnifierChkBx
         // 
         this.MagnifierChkBx.AutoSize = true;
         this.MagnifierChkBx.Location = new System.Drawing.Point(6, 65);
         this.MagnifierChkBx.Name = "MagnifierChkBx";
         this.MagnifierChkBx.Size = new System.Drawing.Size(98, 17);
         this.MagnifierChkBx.TabIndex = 22;
         this.MagnifierChkBx.Text = "Show magnifier";
         this.MagnifierChkBx.UseVisualStyleBackColor = true;
         // 
         // PreviewChkBx
         // 
         this.PreviewChkBx.AutoSize = true;
         this.PreviewChkBx.Location = new System.Drawing.Point(6, 42);
         this.PreviewChkBx.Name = "PreviewChkBx";
         this.PreviewChkBx.Size = new System.Drawing.Size(104, 17);
         this.PreviewChkBx.TabIndex = 21;
         this.PreviewChkBx.Text = "Preview in editor";
         this.PreviewChkBx.UseVisualStyleBackColor = true;
         // 
         // CursorChkBx
         // 
         this.CursorChkBx.AutoSize = true;
         this.CursorChkBx.Location = new System.Drawing.Point(6, 19);
         this.CursorChkBx.Name = "CursorChkBx";
         this.CursorChkBx.Size = new System.Drawing.Size(93, 17);
         this.CursorChkBx.TabIndex = 20;
         this.CursorChkBx.Text = "Include cursor";
         this.CursorChkBx.UseVisualStyleBackColor = true;
         // 
         // MiscOptsGroupBox
         // 
         this.MiscOptsGroupBox.Controls.Add(this.MagnifierChkBx);
         this.MiscOptsGroupBox.Controls.Add(this.PreviewChkBx);
         this.MiscOptsGroupBox.Controls.Add(this.CursorChkBx);
         this.MiscOptsGroupBox.Location = new System.Drawing.Point(12, 73);
         this.MiscOptsGroupBox.Name = "MiscOptsGroupBox";
         this.MiscOptsGroupBox.Size = new System.Drawing.Size(167, 86);
         this.MiscOptsGroupBox.TabIndex = 31;
         this.MiscOptsGroupBox.TabStop = false;
         this.MiscOptsGroupBox.Text = "Misc Options";
         // 
         // txtBoxErrors
         // 
         this.txtBoxErrors.Location = new System.Drawing.Point(50, 230);
         this.txtBoxErrors.Name = "txtBoxErrors";
         this.txtBoxErrors.Size = new System.Drawing.Size(223, 20);
         this.txtBoxErrors.TabIndex = 33;
         this.txtBoxErrors.Text = "None";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(12, 233);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(32, 13);
         this.label2.TabIndex = 32;
         this.label2.Text = "Error:";
         // 
         // CaptureBttn
         // 
         this.CaptureBttn.Location = new System.Drawing.Point(105, 181);
         this.CaptureBttn.Name = "CaptureBttn";
         this.CaptureBttn.Size = new System.Drawing.Size(75, 23);
         this.CaptureBttn.TabIndex = 34;
         this.CaptureBttn.Text = "Capture";
         this.CaptureBttn.UseVisualStyleBackColor = true;
         this.CaptureBttn.Click += new System.EventHandler(this.CaptureBttn_Click);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(139, 26);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(86, 13);
         this.label1.TabIndex = 35;
         this.label1.Text = "Select input type";
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 262);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.CaptureBttn);
         this.Controls.Add(this.txtBoxErrors);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.MiscOptsGroupBox);
         this.Controls.Add(this.CaptureType);
         this.Name = "Form1";
         this.Text = "Image Capture Sample";
         this.MiscOptsGroupBox.ResumeLayout(false);
         this.MiscOptsGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ComboBox CaptureType;
      private System.Windows.Forms.CheckBox MagnifierChkBx;
      private System.Windows.Forms.CheckBox PreviewChkBx;
      private System.Windows.Forms.CheckBox CursorChkBx;
      private System.Windows.Forms.GroupBox MiscOptsGroupBox;
      private System.Windows.Forms.TextBox txtBoxErrors;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button CaptureBttn;
      private System.Windows.Forms.Label label1;
   }
}

