//---------------------------------------------------------------------------
// Filename: ImageCaptureDlg.h - Class declaration file for the 
//           CImageCaptureDlg class for the SnagIt COM server MFC/CPP 
//           ImageCapture sample.
//  
// Support e-mail: support@techsmith.zendesk.com  
//  
// Copyright © 2003-2014 TechSmith Corporation. All rights reserved.  
//---------------------------------------------------------------------------

#if !defined(AFX_IMAGECAPTUREDLG_H__4C9E8BAE_32D3_4DE2_9EA9_A7618C3D1420__INCLUDED_)
#define AFX_IMAGECAPTUREDLG_H__4C9E8BAE_32D3_4DE2_9EA9_A7618C3D1420__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CImageCaptureDlg dialog

extern _ATL_FUNC_INFO SnagItStateInfo;

class CImageCaptureDlg : public CDialog,
   public IDispEventSimpleImpl<1, CImageCaptureDlg, &__uuidof(SnagIt::_ICaptureEvents)>
{
// Construction
public:
	CImageCaptureDlg(CWnd* pParent = NULL);	// standard constructor

   typedef IDispEventSimpleImpl</*nID =*/ 1, CImageCaptureDlg, &__uuidof( SnagIt::_ICaptureEvents )> SnagItStateEvents;

   BEGIN_SINK_MAP(CImageCaptureDlg)
      SINK_ENTRY_INFO( 1, __uuidof( SnagIt::_ICaptureEvents ),/*dispid*/ 0x02, OnSnagItState, &SnagItStateInfo )
   END_SINK_MAP()

   void __stdcall OnSnagItState( SnagIt::snagCaptureState nStateCode );

// Dialog Data
	//{{AFX_DATA(CImageCaptureDlg)
	enum { IDD = IDD_IMAGECAPTURE_DIALOG };
	CButton	m_Preview;
	CButton	m_Border;
	CButton	m_Brightness;
	CButton	m_AutoScroll;
	CButton m_KeepLinks;
	CComboBox	m_OutputBox;
	CComboBox	m_InputBox;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CImageCaptureDlg)
	public:
	virtual BOOL DestroyWindow();
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CImageCaptureDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnCapture();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()


private:
   // The image capture object. The SnagIt library was imported
   // in stdafx.h
   SnagIt::IImageCapture2Ptr m_pImageCapture;

   void LoadInputs(void);
   void LoadOutputs(void);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_IMAGECAPTUREDLG_H__4C9E8BAE_32D3_4DE2_9EA9_A7618C3D1420__INCLUDED_)
