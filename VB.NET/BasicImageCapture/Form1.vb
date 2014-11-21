'======================================================================================
' This is a VB.NET image capture example. It demonstrates the basics of using the 
' Snagit COM to perform a window or a region image capture and prompt to save the
' file. It also shows how to include the cursor in the capture and to preview the
' resulting capture in the Snagit Editor before saving to file.
' Note: This sample requires Snagit 6.2.0 or later.
'
' Support e-mail: support@techsmith.zendesk.com
' This software is provided under the MIT License (http://opensource.org/licenses/MIT)
' Copyright (c) 2014 TechSmith Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this
' software and associated documentation files (the "Software"), to deal in the Software
' without restriction, including without limitation the rights to use, copy, modify, merge,
' publish, distribute, sub-license, and/or sell copies of the Software, and to permit persons
' to whom the Software is furnished to do so, subject to the following conditions:
'    The above copyright notice and this permission notice shall be included in all copies
'    or substantial portions of the Software.
'    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
'    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
'    PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
'    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
'    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'======================================================================================

Public Class Form1
   Inherits System.Windows.Forms.Form

   ' The reference to the SnagIt COM object was created by
   ' right-clicking on "References" in the solution explorer, and choosing
   ' "Add Reference". Then, under the COM tab, find "SnagIt 1.0 Type Library".
   ' Press "Select" here and click OK. Now VB.NET knows about the SnagIt COM
   ' object and recognizes the functions/properties held in SNAGITLib

   ' Here we declare a global ImageCapture object. This instantiates an 
   ' instance of the COM server. This cannot be local to a function, or it 
   ' would be destroyed before the capture was completed!
   Dim SnagImg As SNAGITLib.ImageCapture = New SNAGITLib.ImageCaptureClass()

#Region " Windows Form Designer generated code "

   Public Sub New()
      MyBase.New()

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call

   End Sub

   'Form overrides dispose to clean up the component list.
   Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
         If Not (components Is Nothing) Then
            components.Dispose()
         End If
      End If
      MyBase.Dispose(disposing)
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   Friend WithEvents BtnWindow As System.Windows.Forms.Button
   Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
   Friend WithEvents ChkPreviewWindow As System.Windows.Forms.CheckBox
   Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
   Friend WithEvents BtnRegion As System.Windows.Forms.Button
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents ChkIncludeCursor As System.Windows.Forms.CheckBox
   <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.BtnWindow = New System.Windows.Forms.Button()
      Me.ChkIncludeCursor = New System.Windows.Forms.CheckBox()
      Me.ChkPreviewWindow = New System.Windows.Forms.CheckBox()
      Me.GroupBox1 = New System.Windows.Forms.GroupBox()
      Me.GroupBox2 = New System.Windows.Forms.GroupBox()
      Me.BtnRegion = New System.Windows.Forms.Button()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.GroupBox1.SuspendLayout()
      Me.GroupBox2.SuspendLayout()
      Me.SuspendLayout()
      '
      'BtnWindow
      '
      Me.BtnWindow.Location = New System.Drawing.Point(16, 16)
      Me.BtnWindow.Name = "BtnWindow"
      Me.BtnWindow.Size = New System.Drawing.Size(96, 24)
      Me.BtnWindow.TabIndex = 0
      Me.BtnWindow.Text = "Window Capture"
      '
      'ChkIncludeCursor
      '
      Me.ChkIncludeCursor.Location = New System.Drawing.Point(16, 24)
      Me.ChkIncludeCursor.Name = "ChkIncludeCursor"
      Me.ChkIncludeCursor.Size = New System.Drawing.Size(104, 24)
      Me.ChkIncludeCursor.TabIndex = 1
      Me.ChkIncludeCursor.Text = "Include Cursor"
      '
      'ChkPreviewWindow
      '
      Me.ChkPreviewWindow.Location = New System.Drawing.Point(16, 48)
      Me.ChkPreviewWindow.Name = "ChkPreviewWindow"
      Me.ChkPreviewWindow.Size = New System.Drawing.Size(112, 24)
      Me.ChkPreviewWindow.TabIndex = 2
      Me.ChkPreviewWindow.Text = "Preview Window"
      '
      'GroupBox1
      '
      Me.GroupBox1.Controls.Add(Me.ChkPreviewWindow)
      Me.GroupBox1.Controls.Add(Me.ChkIncludeCursor)
      Me.GroupBox1.Location = New System.Drawing.Point(171, 16)
      Me.GroupBox1.Name = "GroupBox1"
      Me.GroupBox1.Size = New System.Drawing.Size(136, 80)
      Me.GroupBox1.TabIndex = 3
      Me.GroupBox1.TabStop = False
      Me.GroupBox1.Text = "Options"
      '
      'GroupBox2
      '
      Me.GroupBox2.Controls.Add(Me.BtnRegion)
      Me.GroupBox2.Controls.Add(Me.BtnWindow)
      Me.GroupBox2.Location = New System.Drawing.Point(35, 16)
      Me.GroupBox2.Name = "GroupBox2"
      Me.GroupBox2.Size = New System.Drawing.Size(128, 80)
      Me.GroupBox2.TabIndex = 4
      Me.GroupBox2.TabStop = False
      Me.GroupBox2.Text = "Capture Type"
      '
      'BtnRegion
      '
      Me.BtnRegion.Location = New System.Drawing.Point(16, 48)
      Me.BtnRegion.Name = "BtnRegion"
      Me.BtnRegion.Size = New System.Drawing.Size(96, 23)
      Me.BtnRegion.TabIndex = 1
      Me.BtnRegion.Text = "Region Capture"
      '
      'Label1
      '
      Me.Label1.Location = New System.Drawing.Point(43, 104)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(264, 40)
      Me.Label1.TabIndex = 5
      Me.Label1.Text = "For simplicity, we will always output to a file, the name of which will be suppli" & _
    "ed by the user when prompted."
      '
      'Form1
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(343, 158)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.GroupBox2)
      Me.Controls.Add(Me.GroupBox1)
      Me.Name = "Form1"
      Me.Text = "Snagit COM Image Capture Sample"
      Me.GroupBox1.ResumeLayout(False)
      Me.GroupBox2.ResumeLayout(False)
      Me.ResumeLayout(False)

   End Sub

#End Region

   Private Sub BtnWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnWindow.Click
      ' Choose an input and an output:
      SnagImg.Input = SNAGITLib.snagImageInput.siiWindow
      SnagImg.Output = SNAGITLib.snagImageOutput.sioFile

      ' Prompting for the file name is the default, but it cannot hurt to 
      ' set this explicitly
      SnagImg.OutputImageFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmPrompt

      ' Show Preview Window?
      SnagImg.EnablePreviewWindow = ChkPreviewWindow.Checked

      ' Include cursor if set
      SnagImg.IncludeCursor = ChkIncludeCursor.Checked

      ' Begin the capture process trapping any errors.
      Try
         SnagImg.Capture()
      Catch captureError As Exception
         If SnagImg.LastError = SNAGITLib.snagError.serrSnagItExpired Then
            MsgBox("Unable to capture: SnagIt evaluation has expired")
         End If
      End Try

   End Sub

   Private Sub BtnRegion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRegion.Click

      ' Choose an input and an output:
      SnagImg.Input = SNAGITLib.snagImageInput.siiRegion
      SnagImg.Output = SNAGITLib.snagImageOutput.sioFile

      ' Prompting for the file name is the default, but it cannot hurt to set this explicitly
      SnagImg.OutputImageFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmPrompt

      ' Show Preview Window?
      SnagImg.EnablePreviewWindow = ChkPreviewWindow.Checked

      ' Include cursor if set
      SnagImg.IncludeCursor = ChkIncludeCursor.Checked

      ' Begin the capture process trapping any errors.
      Try
         SnagImg.Capture()
      Catch captureError As Exception
         If SnagImg.LastError = SNAGITLib.snagError.serrSnagItExpired Then
            MsgBox("Unable to capture: SnagIt evaluation has expired")
         End If
      End Try

   End Sub

End Class
