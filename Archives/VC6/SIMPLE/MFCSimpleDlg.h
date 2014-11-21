//---------------------------------------------------------------------------
// Filename: MFCSimpleDlg.h - Class declaraion file for CMFCSimpleDlg for the
//           SnagIt COM server CPP/MFC simple sample..
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#if !defined(AFX_MFCSIMPLEDLG_H__2B7BBBA6_DD04_498F_BB6F_0CFD93212914__INCLUDED_)
#define AFX_MFCSIMPLEDLG_H__2B7BBBA6_DD04_498F_BB6F_0CFD93212914__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CMFCSimpleDlg dialog

class CMFCSimpleDlg : public CDialog
{
// Construction
public:
	CMFCSimpleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMFCSimpleDlg)
	enum { IDD = IDD_MFCSIMPLE_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMFCSimpleDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
   virtual BOOL DestroyWindow();
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

   SnagIt::IImageCapturePtr m_pImageCapture; // The SnagIt image capture object.

	// Generated message map functions
	//{{AFX_MSG(CMFCSimpleDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnWincaptureButton();
	afx_msg void OnRegcaptureButton();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MFCSIMPLEDLG_H__2B7BBBA6_DD04_498F_BB6F_0CFD93212914__INCLUDED_)
