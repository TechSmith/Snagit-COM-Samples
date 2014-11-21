using System;
using System.Drawing;
using System.Windows.Forms;


namespace ImageCaptureSample
{
   static class Helpers
   {
      //Converts a string to a number. Returns zero upon failure (and possibly on success).
      public static Int16 ConvertToNumeric( string str )
      {
         Int16 number;

         try
         {
            number = Convert.ToInt16( str );
         } catch
         {
            number = 0;
         }

         return number;
      }

      public static int ConvertToRGB( Color clr )
      {
         var rgb = clr.R << 8;
         rgb = ( rgb | clr.G ) << 8;
         return ( rgb | clr.B );
      }

      public static int ConvertToCOLORREF( Color clr )
      {
         //Note: COLORREF has this format: 0x00bbggrr
         var rgb = clr.B << 8;
         rgb = ( rgb | clr.G ) << 8;
         return ( rgb | clr.R );
      }

      public static Color ConvertRGBToColor( int rgb )
      {
         var red = ( rgb & 0xFF0000 ) >> 16;
         var green = ( rgb & 0x00FF00 ) >> 8;
         return Color.FromArgb( 255, red, green, rgb & 0xFF );
      }

      //Returns true if the user selected a color
      public static bool SelectColor( out Color clr )
      {
         var clrDlg = new ColorDialog();
         clr = Color.Transparent;
         if ( clrDlg.ShowDialog() == DialogResult.OK )
         {
            clr = clrDlg.Color;
            return true;
         }

         return false;
      }

      public static String BrowseOutputFolder()
      {
         String folderPath = "";
         FolderBrowserDialog browseFoldersDialog = new FolderBrowserDialog();
         browseFoldersDialog.Description = "Select a folder";

         if ( browseFoldersDialog.ShowDialog() == DialogResult.OK )
         {
            folderPath = browseFoldersDialog.SelectedPath;
         }

         return folderPath;
      }

      public static String BrowseForFile()
      {
         String filename = "";
         OpenFileDialog brosweFilesDialog = new OpenFileDialog();
         DialogResult result = brosweFilesDialog.ShowDialog();
         if ( result == DialogResult.OK )
         {
            filename = brosweFilesDialog.FileName;
         }

         return filename;
      }
   }
}
