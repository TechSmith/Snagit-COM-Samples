'/////////////////////////////////////////////////////////////////////////////
'// 8.1 - ImageWindowToAutoSWFFile.vbs - VB Script image capture example
'//
'// SnagIt COM server example code
'// This script shows some of the new functionality added in SnagIt v8.1
'//
'// Support e-mail: support@techsmith.zendesk.com
'//
'// Copyright 2003-2014 TechSmith Corporation.  All rights reserved.
'/////////////////////////////////////////////////////////////////////////////

On Error Resume Next

' Defines found in SnagIt.tlb
Const InputWindow          = 1
Const OutputFile           = 2
Const AutoFileNaming       = 2
Const ImageTypeSWF         = 26
Const ErrorSnagItExpired   = 1
Const shtLinksOnly         = 1
Const spOutsideBottom      = 11

' Create and image capture object
set obj = CreateObject("SNAGIT.ImageCapture")

' Set the input and output
obj.Input = InputWindow
obj.Output = OutputFile

' autofile naming to root directory on c:
obj.OutputImageFile.FileNamingMethod = AutoFileNaming
obj.OutputImageFile.Directory = "c:\"

' use preview window
obj.EnablePreviewWindow = false

' /////////////////////////////////////////////////////
' The following is new in SnagIt version 8.1

' Use Keep Links feature:
obj.HotspotType = shtLinksOnly

' place a caption, outside the image:
obj.Filters.Annotation.EnableCaption = true
obj.Filters.Annotation.CaptionText = "Example Caption: Window!"
obj.Filters.Annotation.CaptionOptions.Placement = spOutsideBottom

' set output to SWF
obj.OutputImageFile.LoadImageDefaults ImageTypeSWF   ' SWF file type new in v8.1

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

' Read out the capture results; new in v8.1
dim strResults
strResults = "Capture rect: ("
strResults = strResults + CStr( obj.CaptureResults.SelectedArea.StartX )
strResults = strResults + ", "
strResults = strResults + CStr( obj.CaptureResults.SelectedArea.StartY )
strResults = strResults + "), W="
strResults = strResults + CStr( obj.CaptureResults.SelectedArea.Width )
strResults = strResults + ", H="
strResults = strResults + CStr( obj.CaptureResults.SelectedArea.Height )

MsgBox strResults

On Error GoTo 0
