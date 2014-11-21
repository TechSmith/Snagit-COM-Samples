***********************************************
* Visual FoxPro 9 sample program that uses the
* Snagit COM interface (imported into snagitcom.prg)
* to peform a simple image capture that is previewed
* in the Snagit editor. This sample requires
* Snagit 12.x since the interfaces were imported
* from that version.
*
* Support e-mail: support@techsmith.zendesk.com
* This software is provided under the MIT License (http://opensource.org/licenses/MIT)
* Copyright (c) 2014 TechSmith Corporation
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this
* software and associated documentation files (the "Software"), to deal in the Software
* without restriction, including without limitation the rights to use, copy, modify, merge,
* publish, distribute, sub-license, and/or sell copies of the Software, and to permit persons
* to whom the Software is furnished to do so, subject to the following conditions:
*    The above copyright notice and this permission notice shall be included in all copies
*    or substantial portions of the Software.
*    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
*    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
*    PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
*    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
*    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
***********************************************
CLEAR
CLEAR ALL

***********************************************
* Defines
***********************************************
* Image capture input types
#DEFINE siiDesktop	       0	
#DEFINE siiWindow	       1	
#DEFINE siiRegion	       4	
#DEFINE siiGraphicFile	   6	
#DEFINE siiClipboard	   7	
#DEFINE siiMenu	           9	
#DEFINE siiObject	      10	
#DEFINE siiFreehand	      12	
#DEFINE siiCustomScroll	  18	
#DEFINE siiTWAIN	      19	
#DEFINE siiExtendedWindow 23	
#DEFINE siiCapture	      25	

* Image capture output types
#DEFINE sioNone	           0	
#DEFINE sioPrinter	       1	
#DEFINE sioFile	           2	
#DEFINE sioClipboard	   4	
#DEFINE sioMail	           8	
#DEFINE sioFTP	          32	

* Image autoscroll method
#DEFINE sasmNone	    0	
#DEFINE sasmVertical	1	
#DEFINE sasmHorizontal	2	
#DEFINE sasmBoth	    3	

* Image autoscroll start position
#DEFINE sasspCurrent	0	
#DEFINE sasspTop	    1	
#DEFINE sasspLeft	    2	
#DEFINE sasspTopLeft	3	

* Image file naming method
#DEFINE sofnmPrompt	0	
#DEFINE sofnmFixed	1	
#DEFINE sofnmAuto	2	

* Image file types
#DEFINE siftUnknown	-1	
#DEFINE siftBMP	  0	
#DEFINE siftTIFF  2	
#DEFINE siftJPEG  3	
#DEFINE siftGIF	  4	
#DEFINE siftPNG	  5	
#DEFINE siftTGA	  6	
#DEFINE siftRAS	  7	
#DEFINE siftWMF	  9	
#DEFINE siftEPS	 11	
#DEFINE siftOS2	 17	
#DEFINE siftWFX	 18	
#DEFINE siftEMF	 19	
#DEFINE siftWPG	 20	
#DEFINE siftPSD	 21	
#DEFINE siftICO	 22	
#DEFINE siftCUR	 23	
#DEFINE siftPDF	 24	
#DEFINE siftSNAG 25	
#DEFINE siftSWF	 26	
#DEFINE siftMHT	 27	

* Image subfile types
#DEFINE sifstUnknown	                -1	
#DEFINE sifstBMP_Uncompressed	         1	
#DEFINE sifstBMP_RLE	                 2	
#DEFINE sifstGIF_NonInterlaced	         4	
#DEFINE sifstGIF_Interlaced	             8	
#DEFINE sifstJFIF_444	                16	
#DEFINE sifstJFIF_422	                32	
#DEFINE sifstJFIF_411	                64	
#DEFINE sifstJFIF_444_Progressive	   128	
#DEFINE sifstJFIF_422_Progressive	   256	
#DEFINE sifstJFIF_411_Progressive	   512	
#DEFINE sifstJFIF_Gray	              1024	
#DEFINE sifstJFIF_Gray_Progressive	  2048	
#DEFINE sifstCCITT	                  4096	
#DEFINE sifstCCITT_Group3_1Dimension  8192	
#DEFINE sifstCCITT_Group3_2Dimension 16384	
#DEFINE sifstCCITT_Group4	         32768	
#DEFINE sifstTIF_Uncompressed	     65536	
#DEFINE sifstTIF_PackBits	        131072	
#DEFINE sifstTIF_LZW	            262144	
#DEFINE sifstJTIF_Gray	            524288	
#DEFINE sifstTIF_CMYK	           1048576	
#DEFINE sifstTIF_YCC	           2097152	
#DEFINE sifstTIF_PACK_CMYK	       4194304	
#DEFINE sifstTIF_PACK_YCC	       8388608	
#DEFINE sifstTIF_LZW_CMYK	      16777216	
#DEFINE sifstTIF_LZW_YCC	      33554432	
#DEFINE sifstJTIF_444	          67108864	
#DEFINE sifstJTIF_422	         134217728	
#DEFINE sifstJTIF_411	         268435456	

* Image color depth
#DEFINE sicdAuto	0	
#DEFINE sicd1Bit	1	
#DEFINE sicd2Bit	2	
#DEFINE sicd3Bit	3	
#DEFINE sicd4Bit	4	
#DEFINE sicd5Bit	5	
#DEFINE sicd6Bit	6	
#DEFINE sicd7Bit	7	
#DEFINE sicd8Bit	8	
#DEFINE sicd16Bit	16	
#DEFINE sicd24Bit	24	
#DEFINE sicd32Bit	32	


***********************************************
* Create the Snagit ImageCapture object.
***********************************************
LOCAL loSnagit, loSnagitImgInterface 
IF TYPE('loSnagIt') == "O"
	QUIT
ENDIF
 
loSnagit = CreateObject("SnagIt.ImageCapture")
loSnagitImgInterface = NEWOBJECT("ImageCapture", "snagitcom.prg") 

***********************************************
* I don't know how events work in VFP so commenting 
* out. I just wanted to point out that you can handle
* these two _ICaptureEvents: (See Snagit COM Doc for more info)
*   OnError
*   OnStateChange
***********************************************
*EVENTHANDLER(loSnagIt,loSnagItImgInterface) 
*ENDIF


***********************************************
* Setup the input options for the capture.
***********************************************
loSnagit.Input = siiCapture
loSnagit.Output = sioFile

* If not using siiCapture, and you want to auto-scroll
* a window, then you need to set the scrolling method
*loSnagit.AutoScrollOptions.AutoScrollMethod = sasmVertical
*loSnagit.AutoScrollOptions.StartingPosition = sasspTopLeft


***********************************************
* Setup the output options for the capture.
***********************************************
loSnagit.OutputImageFile.FileNamingMethod = sofnmPrompt
loSnagit.OutputImageFile.FileType = siftPNG  &&Default to the PNG file type

***********************************************
* Example of enabling previewing a capture in
* the Snagit Editor.
***********************************************
loSnagit.EnablePreviewWindow = -1

***********************************************
*Fire off the capture...
***********************************************
TRY
	loSnagit.Capture()
CATCH
	WAIT WINDOW "Oops! There was a problem."
	CANCEL
ENDTRY

***********************************************
*Wait for the capture to finish (handling the event would be better)
***********************************************
DO WHILE !(loSnagit.IsCaptureDone)
ENDDO

***********************************************
*Check for errors (handling the event would be better)
***********************************************
IF loSnagit.LastError <> 0 THEN 
	WAIT WINDOW 'Capture error: ' + loSnagit.LastError
ELSE
	WAIT WINDOW "Capture completed."
ENDIF

