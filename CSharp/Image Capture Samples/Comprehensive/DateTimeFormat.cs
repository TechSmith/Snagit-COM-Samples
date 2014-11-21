using System;
using System.Windows.Forms;
using SNAGITLib;

namespace ImageCaptureSample
{
   public partial class DateTimeFormatOptions : Form
   {
      #region properties
      private snagTimeDateOrder _dateTimeOrder;

      public snagTimeDateOrder DateTimeOrder
      {
         get { return _dateTimeOrder; }
         set
         {
            _dateTimeOrder = value;
            switch ( _dateTimeOrder )
            {
               case snagTimeDateOrder.stdoDateOnly:
               {
                  DateTimeOrderSelector.SelectedIndex = DateTimeOrderSelector.Items.IndexOf( "Date only" );
                  WindowsDateFormatChkBx.Checked = true;
                  WindowsTimeFormatChkBx.Checked = false;
                  CustomDateFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
                  CustomTimeFormatTxtBx.Enabled = false;
                  break;
               }
               case snagTimeDateOrder.stdoTimeOnly:
               {
                  DateTimeOrderSelector.SelectedIndex = DateTimeOrderSelector.Items.IndexOf( "Time only" );
                  WindowsTimeFormatChkBx.Checked = true;
                  WindowsDateFormatChkBx.Checked = false;
                  CustomDateFormatTxtBx.Enabled = false;
                  CustomTimeFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
                  break;
               }
               case snagTimeDateOrder.stdoDateThenTime:
               {
                  DateTimeOrderSelector.SelectedIndex = DateTimeOrderSelector.Items.IndexOf( "Date then time" );
                  WindowsTimeFormatChkBx.Checked = true;
                  WindowsDateFormatChkBx.Checked = true;
                  CustomDateFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
                  CustomTimeFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
                  break;
               }
               default:
               {
                  DateTimeOrderSelector.SelectedIndex = DateTimeOrderSelector.Items.IndexOf( "Time then date" );
                  WindowsTimeFormatChkBx.Checked = true;
                  WindowsDateFormatChkBx.Checked = true;
                  CustomDateFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
                  CustomTimeFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
                  break;
               }
            }
         }
      }

      private string _customDateFormat;
      public string CustomDateFormat
      {
         get
         {
            return _customDateFormat;
         }
         set
         {
            _customDateFormat = value;
            CustomDateFormatTxtBx.Text = value;
            CustomDateTimeFormatChkBx.Checked = CustomDateTimeFormatChkBx.Checked || _customDateFormat.Length > 0;
         }
      }

      private string _customTimeFormat;
      public string CustomTimeFormat
      {
         get
         {
            return _customTimeFormat;
         }
         set
         {
            _customTimeFormat = value;
            CustomDateTimeFormatChkBx.Text = value;
            CustomDateTimeFormatChkBx.Checked = CustomDateTimeFormatChkBx.Checked || _customTimeFormat.Length > 0;
         }
      }

      private bool _useWindowsTimeFormat;

      public bool UseWindowsTimeFormat
      {
         get
         {
            return _useWindowsTimeFormat;
         }
         set 
         { _useWindowsTimeFormat = value;
            WindowsTimeFormatChkBx.Checked = value;
         }
      }

      private bool _useWindowsDateFormat;
      public bool UseWindowsDateFormat
      {
         get
         {
            return _useWindowsDateFormat;
         }
         set
         {
            _useWindowsDateFormat = value;
            WindowsDateFormatChkBx.Checked = value;
         }
      }
      #endregion

      public DateTimeFormatOptions()
      {
         InitializeComponent();
         CustomDateFormatTxtBx.Enabled = false;
         CustomTimeFormatTxtBx.Enabled = false;
      }

      public void OkBttn_Click( object sender, EventArgs e )
      {
         UseWindowsDateFormat = WindowsDateFormatChkBx.Checked;
         UseWindowsTimeFormat = WindowsTimeFormatChkBx.Checked;
         CustomDateFormat = CustomDateFormatTxtBx.Text;
         CustomTimeFormat = CustomTimeFormatTxtBx.Text;
         DialogResult = DialogResult.OK;
         Close();
      }

      public void CancelBttn_Click( object sender, EventArgs e )
      {
         DialogResult = DialogResult.Cancel;
         Close();
      }

      private void DateTimeOrderSelector_SelectedIndexChanged( object sender, EventArgs e )
      {
         switch ( DateTimeOrderSelector.Text )
         {
            case "Date only":
            {
               DateTimeOrder = snagTimeDateOrder.stdoDateOnly;
               WindowsDateFormatChkBx.Checked = true;
               WindowsTimeFormatChkBx.Checked = false;
               CustomDateFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
               CustomTimeFormatTxtBx.Enabled = false;
               break;
            }
            case "Time only":
            {
               DateTimeOrder = snagTimeDateOrder.stdoTimeOnly;
               WindowsTimeFormatChkBx.Checked = true;
               WindowsDateFormatChkBx.Checked = false;
               CustomDateFormatTxtBx.Enabled = false;
               CustomTimeFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
               break;
            }
            case "Date then time":
            {
               DateTimeOrder = snagTimeDateOrder.stdoDateThenTime;
               WindowsTimeFormatChkBx.Checked = true;
               WindowsDateFormatChkBx.Checked = true;
               CustomDateFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
               CustomTimeFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
               break;
            }
            default:
            {
               DateTimeOrder = snagTimeDateOrder.stdoTimeThenDate;
               WindowsTimeFormatChkBx.Checked = true;
               WindowsDateFormatChkBx.Checked = true;
               CustomDateFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
               CustomTimeFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked;
               break;
            }
         }
      }

      private void CustomDateTimeFormatChkBx_CheckedChanged( object sender, EventArgs e )
      {
         CustomDateFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked && DateTimeOrderSelector.Text != "Time only";
         CustomTimeFormatTxtBx.Enabled = CustomDateTimeFormatChkBx.Checked && DateTimeOrderSelector.Text != "Date only";
      }
   }
}
