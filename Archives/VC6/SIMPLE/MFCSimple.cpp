//---------------------------------------------------------------------------
// Filename: MFCSimple.cpp - Implementation file for the CMFCSimpleApp
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
// CMFCSimpleApp

BEGIN_MESSAGE_MAP(CMFCSimpleApp, CWinApp)
	//{{AFX_MSG_MAP(CMFCSimpleApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleApp construction

CMFCSimpleApp::CMFCSimpleApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CMFCSimpleApp object

CMFCSimpleApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleApp initialization

BOOL CMFCSimpleApp::InitInstance()
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

	CMFCSimpleDlg dlg;
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
