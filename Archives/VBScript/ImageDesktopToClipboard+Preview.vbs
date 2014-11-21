'/////////////////////////////////////////////////////////////////////////////
'// ImageDesktopToClipboard+Preview.vbs - VB Script image capture example
'//
'// SnagIt COM server example code
'//
'// Support e-mail: support@techsmith.zendesk.com
'//
'// Copyright 2003-2014 TechSmith Corporation.  All rights reserved.
'/////////////////////////////////////////////////////////////////////////////

On Error Resume Next

' Create and image capture object
set obj = CreateObject("SNAGIT.ImageCapture")

' Defines found in SnagIt.tlb
Const InputDesktop = 0
Const OutputClipboard = 4
Const ErrorSnagItExpired = 1

' Set the input and output
obj.Input = InputDesktop
obj.Output = OutputClipboard 

' use preview window
obj.EnablePreviewWindow = true

' do the capture
obj.Capture

' sleep until the capture finishes
' this makes sure that the script doesn't
' exit, which would cancel the capture
Do Until obj.IsCaptureDone        
      WScript.Sleep 10  
Loop                    

If obj.LastError = ErrorSnagItExpired Then
   MsgBox "Unable to capture: SnagIt evaluation has expired"
End If