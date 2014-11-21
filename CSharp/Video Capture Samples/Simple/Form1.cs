
//---------------------------------------------------------------------------
// This is a sample C# sample application to demonstrates how to
// perform a Snagit COM video capture and save it to a file.
// The recording can be previewed in the Snagit Editor. Clicking on
// The green "Finish" button in the editor will prompt you for
// the name and location to save the capture. Snagit only saves
// video captures to the MP4 format using H.264/AAC encoding. 
// Note: This sample was created using the Snagit 12.2.2 COM interface
//       and was setup to support .NET 4.
//       This sample requires Snagit 11.1.0 or later.
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

namespace SimpleVideoCapture
{
   public partial class Form1 : Form
   {
      // Declare the SnagIt VideoCaptureClass object.  
      // NOTE: First you must add a reference to 
      // the SNAGIT 1.0 Type Library in order for
      // this line to compile. Please be sure to
      // set the Embed Interop Types to False in
      // the SNAGITLib reference properties.
      private SNAGITLib.VideoCaptureClass snagVideo;
      public delegate void UpdateRecorderErrorTextCallback( string errorText );

      public Form1()
      {
         InitializeComponent();

         // Create a new SnagIt Video Capture object.
         // NOTE: First you must add a reference to 
         // the SNAGIT 1.0 Type Library
         snagVideo = new VideoCaptureClass();

         CaptureBttn.Enabled = false;

         //Hook up event handler for recording errors
         snagVideo.OnRecorderError += RecorderErrorTextEventListener;
      }

      ~Form1()
      {
         //Unhook the event handler when we are finished
         snagVideo.OnRecorderError -= RecorderErrorTextEventListener;
      }

      #region Handling Recording Error Events
      private void UpdateRecorderErrorText( string errorText )
      {
         txtBoxErrors.Text = errorText;
      }

      //Event handler for the OnRecorderError recorder event
      public void RecorderErrorTextEventListener( snagRecorderError errorNum )
      {
         var strErrorNum = errorNum == 0 ? "None" : errorNum.ToString();
         var strErrMsg = strErrorNum;

         //There are two categories of errors: 1)From the recorder  2)From the encoder
         if ( errorNum == snagRecorderError.srErrEncoderThrownCode || errorNum == snagRecorderError.srErrRecorderThrownCode )
         {
            var bRecorderCode = errorNum == snagRecorderError.srErrRecorderThrownCode; //Determine if originating from the recorder
            var uCode = bRecorderCode ? snagVideo.RecorderErrorCode : snagVideo.EncoderErrorCode;
            strErrMsg = ( bRecorderCode ? "Recorder" : "Encoder" ) + " error code:" + uCode;
         }

         txtBoxErrors.Invoke( new UpdateRecorderErrorTextCallback( UpdateRecorderErrorText ), new object[]
                                                                          {
                                                                             strErrMsg
                                                                          } );
      }
      #endregion

      private void CaptureType_SelectedIndexChanged( object sender, EventArgs e )
      {
         CaptureBttn.Enabled = true;
         MagnifierChkBx.Enabled = true;

         // Set the capture input type
         switch ( CaptureType.Text )
         {
            case "Region":
            {
               snagVideo.Input = snagVideoInput.sviCapture;
               break;
            }
            case "Window":
            {
               MagnifierChkBx.Enabled = false;
               MagnifierChkBx.Enabled = false;
               snagVideo.Input = snagVideoInput.sviCapture;
               break;
            }
            default: // "All-in-One"
            {
               snagVideo.Input = snagVideoInput.sviCapture;
               break;
            }
         }
      }

      private void CaptureBttn_Click( object sender, EventArgs e )
      {
         snagVideo.Output = snagVideoOutput.svoFile;
         snagVideo.OutputVideoFile.FileNamingMethod = snagOuputFileNamingMethod.sofnmPrompt;

         // Show Preview Window?
         snagVideo.EnablePreviewWindow = PreviewChkBx.Checked;

         // Include the cursor in the capture?
         snagVideo.IncludeCursor = CursorChkBx.Checked;

         // Show the magnifier when selecting
         snagVideo.UseMagnifierWindow = MagnifierChkBx.Checked && CaptureType.Text != "Window";

         // Try to initiate the capture, catch any errors and display an 
         // appropriate error message.  Here, SnagIt expiration error is shown
         // as an example. See the Snagit COM documentation for the rest of the
         // error types in the snagError enum.
         try
         {
            snagVideo.Capture(); //Starts the Snagit selection UI and then displays the recording UI
         } 
         catch
         {
            txtBoxErrors.Text = snagVideo.LastError.ToString();
         }
      }
   }
}
