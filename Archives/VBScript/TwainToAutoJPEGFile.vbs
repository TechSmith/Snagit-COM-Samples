'======================================================================================
' This is a VBScript example to show how to capture from a TWAIN device
' and output to a JPEG file.
'
' Note: This sample requires Snagit 8.1.0 or later.
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

' Create and image capture object
set SnagImg = CreateObject("SNAGIT.ImageCapture")

' Capture Settings
Const InputType = 19                            ' Use Twain Input (Region Input = 4)
Const NotificationType = 3                      ' Show All Notifications

' Output Settings
Const OutputType = 2                            ' Output to file
Const OutputFileType = 3                        ' Output to JPG
Const OutputFileQuality = 100                   ' 100%, highest quality setting
Const OutputFileDirectory = "c:\Images\SnagIt"  ' Output directory
Const FileNamingMethod = 2                      ' Auto name files
Const EnablePreviewWindow = true                ' Preview on/off (true/false)

' Apply input settings
SnagImg.Input = InputType
SnagImg.NotificationType = NotificationType
 
' Apply output settings   
SnagImg.Output = OutputType
SnagImg.OutputImageFile.FileType = OutputFileType
SnagImg.OutputImageFile.Quality = OutputFileQuality
SnagImg.OutputImageFile.Directory = OutputFileDirectory
SnagImg.OutputImageFile.FileNamingMethod = FileNamingMethod
SnagImg.EnablePreviewWindow = EnablePreviewWindow

' Do the capture
SnagImg.Capture

' Sleep until the capture finishes this makes sure that the script doesn't
' exit, which would cancel the capture
Do Until SnagImg.IsCaptureDone        
      WScript.Sleep 10  
Loop                    

Select Case SnagImg.LastError
    Case -1 
        MsgBox "Error: Unknown Error."        
    Case 1
        MsgBox "Unable to capture: SnagIt evaluation has expired."
    Case 2
        MsgBox "Error: Invalid Input."
    Case 3
        MsgBox "Error: Invalid Output."
    Case 4
        MsgBox "Error: Snagit Busy. Probably doing another capture."
    Case 5
        MsgBox "Error: Invalid Scroll Delay."
    Case 6
        MsgBox "Error: Invalid Delay."
    Case 7
        MsgBox "Error: Invalid Color Effect Value."
    Case 8
        MsgBox "Error: Invalid File Progressive Value."
    Case 9
        MsgBox "Error: Invalid File Quality Value."
    Case 10
        MsgBox "Error: Invalid Output File Directory."
    Case 11
        MsgBox "Error: Invalid Color Conversion Value."
    Case 12
        MsgBox "Error: Invalid Image Resolution."
'    Case Else
'    The only other condition is '0' which is 'SUCCESS'
End Select

