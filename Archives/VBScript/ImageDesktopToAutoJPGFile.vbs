'/////////////////////////////////////////////////////////////////////////////
'// ImageDesktopToAutoJPGFile.vbs - VB Script image capture example
'//
'// SnagIt COM server example code
'//
'// Support e-mail: support@techsmith.zendesk.com
'//
'// Copyright 2003-2014 TechSmith Corporation.  All rights reserved.
'/////////////////////////////////////////////////////////////////////////////

On Error Resume Next

' Defines found in SnagIt.tlb
Const InputDesktop = 0
Const OutputFile = 2
Const AutoFileNaming = 2
Const ImageTypeJPEG = 3
Const ErrorSnagItExpired = 1

' Create and image capture object
set obj = CreateObject("SNAGIT.ImageCapture")

' Set the input and output
obj.Input = InputDesktop
obj.Output = OutputFile

' set output to jpeg
obj.OutputImageFile.LoadImageDefaults ImageTypeJPEG

' autofile naming to root directory on c:
obj.OutputImageFile.FileNamingMethod = AutoFileNaming
obj.OutputImageFile.Directory = "c:\"

' use preview window
obj.EnablePreviewWindow = false

' do the capture
obj.Capture()

' sleep until the capture finishes
' this makes sure that the script doesn't
' exit, which would cancel the capture
Do Until obj.IsCaptureDone
      WScript.Sleep 10
Loop

If obj.LastError = ErrorSnagItExpired Then
   MsgBox "Unable to capture: SnagIt evaluation has expired"
End If

On Error GoTo 0
