//---------------------------------------------------------------------------
// Filename: ImageCaptureDlg.cpp - Class definition file for the 
//           CImageCaptureDlg class for the SnagIt COM server MFC/CPP 
//           ImageCapture sample.
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#include "stdafx.h"
#include "ImageCapture.h"
#include "ImageCaptureDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

_ATL_FUNC_INFO SnagItStateInfo = { CC_STDCALL, VT_EMPTY, 1, { VT_I4 } };

/////////////////////////////////////////////////////////////////////////////
// CImageCaptureDlg dialog

CImageCaptureDlg::CImageCaptureDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CImageCaptureDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CImageCaptureDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CImageCaptureDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CImageCaptureDlg)
	DDX_Control(pDX, IDC_SHOWPREVIEW, m_Preview);
	DDX_Control(pDX, IDC_BORDER, m_Border);
	DDX_Control(pDX, IDC_BRIGHT, m_Brightness);
	DDX_Control(pDX, IDC_AUTOSCROLL, m_AutoScroll);
	DDX_Control(pDX, IDC_OUTPUT, m_OutputBox);
	DDX_Control(pDX, IDC_INPUT, m_InputBox);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CImageCaptureDlg, CDialog)
	//{{AFX_MSG_MAP(CImageCaptureDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_CAPTURE, OnCapture)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CImageCaptureDlg message handlers

BOOL CImageCaptureDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// Initialize the COM system (Has to be done before creating the SnagIt
   // Image capture object.
   CoInitialize( NULL );

   // Create the SnagIt ImageCapture object.
   HRESULT hr = m_pImageCapture.CreateInstance( __uuidof(SnagIt::ImageCapture) );

   // Subscribe to SnagIt state events.
   hr = SnagItStateEvents::DispEventAdvise( m_pImageCapture );

   // Add the items to the lists:
   LoadInputs();
   LoadOutputs();
   
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CImageCaptureDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CImageCaptureDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CImageCaptureDlg::OnCapture() 
{
   // Set the input to the input currently selected in the input combo box
   m_pImageCapture->Input = (SnagIt::snagImageInput) m_InputBox.GetItemData( m_InputBox.GetCurSel() );

   // Handle the region cases.
   if ( m_pImageCapture->Input == SnagIt::siiRegion )
   {
      CString strSelected;
      m_InputBox.GetLBText( m_InputBox.GetCurSel(), strSelected );
      if ( strSelected == "Fixed Region" )
      {
         m_pImageCapture->InputRegionOptions->SelectionMethod = SnagIt::srsmFixed;
         m_pImageCapture->InputRegionOptions->Height = 480;
         m_pImageCapture->InputRegionOptions->Width = 640;
         m_pImageCapture->InputRegionOptions->UseStartPosition = VARIANT_FALSE;
      }
      else
      {
         m_pImageCapture->InputRegionOptions->SelectionMethod = SnagIt::srsmInteractive;
      }
   }

   // Handle the window cases.
   if ( m_pImageCapture->Input == SnagIt::siiWindow )
   {
      CString strSelected;
      m_InputBox.GetLBText( m_InputBox.GetCurSel(), strSelected );
      if ( strSelected == "Active Window" )
      {
         m_pImageCapture->InputWindowOptions->SelectionMethod = SnagIt::swsmActive;
      }
      else
      {
         m_pImageCapture->InputWindowOptions->SelectionMethod = SnagIt::swsmInteractive;
      }
   }

   // Set the output to the output currently selected in the output combo box
   m_pImageCapture->Output = (SnagIt::snagImageOutput) m_OutputBox.GetItemData( m_OutputBox.GetCurSel() );

   // Enable preview window if needed
   m_pImageCapture->EnablePreviewWindow = m_Preview.GetCheck();

   // Simplify auto scroll option to be on or off
   if ( m_AutoScroll.GetCheck() )
   {
      m_pImageCapture->AutoScrollOptions->AutoScrollMethod = SnagIt::sasmVertical;
   }
   else
   {
      m_pImageCapture->AutoScrollOptions->AutoScrollMethod = SnagIt::sasmNone;
   }

   // Brightness filter (checkbox)
   if ( m_Brightness.GetCheck() )
   {
      m_pImageCapture->Filters->ColorEffects->Brightness = 50;
   }
   else
   {
      m_pImageCapture->Filters->ColorEffects->Brightness = 0;
   }

   // Enable a border (checkbox)
   if ( m_Border.GetCheck() )
   {
      m_pImageCapture->Filters->Border->EnableBorder = VARIANT_TRUE;
      m_pImageCapture->Filters->Border->TotalWidth = 3;
   }
   else
   {
      m_pImageCapture->Filters->Border->EnableBorder = VARIANT_FALSE;
   }

   // Try to initiate the capture.. catch any errors and display an 
   // appropriate error mesasge.  Here, SnagIt expiration is shown
   // as an example.
   try
   {
      m_pImageCapture->Capture();
   }
   catch ( _com_error captureError )
   {

      if ( m_pImageCapture->LastError == SnagIt::serrSnagItExpired )
      {
         AfxMessageBox( _T("Unable to capture: SnagIt Evaluation is expired."), MB_ICONINFORMATION );
      }
   }
}


void CImageCaptureDlg::LoadInputs()
{
   int i=0;
   
   m_InputBox.AddString("Window");
   m_InputBox.SetItemData(i, SnagIt::siiWindow );
   i++;
   
   m_InputBox.AddString( "Active Window");
   m_InputBox.SetItemData(i, SnagIt::siiWindow );
   i++;
   
   m_InputBox.AddString( "Extended Window");
   m_InputBox.SetItemData(i, SnagIt::siiExtendedWindow );
   i++;
   
   m_InputBox.AddString( "Region");
   m_InputBox.SetItemData(i, SnagIt::siiRegion );
   i++;
   
   m_InputBox.AddString( "Fixed Region");
   m_InputBox.SetItemData(i, SnagIt::siiRegion );
   i++;
   
   m_InputBox.AddString( "Clipboard");
   m_InputBox.SetItemData(i, SnagIt::siiClipboard );
   i++;
   
   m_InputBox.AddString( "Custom Scroll");
   m_InputBox.SetItemData(i, SnagIt::siiCustomScroll );
   i++;
   
   m_InputBox.AddString( "Full Screen");
   m_InputBox.SetItemData(i, SnagIt::siiDesktop );
   i++;
   
   m_InputBox.AddString( "Shapes - Ellipse");
   m_InputBox.SetItemData(i, SnagIt::siiEllipse );
   i++;
   
   m_InputBox.AddString( "Shapes - Freehand");
   m_InputBox.SetItemData(i, SnagIt::siiFreehand );
   i++;
   
   m_InputBox.AddString( "Shapes - Polygon");
   m_InputBox.SetItemData(i, SnagIt::siiPolygon );
   i++;
   
   m_InputBox.AddString( "Shapes - Triangle");
   m_InputBox.SetItemData(i, SnagIt::siiTriangle );
   i++;
   
   m_InputBox.AddString( "Graphic File");
   m_InputBox.SetItemData(i, SnagIt::siiGraphicFile );
   i++;
   
   m_InputBox.AddString( "Menu");
   m_InputBox.SetItemData(i, SnagIt::siiMenu );
   i++;
   
   m_InputBox.AddString( "Object");
   m_InputBox.SetItemData(i, SnagIt::siiObject );
   i++;
   
   m_InputBox.AddString( "Shapes - Rounded Rectangle");
   m_InputBox.SetItemData(i, SnagIt::siiRoundedRect );
   i++;
   
   m_InputBox.AddString( "Program File");
   m_InputBox.SetItemData(i, SnagIt::siiProgramFile );
   i++;
   
   m_InputBox.AddString( "TWAIN source");
   m_InputBox.SetItemData(i, SnagIt::siiTWAIN );
   i++;
   
   m_InputBox.AddString( "Desktop Wallpaper");
   m_InputBox.SetItemData(i, SnagIt::siiWallpaper );
   i++;
   
   m_InputBox.SetCurSel(0); 
}


void CImageCaptureDlg::LoadOutputs()
{
   int i=0;
   
   m_OutputBox.AddString( "Clipboard" );
   m_OutputBox.SetItemData( i, SnagIt::sioClipboard );
   i++;
   
   m_OutputBox.AddString( "File" );
   m_OutputBox.SetItemData( i, SnagIt::sioFile );
   i++;
   
   m_OutputBox.AddString( "FTP" );
   m_OutputBox.SetItemData( i, SnagIt::sioFTP );
   i++;
   
   m_OutputBox.AddString( "Mail" );
   m_OutputBox.SetItemData( i, SnagIt::sioMail );
   i++;
   
   m_OutputBox.AddString( "Printer" );
   m_OutputBox.SetItemData( i, SnagIt::sioPrinter );
   i++;
   
   m_OutputBox.SetCurSel(0);
}

BOOL CImageCaptureDlg::DestroyWindow() 
{
   // Stop receiving events from the ImageCapture object.
   SnagItStateEvents::DispEventUnadvise( m_pImageCapture );

   // Release the SnagIt capture object BEFORE uninitializing COM.
	m_pImageCapture.Release();

   // Uninitialize COM...
   ::CoUninitialize();
	
	return CDialog::DestroyWindow();
}

//
// Message handler for SnagIt state changes.
// 
void __stdcall CImageCaptureDlg::OnSnagItState( SnagIt::snagCaptureState nStateCode )
{
   switch ( nStateCode )
   {
      case SnagIt::scsCaptureSucceeded:
      {
         GetDlgItem( IDC_STATUS )->SetWindowText( "Success" );
         break;
      }
      case SnagIt::scsCaptureFailed:
      {
         GetDlgItem( IDC_STATUS )->SetWindowText( "Failed" );
         break;
      }
   }
}

