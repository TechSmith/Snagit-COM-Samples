//---------------------------------------------------------------------------
// Filename: ImageCapture.cpp - Class definition file for the 
//           CImageCaptureApp class for the SnagIt COM server MFC/CPP 
//           ImageCapture sample.
//  
// Support e-mail: support.snagitcom@techsmith.com  
//  
// Copyright © 2003 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#include "stdafx.h"
#include "ImageCapture.h"
#include "ImageCaptureDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CImageCaptureApp

BEGIN_MESSAGE_MAP(CImageCaptureApp, CWinApp)
	//{{AFX_MSG_MAP(CImageCaptureApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CImageCaptureApp construction

CImageCaptureApp::CImageCaptureApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CImageCaptureApp object

CImageCaptureApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CImageCaptureApp initialization

BOOL CImageCaptureApp::InitInstance()
{
	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.

#ifdef _AFXDLL
	Enable3dControls();			// Call this when using MFC in a shared DLL
#else
	Enable3dControlsStatic();	// Call this when linking to MFC statically
#endif

	CImageCaptureDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}
