#---------------------------------------------------------------------------
# This is a Powershell sample application to demonstrates how to
# perform a Snagit COM image capture and save it to a file.
# The image can be previewed in the Snagit Editor if you decide so.
# Note: This code is backward compatible clear back to Snagit 8.1.0.
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

$object = New-Object -ComObject SNAGIT.ImageCapture

#
# set the input
#
write-host
foreach ($v in [enum]::GetValues([snagImageInput]))
{
    write-host ([int]$v) for $v
}
$object.Input = [snagImageInput](Read-Host "`nWhat input do you want to use?");

#
# set the output
#
write-host
foreach ($v in [enum]::GetValues([snagImageOutput]))
{
    write-host ([int]$v) for $v
}
$object.Output = [snagImageOutput](Read-Host "`nWhat output do you want to use?");

#
# Preview or no preview?
#
if ( $object.Output -ne [snagImageOutput]::sioNone )
{
    $object.EnablePreviewWindow = [Int32](Read-Host "`nEnable Preview (0:no preview, 1:with preview)?");
}
else
{
    $object.EnablePreviewWindow = $true
}

#
# Set file type (if output is File, Mail or FTP)
#
if ( $object.Output -eq [snagImageOutput]::sioFile -Or
     $object.Output -eq [snagImageOutput]::sioMail -Or
     $object.Output -eq [snagImageOutput]::sioFTP )
{
    write-host
    foreach ($v in [enum]::GetValues([snagImageFileType]))
    {
        write-host ([int]$v) for $v
    }
    $object.OutputImageFile.LoadImageDefaults( [snagImageFileType](Read-Host "`nWhat file type do you want to use?") )

    #
    # Set File Naming method to auto
    #
    $object.OutputImageFile.FileNamingMethod = [snagOuputFileNamingMethod]::sofnmAuto;
}

#
# Set output directory to TEMP
#
$object.OutputImageFile.Directory = $env:Temp

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
}
if ( $object.Output -eq ([snagImageOutput]::sioFile) )
{
    write-host "File written: " $object.LastFileWritten
}
"`nDone"

# pause if needed
if ($host.name -eq 'ConsoleHost')
{
    Write-Host "Press any key to continue ..."

    $x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
}