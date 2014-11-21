//---------------------------------------------------------------------------
// Filename: MFCSimple.h - Class declaraion file for CMFCSimpleApp for the
//           SnagIt COM server CPP/MFC simple sample..
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#if !defined(AFX_MFCSIMPLE_H__FEEC740D_9D25_4ED4_A18D_A7C985FE6A28__INCLUDED_)
#define AFX_MFCSIMPLE_H__FEEC740D_9D25_4ED4_A18D_A7C985FE6A28__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleApp:
// See MFCSimple.cpp for the implementation of this class
//

class CMFCSimpleApp : public CWinApp
{
public:
	CMFCSimpleApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMFCSimpleApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMFCSimpleApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MFCSIMPLE_H__FEEC740D_9D25_4ED4_A18D_A7C985FE6A28__INCLUDED_)
