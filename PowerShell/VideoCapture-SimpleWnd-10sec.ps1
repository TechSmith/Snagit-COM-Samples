#---------------------------------------------------------------------------
# This is a Powershell sample application to demonstrates how to
# perform a Snagit COM video capture and save it to a file.
# The code has been set up to capture from a window, capture approximately
# 10 seconds of video, and preview the video in the Snagit Editor.
# Note: This sample requires Snagit 11.1.0 or later.
# 
# Support e-mail: support@techsmith.zendesk.com
# This software is provided under the MIT License (http://opensource.org/licenses/MIT)
# Copyright (c) 2015 TechSmith Corporation
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
#---------------------------------------------------------------------------

# PowerShell can't import type library from a COM Object
# See http://msdn.microsoft.com/en-us/library/hh228154.aspx
# So let's import those enums from a separate powershell script
. .\Snagit_enums.ps1

$object = New-Object -ComObject SNAGIT.VideoCapture

#
# set the input to Window
#
$object.Input = [snagVideoInput]::siiWindow

#
# set the output to None
#
$object.Output = [snagVideoOutput]::svoNone

#
# set other properties
#
$object.EnablePreviewWindow = $true
$object.IncludeCursor = $true
$object.OutputVideoFile.Directory = $env:Temp

#
# trigger the capture
#
$object.Capture()

#
# Wait until the capture is done
#
while (!$object.IsCaptureDone)
{
    sleep 1
    Write-Host "Recording Duration: " $object.RecordingDuration "ms"
    Write-Host "Frame Count: " $object.FrameCount

    # stop after 10 seconds
    if ( $object.RecordingDuration -ge 10000 )
    {
        $object.Stop();
    }
}
"`nDone"

# pause if needed
if ($host.name -eq 'ConsoleHost')
{
    Write-Host "Press any key to continue ..."

    $x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
}
