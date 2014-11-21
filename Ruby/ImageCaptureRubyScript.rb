require 'win32ole'

# This is a sample Ruby script that demonstrates how to
# perform a Snagit COM image capture and save it to a file.
# It will allow you to interactively select a window to capture.
# The image is then previewed in the Snagit Editor. Clicking on
# The green "Finish" button in the editor will prompt you for
# the name and location to save the capture. It will default to
# the PNG file type as specified in the code below. 
#
# Support e-mail: support@techsmith.zendesk.com
# This software is provided under the MIT License (http://opensource.org/licenses/MIT)
# Copyright (c) 2014 TechSmith Corporation
#
# Permission is hereby granted, free of charge, to any person obtaining a copy of this
# software and associated documentation files (the "Software"), to deal in the Software
# without restriction, including without limitation the rights to use, copy, modify, merge,
# publish, distribute, sub-license, and/or sell copies of the Software, and to permit persons
# to whom the Software is furnished to do so, subject to the following conditions:
#    The above copyright notice and this permission notice shall be included in all copies
#    or substantial portions of the Software.
#    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
#    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
#    PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
#    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
#    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

snagit = WIN32OLE.new('Snagit.ImageCapture')

#set input capture type 
# siiDesktop = 0
# siiWindow = 1
# siiRegion = 4
# siiGraphicFile = 6
# siiClipboard = 7
# siiMenu = 9
# siiObject = 10
# siiFreehand = 12
# siiCustomScroll = 18
# siiTWAIN = 19
# siiExtendedWindow = 23
# siiCapture = 25  - use the default All-in-One selection UI
snagit.Input = 1    

#For window captures there are four ways
#that you can select the window to capture:
# swsmInteractive = 0  - Uses the Snagit selection UI so that the user can interactively make the selection
# swsmActive = 1       - Captures the current active window
# swsmHandle = 2       - Using a window handle (as a integer, not a hex-value)
# swsmPoint = 3        - Coordinates of a point located inside the window to capture
snagit.InputWindowOptions.SelectionMethod = 0   

#If you are capturing a window based on a handle, provide the window handle
#snagit.InputWindowOptions.Handle = 68422

#If you are capturing a window based on point, specify the point
#snagit.InputWindowOptions.XPos = 350 
#snagit.InputWindowOptions.YPos = 350 

#If you are not using the interactive selection mode you can
#still tell Snagit to auto-scroll the window without any user
#input. Specify where to start scrolling and which direction.
#snagit.AutoScrollOptions.ForegroundScrollingWindow = true

# sasspCurrent = 0
# sasspTop = 1
# sasspLeft = 2
# sasspTopLeft = 3
#snagit.AutoScrollOptions.StartingPosition = 3 

# sasmNone = 0      - Forces no scrolling when using interactive mode
# sasmVertical = 1
# sasmHorizontal = 2
# sasmBoth = 3
#snagit.AutoScrollOptions.AutoScrollMethod = 1 


#Specify the output 
#      sioNone = 0
#      sioPrinter = 1
#      sioFile = 2
#      sioClipboard = 4
#      sioMail = 8
#      sioFTP = 32
snagit.Output = 2    

#Enable preview in the editor
snagit.EnablePreviewWindow = true

#Specify the file naming method
# sofnmPrompt = 0
# sofnmFixed  = 1
# sofnmAuto   = 2
snagit.OutputImageFile.FileNamingMethod= 0   

#Set the file type
# siftBMP = 0
# siftTIFF = 2
# siftJPEG = 3
# siftGIF = 4
# siftPNG = 5
# siftTGA = 6
# siftRAS = 7
# siftWMF = 9
# siftEPS = 11
# siftOS2 = 17
# siftWFX = 18
# siftEMF = 19
# siftWPG = 20
# siftPSD = 21
# siftICO = 22
# siftCUR = 23
# siftPDF = 24
# siftSNAG = 25
# siftSWF = 26
# siftMHT = 27
snagit.OutputImageFile.FileType = 5    


#Do the capture 
begin
	snagit.Capture 
rescue WIN32OLERuntimeError
	puts "Failed to start capture!"
	return
end

#Wait for the capture to finish
while !snagit.IsCaptureDone 
end
 
puts "Finished."