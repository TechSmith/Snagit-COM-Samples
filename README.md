## Snagit COM Samples and Documentation ##

Using Snagit’s COM Server, you can easily incorporate Snagit’s screen capture functionality into your organization’s proprietary applications. The COM Server lets you customize capture instructions for Snagit, automate the capture process, and programmatically control Snagit’s entire array of image and video capture features from within your own application.
You can access Snagit’s capture functionality from any programming language that supports COM, including the following:

* C#
* C/C++
* Visual Basic
* Fox Pro 9
* Ruby
* VBScript

### Installation Information ###

After installing Snagit, the COM server should be registered and ready for use with your application. If the COM server is not running, you can re-register the COM server by completing the following:  

1. Open Windows Command Prompt with Administrator privileges.
2. Navigate to the location where Snagit32.exe is installed.
3. Run the following command: `snagit32.exe /register`

### Documentation ###
The most recent version of the Snagit COM Server documentation is located [here](https://assets.techsmith.com/Docs/Snagit-2020-COM-Server-Guide.pdf). For older versions, view the Archives folder.

### Samples Overview ###
We have two comprehensive samples that demonstrate how to use all of the COM interfaces exposed by Snagit. The remaining samples show the basics for using the Snagit COM interface in various programming languages. The samples are organized by language. For languages supported by Visual Studio, we have provided samples that support 2010 and 2013. For older samples, view the Archives folder.

###Description of Samples###
**[C# Image Capture Sample](CSharp/Image Capture Samples/Comprehensive/) ( CSharp\Image Capture Samples\Comprehensive)**

This is a comprehensive C# sample application to demonstrate the use of the Snagit
COM interface for image capture. It covers every option exposed to COM by Snagit,
for capturing images, applying filters, and sharing.
Summary:
* Instantiating the Snagit video capture class
* Handling image capture events
* Setting all input/output/filter options

Note: This sample was updated to use the Snagit 13 COM interface. Most
      of what is shown is backward compatible to Snagit 8.1.0.

**[C# Image Capture Sample](CSharp/Image Capture Samples/Simple/) ( CSharp\Image Capture Samples\Simple\ )**

This is a simple C# sample application to demonstrate how to
perform a Snagit COM image capture and save it to a file.
The image can be previewed in the Snagit Editor. Click 
the green "Finish" button in the Snagit Editor to prompt the user for
the name and location to save the capture. It will default to
the JPEG file type.
 
Note: This sample was updated to use the Snagit 13 COM interface
      and was set up to support .NET 4.
      This code is backward compatible to Snagit 8.1.0.

**[C# Video Capture Sample](CSharp/Video Capture Samples/Comprehensive) ( CSharp\Video Capture Samples\Comprehensive\ )**

This is a comprehensive C# sample application to demonstrate the use of the Snagit
COM interface for recording video. It covers every option for recording
video that is exposed to COM by Snagit.

Summary:
* Instantiating the Snagit video capture class
* Handling recorder events
* Setting all input/output/recording options
* Demonstrates how you can use your own recording interface
````
* Getting recording devices
* Getting user-selected region to record
* Catching volume level events
* Handling recorder states (start/stop pause/resume)
* Catching recorder error events
* Getting recording stats
* Disabling MP4 Moov atom optimization (if not streaming video from the net)
````
Note: This sample requires Snagit 11.1.0 or later.

**[C# Video Capture Sample](CSharp/Video Capture Samples/Simple/) ( CSharp\Video Capture Samples\Simple\ )**

This is a simple C# sample application to demonstrate how to
perform a Snagit COM video capture and save it to a file.
The recording can be previewed in the Snagit Editor. Clicking
the green "Finish" button in the Snagit Editor prompts the user for
the name and location to save the capture. Snagit only saves
video captures to the MP4 format using H.264/AAC encoding.
 
Note: This sample was created using the Snagit 12.2.2 COM interface and was set up to support .NET 4. This sample requires Snagit 11.1.0 or later.

**[PowerShell Samples](PowerShell/) ( PowerShell\ )**

Those are samples that demonstrates how to perform a Snagit COM image or video
capture using using PowerShell:
- [Interactive image capture](PowerShell/ImageCapture-Interactive.ps1)
(ImageCapture-Interactive.ps1) allows you to interactively select the
input, output, preview (on/off) and file type. It then performs the image
capture and saves to the TEMP directory when appropriate.
- [Simple window image capture](PowerShell/ImageCapture-SimpleWnd.ps1)
(ImageCapture-SimpleWnd.ps1) captures from a window (which you have to
interactively select). It then saves to file (no preview) as a PNG file
in the TEMP directory.
- [Simple window video capture](PowerShell/VideoCapture-SimpleWnd-10sec.ps1)
(VideoCapture-SimpleWnd-10sec.ps1) captures from a window (which you have
to interactively select) and send the video to Snagit Editor. It is set up
to include the cursor in the recording, and record for 10 seconds.

**[Ruby Image Capture Sample](Ruby/ImageCaptureRubyScript.rb) ( Ruby\ImageCaptureRubyScript.rb )**

This is a sample Ruby script that demonstrates how to
perform a Snagit COM image capture and save it to a file.
It allows you to interactively select a window to capture.
The image is then previewed in the Snagit Editor. Click 
the green "Finish" button in the Snagit Editor to prompt the user for
the name and location to save the capture. It will default to
the PNG file type. 

**[Visual FoxPro 9 Image Capture Sample](VFoxPro9) ( VFoxPro9\main.prg )**

Visual FoxPro 9 sample program that uses the
Snagit COM interface (imported into snagitcom.prg)
to peform a simple image capture that is previewed
in Snagit Editor. This sample requires
Snagit 13.x since the interfaces were imported
from that version of Snagit.

**[VBScript Image Capture Sample](VBScript/ExcelVBScript.vbs) ( VBScript\ExcelVBScript.vbs )**

This is a VBScript example for triggering a capture from within
Microsoft Excel 2010 and pasting the resulting capture into the currently selected cell. The file CaptureSample.xlsm demonstrates calling the VBScript.
Note: This sample requires Snagit 8.1.0 or later.

**[VBScript TWAIN Image Capture Sample](VBScript/TwainToAutoJPEGFile.vbs) ( VBScript\TwainToAutoJPEGFile.vbs )**

This is a VBScript example to show how to capture from a TWAIN device
and output to a JPEG file.
Note: This sample requires Snagit 8.1.0 or later.

**[VB.Net Basic Image Capture Sample](VB.NET/BasicImageCapture/) ( VB.NET\BasicImageCapture\ )**

This is a VB.Net image capture example. It demonstrates the basics of using the 
Snagit COM to perform a window or a region image capture and prompt to save the
file. It also shows how to include the cursor in the capture and to preview the
resulting capture in the Snagit Editor.
Note: This sample requires Snagit 6.2.0 or later.


### Support Information ###
You can contact our technical support at this email address: support@techsmith.zendesk.com

### License Information ###
This software is provided under the MIT License ( http://opensource.org/licenses/MIT )

Copyright (c) 2014 TechSmith Corporation

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
