using System;
using System.Drawing;
using System.Windows.Forms;
using SNAGITLib;

namespace ImageCaptureSample
{
   public partial class CaptionOptions : Form
   {
      private ImageCaptionOptions _captionOptions;
      public ImageCaptionOptions GetCaptionOptions()
      {
         return _captionOptions;
      }

      public CaptionOptions( ImageCaptionOptions imgOpts )
      {
         InitializeComponent();
         _captionOptions = imgOpts;
         SetDefaults( imgOpts );
      }

      private void SetDefaults( ImageCaptionOptions imgOpts )
      {
         _captionOptions.UseWordWrap = imgOpts.UseWordWrap;
         CaptionOptsWordWrapChkBx.Checked = imgOpts.UseWordWrap;
         _captionOptions.UseTransparentBackground = imgOpts.UseTransparentBackground;
         CaptionOptsTransparentChkBx.Checked = imgOpts.UseTransparentBackground;
         TextColorBttn.BackColor = Helpers.ConvertRGBToColor( imgOpts.TextColor );
         BackgroundColorBttn.BackColor = Helpers.ConvertRGBToColor( imgOpts.BackgroundColor );
         ShadowColorBttn.BackColor = Helpers.ConvertRGBToColor( imgOpts.ShadowColor );
         OutlineColorBttn.BackColor = Helpers.ConvertRGBToColor( imgOpts.OutlineColor );
         
         //Default the caption style selector
         switch ( imgOpts.CaptionStyle )
         {
            case snagCaptionTextStyle.sctsDropShadow:
            {
               CaptionStyleSelector.SelectedIndex = CaptionStyleSelector.Items.IndexOf( "Drop Shadow" );
               break;
            }
            case snagCaptionTextStyle.sctsOutlinedShadow:
            {
               CaptionStyleSelector.SelectedIndex = CaptionStyleSelector.Items.IndexOf( "Outlined" );
               break;
            }
            default: // snagCaptionTextStyle.sctsNormal:
            {
               CaptionStyleSelector.SelectedIndex = CaptionStyleSelector.Items.IndexOf( "Normal" );
               break;
            }
         }

         //Default the caption position selector
         switch ( imgOpts.Placement )
         {
            case snagPlacement.spOutsideRight:
            {
               CaptionPositionSelector.SelectedIndex = CaptionPositionSelector.Items.IndexOf( "Right of Image" );
               break;
            }
            case snagPlacement.spOutsideLeft:
            {
               CaptionPositionSelector.SelectedIndex = CaptionPositionSelector.Items.IndexOf( "Left of Image" );
               break;
            }
            case snagPlacement.spOutsideBottom:
            {
               CaptionPositionSelector.SelectedIndex = CaptionPositionSelector.Items.IndexOf( "Below Image" );
               break;
            }
            case snagPlacement.spCenterTop:
            {
               CaptionPositionSelector.SelectedIndex = CaptionPositionSelector.Items.IndexOf( "Top of Image" );
               break;
            }
            case snagPlacement.spCenterMiddle:
            {
               CaptionPositionSelector.SelectedIndex = CaptionPositionSelector.Items.IndexOf( "Middle of Image" );
               break;
            }
            case snagPlacement.spCenterBottom:
            {
               CaptionPositionSelector.SelectedIndex = CaptionPositionSelector.Items.IndexOf( "Bottom of Image" );
               break;
            }
            default: // snagPlacement.spOutsideTop:
            {
               CaptionPositionSelector.SelectedIndex = CaptionPositionSelector.Items.IndexOf( "Above Image" );
               break;
            }
         }
      }

      private void CaptionPositionSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         //Note: This setting determines where on the image, or next to, the caption
         //      is to be placed.
         switch ( CaptionPositionSelector.Text )
         {
            case "Right of Image":
            {
               _captionOptions.Placement = snagPlacement.spOutsideRight;
               break;
            }
            case "Left of Image":
            {
               _captionOptions.Placement = snagPlacement.spOutsideLeft;
               break;
            }
            case "Below Image":
            {
               _captionOptions.Placement = snagPlacement.spOutsideBottom;
               break;
            }
            case "Top of Image":
            {
               _captionOptions.Placement = snagPlacement.spCenterTop;
               break;
            }
            case "Middle of Image":
            {
               _captionOptions.Placement = snagPlacement.spCenterMiddle;
               break;
            }
            case "Bottom of Image":
            {
               _captionOptions.Placement = snagPlacement.spCenterBottom;
               break;
            }

            //Note: Other possible positions on the image are:
            // spLeftBottom, spLeftMiddle, spLeftTop
            // spRightBottom, spRightMiddle, spRightTop

            default: //Above the image
            {
               _captionOptions.Placement = snagPlacement.spOutsideTop;
               break;
            }
         }
      }

      private void CaptionStyleSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         switch ( CaptionStyleSelector.Text )
         {
            case "Drop Shadow":
            {
               _captionOptions.CaptionStyle = snagCaptionTextStyle.sctsDropShadow;
               break;
            }
            case "Outlined":
            {
               _captionOptions.CaptionStyle = snagCaptionTextStyle.sctsOutlinedShadow;
               break;
            }
            default:
            {
               _captionOptions.CaptionStyle = snagCaptionTextStyle.sctsNormal;
               break;
            }
         }
      }

      private void CaptionOptsWordWrapChkBx_CheckedChanged( object sender, EventArgs e )
      {
         _captionOptions.UseWordWrap = CaptionOptsWordWrapChkBx.Checked;
      }

      private void CaptionOptsTransparentChkBx_CheckedChanged( object sender, EventArgs e )
      {
         _captionOptions.UseTransparentBackground = CaptionOptsTransparentChkBx.Checked;
      }

      private void TextColorBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            TextColorBttn.BackColor = clr;
            _captionOptions.TextColor = Helpers.ConvertToRGB( clr );
         }
      }

      private void BackgroundColorBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            BackgroundColorBttn.BackColor = clr;
            _captionOptions.BackgroundColor = Helpers.ConvertToRGB( clr );
         }
      }

      private void ShadowColorBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            ShadowColorBttn.BackColor = clr;
            _captionOptions.ShadowColor = Helpers.ConvertToRGB( clr );
         }

      }

      private void OutlineColorBttn_Click( object sender, EventArgs e )
      {
         Color clr;
         if ( Helpers.SelectColor( out clr ) )
         {
            OutlineColorBttn.BackColor = clr;
            _captionOptions.OutlineColor = Helpers.ConvertToRGB( clr );
         }

      }

      private void OkBttn_Click( object sender, EventArgs e )
      {
         DialogResult = DialogResult.OK;
         Close();
      }

      private void CancelBttn_Click( object sender, EventArgs e )
      {
         DialogResult = DialogResult.Cancel;
         Close();
      }
   }
}
