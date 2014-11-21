//---------------------------------------------------------------------------
// This is a sample C# sample application to demonstrates how to
// perform a Snagit COM image capture and save it to a file.
// The image can be previewed in the Snagit Editor. Clicking on
// The green "Finish" button in the editor will prompt you for
// the name and location to save the capture. It will default to
// the JPEG file type as specified in the code below. 
// Note: This sample was created using the Snagit 12.2.2 COM interface
//       and was setup to support .NET 4.
//       This code is backward compatible clear back to Snagit 8.1.0.
// 
// Support e-mail: support@techsmith.zendesk.com
// This software is provided under the MIT License (http://opensource.org/licenses/MIT)
// Copyright (c) 2014 TechSmith Corporation
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sub-license, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
//    The above copyright notice and this permission notice shall be included in all copies
//    or substantial portions of the Software.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//    PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//---------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using SNAGITLib;

namespace SimpleImageCapture
{
   public partial class Form1 : Form
   {
      // Declare the SnagIt ImageCapture object.  
      // NOTE: First you must add a reference to 
      // the SNAGIT 1.0 Type Library in order for
      // this line to compile. Please be sure to
      // set the Embed Interop Types to False in
      // the SNAGITLib reference properties.
      public readonly ImageCaptureClass SnagImg;

      public Form1()
      {
         InitializeComponent();

         // Create a new SnagIt ImageCapture object.
         // NOTE: First you must add a reference to 
         // the SNAGIT 1.0 Type Library  
         SnagImg = new ImageCaptureClass();
      
         //Default some controls
         CaptureBttn.Enabled = false;
         MagnifierChkBx.Enabled = false;

      }

      private void CaptureBttn_Click( object sender, EventArgs e )
      {
         // Set the output to file and prompt for the name
         SnagImg.Output = snagImageOutput.sioFile;
         SnagImg.OutputImageFile.FileNamingMethod = snagOuputFileNamingMethod.sofnmPrompt;

         // Default the file type to JPEG
         SnagImg.OutputImageFile.FileType = snagImageFileType.siftJPEG;
         SnagImg.OutputImageFile.Quality = 90; //90% quality
         SnagImg.OutputImageFile.ColorDepth = snagImageColorDepth.sicd24Bit;
         SnagImg.OutputImageFile.FileSubType = snagImageFileSubType.sifstJTIF_444;

         // Show Preview Window?
         SnagImg.EnablePreviewWindow = PreviewChkBx.Checked;

         // Include the cursor in the capture?
         SnagImg.IncludeCursor = CursorChkBx.Checked;

         // Show the magnifier when selecting
         SnagImg.UseMagnifierWindow = MagnifierChkBx.Checked && CaptureType.Text == "Region";

         // Try to initiate the capture.. catch any errors and display an 
         // appropriate error message.  Here, SnagIt expiration error is shown
         // as an example.
         try
         {
            SnagImg.Capture();
         } 
         catch
         {
            txtBoxErrors.Text = SnagImg.LastError.ToString();
         }
      }

      private void CaptureType_SelectedIndexChanged( object sender, EventArgs e )
      {
         CaptureBttn.Enabled = true;
         MagnifierChkBx.Checked = false;
         MagnifierChkBx.Enabled = false;

         // Set the capture input type
         switch ( CaptureType.Text )
         {
            case "Desktop":
            {
               SnagImg.Input = snagImageInput.siiDesktop;
               break;
            }
            case "Window":
            {
               SnagImg.Input = snagImageInput.siiWindow;
               break;
            }
            default: // "Region"
            {
               MagnifierChkBx.Enabled = true;
               SnagImg.Input = snagImageInput.siiRegion;
               break;
            }
         }

      }
   }
}
