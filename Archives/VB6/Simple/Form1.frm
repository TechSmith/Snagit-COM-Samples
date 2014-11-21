VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "SnagIt COM Example"
   ClientHeight    =   2550
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4455
   LinkTopic       =   "Form1"
   ScaleHeight     =   2550
   ScaleWidth      =   4455
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame2 
      Caption         =   "Options"
      Height          =   1335
      Left            =   2160
      TabIndex        =   1
      Top             =   120
      Width           =   2175
      Begin VB.CheckBox ChkPreviewWindow 
         Caption         =   "Preview Window"
         Height          =   375
         Left            =   120
         TabIndex        =   5
         Top             =   600
         Width           =   1935
      End
      Begin VB.CheckBox ChkIncludeCursor 
         Caption         =   "Include Cursor"
         Height          =   375
         Left            =   120
         TabIndex        =   4
         Top             =   240
         Width           =   1935
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Capture Type"
      Height          =   1335
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   1935
      Begin VB.CommandButton BtnRegion 
         Caption         =   "Region Capture"
         Height          =   375
         Left            =   240
         TabIndex        =   3
         Top             =   840
         Width           =   1455
      End
      Begin VB.CommandButton BtnWindow 
         Caption         =   "Window Capture"
         Height          =   375
         Left            =   240
         TabIndex        =   2
         Top             =   360
         Width           =   1455
      End
   End
   Begin VB.Label Label1 
      Caption         =   "For simplicity, we will always output to a file, the name of which will be supplied by the user when prompted."
      Height          =   735
      Left            =   120
      TabIndex        =   6
      Top             =   1680
      Width           =   4215
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'/////////////////////////////////////////////////////////////////////////////
'// Form1.frm - VB6 simple image capture example
'//
'// SnagIt COM server example code
'//
'// Support e-mail: support.snagitcom@techsmith.com
'//
'// Copyright 2003 TechSmith Corporation.  All rights reserved.
'/////////////////////////////////////////////////////////////////////////////


' To use the COM server you must add a "reference" to it by choosing
' "References" from the Project menu. Put a check-mark next to
' "SNAGIT 1.0 Type Library" and click "OK".

'Create an image capture object
Dim SnagImg As SNAGITLib.ImageCapture

Private Sub Form_Load()
    ' Instantiate an instance of the COM object
    Set SnagImg = CreateObject("SNAGIT.ImageCapture")
    
End Sub

Private Sub BtnRegion_Click()
    ' Choose an input and an output:
    SnagImg.Input = SNAGITLib.snagImageInput.siiRegion
    SnagImg.Output = SNAGITLib.snagImageOutput.sioFile

    ' Prompting for the file name is the default, but it cannot hurt to
    ' set this explicitly
    SnagImg.OutputImageFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmPrompt

    ' Show Preview Window?
    SnagImg.EnablePreviewWindow = (ChkPreviewWindow.Value = vbChecked)
    
    ' Include cursor if set
    SnagImg.IncludeCursor = (ChkIncludeCursor.Value = vbChecked)

    'Initiate the capture trapping any errors.  Display a message for
    'the error case where the SnagIt evaluation has expired.
    On Error GoTo captureError
        SnagImg.Capture
    Return
        
captureError:
    If SnagImg.LastError = serrSnagItExpired Then
        MsgBox "Unable to capture: SnagIt evaluation has expired"
    End If

End Sub

Private Sub BtnWindow_Click()
    ' Choose an input and an output:
    SnagImg.Input = SNAGITLib.snagImageInput.siiWindow
    SnagImg.Output = SNAGITLib.snagImageOutput.sioFile

    ' Prompting for the file name is the default, but it cannot hurt to
    ' set this explicitly
    SnagImg.OutputImageFile.FileNamingMethod = SNAGITLib.snagOuputFileNamingMethod.sofnmPrompt

    ' Show Preview Window?
    SnagImg.EnablePreviewWindow = (ChkPreviewWindow.Value = vbChecked)
    
    ' Include cursor if set
    SnagImg.IncludeCursor = (ChkIncludeCursor.Value = vbChecked)

    'Initiate the capture trapping any errors.  Display a message for
    'the error case where the SnagIt evaluation has expired.
    On Error GoTo captureError
        SnagImg.Capture
    Return
        
captureError:
    If SnagImg.LastError = serrSnagItExpired Then
        MsgBox "Unable to capture: SnagIt evaluation has expired"
    End If
    
End Sub


