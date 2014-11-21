***********************************************
* Snagit's IImageCapture2 interface
*
* How to import the Snagit COM interfaces:
* 0) Snagit must be installed on the system first.
* 1) Open up a new blank program (.prg) window
* 2) Go to the menubar and select "Tools" -> "Object Browser"
* 3) In the Object Browser window click on the "Open Type Library"
*    button at the far left of the window toolbar.
* 4) Click on the "COM Libraries" tab in the Open dialog
* 5) Locate and check "SNAGIT 1.0 Type Library" and click OK
* 6) In the Object Browser expand the SNAGITLib node in the tree control
* 7) Expand the Interfaces node
* 8) Click and drag the interface that you want to use onto
*    your blank .prg window.
*    - For image captures use the IImageCapture2 interface
*    - For video recording use the IVideoCapture interface
* 9) After dragging/dropping the interface Visual Fox Pro 9 should
*    automatically create the interface stubs as shown below.
*    Exception: I renamed "myClass" to be "ImageCapture" for clarity.
***********************************************
x=NEWOBJECT("ImageCapture")

DEFINE CLASS ImageCapture AS session OLEPUBLIC

	IMPLEMENTS IImageCapture2 IN "c:\program files (x86)\techsmith\snagit 12\snagit32.exe"

	PROCEDURE IImageCapture2_Capture() AS VOID;
 				HELPSTRING "Initiate Capture"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_Input() AS VARIANT;
 				HELPSTRING "property Input"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_Input(eValue AS VARIANT @);
 				HELPSTRING "property Input"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_Output() AS VARIANT;
 				HELPSTRING "property Output"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_Output(eValue AS VARIANT @);
 				HELPSTRING "property Output"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_IncludeCursor() AS LOGICAL;
 				HELPSTRING "Include cursor in capture"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_IncludeCursor(eValue AS LOGICAL @);
 				HELPSTRING "Include cursor in capture"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_CaptureMultipleAreas() AS LOGICAL;
 				HELPSTRING "Capture multiple areas"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_CaptureMultipleAreas(eValue AS LOGICAL @);
 				HELPSTRING "Capture multiple areas"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_BackgroundColor() AS Number;
 				HELPSTRING "property BackgroundColor"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_BackgroundColor(eValue AS Number @);
 				HELPSTRING "property BackgroundColor"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_NotificationType() AS VARIANT;
 				HELPSTRING "What notifications should be displayed"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_NotificationType(eValue AS VARIANT @);
 				HELPSTRING "What notifications should be displayed"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_EnablePreviewWindow() AS LOGICAL;
 				HELPSTRING "Preview window shows after capture"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_EnablePreviewWindow(eValue AS LOGICAL @);
 				HELPSTRING "Preview window shows after capture"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_ForegroundPreview() AS LOGICAL;
 				HELPSTRING "Force preview window to front"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_ForegroundPreview(eValue AS LOGICAL @);
 				HELPSTRING "Force preview window to front"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_UseMagnifierWindow() AS LOGICAL;
 				HELPSTRING "Display magnifier window"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_UseMagnifierWindow(eValue AS LOGICAL @);
 				HELPSTRING "Display magnifier window"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_ClipboardOptions() AS VARIANT;
 				HELPSTRING "property ClipboardOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_AutoScrollOptions() AS VARIANT;
 				HELPSTRING "property AutoScrollOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_InputTWAINOptions() AS VARIANT;
 				HELPSTRING "property InputTWAINOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_InputMenuOptions() AS VARIANT;
 				HELPSTRING "property InputMenuOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_InputExtendedWindowOptions() AS VARIANT;
 				HELPSTRING "property InputExtendedWindowOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_InputRegionOptions() AS VARIANT;
 				HELPSTRING "property InputRegionOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_OutputPrinterOptions() AS VARIANT;
 				HELPSTRING "property OutputPrinterOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_OutputPrinterPageLayoutOptions() AS VARIANT;
 				HELPSTRING "property OutputPrinterPageLayoutOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_OutputFTPOptions() AS VARIANT;
 				HELPSTRING "property OutputFTPOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_OutputMailOptions() AS VARIANT;
 				HELPSTRING "property OutputMailOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_OutputImageFile() AS VARIANT;
 				HELPSTRING "property OutputImageFile"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_Filters() AS VARIANT;
 				HELPSTRING "property Filters"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_DelayOptions() AS VARIANT;
 				HELPSTRING "property DelayOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_LastError() AS VARIANT;
 				HELPSTRING "property LastError"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_IsCaptureDone() AS LOGICAL;
 				HELPSTRING "property IsCaptureDone"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_LastFileWritten() AS STRING;
 				HELPSTRING "property LastFileWritten"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_CaptureState() AS VARIANT;
 				HELPSTRING "property CaptureState"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_LastCaptureSucceeded() AS LOGICAL;
 				HELPSTRING "property LastCaptureSucceeded"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_InputWindowOptions() AS VARIANT;
 				HELPSTRING "property IWindowOptions"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_HotspotType() AS VARIANT;
 				HELPSTRING "What kinds of hotspots should be captured"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_put_HotspotType(eValue AS VARIANT @);
 				HELPSTRING "What kinds of hotspots should be captured"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_OutputImageFile2() AS VARIANT;
 				HELPSTRING "property OutputImageFile2"
	* add user code here
	ENDPROC

	PROCEDURE IImageCapture2_get_CaptureResults() AS VARIANT;
 				HELPSTRING "property CaptureResults"
	* add user code here
	ENDPROC

ENDDEFINE