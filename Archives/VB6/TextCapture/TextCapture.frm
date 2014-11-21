VERSION 5.00
Begin VB.Form TextCaptureSample 
   Caption         =   "Text Capture"
   ClientHeight    =   4950
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   3180
   LinkTopic       =   "Form1"
   ScaleHeight     =   4950
   ScaleWidth      =   3180
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame2 
      Caption         =   "Filter Options"
      Height          =   1215
      Left            =   240
      TabIndex        =   10
      Top             =   1920
      Width           =   2655
      Begin VB.CheckBox CheckCollapseBlankColumns 
         Caption         =   "Collapse Blank Columns"
         Height          =   255
         Left            =   240
         TabIndex        =   12
         Top             =   720
         Width           =   2295
      End
      Begin VB.CheckBox CheckRemoveBlankLines 
         Caption         =   "Remove Blank Lines"
         Height          =   255
         Left            =   240
         TabIndex        =   11
         Top             =   360
         Width           =   2295
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Status"
      Height          =   615
      Left            =   240
      TabIndex        =   7
      Top             =   3240
      Width           =   2655
      Begin VB.Label LastCaptureStatus 
         Caption         =   "None"
         Height          =   255
         Left            =   1200
         TabIndex        =   9
         Top             =   240
         Width           =   1215
      End
      Begin VB.Label Label3 
         Caption         =   "Last Capture:"
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   240
         Width           =   975
      End
   End
   Begin VB.CheckBox CheckAutoScroll 
      Caption         =   "Auto Scroll"
      Height          =   195
      Left            =   1200
      TabIndex        =   6
      Top             =   1680
      Width           =   1455
   End
   Begin VB.CheckBox CheckPreview 
      Caption         =   "Show Preview"
      Height          =   255
      Left            =   1200
      TabIndex        =   3
      Top             =   1320
      Value           =   1  'Checked
      Width           =   1455
   End
   Begin VB.ComboBox ComboOutput 
      Height          =   315
      Left            =   840
      Style           =   2  'Dropdown List
      TabIndex        =   2
      Top             =   840
      Width           =   1815
   End
   Begin VB.ComboBox ComboInput 
      Height          =   315
      Left            =   840
      Style           =   2  'Dropdown List
      TabIndex        =   1
      Top             =   360
      Width           =   1815
   End
   Begin VB.CommandButton Capture 
      Caption         =   "Capture"
      Height          =   375
      Left            =   2040
      TabIndex        =   0
      Top             =   4320
      Width           =   975
   End
   Begin VB.Label Label2 
      Caption         =   "Output"
      Height          =   255
      Left            =   120
      TabIndex        =   5
      Top             =   840
      Width           =   615
   End
   Begin VB.Label Label1 
      Caption         =   "Input:"
      Height          =   255
      Left            =   120
      TabIndex        =   4
      Top             =   360
      Width           =   615
   End
End
Attribute VB_Name = "TextCaptureSample"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'/////////////////////////////////////////////////////////////////////////////
'// TextCapture.frm - VB6 text capture example
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

'Create a text capture object with event support
Dim WithEvents TextCapture1 As SNAGITLib.TextCapture
Attribute TextCapture1.VB_VarHelpID = -1

Private Sub Capture_Click()
    'Set the intput to the input currently selected in the input combo box
    TextCapture1.Input = ComboInput.ItemData(ComboInput.ListIndex)
    
    'Set the output to the output currently selected in the output combo box
    TextCapture1.Output = ComboOutput.ItemData(ComboOutput.ListIndex)
    
    'Enable preview window if needed
    TextCapture1.EnablePreviewWindow = (CheckPreview.Value = vbChecked)
    
    'Simplify auto scroll option to be on or off
    If (CheckAutoScroll.Value = vbChecked) Then
        TextCapture1.AutoScrollOptions.AutoScrollMethod = sasmVertical
    Else
        TextCapture1.AutoScrollOptions.AutoScrollMethod = sasmNone
    End If
    
    'Load filter settings
    TextCapture1.Filters.Layout.RemoveBlankLines = (CheckRemoveBlankLines.Value = vbChecked)
    TextCapture1.Filters.Layout.CollapseBlankColunms = (CheckCollapseBlankColumns.Value = vbChecked)
    
    'Initiate the capture trapping any errors.  Display a message for
    'the error case where the SnagIt evaluation has expired.
    On Error Resume Next
    TextCapture1.Capture
End Sub

Private Sub Form_Load()

    'Create the text capture object
    Set TextCapture1 = CreateObject("SnagIt.TextCapture")
    
    'initalize the combo boxes
    SetupComboInput
    SetupComboOutput
    
    'initalize filter values from text capture object
    CheckRemoveBlankLines.Value = TextCapture1.Filters.Layout.RemoveBlankLines
    CheckCollapseBlankColumns.Value = TextCapture1.Filters.Layout.CollapseBlankColunms
        
End Sub

Private Sub SetupComboInput()
    ComboInput.AddItem "Desktop"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.stiDesktop
    
    ComboInput.AddItem "Window"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.stiWindow
    
    ComboInput.AddItem "Region"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.stiRegion
    
    ComboInput.AddItem "Clipboard"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.stiClipboard
    
    ComboInput.AddItem "Object"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.stiObject
    
    ComboInput.AddItem "CustomScroll"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.stiCustomScroll
    
    ComboInput.Text = ComboInput.List(0)
End Sub

Private Sub SetupComboOutput()
    ComboOutput.AddItem "Clipboard"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.stoClipboard
    
    ComboOutput.AddItem "File"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.stoFile
    
    ComboOutput.AddItem "FTP"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.stoFTP
    
    ComboOutput.AddItem "Mail"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.stoMail
    
    ComboOutput.AddItem "Printer"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.stoPrinter
    
    ComboOutput.Text = ComboOutput.List(0)
End Sub

'This function handles the OnError event fired by the TextCapture interface
Private Sub TextCapture1_OnError(ByVal Error As SNAGITLib.snagError)

    If TextCapture1.LastError = serrSnagItExpired Then
        MsgBox "Unable to capture: SnagIt evaluation has expired"
    End If

End Sub

'This function handles the OnStateChange event fired by the TextCapture interface
Private Sub TextCapture1_OnStateChange(ByVal capState As SNAGITLib.snagCaptureState)

    If capState = scsCaptureSucceeded Then
        LastCaptureStatus.Caption = "Succeeded"
        LastCaptureStatus.ForeColor = RGB(0, 128, 0)
    ElseIf capState = scsCaptureFailed Then
        LastCaptureStatus.Caption = "Failed"
        LastCaptureStatus.ForeColor = RGB(128, 0, 0)
    End If
    
End Sub


