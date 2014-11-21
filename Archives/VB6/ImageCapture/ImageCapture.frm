VERSION 5.00
Begin VB.Form ImageCaptureSample 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Image Capture"
   ClientHeight    =   4875
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   3570
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4875
   ScaleWidth      =   3570
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame2 
      Caption         =   "Filter Options"
      Height          =   1215
      Left            =   240
      TabIndex        =   10
      Top             =   2160
      Width           =   2655
      Begin VB.CheckBox CheckBorder 
         Caption         =   "Add Border"
         Height          =   255
         Left            =   240
         TabIndex        =   12
         Top             =   720
         Width           =   2295
      End
      Begin VB.CheckBox CheckBrigter 
         Caption         =   "Make Brighter"
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
      Top             =   3480
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
      Left            =   840
      TabIndex        =   6
      Top             =   1680
      Value           =   1  'Checked
      Width           =   1455
   End
   Begin VB.CheckBox CheckPreview 
      Caption         =   "Show Preview"
      Height          =   255
      Left            =   840
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
      Width           =   2655
   End
   Begin VB.ComboBox ComboInput 
      Height          =   315
      Left            =   840
      Style           =   2  'Dropdown List
      TabIndex        =   1
      Top             =   360
      Width           =   2655
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
Attribute VB_Name = "ImageCaptureSample"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'/////////////////////////////////////////////////////////////////////////////
'// ImageCapture.frm - VB6 image capture example
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

'Create an image capture object with event support
Dim WithEvents ImageCapture As SNAGITLib.ImageCapture
Attribute ImageCapture.VB_VarHelpID = -1

Private Sub Capture_Click()
    'Set the intput to the input currently selected in the input combo box
    ImageCapture.Input = ComboInput.ItemData(ComboInput.ListIndex)
    
    'Set the output to the output currently selected in the output combo box
    ImageCapture.Output = ComboOutput.ItemData(ComboOutput.ListIndex)
    
    'Enable preview window if needed
    ImageCapture.EnablePreviewWindow = (CheckPreview.Value = vbChecked)
    
    'Simplify auto scroll option to be on or off
    If CheckAutoScroll.Value Then
        ImageCapture.AutoScrollOptions.AutoScrollMethod = sasmVertical
    Else
        ImageCapture.AutoScrollOptions.AutoScrollMethod = sasmNone
    End If
    
    ' Brightness filter (checkbox)
    If CheckBrigter.Value = vbChecked Then
        ImageCapture.Filters.ColorEffects.Brightness = 50   '%
    Else
        ' Make sure to set back to default for subsequent captures
        ImageCapture.Filters.ColorEffects.Brightness = 0
    End If
        
    ' Enable a border (checkbox)
    If CheckBorder.Value = vbChecked Then
        ImageCapture.Filters.Border.EnableBorder = True
        ImageCapture.Filters.Border.TotalWidth = 3
    Else
        ' Make sure to set back to default for subsequent captures
        ImageCapture.Filters.Border.EnableBorder = False
    End If
        
    'Initiate the capture trapping any errors.  Display a message for
    'the error case where the SnagIt evaluation has expired.
    On Error Resume Next
    ImageCapture.Capture
        
End Sub

Private Sub Form_Load()

    'Create the text capture object
    Set ImageCapture = CreateObject("SnagIt.ImageCapture")
    
    'initalize the combo boxes
    SetupComboInput
    SetupComboOutput
    
End Sub

' Filling the input combo box with the inputs:
' Note some inputs are not usable from a normal window
Private Sub SetupComboInput()
    ComboInput.AddItem "Window"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiWindow
        
    ComboInput.AddItem "Extended Window"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiExtendedWindow
    
    ComboInput.AddItem "Region"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiRegion
    
    ComboInput.AddItem "Clipboard"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiClipboard
    
    ComboInput.AddItem "Custom Scroll"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiCustomScroll
    
    ComboInput.AddItem "Full Screen"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiDesktop
    
    ComboInput.AddItem "Shapes - Ellipse"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiEllipse
    
    ComboInput.AddItem "Shapes - Freehand"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiFreehand
    
    ComboInput.AddItem "Shapes - Polygon"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiPolygon
    
    ComboInput.AddItem "Shapes - Triangle"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiTriangle
    
    ComboInput.AddItem "Graphic File"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiGraphicFile
    
    ComboInput.AddItem "Menu"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiMenu
    
    ComboInput.AddItem "Object"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiObject
    
    ComboInput.AddItem "Shapes - Rounded Rectangle"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiRoundedRect
        
    ComboInput.AddItem "Program File"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiProgramFile
    
    ComboInput.AddItem "TWAIN source"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiTWAIN
    
    ComboInput.AddItem "Desktop Wallpaper"
    ComboInput.ItemData(ComboInput.NewIndex) = SNAGITLib.siiWallpaper
    
    ComboInput.Text = ComboInput.List(0)
End Sub

' filling the output combo box with the outputs:
Private Sub SetupComboOutput()
    ComboOutput.AddItem "Clipboard"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.sioClipboard
    
    ComboOutput.AddItem "File"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.sioFile
    
    ComboOutput.AddItem "FTP"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.sioFTP
    
    ComboOutput.AddItem "Mail"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.sioMail
    
    ComboOutput.AddItem "Printer"
    ComboOutput.ItemData(ComboOutput.NewIndex) = SNAGITLib.sioPrinter
    
    ComboOutput.Text = ComboOutput.List(0)
End Sub

Private Sub ImageCapture_OnError(ByVal Error As SNAGITLib.snagError)

    If Error = serrSnagItExpired Then
        MsgBox "Unable to capture: SnagIt evaluation has expired"
    End If
    
End Sub

' This function handles the OnStateChange event fired by the ImageCapture interface
' Event handling is done simply by putting the event after an _ after the object that
' fires the event. That is, Object_Event
Private Sub ImageCapture_OnStateChange(ByVal capState As SNAGITLib.snagCaptureState)

    If capState = scsCaptureSucceeded Then
        LastCaptureStatus.Caption = "Succeeded"
        LastCaptureStatus.ForeColor = RGB(0, 128, 0)
    ElseIf capState = scsCaptureFailed Then
        LastCaptureStatus.Caption = "Failed"
        LastCaptureStatus.ForeColor = RGB(128, 0, 0)
    End If
    
End Sub
