//---------------------------------------------------------------------------
// Filename: MFCSimpleDlg.cpp - Implementation file for the CMFCSimpleDlg
//           class for the SnagIt COM server CPP/MFC simple sample.
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#include "stdafx.h"
#include "MFCSimple.h"
#include "MFCSimpleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleDlg dialog

CMFCSimpleDlg::CMFCSimpleDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMFCSimpleDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMFCSimpleDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMFCSimpleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMFCSimpleDlg)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMFCSimpleDlg, CDialog)
	//{{AFX_MSG_MAP(CMFCSimpleDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_WINCAPTURE_BUTTON, OnWincaptureButton)
	ON_BN_CLICKED(IDC_REGCAPTURE_BUTTON, OnRegcaptureButton)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleDlg message handlers

BOOL CMFCSimpleDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// NOTE: Make sure to initialize COM before trying to create the 
   // capture object.
   ::CoInitialize( NULL );

   // Create the image capture object.
   m_pImageCapture.CreateInstance( __uuidof( SnagIt::ImageCapture ) );
   if ( m_pImageCapture == NULL )
   {
      // Handle creation error.
      AfxMessageBox( _T("Fatal error: failed to create capture object."), MB_ICONERROR );
      EndDialog( IDCANCEL );
   }
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMFCSimpleDlg::OnPaint() 
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
HCURSOR CMFCSimpleDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleDlg::OnWincaptureButton() 
//
//   Purpose: Handles the event that occures when the "Window Capture"
//            button is clicked on.  It sets up the capture object and
//            then performs the capture.
//
/////////////////////////////////////////////////////////////////////////////
void CMFCSimpleDlg::OnWincaptureButton() 
{
   // Set the input mode as window (siiWindow).
   m_pImageCapture->Input = SnagIt::siiWindow;

   // Set the output mode as file (sioFile).
   m_pImageCapture->Output = SnagIt::sioFile;
   
   // Get the state of the include cursor check button and set the
   // IncludeCursor property depending on the state of the check box.
   if ( GetDlgItem( IDC_CURSOR_CHECK )->SendMessage( BM_GETCHECK ) == BST_CHECKED )
   {
      m_pImageCapture->IncludeCursor = VARIANT_TRUE;
   }
   else
   {
      m_pImageCapture->IncludeCursor = VARIANT_FALSE;
   }

   // Get the state of the "Preview Window" check button and set the
   // EnablePreviewWindow property depending on the state of the check box.
   if ( GetDlgItem( IDC_PREVIEW_CHECK )->SendMessage( BM_GETCHECK ) == BST_CHECKED )
   {
      m_pImageCapture->EnablePreviewWindow = VARIANT_TRUE;
   }
   else
   {
      m_pImageCapture->EnablePreviewWindow = VARIANT_FALSE;
   }

   // Try to initiate the capture.. catch any errors and display an 
   // appropriate error mesasge.  Here, the SnagIt expiration error is shown
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

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleDlg::OnRegcaptureButton() 
//
//   Purpose: Handles the event that occures when the "Window Capture"
//            button is clicked on.  It sets up the capture object and
//            then performs the capture.
//
/////////////////////////////////////////////////////////////////////////////
void CMFCSimpleDlg::OnRegcaptureButton() 
{
   // Set the input mode as window (siiRegion).
   m_pImageCapture->Input = SnagIt::siiRegion;

   // Set the output mode as file (sioFile).
   m_pImageCapture->Output = SnagIt::sioFile;
   
   // Get the state of the include cursor check button and set the
   // IncludeCursor property depending on the state of the check box.
   if ( GetDlgItem( IDC_CURSOR_CHECK )->SendMessage( BM_GETCHECK ) == BST_CHECKED )
   {
      m_pImageCapture->IncludeCursor = VARIANT_TRUE;
   }
   else
   {
      m_pImageCapture->IncludeCursor = VARIANT_FALSE;
   }

   // Get the state of the "Preview Window" check button and set the
   // EnablePreviewWindow property depending on the state of the check box.
   if ( GetDlgItem( IDC_PREVIEW_CHECK )->SendMessage( BM_GETCHECK ) == BST_CHECKED )
   {
      m_pImageCapture->EnablePreviewWindow = VARIANT_TRUE;
   }
   else
   {
      m_pImageCapture->EnablePreviewWindow = VARIANT_FALSE;
   }

   // Try to initiate the capture.. catch any errors and display an 
   // appropriate error mesasge.  Here, the SnagIt expiration error is shown
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

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleDlg::DestroyWindow() 
//
//   Purpose: When the dialog is closed, cleanup is done here.
//
/////////////////////////////////////////////////////////////////////////////
BOOL CMFCSimpleDlg::DestroyWindow()
{
   // Release the SnagIt capture object BEFORE uninitializing COM.
   m_pImageCapture.Release();

   // Make sure to uninitialize COM.
   ::CoUninitialize();

   // Call parent's DestroyWindow function for additional cleanup.
   return CDialog::DestroyWindow();
}
