//---------------------------------------------------------------------------
// Filename: stdafx.h - For precompiled header support.
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#if !defined(AFX_STDAFX_H__2AACE5B1_20F9_4B3C_A0A2_954007458B87__INCLUDED_)
#define AFX_STDAFX_H__2AACE5B1_20F9_4B3C_A0A2_954007458B87__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define VC_EXTRALEAN		// Exclude rarely-used stuff from Windows headers

#include <afxwin.h>         // MFC core and standard components
#include <afxext.h>         // MFC extensions
#include <afxdtctl.h>		// MFC support for Internet Explorer 4 Common Controls
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>			// MFC support for Windows Common Controls
#endif // _AFX_NO_AFXCMN_SUPPORT

#import "..\lib\snagit.tlb" rename_namespace("SnagIt")

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__2AACE5B1_20F9_4B3C_A0A2_954007458B87__INCLUDED_)
