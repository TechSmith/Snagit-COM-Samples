//---------------------------------------------------------------------------
// Filename: ImageCapture.h - Class declaration file for the 
//           CImageCaptureApp class for the SnagIt COM server MFC/CPP 
//           ImageCapture sample.
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#if !defined(AFX_IMAGECAPTURE_H__57BBF908_B18E_4CD1_A477_9D217A38DB8E__INCLUDED_)
#define AFX_IMAGECAPTURE_H__57BBF908_B18E_4CD1_A477_9D217A38DB8E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CImageCaptureApp:
// See ImageCapture.cpp for the implementation of this class
//

class CImageCaptureApp : public CWinApp
{
public:
	CImageCaptureApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CImageCaptureApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CImageCaptureApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_IMAGECAPTURE_H__57BBF908_B18E_4CD1_A477_9D217A38DB8E__INCLUDED_)
