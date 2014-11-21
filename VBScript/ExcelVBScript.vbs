Sub Capture()

'======================================================================================
' This is a VBScript example for triggering a capture from within
' MSExcel 2010 and pasting the resulting capture into the current selected cell.
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

Dim strMsg As String
Dim dteWait

Dim objSnagit
Dim ImageFilters
Dim ColorSub

' Capture input type
Const siiDesktop = 0
Const siiWindow = 1
Const siiRegion = 4
Const siiGraphicFile = 6
Const siiClipboard = 7
Const siiMenu = 9
Const siiObject = 10
Const siiFreehand = 12
Const siiCustomScroll = 18
Const siiTWAIN = 19
Const siiExtendedWindow = 23
Const siiCapture = 25         'Use Snagit's default All-in-One selection UI (requires Snagit 9.0 or later)

' Window selection method
Const swsmInteractive = 0
Const swsmActive = 1
Const swsmHandle = 2
Const swsmPoint = 3

' Capture output type
Const sioNone = 0
Const sioPrinter = 1
Const sioFile = 2
Const sioClipboard = 4
Const sioMail = 8
Const sioFTP = 32

' Output image type
Const siftBMP = 0
Const siftPCX = 1
Const siftTIFF = 2
Const siftJPEG = 3
Const siftGIF = 4
Const siftPNG = 5
Const siftTGA = 6

' Output color depth
Const sicdAuto = 0
Const sicd1Bit = 1
Const sicd2Bit = 2
Const sicd3Bit = 3
Const sicd4Bit = 4
Const sicd5Bit = 5
Const sicd6Bit = 6
Const sicd7Bit = 7
Const sicd8Bit = 8
Const sicd16Bit = 16
Const sicd24Bit = 24
Const sicd32Bit = 32

' Output file naming method
Const sofnmPrompt = 0
Const sofnmFixed = 1
Const sofnmAuto = 2
Const scsmCustom = 2

' SnagIt object
Set objSnagit = CreateObject("SNAGIT.ImageCapture.1")
With objSnagit
    ' Set input options
    .Input = siiRegion
    .inputwindowoptions.selectionmethod = swsmInteractive
    .IncludeCursor = False
        
    ' Set output options
    'Copy image to the clipboard so we can paste it back into Excel
    .Output = sioClipboard
        
    .OutputImageFile.FileType = siftPNG
    .OutputImageFile.ColorDepth = sicd32Bit
        
    ' Capture
    .Capture
        
End With

'Hacky wait for the capture to finish since
dteWait = DateAdd("s", 4, Now())
Do Until (Now() > dteWait)
Loop

' Insert into active worksheet at the active cell or A1
On Error Resume Next
Err.Clear
ActiveSheet.Paste Destination:=ActiveCell
If Err.Number <> 0 Then
    ActiveSheet.Paste Destination:=ActiveSheet.Range("A1:A1")
End If


' Release the objects
Set objSnagit = Nothing

End Sub
