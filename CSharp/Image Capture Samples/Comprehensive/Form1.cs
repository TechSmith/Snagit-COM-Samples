//---------------------------------------------------------------------------
// This is a simple C# sample application to demonstrate the use of the Snagit
// COM interface for image capture. It covers every option, that is exposed to COM by Snagit,
// for capturing images, applying filters and sharing.
// Summary:
//   * Instantiating the Snagit video capture class
//   * Handling image capture events
//   * Setting all input/output/filter options
//
// Note: This sample was created using the Snagit 12.2.2 COM interface. Most
//       of what is shown is backward compatible clear back to Snagit 8.1.0.
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
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SNAGITLib;

namespace ImageCaptureSample
{
   public partial class MainForm : Form
   {
      // Declare the SnagIt ImageCapture object.  
      // NOTE: First you must add a reference to 
      // the SNAGIT 1.0 Type Library in order for
      // this line to compile. Please be sure to
      // set the Embed Interop Types to False in
      // the SNAGITLib reference properties.
      public readonly ImageCaptureClass SnagImg;
      private string _lastSavedFolder;

      #region Event Handlers Defined
      //For more information about the Snagit image capture event handlers please
      //see the Snagit COM documentation under _ICaptureEvents Methods.

      //Update capture status text box
      public delegate void UpdateStatusTextCallback( string errorText );
      private void UpdateStatusText( string errorText )
      {
         StatusTextBox.Text = errorText;
      }

      //Event handler for errors from Snagit
      public void ErrorTextEventListener( snagError errorNum )
      {
         var strErrMsg = errorNum == snagError.serrNone ? "None" : "Snagit error: " + errorNum;
         StatusTextBox.Invoke( new UpdateStatusTextCallback( UpdateStatusText ), new object[]
                                                                          {
                                                                             strErrMsg
                                                                          } );
      }

      //Event handler for Snagit capture state changes
      public void CaptureStateEventListener( snagCaptureState state )
      {
         //OnStateChange
         if ( state == snagCaptureState.scsIdle )
         {
            //When Snagit is idle we know that we are done capturing.
            //Now is a good time to stop listening to events.
            UnhookSnagitEvents();
         }
         StatusTextBox.Invoke( new UpdateStatusTextCallback( UpdateStatusText ), new object[]
                                                                          {
                                                                             state.ToString()
                                                                          } );
         if ( CaptureResultsChkBx.Checked )
         {
            var thread = new Thread( DisplayCaptureSelectionResults);
            thread.Start();
         }
      }

      public void DisplayCaptureSelectionResults()
      {
         //Display rectangle selected by the user
         var resultsMsg = "Capture Results:" + Environment.NewLine + "   Selection: Top = " +
                             SnagImg.CaptureResults.SelectedArea.StartY + Environment.NewLine + "              Left = " +
                             SnagImg.CaptureResults.SelectedArea.StartX + Environment.NewLine + "            Height = " +
                             SnagImg.CaptureResults.SelectedArea.Height + Environment.NewLine + "             Width = " +
                             SnagImg.CaptureResults.SelectedArea.Width;

         //Note: Handle the _snagImg.OnFileWritten event instead of 
         //      directly querying the  _snagImg.CaptureResults.LastFileSaved property
         //      to obtain the filename of the last file saved. Getting the last saved
         //      filename is only valid when the output type is sioFile.

         MessageBox.Show( resultsMsg );
      }

      //Event handler for when Snagit completes writing a capture to file
      public void FileWriteEventListener( string fileWritten )
      {
         var strStatusMsg = "Capture saved to file " + fileWritten;
         StatusTextBox.Invoke( new UpdateStatusTextCallback( UpdateStatusText ), new object[]
                                                                          {
                                                                             strStatusMsg
                                                                          } );
      }

      #region Hook/Unhook Snagit Event Handlers
      private void HookupSnagitEvents()
      {
         //Hook up event handlers
         SnagImg.OnError += ErrorTextEventListener;
         SnagImg.OnStateChange += CaptureStateEventListener;
         SnagImg.OnFileWritten += FileWriteEventListener;
      }

      private void UnhookSnagitEvents()
      {
         //Remove event handlers when finished
         SnagImg.OnError -= ErrorTextEventListener;
         SnagImg.OnStateChange -= CaptureStateEventListener;
         SnagImg.OnFileWritten += FileWriteEventListener;
      }
      #endregion
      #endregion

      public MainForm()
      {
         InitializeComponent();

         // Create a new SnagIt ImageCapture object.
         // NOTE: First you must add a reference to 
         // the SNAGIT 1.0 Type Library  
         SnagImg = new ImageCaptureClass();

         SetDefaults();
         HideAllInputControls();
         ShowAutoScrollControls();
         MagnifierChkBx.Visible = true;
      }

      private void SetDefaults()
      {
         CaptureType.SelectedIndex = CaptureType.Items.IndexOf( "All-in-One" );
         SelectionType.SelectedIndex = SelectionType.Items.IndexOf( "Interactive" );
         CaptureResultsChkBx.Checked = false;

         //Default scrolling options
         ScrollDirection.SelectedIndex = ScrollDirection.Items.IndexOf( "Vertical" );
         ScrollStartPosition.SelectedIndex = ScrollStartPosition.Items.IndexOf( "Current" );
         ScrollDelay.SelectedIndex = ScrollDelay.Items.IndexOf( "0" );
         ScollMethodChkBx.Checked = true;

         OutputType.SelectedIndex = OutputType.Items.IndexOf( "None" );
         ClipBrdDefaultSzChkBx.Checked = true;

         //Default file options
         NamingMethodSelection.Text = "Prompt";
         SpecifyFileFolder.Checked = true;
         FolderPathTextBx.Text = _lastSavedFolder = Environment.GetFolderPath( Environment.SpecialFolder.Desktop );
         FileFormatsList.SelectedIndex = FileFormatsList.Items.IndexOf("PNG");
         FileNameDigitsTextBx.Text = "3";

         DefaultPageLayoutSettings();

         //Default ftp options
         FTPFilenamingType.SelectedIndex = FTPFilenamingType.Items.IndexOf( "Fixed" );
         FTPProgressChkBx.Checked = true;
         FTPReplaceChkBx.Checked = true;
         FTPAuthChkBx.Checked = false;
         FTPProxyChkBx.Checked = false;
         FTPPassiveChkBx.Checked = false;
         FTPUsernameTxtBx.Enabled = false;
         FTPPasswordTxtBx.Enabled = false;
         FTPProxyTxtBx.Enabled = false;
         
         //Default filter options
         TrimTypeSelector.SelectedIndex = TrimTypeSelector.Items.IndexOf( "None" );
         DefaultBorderOptions();
         SetWatermarkDefaults();
         DefaultScalingOptions();
         DefaultCaptionOptions();
         DefaultColorSubOptions();
         DefaultColorConvertOptions();
         DefaultColorFxControls();
         DefaultResolutionControls();

         UpdateStatusText(snagCaptureState.scsIdle.ToString());
      }


      #region Show/Hide Input Controls

      private void HideAllInputControls()
      {
         ShowSelectionTypeControls( false );
         ShowFixedRegionControls( false );
         ShowWindowPositionControls( false );
         ShowWindowHandleControls( false );
         ShowExtendedWindowControls( false );
         ShowAutoScrollControls( false) ;
         ShowMenuCaptureControls( false );
         ShowTwainControls(false);
         MagnifierChkBx.Visible = false;
      }

      private void ShowFixedRegionControls(bool bVisibleState = true)
      {
         WidthTxtBx.Visible = bVisibleState;
         WidthLabel.Visible = bVisibleState;
         HeightTxtBx.Visible = bVisibleState;
         HeightLabel.Visible = bVisibleState;
         OffsetChkBx.Visible = bVisibleState;
         XOffsetTxtBx.Visible = bVisibleState;
         XOffsetLabel.Visible = bVisibleState;
         YOffsetTxtBx.Visible = bVisibleState;
         YOffsetLabel.Visible = bVisibleState;
         WidthTxtBx.Enabled = true;
         HeightTxtBx.Enabled = true;
      }

      private void ShowWindowPositionControls( bool bVisibleState = true )
      {
         XPosTxtBx.Visible = bVisibleState;
         XPosLabel.Visible = bVisibleState;
         YPosTxtBx.Visible = bVisibleState;
         YPosLabel.Visible = bVisibleState;
      }

      private void ShowWindowHandleControls( bool bVisibleState = true )
      {
         HandleTxtBx.Visible = bVisibleState;
         HandleLabel.Visible = bVisibleState;
         HandleLabel.Visible = bVisibleState;
      }

      private void ShowSelectionTypeControls( bool bVisibleState = true )
      {
         SelectionType.Visible = bVisibleState;       
         ShowWindowHandleControls( false );
         ShowWindowPositionControls( false );

         if ( bVisibleState )
         {
            switch ( SelectionType.Text )
            {
               case "Active Window":
               {
                  MagnifierChkBx.Visible = false;
                  break;
               }
               case "Window Handle":
               {
                  ShowWindowHandleControls( true );
                  MagnifierChkBx.Visible = false;
                  break;
               }
               case "Point on the Desktop":
               {
                  ShowWindowPositionControls( true );
                  MagnifierChkBx.Visible = false;
                  break;
               }
               default:
               {
                  MagnifierChkBx.Visible = true;
                  break;
               }
            }
         }
      }

      private void ShowExtendedWindowControls( bool bVisibleState = true )
      {
         WindowResizePreviewChkBx.Visible = bVisibleState;
         defaultWindowSizeChkBx.Visible = bVisibleState;
         WidthTxtBx.Visible = bVisibleState;
         WidthLabel.Visible = bVisibleState;
         HeightTxtBx.Visible = bVisibleState;
         HeightLabel.Visible = bVisibleState;
         WidthTxtBx.Enabled = defaultWindowSizeChkBx.Checked;
         HeightTxtBx.Enabled = defaultWindowSizeChkBx.Checked;
      }
      private void ShowAutoScrollControls( bool bVisibleState = true )
      {
         AutoScrollChkBx.Visible = bVisibleState;
         ForeGroundChkBx.Visible = bVisibleState;
         ScrollDirection.Visible = bVisibleState;
         ScrollDirectionLabel.Visible = bVisibleState;
         ScrollStartPosition.Visible = bVisibleState;
         ScrollStartLabel.Visible = bVisibleState;
         ScrollDelay.Visible = bVisibleState;
         ScrollDelayLabel.Visible = bVisibleState;
         ScollMethodChkBx.Visible = bVisibleState;

         ForeGroundChkBx.Enabled = AutoScrollChkBx.Checked;
         ScollMethodChkBx.Enabled = AutoScrollChkBx.Checked;
         ScrollDirection.Enabled = AutoScrollChkBx.Checked;
         ScrollDirectionLabel.Enabled = AutoScrollChkBx.Checked;
         ScrollStartPosition.Enabled = AutoScrollChkBx.Checked;
         ScrollStartLabel.Enabled = AutoScrollChkBx.Checked;
         ScrollDelay.Enabled = AutoScrollChkBx.Checked;
         ScrollDelayLabel.Enabled = AutoScrollChkBx.Checked;
      }

      private void ShowMenuCaptureControls( bool bVisibleState = true )
      {
         MenuBarChkBx.Visible = bVisibleState;
         CascadedMenusChkBx.Visible = bVisibleState;
      }

      private void ShowTwainControls( bool bVisibleState = true )
      {
         TwainBttn.Visible = bVisibleState;
         TwainTxtBx.Visible = bVisibleState;
      }
      #endregion

      #region Input Control Handlers
      private void TwainBttn_Click( object sender, EventArgs e )
      {
         //Show device selection dialog
         //Populate read-only text edit with the device name
         if ( SnagImg.InputTWAINOptions.ShowSelectionDialog( (int)TwainBttn.Handle ) )
         {
            TwainTxtBx.Text = SnagImg.InputTWAINOptions.Source;
         }
      }

      private void CaptureType_SelectedIndexChanged( object sender, EventArgs e )
      {
         switch ( CaptureType.Text )
         {
            case "Desktop":
            {
               HideAllInputControls();

               SnagImg.Input = snagImageInput.siiDesktop;
               break;
            }
            case "Window":
            {
               HideAllInputControls();
               ShowSelectionTypeControls();
               ShowAutoScrollControls();

               SnagImg.Input = snagImageInput.siiWindow;
               break;
            }
            case "Region":
            {
               HideAllInputControls();
               ShowAutoScrollControls();
               MagnifierChkBx.Visible = true;

               SnagImg.Input = snagImageInput.siiRegion;
               break;
            }
            case "Fixed Region":
            {
               HideAllInputControls();

               //Show region selection options
               ShowFixedRegionControls();

               SnagImg.Input = snagImageInput.siiRegion;
               break;
            }
            case "Free Hand":
            {
               HideAllInputControls();
               MagnifierChkBx.Visible = true;

               SnagImg.Input = snagImageInput.siiFreehand;
               break;
            }
            case "Clipboard":
            {
               HideAllInputControls();

               SnagImg.Input = snagImageInput.siiClipboard;
               break;
            }
            case "Menu":
            {
               HideAllInputControls();
               ShowMenuCaptureControls();

               SnagImg.Input = snagImageInput.siiMenu;
               break;
            }
            case "Object":
            {
               HideAllInputControls();

               SnagImg.Input = snagImageInput.siiObject;
               break;
            }
            case "Extended Window":
            {
               HideAllInputControls();
               ShowExtendedWindowControls();

               SnagImg.Input = snagImageInput.siiExtendedWindow;
               break;
            }
            case "Custom Scroll":
            {
               HideAllInputControls();
               ShowAutoScrollControls();

               SnagImg.Input = snagImageInput.siiCustomScroll;
               break;
            }
            case "TWAIN":
            {
               HideAllInputControls();
               ShowTwainControls();

               SnagImg.Input = snagImageInput.siiTWAIN;
               break;
            }
            case "Graphic File":
            {
               HideAllInputControls();

               SnagImg.Input = snagImageInput.siiGraphicFile;
               break;
            }
            default: //All-in-One
            {
               HideAllInputControls();
               ShowAutoScrollControls();
               MagnifierChkBx.Visible = true;

               SnagImg.Input = snagImageInput.siiCapture;
               break;
            }
         }
      }
      
      private void SelectionType_SelectedIndexChanged( object sender, EventArgs e )
      {
         ShowSelectionTypeControls( true );
      }

      #region Checkbox Event Handlers
      private void OffsetChkBx_CheckedChanged( object sender, EventArgs e )
      {
         XOffsetTxtBx.Enabled = OffsetChkBx.Checked;
         YOffsetTxtBx.Enabled = OffsetChkBx.Checked;
      }

      private void defaultWindowSizeChkBx_CheckedChanged( object sender, EventArgs e )
      {
         WidthTxtBx.Enabled = defaultWindowSizeChkBx.Checked;
         HeightTxtBx.Enabled = defaultWindowSizeChkBx.Checked;
      }

      private void AutoScrollChkBx_CheckedChanged( object sender, EventArgs e )
      {
         ForeGroundChkBx.Enabled = AutoScrollChkBx.Checked;
         ScrollDirection.Enabled = AutoScrollChkBx.Checked;
         ScrollDirectionLabel.Enabled = AutoScrollChkBx.Checked;
         ScrollStartPosition.Enabled = AutoScrollChkBx.Checked;
         ScrollStartLabel.Enabled = AutoScrollChkBx.Checked;
         ScrollDelay.Enabled = AutoScrollChkBx.Checked;
         ScrollDelayLabel.Enabled = AutoScrollChkBx.Checked;
         ScollMethodChkBx.Enabled = AutoScrollChkBx.Checked;
      }

      private void DelayChkBx_CheckedChanged( object sender, EventArgs e )
      {
         CountDownChkBx.Enabled = DelayChkBx.Checked;
         DelaySeconds.Enabled = DelayChkBx.Checked;
         DelayTimeLabel.Enabled = DelayChkBx.Checked;
      }
      #endregion

      #endregion

      #region Output Control Handlers
      private void OutputType_SelectedIndexChanged( object sender, EventArgs e )
      {
         PreviewChkBx.Enabled = true;
         switch ( OutputType.Text )
         {
            case "File":
            {
               OutputTabControl.SelectTab( tabFile );
               SnagImg.Output = snagImageOutput.sioFile;
               break;
            }
            case "Clipboard":
            {
               OutputTabControl.SelectTab( tabClipboard );
               SnagImg.Output = snagImageOutput.sioClipboard;
               break;
            }
            case "FTP":
            {
               OutputTabControl.SelectTab( tabFTP );
               SnagImg.Output = snagImageOutput.sioFTP;
               break;
            }
            case "E-mail":
            {
               OutputTabControl.SelectTab( tabEmail );
               SnagImg.Output = snagImageOutput.sioMail;
               break;
            }
            case "Printer":
            {
               OutputTabControl.SelectTab( tabPrinter );
               SnagImg.Output = snagImageOutput.sioPrinter;
               break;
            }
            default: //None
            {
               PreviewChkBx.Checked = true;
               PreviewChkBx.Enabled = false;
               OutputTabControl.SelectTab( tabNone );

               SnagImg.Output = snagImageOutput.sioNone;
               break;
            }
         }
      }

      #region Clipboard tab controls
      private void ClipBrdDefaultSzChkBx_CheckedChanged( object sender, EventArgs e )
      {
         if ( ClipBrdDefaultSzChkBx.Checked )
         {
            //This is the default size set by Snagit.
            //Just pointing this out by exposing it as an option.
            ClipBrdTxtBx.Text = "640";
            ClipbrdDesktopSzChkBx.Checked = false;
         }
         ClipBrdTxtBx.Enabled = !ClipbrdDesktopSzChkBx.Checked && !ClipBrdDefaultSzChkBx.Checked;
      }

      private void ClipbrdDesktopSzChkBx_CheckedChanged( object sender, EventArgs e )
      {
         if ( ClipbrdDesktopSzChkBx.Checked )
         {
            //Set the clipboard width to zero to use the windows desktop size.
            ClipBrdTxtBx.Text = "0";
            ClipBrdDefaultSzChkBx.Checked = false;
         }

         ClipBrdTxtBx.Enabled = !ClipbrdDesktopSzChkBx.Checked && !ClipBrdDefaultSzChkBx.Checked;
      }
      #endregion

      #region File tab controls
      private void NamingMethodSelection_SelectedIndexChanged( object sender, EventArgs e )
      {
         FileNameTxtBx.Enabled = false;
         FileNameDigitsTextBx.Enabled = false;
         FilePrefixTextBx.Enabled = false;
         FileLastUsedFolder.Enabled = false;

         switch ( NamingMethodSelection.Text )
         {
            case "Auto":
            {
               FileNameDigitsTextBx.Enabled = true;
               FilePrefixTextBx.Enabled = true;
               break;
            }
            case "Fixed":
            {
               FileNameTxtBx.Enabled = true;
               break;
            }
            default: //Prompt
            {
               FileLastUsedFolder.Enabled = true;
               break;
            }
         }
      }

      private void FileLastUsedFolder_CheckedChanged( object sender, EventArgs e )
      {
         if ( FileLastUsedFolder.Checked )
         {
            SpecifyFileFolder.Checked = false;
         }

         FolderBrowseBttn.Enabled = !FileLastUsedFolder.Checked;
         FolderPathTextBx.Enabled = !FileLastUsedFolder.Checked;
      }

      private void SpecifyFileFolder_CheckedChanged( object sender, EventArgs e )
      {
         if ( SpecifyFileFolder.Checked )
         {
            FileLastUsedFolder.Checked = false;
         }

         FolderBrowseBttn.Enabled = SpecifyFileFolder.Checked;
         FolderPathTextBx.Enabled = SpecifyFileFolder.Checked;

      }

      private void FolderBrowseBttn_Click( object sender, EventArgs e )
      {
         var newFolder = Helpers.BrowseOutputFolder();
         if ( newFolder.Length > 0 )
         {
            _lastSavedFolder = FolderPathTextBx.Text;
            FolderPathTextBx.Text = newFolder;
         }
      }

      private void FileFormatsList_SelectedIndexChanged( object sender, EventArgs e )
      {
         switch ( FileFormatsList.Text )
         {
            case "BMP":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftBMP;
               break;
            }
            case "CUR":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftCUR;
               break;
            }
            case "EMF":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftEMF;
               break;
            }
            case "EPS":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftEPS;
               break;
            }
            case "GIF":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftGIF;
               break;
            }
            case "ICO":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftICO;
               break;
            }
            case "JPG":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftJPEG;
               break;
            }
            case "MHT":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftMHT;
               break;
            }
            case "OS2":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftOS2;
               break;
            }
            case "PDF":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftPDF;
               break;
            }
            case "PSD":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftPSD;
               break;
            }
            case "RAS":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftRAS;
               break;
            }
            case "SNAG":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftSNAG;
               break;
            }
            case "SWF":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftSWF;
               break;
            }
            case "TGA":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftTGA;
               break;
            }
            case "TIF":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftTIFF;
               break;
            }
            case "WFX":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftWFX;
               break;
            }
            case "WMF":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftWMF;
               break;
            }
            case "WPG":
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftWPG;
               break;
            }
            default: //PNG
            {
               SnagImg.OutputImageFile.FileType = snagImageFileType.siftPNG;
               break;
            }
         }
      }

      private void FormatOptionsBttn_Click( object sender, EventArgs e )
      {
         #region Note about additional format options
         // Instead of calling this dialog to set the options
         // you can set them directly in code as follows:
         //  _snagImg.OutputImageFile.FileSubType = snagImageFileSubType.sifstJFIF_422_Progressive;
         //  _snagImg.OutputImageFile.ColorDepth = snagImageColorDepth.sicd8Bit;
         //  _snagImg.OutputImageFile.ProgressiveOption = 6;     //JPG only. Can be set to 0 - 12
         //  _snagImg.OutputImageFile.Quality = 25;              //JPG only. Can be set 1 - 100
         //
         //See the Snagit COM documentation for more details about these options settings.
         #endregion


         try
         {
            if ( !SnagImg.OutputImageFile.ShowFileSettingDialog( (int) FormatOptionsBttn.Handle, SnagImg.OutputImageFile.FileType ) )
            {
               var msg = "Using default format settings for " + FileFormatsList.Text;
               MessageBox.Show( msg );
            }
         } 
         catch ( ArgumentException )
         {
            var msg = "Additional format options are only" + Environment.NewLine + "available for these file types:" +
                         Environment.NewLine + "   siftBMP" +
                         Environment.NewLine + "   siftGIF" +
                         Environment.NewLine + "   siftJPEG" +
                         Environment.NewLine + "   siftPNG" +
                         Environment.NewLine + "   siftTGA" +
                         Environment.NewLine + "   siftTIFF";
            MessageBox.Show( msg );
         }
      }

      private void ResetFormatBttn_Click( object sender, EventArgs e )
      {
         MessageBox.Show("Please use the [Default] button in the format options dialog to reset values");

         // If you were not using Snagit's format options dialog, you can
         // get the default values by calling LoadImageDefaults() as shown below:
         // _snagImg.OutputImageFile.LoadImageDefaults( _snagImg.OutputImageFile.FileType );
         // snagImageFileSubType sb = _snagImg.OutputImageFile.FileSubType;
         // snagImageColorDepth cd = _snagImg.OutputImageFile.ColorDepth;
         // int g = _snagImg.OutputImageFile.ProgressiveOption;
         // int f = _snagImg.OutputImageFile.Quality;

         // Example how to set the transparency flag and color for the GIF format
         // ( (IImageFile2) ( _snagImg.AutoScrollOptions ) ).UseGIFTransparency = true;
         // ( (IImageFile2) ( _snagImg.AutoScrollOptions ) ).TransparentColorForGIF = 0;  //RGB color value
      }

      #endregion

      #region Email tab controls
      private void EmailPromptChkBx_CheckedChanged( object sender, EventArgs e )
      {
         EmailMsgTxtBx.Enabled = !EmailPromptChkBx.Checked;
         EmailSubjectTxtBx.Enabled = !EmailPromptChkBx.Checked;
         EmailAddrTxtBx.Enabled = !EmailPromptChkBx.Checked;
         EmailNameTxtBx.Enabled = !EmailPromptChkBx.Checked;
      }
      #endregion

      #region Printer tab controls
      private void DefaultPrinterChkBx_CheckedChanged( object sender, EventArgs e )
      {
         PrinterSelectBttn.Enabled = !DefaultPrinterChkBx.Checked;
      }

      private void PrinterSelectBttn_Click( object sender, EventArgs e )
      {
         if ( SnagImg.OutputPrinterOptions.ShowSelectionDialog((int) PrinterSelectBttn.Handle) )
         {
            string printerName;
            string driverName;
            string port;
            SnagImg.OutputPrinterOptions.GetData( out printerName, out driverName, out port );
            PrntrNameTxtBx.Text = printerName;
            PrntrDriverTxtBx.Text = driverName;
            PrntrPortTxtBx.Text = port;
         }
      }

      private void PrntrImgPlacement_SelectedIndexChanged( object sender, EventArgs e )
      {
         switch ( PrntrImgPlacement.Text )
         {
            case "Top Center":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spCenterTop;
               break;
            }
            case "Top Right":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spRightTop;
               break;
            }
            case "Middle Left":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spLeftMiddle;
               break;
            }
            case "Middle Center":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spCenterMiddle;
               break;
            }
            case "Middle Right":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spRightMiddle;
               break;
            }
            case "Bottom Left":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spLeftBottom;
               break;
            }
            case "Bottom Center":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spCenterBottom;
               break;
            }
            case "Bottom Right":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spRightBottom;
               break;
            }
            case "Outside Left":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spOutsideLeft;
               break;
            }
            case "Outside Right":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spOutsideRight;
               break;
            }
            case "Outside Top":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spOutsideTop;
               break;
            }
            case "Outside Bottom":
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spOutsideBottom;
               break;
            }
            default: //Top Left
            {
               SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition = snagPlacement.spLeftTop;
               break;
            }
         }
      }

      private void prntrImgScaling_SelectedIndexChanged( object sender, EventArgs e )
      {
         PrntrPercentTxtBx.Enabled = false;
         PrntrHeightTxtBx.Enabled = false;
         PrntrWidthTxtBx.Enabled = false;

         switch ( prntrImgScaling.Text )
         {
            case "Percent":
            {
               PrntrPercentTxtBx.Enabled = true;
               SnagImg.OutputPrinterPageLayoutOptions.ScalingType = snagPrintScale.spsPercentScale;
               break;
            }
            case "Fixed Size":
            {
               PrntrHeightTxtBx.Enabled = true;
               PrntrWidthTxtBx.Enabled = true;
               SnagImg.OutputPrinterPageLayoutOptions.ScalingType = snagPrintScale.spsFixedSize;
               break;
            }
            case "Shrink to Page":
            {
               SnagImg.OutputPrinterPageLayoutOptions.ScalingType = snagPrintScale.spsSinglePage;
               break;
            }
            case "Stretch to Page":
            {
               SnagImg.OutputPrinterPageLayoutOptions.ScalingType = snagPrintScale.spsSinglePageMaximize;
               break;
            }
            case "Stretch to Width":
            {
               SnagImg.OutputPrinterPageLayoutOptions.ScalingType = snagPrintScale.spsProportionalToScreen;
               break;
            }
            default: //Auto scaling
            {
               SnagImg.OutputPrinterPageLayoutOptions.ScalingType = snagPrintScale.spsAutoScale;
               break;
            }
         }
      }

      private void DefaultPageLayoutSettings()
      {
         DefaultPrinterChkBx.Checked = SnagImg.OutputPrinterOptions.UseDefaultPrinter;

         string printerName;
         string driverName;
         string port;
         SnagImg.OutputPrinterOptions.GetData( out printerName, out driverName, out port);
         PrntrNameTxtBx.Text = printerName;
         PrntrDriverTxtBx.Text = driverName;
         PrntrPortTxtBx.Text = port;

         PrntrPercentTxtBx.Text = SnagImg.OutputPrinterPageLayoutOptions.Scale.ToString();
         PrntrHeightTxtBx.Text = SnagImg.OutputPrinterPageLayoutOptions.Height.ToString();
         PrntrWidthTxtBx.Text = SnagImg.OutputPrinterPageLayoutOptions.Width.ToString();

         MarginLeftTxtBx.Text = SnagImg.OutputPrinterPageLayoutOptions.MarginLeft.ToString();
         MarginRightTxtBx.Text = SnagImg.OutputPrinterPageLayoutOptions.MarginRight.ToString();
         MarginTopTxtBx.Text = SnagImg.OutputPrinterPageLayoutOptions.MarginTop.ToString();
         MarginBottomTxtBx.Text = SnagImg.OutputPrinterPageLayoutOptions.MarginBottom.ToString();

         #region Scaling

         switch ( SnagImg.OutputPrinterPageLayoutOptions.ScalingType )
         {
            case snagPrintScale.spsPercentScale:
            {
               prntrImgScaling.SelectedIndex = prntrImgScaling.Items.IndexOf( "Percent" );
               break;
            }
            case snagPrintScale.spsFixedSize:
            {
               prntrImgScaling.SelectedIndex = prntrImgScaling.Items.IndexOf( "Fixed Size" );
               break;
            }
            case snagPrintScale.spsSinglePage:
            {
               prntrImgScaling.SelectedIndex = prntrImgScaling.Items.IndexOf( "Shrink to Page" );
               break;
            }
            case snagPrintScale.spsSinglePageMaximize:
            {
               prntrImgScaling.SelectedIndex = prntrImgScaling.Items.IndexOf( "Stretch to Page" );
               break;
            }
            case snagPrintScale.spsProportionalToScreen:
            {
               prntrImgScaling.SelectedIndex = prntrImgScaling.Items.IndexOf( "Stretch to Width" );
               break;
            }
            default:
            {
               prntrImgScaling.SelectedIndex = prntrImgScaling.Items.IndexOf( "Auto" );
               break;
            }
         }
         #endregion

         #region Layout position
         switch ( SnagImg.OutputPrinterPageLayoutOptions.LayoutPosition )
         {
            case snagPlacement.spCenterTop:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Top Center" );
               break;
            }
            case snagPlacement.spRightTop:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Top Right" );
               break;
            }
            case snagPlacement.spLeftMiddle:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Middle Left" );
               break;
            }
            case snagPlacement.spCenterMiddle:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Middle Center" );
               break;
            }
            case snagPlacement.spRightMiddle:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Middle Right" );
               break;
            }
            case snagPlacement.spLeftBottom:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Bottom Left" );
               break;
            }
            case snagPlacement.spCenterBottom:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Bottom Center" );
               break;
            }
            case snagPlacement.spRightBottom:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Bottom Right" );
               break;
            }
            case snagPlacement.spOutsideLeft:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Outside Left" );
               break;
            }
            case snagPlacement.spOutsideRight:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Outside Right" );
               break;
            }
            case snagPlacement.spOutsideTop:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Outside Top" );
               break;
            }
            case snagPlacement.spOutsideBottom:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Outside Bottom" );
               break;
            }
            default:
            {
               PrntrImgPlacement.SelectedIndex = PrntrImgPlacement.Items.IndexOf( "Top Left" );
               break;
            }
         }
         #endregion
      }
      #endregion

      #region FTP output options
      private void FTPFilenamingType_SelectedIndexChanged( object sender, EventArgs e )
      {
         FTPSequenceTxBx.Enabled = FTPFilenamingType.Text == "Auto";
         SnagImg.OutputFTPOptions.UseAutomaticFileNaming = FTPFilenamingType.Text == "Auto";
      }

      private void FTPReplaceChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.OutputFTPOptions.UseTempFileReplace = FTPReplaceChkBx.Checked;
      }

      private void FTPPassiveChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.OutputFTPOptions.UsePassiveFTP = FTPPassiveChkBx.Checked;
      }

      private void FTPProgressChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.OutputFTPOptions.EnableProgressDialog = FTPProgressChkBx.Checked;
      }

      private void FTPAuthChkBx_CheckedChanged( object sender, EventArgs e )
      {
         FTPUsernameTxtBx.Enabled = FTPAuthChkBx.Checked;
         FTPPasswordTxtBx.Enabled = FTPAuthChkBx.Checked;
         SnagImg.OutputFTPOptions.ServerRequiresAuthentication = FTPAuthChkBx.Checked;
      }

      private void FTPProxyChkBx_CheckedChanged( object sender, EventArgs e )
      {
         FTPProxyTxtBx.Enabled = FTPProxyChkBx.Checked;
         SnagImg.OutputFTPOptions.UseProxyServer = FTPProxyChkBx.Checked;
      }
      #endregion

      #endregion

      #region Filter Control Hanlders
      #region Trim controls
      private void TrimTypeSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         TrimLeftTxtBx.Enabled = false;
         TrimRightTxtBx.Enabled = false;
         TrimTopTxtBx.Enabled = false;
         TrimBottomTxtBx.Enabled = false;

         switch ( TrimTypeSelector.Text )
         {
            case "Auto":
            {
               SnagImg.Filters.Trim.TrimMethod = snagTrimMethod.stmAuto;
               break;
            }
            case "Manual":
            {
               TrimLeftTxtBx.Enabled = true;
               TrimRightTxtBx.Enabled = true;
               TrimTopTxtBx.Enabled = true;
               TrimBottomTxtBx.Enabled = true;
               SnagImg.Filters.Trim.TrimMethod = snagTrimMethod.stmManual;
               break;
            }
            default: //None
            {
               SnagImg.Filters.Trim.TrimMethod = snagTrimMethod.stmNone;
               break;
            }
         }
      }
      #endregion

      #region Border controls
      private void DefaultBorderOptions()
      {
         EnableBorderChkBx.Checked = false;
         BorderWidthTxtBx.Text = "8";
         BorderShadowTxtBx.Text = "2";
         BorderOutsideChkBx.Checked = true;
         Border3dChkBx.Checked = true;
         EnableBorderControls(false);
      }

      private void EnableBorderControls( bool enable )
      {
         Border3dChkBx.Enabled = enable;
         BorderOutsideChkBx.Enabled = enable;
         BorderMainClrBttn.Enabled = enable;
         BorderHighLtClrBttn.Enabled = enable;
         BorderShadowClrBttn.Enabled = enable;
         BorderWidthTxtBx.Enabled = enable;
         BorderShadowTxtBx.Enabled = enable;
      }

      private void EnableBorderChkBx_CheckedChanged( object sender, EventArgs e )
      {
         EnableBorderControls( EnableBorderChkBx.Checked );
         SnagImg.Filters.Border.EnableBorder = EnableBorderChkBx.Checked;
      }

      private void Border3dChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Border.Use3DEffect = Border3dChkBx.Checked;
      }

      private void BorderOutsideChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Border.FrameOutsideImage = BorderOutsideChkBx.Checked;
      }

      private void BorderMainClrBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor(out clr) )
         {
            // Set button background to the selected color.
            BorderMainClrBttn.BackColor = clr;
            SnagImg.Filters.Border.MainColor = Helpers.ConvertToRGB( clr );
         }
      }

      private void BorderHighLtClrBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            // Set button background to the selected color.
            BorderHighLtClrBttn.BackColor = clr;
            SnagImg.Filters.Border.HighlightColor = Helpers.ConvertToRGB( clr );
         }
      }

      private void BorderShadowClrBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            // Set button background to the selected color.
            BorderShadowClrBttn.BackColor = clr;
            SnagImg.Filters.Border.ShadowColor = Helpers.ConvertToRGB( clr );
         }
      }
      #endregion

      #region Watermark controls
      private void SetWatermarkDefaults()
      {
         EnableWatermarkChkBx.Checked = false;
         WatermarkEmbossChkBx.Checked = true;
         WatermarkAspectChkBx.Checked = true;
         WatermarkSmoothChkBx.Checked = true;
         WatermarkFxSelector.SelectedIndex = WatermarkFxSelector.Items.IndexOf( "Underlay" );
         WatermarkDirectionSelector.SelectedIndex = WatermarkDirectionSelector.Items.IndexOf( "North" );
         WatermarkDepthTxtBx.Text = "500";
         WatermarkHOffChkBx.Text = "10";
         WatermarkVOffChkBx.Text = "10";
         WatermarkSizeTxtBx.Text = "15";

         EnableWatermarkControls( false );
      }

      private void EnableWatermarkControls( bool enable )
      {
         WatermarkFxSelector.Enabled = enable;
         WatermarkDirectionSelector.Enabled = enable;
         WatermarkFileTxtBx.Enabled = enable;
         WatermarkBrowseBttn.Enabled = enable;
         WatermarkDepthTxtBx.Enabled = enable;
         WatermarkHOffChkBx.Enabled = enable;
         WatermarkVOffChkBx.Enabled = enable;
         WatermarkSizeTxtBx.Enabled = enable;
         WatermarkSmoothChkBx.Enabled = enable;
         WatermarkEmbossChkBx.Enabled = enable;
         WatermarkAspectChkBx.Enabled = enable;
      }

      private void EnableWatermarkChkBx_CheckedChanged( object sender, EventArgs e )
      {
         EnableWatermarkControls( EnableWatermarkChkBx.Checked );
         SnagImg.Filters.Watermark.IncludeWatermark = EnableWatermarkChkBx.Checked;
      }

      private void WatermarkFxSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Watermark.UseOverlay = WatermarkFxSelector.Text == "Overlay";
      }

      private void WatermarkBrowseBttn_Click( object sender, EventArgs e )
      {
         WatermarkFileTxtBx.Text = Helpers.BrowseForFile();
      }
      private void WatermarkAspectChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Watermark.KeepAspectRatio = WatermarkAspectChkBx.Checked;
      }

      private void WatermarkSmoothChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Watermark.UseSmoothScaling = WatermarkSmoothChkBx.Checked;
      }

      private void WatermarkEmbossChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Watermark.UseEmboss = WatermarkEmbossChkBx.Checked;
      }

      private void WatermarkDirectionSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         switch ( WatermarkDirectionSelector.Text )
         {
            case "North-East":
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdNorthEast;
               break;
            }
            case "East":
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdEast;
               break;
            }
            case "South-East":
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdSouthEast;
               break;
            }
            case "South":
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdSouth;
               break;
            }
            case "South-West":
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdSouthWest;
               break;
            }
            case "West":
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdWest;
               break;
            }
            case "North-West":
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdNorthWest;
               break;
            }
            default: // "North"
            {
               SnagImg.Filters.Watermark.EmbossDirection = snagCompassDirection.scdNorth;
               break;
            }
         }
      }
      #endregion

      #region Scaling options
      private void DefaultScalingOptions()
      {
         ScalingMethodSelector.SelectedIndex = ScalingMethodSelector.Items.IndexOf( "None" );
         ScalingSmoothChkBx.Checked = true;
         ScalingAspectRatioChkBx.Checked = true;
         ScalingUnitWidthLabel.Text = "%";
         ScalingUnitHeightLabel.Text = "%";
         ScalingWidthTxtBx.Text = "100";
         ScalingHeightTxtBx.Text = "100";
         EnableScalingControls( false );
      }

      private void EnableScalingControls( bool enable )
      {
         ScalingSmoothChkBx.Enabled = enable;
         ScalingAspectRatioChkBx.Enabled = enable;
         ScalingWidthTxtBx.Enabled = enable;
         ScalingHeightTxtBx.Enabled = enable;
      }

      private void ScalingMethodSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         EnableScalingControls( true );
         ScalingUnitWidthLabel.Text = "pixels";
         ScalingUnitHeightLabel.Text = "pixels";

         switch ( ScalingMethodSelector.Text )
         {
            case "Percentage":
            {
               ScalingUnitWidthLabel.Text = "%";
               ScalingUnitHeightLabel.Text = "%";
               ScalingHeightTxtBx.Enabled = !ScalingAspectRatioChkBx.Checked;
               break;
            }
            case "Width":
            {
               ScalingHeightTxtBx.Enabled = false;
               break;
            }
            case "Height":
            {
               ScalingWidthTxtBx.Enabled = false;
               break;
            }
            case "Fixed":
            {
               ScalingAspectRatioChkBx.Enabled = false;
               break;
            }
            default: //None
            {
               EnableScalingControls( false );
               break;
            }
         }
      }

      private void ScalingSmoothChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Scale.UseSmoothScaling = ScalingSmoothChkBx.Checked;
      }

      private void ScalingAspectRatioChkBx_CheckedChanged( object sender, EventArgs e )
      {
         if ( ScalingMethodSelector.Text == "Percentage" )
         {
            ScalingHeightTxtBx.Enabled = !ScalingAspectRatioChkBx.Checked;
         }

         SnagImg.Filters.Scale.KeepAspectRatio = ScalingAspectRatioChkBx.Checked;
      }
      #endregion

      #region Color Convert options
      private void DefaultColorConvertOptions()
      {
         ColorConvertSelector.SelectedIndex = ColorConvertSelector.Items.IndexOf( "None" );
         MonoTrkBar.Value = SnagImg.Filters.ColorConversion.MonochromeThreshold;
         MonoIntensityTxtBx.Text = MonoTrkBar.Value.ToString();
         MonoTrkBar.Enabled = false;
      }

      private void ColorConvertSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         MonoTrkBar.Enabled = false;
         switch ( ColorConvertSelector.Text )
         {
            case "Monochrome":
            {
               MonoTrkBar.Enabled = true;
               SnagImg.Filters.ColorConversion.ConversionMethod = snagColorConversionMethod.sccmMonochrome;
               break;
            }
            case "Halftone":
            {
               SnagImg.Filters.ColorConversion.ConversionMethod = snagColorConversionMethod.sccmHalftone;
               break;
            }
            case "Grayscale":
            {
               SnagImg.Filters.ColorConversion.ConversionMethod = snagColorConversionMethod.sccmGrayscale;
               break;
            }
            default: //None
            {
               SnagImg.Filters.ColorConversion.ConversionMethod = snagColorConversionMethod.sccmNone;
               break;
            }
         }
      }

      private void MonoTrkBar_Scroll( object sender, EventArgs e )
      {
         MonoIntensityTxtBx.Text = MonoTrkBar.Value.ToString();
      }
      #endregion

      #region Caption options
      private void DefaultCaptionOptions()
      {
         CaptionPromptChkBx.Checked = true;
         EnableCaptionControls(false);
      }

      private void EnableCaptionControls( bool enable )
      {
         CaptionPrintChkBx.Enabled = enable;
         CaptionPromptChkBx.Enabled = enable;
         CaptionFontBttn.Enabled = enable;
         CaptionTxtBx.Enabled = enable && !CaptionPromptChkBx.Checked;
         CaptionTxtSettingsBttn.Enabled = enable && ( CaptionPromptChkBx.Checked || CaptionTxtBx.TextLength > 0 );
         CaptionComputerChkBx.Enabled = enable;
         CaptionUsernameChkBx.Enabled = enable;
         CaptionTimeChkBx.Enabled = enable;
         CaptionDateFormatBttn.Enabled = enable && CaptionTimeChkBx.Checked;
         SystemCaptionSettingsBttn.Enabled = enable && ( CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked );
         SystemFontBttn.Enabled = enable && ( CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked );
      }

      private void EnableCaptionChkBx_CheckedChanged( object sender, EventArgs e )
      {
         EnableCaptionControls( EnableCaptionChkBx.Checked );
         SnagImg.Filters.Annotation.EnableCaption = EnableCaptionChkBx.Checked;
      }

      private void CaptionTxtBx_TextChanged( object sender, EventArgs e )
      {
         CaptionTxtSettingsBttn.Enabled = CaptionPromptChkBx.Checked || CaptionTxtBx.TextLength > 0;
      }

      private void CaptionPrintChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SnagImg.Filters.Annotation.PrintCaptionsAtPageBottom = CaptionPrintChkBx.Checked;
      }


      private void CaptionComputerChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SystemCaptionSettingsBttn.Enabled = CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked;
         SystemFontBttn.Enabled =  CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked;
         SnagImg.Filters.Annotation.IncludeComputerName = CaptionComputerChkBx.Checked;

      }

      private void CaptionUsernameChkBx_CheckedChanged( object sender, EventArgs e )
      {
         SystemCaptionSettingsBttn.Enabled = CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked;
         SystemFontBttn.Enabled = CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked;
         SnagImg.Filters.Annotation.IncludeUserName = CaptionUsernameChkBx.Checked;

      }

      private void CaptionTimeChkBx_CheckedChanged( object sender, EventArgs e )
      {
         CaptionDateFormatBttn.Enabled = CaptionTimeChkBx.Checked;
         SystemCaptionSettingsBttn.Enabled = CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked;
         SystemFontBttn.Enabled = CaptionTimeChkBx.Checked || CaptionUsernameChkBx.Checked || CaptionComputerChkBx.Checked;
         SnagImg.Filters.Annotation.IncludeTimeDate = CaptionTimeChkBx.Checked;
      }

      private void CaptionPromptChkBx_CheckedChanged( object sender, EventArgs e )
      {
         CaptionTxtBx.Enabled = !CaptionPromptChkBx.Checked;
         CaptionTxtSettingsBttn.Enabled = CaptionPromptChkBx.Checked || CaptionTxtBx.TextLength > 0;
         SnagImg.Filters.Annotation.PromptForCaption = CaptionPromptChkBx.Checked;
      }

      private void CaptionFontBttn_Click( object sender, EventArgs e )
      {
         var fontDlg = new FontDialog();

         // You can also get the current font settings from Snagit for caption text
         // ?? = _snagImg.Filters.Annotation.CaptionOptions.Font
         if ( fontDlg.ShowDialog() == DialogResult.OK )
         {
            var fontChanges = fontDlg.Font;
            SnagImg.Filters.Annotation.CaptionOptions.Font.CharacterSet = fontChanges.GdiCharSet;
            SnagImg.Filters.Annotation.CaptionOptions.Font.FaceName = fontChanges.Name;
            SnagImg.Filters.Annotation.CaptionOptions.Font.Underline = fontChanges.Underline;
            SnagImg.Filters.Annotation.CaptionOptions.Font.Italic = fontChanges.Italic;
            SnagImg.Filters.Annotation.CaptionOptions.Font.Height = fontChanges.Height;

            // You can also set the font weight and family
            //_snagImg.Filters.Annotation.CaptionOptions.Font.Weight = ??;
            //_snagImg.Filters.Annotation.CaptionOptions.Font.PitchFamily = ??;
         }
      }

      private void SystemFontBttn_Click( object sender, EventArgs e )
      {
         var fontDlg = new FontDialog();

         // You can also get the current font settings from Snagit for system caption text
         // ?? = _snagImg.Filters.Annotation.SystemCaptionOptions.Font
         if ( fontDlg.ShowDialog() == DialogResult.OK )
         {
            var fontChanges = fontDlg.Font;
            SnagImg.Filters.Annotation.SystemCaptionOptions.Font.CharacterSet = fontChanges.GdiCharSet;
            SnagImg.Filters.Annotation.SystemCaptionOptions.Font.FaceName = fontChanges.Name;
            SnagImg.Filters.Annotation.SystemCaptionOptions.Font.Underline = fontChanges.Underline;
            SnagImg.Filters.Annotation.SystemCaptionOptions.Font.Italic = fontChanges.Italic;
            SnagImg.Filters.Annotation.SystemCaptionOptions.Font.Height = fontChanges.Height;

            // You can also set the font weight and family
            //_snagImg.Filters.Annotation.CaptionOptions.Font.Weight = ??;
            //_snagImg.Filters.Annotation.CaptionOptions.Font.PitchFamily = ??;
         }
      }

      private void CaptionDateFormatBttn_Click( object sender, EventArgs e )
      {
         var dtForm = new DateTimeFormatOptions
         {
            DateTimeOrder = SnagImg.Filters.Annotation.TimeDateDisplayOrder,
            CustomDateFormat = SnagImg.Filters.Annotation.CustomDateFormat,
            CustomTimeFormat = SnagImg.Filters.Annotation.CustomTimeFormat
         };

         if ( dtForm.ShowDialog( this ) == DialogResult.OK )
         {
            SnagImg.Filters.Annotation.TimeDateDisplayOrder = dtForm.DateTimeOrder;
            SnagImg.Filters.Annotation.UseWindowsDateFormat = dtForm.UseWindowsDateFormat;
            SnagImg.Filters.Annotation.UseWindowsTimeFormat = dtForm.UseWindowsTimeFormat;
            SnagImg.Filters.Annotation.CustomDateFormat = dtForm.CustomDateFormat;
            SnagImg.Filters.Annotation.CustomTimeFormat = dtForm.CustomTimeFormat;
         }
      }

      private void CaptionTxtSettingsBttn_Click( object sender, EventArgs e )
      {
         var captionOptions = SnagImg.Filters.Annotation.CaptionOptions;
         var optForm = new CaptionOptions( captionOptions );

         if ( optForm.ShowDialog( this ) == DialogResult.OK )
         {
            var newOptions = optForm.GetCaptionOptions();
            SnagImg.Filters.Annotation.CaptionOptions.BackgroundColor = newOptions.BackgroundColor;
            SnagImg.Filters.Annotation.CaptionOptions.UseWordWrap = newOptions.UseWordWrap;
            SnagImg.Filters.Annotation.CaptionOptions.UseTransparentBackground = newOptions.UseTransparentBackground;
            SnagImg.Filters.Annotation.CaptionOptions.CaptionStyle = newOptions.CaptionStyle;
            SnagImg.Filters.Annotation.CaptionOptions.Placement = newOptions.Placement;
            SnagImg.Filters.Annotation.CaptionOptions.TextColor = newOptions.TextColor;
            SnagImg.Filters.Annotation.CaptionOptions.BackgroundColor = newOptions.BackgroundColor;
            SnagImg.Filters.Annotation.CaptionOptions.ShadowColor = newOptions.ShadowColor;
            SnagImg.Filters.Annotation.CaptionOptions.OutlineColor = newOptions.OutlineColor;
         }
      }

      private void SystemCaptionSettingsBttn_Click( object sender, EventArgs e )
      {
         var captionOptions = SnagImg.Filters.Annotation.CaptionOptions;
         var optForm = new CaptionOptions( captionOptions );
         if ( optForm.ShowDialog( this ) == DialogResult.OK )
         {
            var newOptions = optForm.GetCaptionOptions();
            SnagImg.Filters.Annotation.SystemCaptionOptions.BackgroundColor = newOptions.BackgroundColor;
            SnagImg.Filters.Annotation.SystemCaptionOptions.UseWordWrap = newOptions.UseWordWrap;
            SnagImg.Filters.Annotation.SystemCaptionOptions.UseTransparentBackground = newOptions.UseTransparentBackground;
            SnagImg.Filters.Annotation.SystemCaptionOptions.CaptionStyle = newOptions.CaptionStyle;
            SnagImg.Filters.Annotation.SystemCaptionOptions.Placement = newOptions.Placement;
            SnagImg.Filters.Annotation.SystemCaptionOptions.TextColor = newOptions.TextColor;
            SnagImg.Filters.Annotation.SystemCaptionOptions.BackgroundColor = newOptions.BackgroundColor;
            SnagImg.Filters.Annotation.SystemCaptionOptions.ShadowColor = newOptions.ShadowColor;
            SnagImg.Filters.Annotation.SystemCaptionOptions.OutlineColor = newOptions.OutlineColor;
         } 
      }

      #endregion

      #region Color Effects options
      private void ClrFxBrightnessTrkBar_Scroll( object sender, EventArgs e )
      {
         ClrFxBrightnessTxBx.Text = (ClrFxBrightnessTrkBar.Value *10 ).ToString();
      }

      private void ClrFxContrastTrkBar_Scroll( object sender, EventArgs e )
      {
         ClrFxContrastTxtBx.Text = (ClrFxContrastTrkBar.Value *10).ToString();
      }

      private void ClrFxHueTrkBar_Scroll( object sender, EventArgs e )
      {
         ClrFxHueTxtBx.Text = ClrFxHueTrkBar.Value.ToString();
      }

      private void ClrFxSaturationTrkBar_Scroll( object sender, EventArgs e )
      {
         ClrFxSaturationTxtBx.Text = (ClrFxSaturationTrkBar.Value *10).ToString();
      }

      private void ClrFxGammaTrkBar_Scroll( object sender, EventArgs e )
      {
         ClrFxGammaTxtBx.Text = ClrFxGammaTrkBar.Value.ToString();
      }

      private void ClrFxDefaultsBttn_Click( object sender, EventArgs e )
      {
         ClrFxBrightnessTxBx.Text = "0";
         ClrFxBrightnessTrkBar.Value = 0;
         ClrFxContrastTxtBx.Text = "0";
         ClrFxContrastTrkBar.Value = 0;
         ClrFxHueTxtBx.Text = "0";
         ClrFxHueTrkBar.Value = 0;
         ClrFxSaturationTxtBx.Text = "0";
         ClrFxSaturationTrkBar.Value = 0;
         ClrFxGammaTxtBx.Text = "1.00";
         ClrFxGammaTrkBar.Value = 100;
      }

      private void DefaultColorFxControls()
      {
         ClrFxBrightnessTrkBar.Value = SnagImg.Filters.ColorEffects.Brightness;
         ClrFxBrightnessTxBx.Text = ClrFxBrightnessTrkBar.Value.ToString();
         ClrFxContrastTrkBar.Value = SnagImg.Filters.ColorEffects.Contrast;
         ClrFxContrastTxtBx.Text = ClrFxContrastTrkBar.Value.ToString();
         ClrFxHueTrkBar.Value = SnagImg.Filters.ColorEffects.Hue;
         ClrFxHueTxtBx.Text = ClrFxHueTrkBar.Value.ToString();
         ClrFxSaturationTrkBar.Value = SnagImg.Filters.ColorEffects.Saturation;
         ClrFxSaturationTxtBx.Text = ClrFxSaturationTrkBar.Value.ToString();
         ClrFxGammaTrkBar.Value = SnagImg.Filters.ColorEffects.Gamma;
         ClrFxGammaTxtBx.Text = ClrFxGammaTrkBar.Value.ToString();
      }
      #endregion

      #region Color Substitution options
      private void DefaultColorSubOptions()
      {
         ColorSubSelector.SelectedIndex = ColorSubSelector.Items.IndexOf( "None" );
         ColorSubOldBttn.BackColor = Color.White;
         ColorSubNewBttn.BackColor = Color.Black;
         ColorSubAccuracyTxtBx.Text = "100";
         EnableColorSubControls( false );
      }

      private void EnableColorSubControls( bool enable )
      {
         ColorSubDialogBttn.Enabled = enable;
         ColorSubOldBttn.Enabled = enable;
         ColorSubNewBttn.Enabled = enable;
         ColorSubAddBttn.Enabled = enable;
         ColorSubDeleteBttn.Enabled = enable;
         ColorSubSwapChkBx.Enabled = enable;
         ColorSubAccuracyTxtBx.Enabled = enable;
         ColorSubList.Enabled = enable;
      }

      private void ColorSubSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         switch ( ColorSubSelector.Text )
         {
            case "Invert":
            {
               EnableColorSubControls( false );
               SnagImg.Filters.ColorSubstitution.ColorSubMethod = snagColorSubMethod.scsmInvert;
               break;
            }
            case "Custom":
            {
               EnableColorSubControls( true );
               ColorSubDialogBttn.Enabled = false;
               SnagImg.Filters.ColorSubstitution.ColorSubMethod = snagColorSubMethod.scsmCustom;
               break;
            }
            case "Use Snagit Dialog":
            {
               EnableColorSubControls( false );
               ColorSubDialogBttn.Enabled = true;
               SnagImg.Filters.ColorSubstitution.ColorSubMethod = snagColorSubMethod.scsmCustom;
               break;
            }
            default: //None
            {
               EnableColorSubControls( false );
               SnagImg.Filters.ColorSubstitution.ColorSubMethod = snagColorSubMethod.scsmNone;
               break;
            }
         }
      }

      private void ColorSubOldBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            // Set button background to the selected color.
            ColorSubOldBttn.BackColor = clr;
         }
      }

      private void ColorSubNewBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            // Set button background to the selected color.
            ColorSubNewBttn.BackColor = clr;
         }
      }

      private void ColorSubDialogBttn_Click( object sender, EventArgs e )
      {
         SnagImg.Filters.ColorSubstitution.ShowColorSubDialog( (int) ColorSubDialogBttn.Handle );
      }


      private void ColorSubAddBttn_Click( object sender, EventArgs e )
      {
         var oldColor = Helpers.ConvertToCOLORREF( ColorSubOldBttn.BackColor );
         var newColor = Helpers.ConvertToCOLORREF( ColorSubNewBttn.BackColor );
         int accuracy = Helpers.ConvertToNumeric( ColorSubAccuracyTxtBx.Text );
         if ( accuracy < 0 || accuracy > 100 )
         {
            MessageBox.Show( "Color accuracy must be between 0 and 100" );
            tabFilters.Focus();
            tabFilters.SelectTab( 8 );
            ColorSubAccuracyTxtBx.Focus();
            return;
         }
         SnagImg.Filters.ColorSubstitution.AddColorSub( oldColor, newColor, accuracy, ColorSubSwapChkBx.Checked );

         var subOperation = ColorSubSwapChkBx.Checked ? " <swap> " : " replace > ";
         var newConversion = ColorSubOldBttn.BackColor.ToKnownColor() + subOperation + ColorSubNewBttn.BackColor.ToKnownColor();
         ColorSubList.Items.Add( newConversion );
      }

      private void ColorSubDeleteBttn_Click( object sender, EventArgs e )
      {
         ColorSubList.Clear();
         SnagImg.Filters.ColorSubstitution.ClearColorSub();
      }
      #endregion

      #region Resolution options
      private void DefaultResolutionControls()
      {
         ResolutionChkBx.Checked = true;
         ResolutionTxtBx.Text = SnagImg.Filters.Resolution.Resolution.ToString();
         ResolutionTxtBx.Enabled = false;
      }

      private void ResolutionChkBx_CheckedChanged( object sender, EventArgs e )
      {
         ResolutionTxtBx.Enabled = !ResolutionChkBx.Checked;
         SnagImg.Filters.Resolution.UseAutoResolution = ResolutionChkBx.Checked; 
      }
      #endregion

      #endregion

      #region Start Capture
      private void CaptureBttn_Click( object sender, EventArgs e )
      {
         if ( !SetMiscOptions() ) return;
         if ( !SetInputOptions() ) return;
         if ( !SetOutputOptions() ) return;
         if ( !SetCaptureFilterOptions() ) return;

         UpdateStatusText( "Capturing..." );

         //Call the capture action on a separate thread so as not to lock up the UI
         var startThread = new Thread( StartThreadWorker );
         startThread.Start();
      }

      private void StartThreadWorker()
      {
         HookupSnagitEvents();
         try
         {
            SnagImg.Capture();
         } 
         catch
         {
            var strErrorMessage = "Snagit error: " + SnagImg.LastError;
            MessageBox.Show( strErrorMessage );
         }
      }

      //Returns false upon input validation error
      private bool SetMiscOptions()
      {
         SnagImg.EnablePreviewWindow = PreviewChkBx.Checked;
         SnagImg.IncludeCursor = CursorChkBx.Checked;
         SnagImg.UseMagnifierWindow = MagnifierChkBx.Checked;

         //Set option to keep links
         SnagImg.HotspotType = HotSpotObjsRadio.Checked ? snagHotspotType.shtLinksAndControls : HotSpotLinksRadio.Checked ? snagHotspotType.shtLinksOnly : snagHotspotType.shtNone;

         //Set capture delay options
         if ( DelayChkBx.Checked )
         {
            if ( DelaySeconds.TextLength < 1 )
            {
               MessageBox.Show("Please enter the number of seconds to delay before starting a capture");
               DelaySeconds.Focus();
               return false;
            }

            SnagImg.DelayOptions.EnableDelayedCapture = true;
            SnagImg.DelayOptions.EnableCountdownWindow = CountDownChkBx.Checked;
            SnagImg.DelayOptions.DelaySeconds = Convert.ToInt16(DelaySeconds.Text);
         }

         return true;
      }

      private bool SetInputOptions()
      {
         UpdateStatusText( "Setting input options..." );

         #region Set auto scrolling options
         //Only enable auto-scrolling for the supported capture modes
         AutoScrollChkBx.Checked = SnagImg.Input == snagImageInput.siiCapture ||
                                   SnagImg.Input == snagImageInput.siiCustomScroll ||
                                   SnagImg.Input == snagImageInput.siiRegion ||
                                   SnagImg.Input == snagImageInput.siiWindow;

         if ( AutoScrollChkBx.Checked )
         {
            switch ( ScrollDirection.Text )
            {
               case "Both":
               {
                  SnagImg.AutoScrollOptions.AutoScrollMethod = snagAutoScrollMethod.sasmBoth;
                  break;
               }
               case "Horizontal":
               {
                  SnagImg.AutoScrollOptions.AutoScrollMethod = snagAutoScrollMethod.sasmHorizontal;
                  break;
               }
               default: //Vertical
               {
                  SnagImg.AutoScrollOptions.AutoScrollMethod = snagAutoScrollMethod.sasmNone;
                  break;
               }
            }

            switch ( ScrollStartPosition.Text )
            {
               case "Top":
               {
                  SnagImg.AutoScrollOptions.StartingPosition = snagAutoScrollStartingPosition.sasspTop;
                  break;
               }
               case "Left":
               {
                  SnagImg.AutoScrollOptions.StartingPosition = snagAutoScrollStartingPosition.sasspLeft;
                  break;
               }
               case "Top Left":
               {
                  SnagImg.AutoScrollOptions.StartingPosition = snagAutoScrollStartingPosition.sasspTopLeft;
                  break;
               }
               default: //current position
               {
                  SnagImg.AutoScrollOptions.StartingPosition = snagAutoScrollStartingPosition.sasspCurrent;
                  break;
               }
            }

            SnagImg.AutoScrollOptions.ForegroundScrollingWindow = ForeGroundChkBx.Checked;
            SnagImg.AutoScrollOptions.Delay = Convert.ToInt16(ScrollDelay.Text);

            //This option appears to be undocumented. I may have missed it :(
            //This used to pertain to the fastest scrolling method where we simply resize the window
            //and take the capture (same as with extended window capture). Now it means that we use
            //vision technology to perform scrolling captures. Setting this option to "false" will
            //force Snagit to use the older extended window scrolling technology.
            ((IAutoScrollOptions2) ( SnagImg.AutoScrollOptions )).FastestScrollingMethod = ScollMethodChkBx.Checked;
         }
         else
         {
            SnagImg.AutoScrollOptions.AutoScrollMethod = snagAutoScrollMethod.sasmNone;
         }
         #endregion

         #region Set window capture options
         if ( SnagImg.Input == snagImageInput.siiWindow )
         {
            switch ( SelectionType.Text )
            {
               case "Active Window":
               {
                  SnagImg.InputWindowOptions.SelectionMethod = snagWindowSelectionMethod.swsmActive;
                  break;
               }
               case "Window Handle":
               {
                  if ( HandleTxtBx.TextLength < 1 || Helpers.ConvertToNumeric( HandleTxtBx.Text ) == 0 )
                  {
                     MessageBox.Show("Please enter a window handle(not in hexadecimal)");
                     HandleTxtBx.Focus();
                     return false;
                  }

                  SnagImg.InputWindowOptions.Handle = Helpers.ConvertToNumeric( HandleTxtBx.Text );
                  SnagImg.InputWindowOptions.SelectionMethod = snagWindowSelectionMethod.swsmHandle;
                  break;
               }
               case "Point on the Desktop":
               {
                  SnagImg.InputWindowOptions.XPos = Helpers.ConvertToNumeric( XPosTxtBx.Text );
                  SnagImg.InputWindowOptions.YPos = Helpers.ConvertToNumeric( YPosTxtBx.Text );
                  SnagImg.InputWindowOptions.SelectionMethod = snagWindowSelectionMethod.swsmPoint;
                  break;
               }
               default: //Interactive
               {
                  SnagImg.InputWindowOptions.SelectionMethod = snagWindowSelectionMethod.swsmInteractive;
                  break;
               }
            }
         }
         #endregion

         #region Set fixed region capture options
         if ( CaptureType.Text == "Fixed Region" )
         {
            if ( Helpers.ConvertToNumeric( HeightTxtBx.Text ) == 0 )
            {
               MessageBox.Show( "Please enter a height greater than zero" );
               HeightTxtBx.Focus();
               return false;
            } 
            
            if ( Helpers.ConvertToNumeric( WidthTxtBx.Text ) == 0 )
            {
               MessageBox.Show( "Please enter a width greater than zero" );
               WidthTxtBx.Focus();
               return false;
            }

            SnagImg.InputRegionOptions.Height = Helpers.ConvertToNumeric( HeightTxtBx.Text );
            SnagImg.InputRegionOptions.Width = Helpers.ConvertToNumeric( WidthTxtBx.Text );

            if ( OffsetChkBx.Checked )
            {
               SnagImg.InputRegionOptions.UseStartPosition = true;
               SnagImg.InputRegionOptions.StartX = Helpers.ConvertToNumeric( XOffsetTxtBx.Text );
               SnagImg.InputRegionOptions.StartY = Helpers.ConvertToNumeric( YOffsetTxtBx.Text );
            }
         }
         #endregion

         #region Set menu capture options
         if ( CaptureType.Text == "Menu" )
         {
            SnagImg.InputMenuOptions.CaptureCascaded = CascadedMenusChkBx.Checked;
            SnagImg.InputMenuOptions.IncludeBar = MenuBarChkBx.Checked;
         }
         #endregion

         #region Set extended window capture options
         if ( CaptureType.Text == "Extended Window" )
         {
            SnagImg.InputExtendedWindowOptions.EnablePreview = WindowResizePreviewChkBx.Checked;
            if ( defaultWindowSizeChkBx.Checked )
            {
               if ( Helpers.ConvertToNumeric( HeightTxtBx.Text ) == 0 )
               {
                  MessageBox.Show( "Please enter a height greater than zero" );
                  HeightTxtBx.Focus();
                  return false;
               }

               if ( Helpers.ConvertToNumeric( WidthTxtBx.Text ) == 0 )
               {
                  MessageBox.Show( "Please enter a width greater than zero" );
                  WidthTxtBx.Focus();
                  return false;
               }

               SnagImg.InputExtendedWindowOptions.UseSpecifiedCaptureSize = true;
               SnagImg.InputExtendedWindowOptions.Height = Helpers.ConvertToNumeric( HeightTxtBx.Text );
               SnagImg.InputExtendedWindowOptions.Width = Helpers.ConvertToNumeric( WidthTxtBx.Text );
            }
         }
         #endregion

         #region Set Twain capture options
         if ( CaptureType.Text == "TWAIN" )
         {
            if ( TwainTxtBx.TextLength < 1 )
            {
               MessageBox.Show( "Please select a Twain device" );
               TwainBttn.Focus();
               return false;
            }
            SnagImg.InputTWAINOptions.Source = TwainTxtBx.Text;
         }
         #endregion

         return true;
      }

      private bool SetOutputOptions()
      {
         UpdateStatusText( "Setting output options..." );

         #region Set clipboard output options
         if ( OutputType.Text == "Clipboard" )
         {
            SnagImg.ClipboardOptions.WidthInPixels = Helpers.ConvertToNumeric( ClipBrdTxtBx.Text );
         }
         #endregion

         #region Set file output options
         if ( OutputType.Text == "File" )
         {
            switch ( NamingMethodSelection.Text )
            {
               case "Auto":
               {
                  if ( FileNameDigitsTextBx.TextLength < 1 || Helpers.ConvertToNumeric( FileNameDigitsTextBx.Text ) == 0 )
                  {
                     MessageBox.Show( "Please set the number of digits for auto file naming" );
                     OutputTabControl.Focus();
                     OutputTabControl.SelectTab(1);
                     FileNameDigitsTextBx.Focus();
                     return false;
                  }

                  SnagImg.OutputImageFile.AutoNumPrefixDigits = Helpers.ConvertToNumeric( FileNameDigitsTextBx.Text );
                  SnagImg.OutputImageFile.AutoFilePrefix = FilePrefixTextBx.Text;
                  SnagImg.OutputImageFile.FileNamingMethod = snagOuputFileNamingMethod.sofnmAuto;
                  break;
               }
               case "Fixed":
               {
                  if ( FileNameTxtBx.TextLength < 1 )
                  {
                     MessageBox.Show( "Please specify a filename for your capture" );
                     OutputTabControl.Focus();
                     OutputTabControl.SelectTab( 1 );
                     FileNameTxtBx.Focus();
                     return false;
                  }

                  SnagImg.OutputImageFile.Filename = FileNameTxtBx.Text;
                  SnagImg.OutputImageFile.FileNamingMethod = snagOuputFileNamingMethod.sofnmFixed;
                  break;
               }
               default: //Prompt
               {
                  SnagImg.OutputImageFile.FileNamingMethod = snagOuputFileNamingMethod.sofnmPrompt;
                  break;
               }
            }

            if ( SpecifyFileFolder.Checked && FolderPathTextBx.TextLength < 1 )
            {
               MessageBox.Show( "Please select a folder to save to" );
               OutputTabControl.Focus();
               OutputTabControl.SelectTab( 1 );
               FolderBrowseBttn.Focus();
               return false;
            }

            SnagImg.OutputImageFile.Directory = SpecifyFileFolder.Checked ? FolderPathTextBx.Text : _lastSavedFolder;
         }
         #endregion

         #region Set email output options
         if ( OutputType.Text == "E-mail" )
         {

            if ( !EmailPromptChkBx.Checked )
            {
               if ( EmailAddrTxtBx.TextLength < 1 )
               {
                  MessageBox.Show("Please enter the recipients email address");
                  OutputTabControl.Focus();
                  OutputTabControl.SelectTab(4);
                  EmailAddrTxtBx.Focus();
                  return false;
               }

               if ( EmailNameTxtBx.TextLength < 1 )
               {
                  MessageBox.Show("Please enter the recipients name");
                  OutputTabControl.Focus();
                  OutputTabControl.SelectTab(4);
                  EmailNameTxtBx.Focus();
                  return false;
               }

               SnagImg.OutputMailOptions.Address = EmailAddrTxtBx.Text;
               SnagImg.OutputMailOptions.Name = EmailNameTxtBx.Text;
               SnagImg.OutputMailOptions.Subject = EmailMsgTxtBx.Text;
               SnagImg.OutputMailOptions.MessageText = EmailSubjectTxtBx.Text;
            }
         }
         #endregion

         #region Set printer output options
         if ( OutputType.Text == "Printer" )
         {
            if ( PrntrNameTxtBx.TextLength < 1 || PrntrDriverTxtBx.TextLength < 1 || PrntrPortTxtBx.TextLength < 1 )
            {
               MessageBox.Show("Please select a printer");
               OutputTabControl.Focus();
               OutputTabControl.SelectTab(5);
               PrinterSelectBttn.Focus();
               return false;
            }

            if ( SnagImg.OutputPrinterPageLayoutOptions.ScalingType == snagPrintScale.spsPercentScale &&
                 ( PrntrPercentTxtBx.TextLength < 1 || Helpers.ConvertToNumeric( PrntrPercentTxtBx.Text ) < 1 ) )
            {
               MessageBox.Show("Please set the printer scaling percent");
               OutputTabControl.Focus();
               OutputTabControl.SelectTab(5);
               PrntrPercentTxtBx.Focus();
               return false;
            }

            if ( SnagImg.OutputPrinterPageLayoutOptions.ScalingType == snagPrintScale.spsFixedSize )
            {
               if ( PrntrHeightTxtBx.TextLength < 1 || Helpers.ConvertToNumeric( PrntrHeightTxtBx.Text ) < 1 )
               {
                  MessageBox.Show("Please set the printer scaling height");
                  OutputTabControl.Focus();
                  OutputTabControl.SelectTab(5);
                  PrntrHeightTxtBx.Focus();
                  return false;
               }

               if ( PrntrWidthTxtBx.TextLength < 1 || Helpers.ConvertToNumeric( PrntrWidthTxtBx.Text ) < 1 )
               {
                  MessageBox.Show("Please set the printer scaling width");
                  OutputTabControl.Focus();
                  OutputTabControl.SelectTab(5);
                  PrntrWidthTxtBx.Focus();
                  return false;
               }
            }

            SnagImg.OutputPrinterPageLayoutOptions.MarginLeft = Helpers.ConvertToNumeric( MarginLeftTxtBx.Text );
            SnagImg.OutputPrinterPageLayoutOptions.MarginRight = Helpers.ConvertToNumeric( MarginRightTxtBx.Text );
            SnagImg.OutputPrinterPageLayoutOptions.MarginTop = Helpers.ConvertToNumeric( MarginTopTxtBx.Text );
            SnagImg.OutputPrinterPageLayoutOptions.MarginBottom = Helpers.ConvertToNumeric( MarginBottomTxtBx.Text );

            SnagImg.OutputPrinterPageLayoutOptions.Scale = Helpers.ConvertToNumeric( PrntrPercentTxtBx.Text );
            SnagImg.OutputPrinterPageLayoutOptions.Height = Helpers.ConvertToNumeric( PrntrHeightTxtBx.Text );
            SnagImg.OutputPrinterPageLayoutOptions.Width = Helpers.ConvertToNumeric( PrntrWidthTxtBx.Text );
         }
         #endregion

         #region Set ftp output options
         if ( OutputType.Text == "FTP" )
         {
            if ( TFPServerNameTxtBx.TextLength < 1 )
            {
               MessageBox.Show("Please fill in the FTP server name");
               OutputTabControl.Focus();
               OutputTabControl.SelectTab(3);
               TFPServerNameTxtBx.Focus();
               return false;
            }

            if ( FTPAuthChkBx.Checked )
            {
               if ( FTPUsernameTxtBx.TextLength < 1 )
               {
                  MessageBox.Show("Please fill in the user name for the FTP server");
                  OutputTabControl.Focus();
                  OutputTabControl.SelectTab(3);
                  FTPUsernameTxtBx.Focus();
                  return false;
               }
               if ( FTPPasswordTxtBx.TextLength < 1 )
               {
                  MessageBox.Show("Please fill in the password for the FTP server");
                  OutputTabControl.Focus();
                  OutputTabControl.SelectTab(3);
                  FTPPasswordTxtBx.Focus();
                  return false;
               }
            }

            if ( FTPProxyChkBx.Checked && FTPProxyTxtBx.TextLength < 1 )
            {
               MessageBox.Show("Please fill in the FTP proxy server and port");
               OutputTabControl.Focus();
               OutputTabControl.SelectTab(3);
               FTPProxyTxtBx.Focus();
               return false;
            }

            SnagImg.OutputFTPOptions.Port = Helpers.ConvertToNumeric( FTPPortTxtBx.Text );
            SnagImg.OutputFTPOptions.RemotePath = FTPServerPathTxtBx.Text;
            SnagImg.OutputFTPOptions.Server = TFPServerNameTxtBx.Text;
            SnagImg.OutputFTPOptions.UserName = FTPUsernameTxtBx.Text;
            SnagImg.OutputFTPOptions.Password = FTPPasswordTxtBx.Text;
            SnagImg.OutputFTPOptions.ProxyServer = FTPProxyTxtBx.Text;
            SnagImg.OutputFTPOptions.SequenceLimit = Helpers.ConvertToNumeric( FTPSequenceTxBx.Text );
            SnagImg.OutputFTPOptions.Filename = FTPFilenameTxtBx.Text;
         }
         #endregion

         return true;
      }

      private bool SetCaptureFilterOptions()
      {
         UpdateStatusText("Setting capture filters...");

         #region Trim options
         if ( TrimTypeSelector.Text != "None" )
         {
            if ( TrimTypeSelector.Text == "Manual" )
            {
               SnagImg.Filters.Trim.Left = Helpers.ConvertToNumeric( TrimLeftTxtBx.Text );
               SnagImg.Filters.Trim.Right = Helpers.ConvertToNumeric( TrimRightTxtBx.Text );
               SnagImg.Filters.Trim.Top = Helpers.ConvertToNumeric( TrimTopTxtBx.Text );
               SnagImg.Filters.Trim.Bottom = Helpers.ConvertToNumeric( TrimBottomTxtBx.Text );
            }
         }
         #endregion

         #region Border options
         if ( EnableBorderChkBx.Checked )
         {
            if ( Helpers.ConvertToNumeric( BorderWidthTxtBx.Text ) < 1 )
            {
               MessageBox.Show( "Border width must be greater than zero" );
               tabFilters.Focus();
               tabFilters.SelectTab( 1 );
               BorderWidthTxtBx.Focus();
               return false;
            }
            if ( Helpers.ConvertToNumeric( BorderWidthTxtBx.Text ) <= Helpers.ConvertToNumeric( BorderShadowTxtBx.Text ) )
            {
               MessageBox.Show( "Border shadow width must be less than the total width" );
               tabFilters.Focus();
               tabFilters.SelectTab( 1 );
               BorderShadowTxtBx.Focus();
               return false;
            }

            SnagImg.Filters.Border.TotalWidth = Helpers.ConvertToNumeric( BorderWidthTxtBx.Text );
            SnagImg.Filters.Border.ShadowWidth = Helpers.ConvertToNumeric( BorderShadowTxtBx.Text );
         }
         #endregion

         #region Watermark options
         if ( EnableWatermarkChkBx.Checked )
         {
            if ( WatermarkFileTxtBx.TextLength < 1 )
            {
               MessageBox.Show( "Please select a image for to use for the watermark" );
               tabFilters.Focus();
               tabFilters.SelectTab( 2 );
               WatermarkBrowseBttn.Focus();
               return false;
            }

            if ( Helpers.ConvertToNumeric( WatermarkDepthTxtBx.Text ) < 0 || Helpers.ConvertToNumeric( WatermarkDepthTxtBx.Text ) > 1000 )
            {
               MessageBox.Show( "Please enter a watermark depth between zero and 1000" );
               tabFilters.Focus();
               tabFilters.SelectTab( 2 );
               WatermarkDepthTxtBx.Focus();
               return false;
            }

            if ( Helpers.ConvertToNumeric( WatermarkHOffChkBx.Text ) < 0 || Helpers.ConvertToNumeric( WatermarkHOffChkBx.Text ) > 100 )
            {
               MessageBox.Show( "Please enter a watermark horizontal offset between zero and 100" );
               tabFilters.Focus();
               tabFilters.SelectTab( 2 );
               WatermarkHOffChkBx.Focus();
               return false;
            }

            if ( Helpers.ConvertToNumeric( WatermarkVOffChkBx.Text ) < 0 || Helpers.ConvertToNumeric( WatermarkVOffChkBx.Text ) > 100 )
            {
               MessageBox.Show( "Please enter a watermark vertical offset between zero and 100" );
               tabFilters.Focus();
               tabFilters.SelectTab( 2 );
               WatermarkVOffChkBx.Focus();
               return false;
            }

            if ( Helpers.ConvertToNumeric( WatermarkSizeTxtBx.Text ) < 1 || Helpers.ConvertToNumeric( WatermarkSizeTxtBx.Text ) > 100 )
            {
               MessageBox.Show( "Please enter a watermark scaling size between 1 and 100" );
               tabFilters.Focus();
               tabFilters.SelectTab( 2 );
               WatermarkSizeTxtBx.Focus();
               return false;
            }

            SnagImg.Filters.Watermark.OffsetHorizontal = Helpers.ConvertToNumeric( WatermarkHOffChkBx.Text );
            SnagImg.Filters.Watermark.OffsetVertical = Helpers.ConvertToNumeric( WatermarkVOffChkBx.Text );
            SnagImg.Filters.Watermark.Scale = Helpers.ConvertToNumeric( WatermarkSizeTxtBx.Text );
            SnagImg.Filters.Watermark.EmbossDepth = Helpers.ConvertToNumeric( WatermarkDepthTxtBx.Text );
            SnagImg.Filters.Watermark.ImageFilename = WatermarkFileTxtBx.Text; 
         }
         #endregion

         #region Scaling options
         if ( ScalingMethodSelector.Text != "None" && ScalingMethodSelector.Text != "Height" && Helpers.ConvertToNumeric( ScalingWidthTxtBx.Text ) < 1 )
         {
            MessageBox.Show( "Scale width must be greater than zero" );
            tabFilters.Focus();
            tabFilters.SelectTab( 3 );
            ScalingWidthTxtBx.Focus();
            return false;
         }

         switch ( ScalingMethodSelector.Text )
         {
            case "Percentage":
            {
               if ( ScalingAspectRatioChkBx.Checked )
               {
                  //Scale whole image by a percentage
                  SnagImg.Filters.Scale.ScaleMethod = snagImageScaleMethod.sismPercentage;
                  SnagImg.Filters.Scale.Factor = Helpers.ConvertToNumeric( ScalingWidthTxtBx.Text );
               }
               else
               {
                  //Scale the width and height independently by percentage
                  if ( Helpers.ConvertToNumeric( ScalingHeightTxtBx.Text ) < 1 )
                  {
                     MessageBox.Show( "Scale height must be greater than zero" );
                     tabFilters.Focus();
                     tabFilters.SelectTab( 3 );
                     ScalingHeightTxtBx.Focus();
                     return false;
                  }
                  SnagImg.Filters.Scale.ScaleMethod = snagImageScaleMethod.sismCustom;
                  SnagImg.Filters.Scale.ScaleBy = snagImageScaleBy.sisbFactor;
                  SnagImg.Filters.Scale.FactorWidth = Helpers.ConvertToNumeric( ScalingWidthTxtBx.Text );
                  SnagImg.Filters.Scale.FactorHeight = Helpers.ConvertToNumeric( ScalingHeightTxtBx.Text );
               }
               break;
            }
            case "Width":
            {
               //Scale only the width, unless keep aspect ratio is checked. Otherwise, scale
               //the whole image based on the width entered.
               SnagImg.Filters.Scale.Width = Helpers.ConvertToNumeric( ScalingWidthTxtBx.Text );
               SnagImg.Filters.Scale.ScaleBy = snagImageScaleBy.sisbWidth;
               SnagImg.Filters.Scale.ScaleMethod = snagImageScaleMethod.sismCustom;
               break;
            }
            case "Height":
            {
               //Scale only the height, unless keep aspect ratio is checked. Otherwise, scale
               //the whole image based on the height entered.
               if ( Helpers.ConvertToNumeric( ScalingHeightTxtBx.Text ) < 1 )
               {
                  MessageBox.Show( "Scale height must be greater than zero" );
                  tabFilters.Focus();
                  tabFilters.SelectTab( 3 );
                  ScalingHeightTxtBx.Focus();
                  return false;
               }
               SnagImg.Filters.Scale.Height = Helpers.ConvertToNumeric( ScalingHeightTxtBx.Text );
               SnagImg.Filters.Scale.ScaleBy = snagImageScaleBy.sisbHeight;
               SnagImg.Filters.Scale.ScaleMethod = snagImageScaleMethod.sismCustom;
               break;
            }
            case "Fixed":
            {
               //Scale the image based on width and height input.
               if ( Helpers.ConvertToNumeric( ScalingHeightTxtBx.Text ) < 1 )
               {
                  MessageBox.Show( "Scale height must be greater than zero" );
                  tabFilters.Focus();
                  tabFilters.SelectTab( 3 );
                  ScalingHeightTxtBx.Focus();
                  return false;
               }
               SnagImg.Filters.Scale.Width = Helpers.ConvertToNumeric( ScalingWidthTxtBx.Text );
               SnagImg.Filters.Scale.Height = Helpers.ConvertToNumeric( ScalingHeightTxtBx.Text );
               SnagImg.Filters.Scale.ScaleBy = snagImageScaleBy.sisbWidthAndHeight;
               SnagImg.Filters.Scale.ScaleMethod = snagImageScaleMethod.sismCustom;
               break;
            }
            default: //None
            {
               //Turn off the scaling filter
               SnagImg.Filters.Scale.ScaleMethod = snagImageScaleMethod.sismNone;
               break;
            }
         }
         #endregion

         #region Color Convert options
         if ( ColorConvertSelector.Text != "None" )
         {
            if ( ColorConvertSelector.Text == "Monochrome" )
            {
               int threshold = Helpers.ConvertToNumeric( MonoIntensityTxtBx.Text );
               if ( threshold < 0 || threshold > 100 )
               {
                  MessageBox.Show( "Monochrome intensity threshold must be between 0 and 100" );
                  tabFilters.Focus();
                  tabFilters.SelectTab( 4 );
                  MonoIntensityTxtBx.Focus();
                  return false;
               }

               SnagImg.Filters.ColorConversion.MonochromeThreshold = threshold;
            }
         }
         #endregion

         #region Caption options
         if ( EnableCaptionChkBx.Checked )
         {
            if ( !CaptionPromptChkBx.Checked && CaptionTxtBx.TextLength < 1 )
            {
               MessageBox.Show( "Please check prompt for caption or enter the caption text" );
               tabFilters.Focus();
               tabFilters.SelectTab( 5 );
               CaptionPromptChkBx.Focus();
               return false;
            }

            SnagImg.Filters.Annotation.CaptionText = CaptionTxtBx.Text;
         }
         #endregion

         #region Color Effects options
         SnagImg.Filters.ColorEffects.Brightness = Helpers.ConvertToNumeric(ClrFxBrightnessTxBx.Text);
         SnagImg.Filters.ColorEffects.Contrast = Helpers.ConvertToNumeric( ClrFxContrastTxtBx.Text );
         SnagImg.Filters.ColorEffects.Hue = Helpers.ConvertToNumeric( ClrFxHueTxtBx.Text );
         SnagImg.Filters.ColorEffects.Saturation = Helpers.ConvertToNumeric( ClrFxSaturationTxtBx.Text );
         SnagImg.Filters.ColorEffects.Gamma = Helpers.ConvertToNumeric( ClrFxGammaTxtBx.Text );
         #endregion

         #region Color Substitution options
         if ( ColorSubSelector.Text != "None" )
         {
            if ( ColorSubSelector.Text == "Custom" )
            {               
               if ( ColorSubList.Items.Count < 1 )
               {
                  MessageBox.Show( "Please add a custom color substitution filter to the list" );
                  tabFilters.Focus();
                  tabFilters.SelectTab( 8 );
                  ColorSubAddBttn.Focus();
                  return false;
               }
            }
         }
         #endregion

         #region Resolution options
         if ( !ResolutionChkBx.Checked )
         {
            if ( Helpers.ConvertToNumeric( ResolutionTxtBx.Text ) < 0 || Helpers.ConvertToNumeric( ResolutionTxtBx.Text ) > 32000 )
            {
               MessageBox.Show( "DPI resolution must be set between 0 and 32000" );
               tabFilters.Focus();
               tabFilters.SelectTab( 7 );
               ResolutionTxtBx.Focus();
               return false;
            }

            SnagImg.Filters.Resolution.Resolution = Helpers.ConvertToNumeric(ResolutionTxtBx.Text);
         }
         #endregion

         return true;
      }
      #endregion
   }
}
