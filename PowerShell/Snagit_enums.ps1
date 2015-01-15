#---------------------------------------------------------------------------
# This file defines all the enums needed for using Snagit COM server
# Note: This code is setup for using Snagit 8.1.0 or later.
# 
# Support e-mail: support@techsmith.zendesk.com
# This software is provided under the MIT License (http://opensource.org/licenses/MIT)
# Copyright (c) 2015 TechSmith Corporation
#
# Permission is hereby granted, free of charge, to any person obtaining a copy of this
# software and associated documentation files (the "Software"), to deal in the Software
# without restriction, including without limitation the rights to use, copy, modify, merge,
# publish, distribute, sub-license, and/or sell copies of the Software, and to permit persons
# to whom the Software is furnished to do so, subject to the following conditions:
#    The above copyright notice and this permission notice shall be included in all copies
#    or substantial portions of the Software.
#    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
#    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
#    PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
#    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
#    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#---------------------------------------------------------------------------

Add-Type -TypeDefinition @"
public enum snagImageInput
{
   siiDesktop	= 0,
   siiWindow	= 1,
   siiRegion	= 4,
   siiGraphicFile	= 6,
   siiClipboard	= 7,
   siiMenu	= 9,
   siiObject	= 10,
   siiFreehand	= 12,
   siiCustomScroll	= 18,
   siiTWAIN	= 19,
   siiExtendedWindow	= 23,
   siiCapture	= 25
};
"@

Add-Type -TypeDefinition @"
public enum snagVideoInput
{
   sviWindow	= 1,
   sviRegion	= 4,
   sviCapture	= 25
};
"@

Add-Type -TypeDefinition @"
public enum snagImageOutput
{
   sioNone	= 0,
   sioPrinter	= 1,
   sioFile	= 2,
   sioClipboard	= 4,
   sioMail	= 8,
   sioFTP	= 32
};
"@

Add-Type -TypeDefinition @"
public enum snagVideoOutput
{
   svoNone	= 0,
   svoFile	= 2,
   svoFTP	= 32
};
"@

Add-Type -TypeDefinition @"
public enum snagNotificationType
{
   sntNone	= 0,
   sntInformation	= 1,
   sntErrors	= 2,
   sntAll	= 3
};
"@

Add-Type -TypeDefinition @"
public enum snagError
{
   serrUnknown	= -1,
   serrNone	= 0,
   serrSnagItExpired	= 1,
   serrInvalidInput	= 2,
   serrInvalidOutput	= 3,
   serrEngineBusy	= 4,
   serrInvalidScrollDelay	= 5,
   serrInvalidDelay	= 6,
   serrInvalidColorEffectValue	= 7,
   serrInvalidFileProgressiveValue	= 8,
   serrInvalidFileQualityValue	= 9,
   serrInvalidFileDirectory	= 10,
   serrInvalidColorConversionValue	= 11,
   serrInvalidImageResolution	= 12
};
"@

Add-Type -TypeDefinition @"
public enum snagCaptureState
{
   scsIdle	= 0,
   scsCaptureSucceeded	= 10,
   scsCaptureFailed	= 11,
   scsBusy	= 12
};
"@

Add-Type -TypeDefinition @"
public enum snagAutoScrollMethod
{
   sasmNone	= 0,
   sasmVertical	= 1,
   sasmHorizontal	= 2,
   sasmBoth	= 3
};
"@

Add-Type -TypeDefinition @"
public enum snagAutoScrollStartingPosition
{
   sasspCurrent	= 0,
   sasspTop	= 1,
   sasspLeft	= 2,
   sasspTopLeft	= 3
};
"@

Add-Type -TypeDefinition @"
public enum snagImageFileType
{
   siftUnknown	= -1,
   siftBMP	= 0,
   siftTIFF	= 2,
   siftJPEG	= 3,
   siftGIF	= 4,
   siftPNG	= 5,
   siftTGA	= 6,
   siftRAS	= 7,
   siftWMF	= 9,
   siftEPS	= 11,
   siftOS2	= 17,
   siftWFX	= 18,
   siftEMF	= 19,
   siftWPG	= 20,
   siftPSD	= 21,
   siftICO	= 22,
   siftCUR	= 23,
   siftPDF	= 24,
   siftSNAG	= 25,
   siftSWF	= 26,
   siftMHT	= 27
};
"@

Add-Type -TypeDefinition @"
public enum snagVideoFileType
{
   svftUnknown	= -1,
   svftMP4	= 0
};
"@

Add-Type -TypeDefinition @"
public enum snagImageFileSubType
{
   sifstUnknown	= -1,
   sifstBMP_Uncompressed	= 0x1,
   sifstBMP_RLE	= 0x2,
   sifstGIF_NonInterlaced	= 0x4,
   sifstGIF_Interlaced	= 0x8,
   sifstJFIF_444	= 0x10,
   sifstJFIF_422	= 0x20,
   sifstJFIF_411	= 0x40,
   sifstJFIF_444_Progressive	= 0x80,
   sifstJFIF_422_Progressive	= 0x100,
   sifstJFIF_411_Progressive	= 0x200,
   sifstJFIF_Gray	= 0x400,
   sifstJFIF_Gray_Progressive	= 0x800,
   sifstCCITT	= 0x1000,
   sifstCCITT_Group3_1Dimension	= 0x2000,
   sifstCCITT_Group3_2Dimension	= 0x4000,
   sifstCCITT_Group4	= 0x8000,
   sifstTIF_Uncompressed	= 0x10000,
   sifstTIF_PackBits	= 0x20000,
   sifstTIF_LZW	= 0x40000,
   sifstJTIF_Gray	= 0x80000,
   sifstTIF_CMYK	= 0x100000,
   sifstTIF_YCC	= 0x200000,
   sifstTIF_PACK_CMYK	= 0x400000,
   sifstTIF_PACK_YCC	= 0x800000,
   sifstTIF_LZW_CMYK	= 0x1000000,
   sifstTIF_LZW_YCC	= 0x2000000,
   sifstJTIF_444	= 0x4000000,
   sifstJTIF_422	= 0x8000000,
   sifstJTIF_411	= 0x10000000
};
"@

Add-Type -TypeDefinition @"
public enum snagImageColorDepth
{
   sicdAuto	= 0,
   sicd1Bit	= 1,
   sicd2Bit	= 2,
   sicd3Bit	= 3,
   sicd4Bit	= 4,
   sicd5Bit	= 5,
   sicd6Bit	= 6,
   sicd7Bit	= 7,
   sicd8Bit	= 8,
   sicd16Bit	= 16,
   sicd24Bit	= 24,
   sicd32Bit	= 32
};
"@

Add-Type -TypeDefinition @"
public enum snagColorConversionMethod
{
   sccmNone	= 0,
   sccmMonochrome	= 1,
   sccmHalftone	= 2,
   sccmGrayscale	= 3
};
"@

Add-Type -TypeDefinition @"
public enum snagOuputFileNamingMethod
{
   sofnmPrompt	= 0,
   sofnmFixed	= 1,
   sofnmAuto	= 2
};
"@

Add-Type -TypeDefinition @"
public enum snagPlacement
{
   spUnknown	= -1,
   spLeftTop	= 0,
   spRightTop	= 1,
   spLeftBottom	= 2,
   spRightBottom	= 3,
   spCenterMiddle	= 4,
   spCenterTop	= 5,
   spLeftMiddle	= 6,
   spRightMiddle	= 7,
   spCenterBottom	= 8,
   spOutsideTop	= 9,
   spOutsideLeft	= 10,
   spOutsideBottom	= 11,
   spOutsideRight	= 12
};
"@

Add-Type -TypeDefinition @"
public enum snagPrintScale
{
   spsSinglePage	= 0,
   spsPercentScale	= 1,
   spsProportionalToScreen	= 2,
   spsFixedSize	= 3,
   spsSinglePageMaximize	= 4,
   spsAutoScale	= 5
};
"@

Add-Type -TypeDefinition @"
public enum snagImageScaleMethod
{
   sismCustom	= 0,
   sismPercentage	= 1,
   sismNone	= 2
};
"@

Add-Type -TypeDefinition @"
public enum snagImageScaleBy
{
   sisbFactor	= 0,
   sisbWidthAndHeight	= 1,
   sisbWidth	= 2,
   sisbHeight	= 3
};
"@

Add-Type -TypeDefinition @"
public enum snagTrimMethod
{
   stmNone	= 0,
   stmManual	= 1,
   stmAuto	= 2
};
"@

Add-Type -TypeDefinition @"
public enum snagColorSubMethod
{
   scsmNone	= 0,
   scsmInvert	= 1,
   scsmCustom	= 2
};
"@

Add-Type -TypeDefinition @"
public enum snagTextLayout
{
   stlSpaceFormatted	= 0,
   stlColumnDelmited	= 1
};
"@

Add-Type -TypeDefinition @"
public enum snagCompassDirection
{
   scdNorth	= 0,
   scdNorthEast	= 1,
   scdEast	= 2,
   scdSouthEast	= 3,
   scdSouth	= 4,
   scdSouthWest	= 5,
   scdWest	= 6,
   scdNorthWest	= 7
};
"@

Add-Type -TypeDefinition @"
public enum snagCaptionTextStyle
{
   sctsNormal	= 0,
   sctsDropShadow	= 1,
   sctsOutlinedShadow	= 2
};
"@

Add-Type -TypeDefinition @"
public enum snagTimeDateOrder
{
   stdoTimeThenDate	= 0,
   stdoTimeOnly	= 1,
   stdoDateThenTime	= 2,
   stdoDateOnly	= 3
};
"@

Add-Type -TypeDefinition @"
public enum snagWindowSelectionMethod
{
   swsmInteractive	= 0,
   swsmActive	= 1,
   swsmHandle	= 2,
   swsmPoint	= 3
};
"@

Add-Type -TypeDefinition @"
public enum snagRegionSelectionMethod
{
   srsmInteractive	= 0,
   srsmFixed	= 1
};
"@

Add-Type -TypeDefinition @"
public enum snagHotspotType
{
   shtUnknown	= -1,
   shtNone	= 0,
   shtLinksOnly	= 1,
   shtLinksAndControls	= 2
};
"@

Add-Type -TypeDefinition @"
public enum snagRecorderError
{
   srErrNone	= 0,
   srErrInitRecorderFailed	= 1,
   srErrInitEncoderFailed	= 2,
   srErrRecorderThrownCode	= 3,
   srErrEncoderThrownCode	= 4,
   srErrStarting	= 5,
   srErrPausing	= 6,
   srErrResuming	= 7,
   srErrStopping	= 8,
   srErrDiskSpaceLow	= 9,
   srErrInvalidRecordingRect	= 10,
   srErrSystemAudioNotAvailable	= 11,
   srErrUnknown	= 99
};
"@

Add-Type -TypeDefinition @"
public enum snagRecorderState
{
   srStateInitialized	= -1,
   srStateCounting	= 0,
   srStateRecording	= 1,
   srStatePausing	= 2,
   srStateStopping	= 3,
   srReselecting	= 4
};
"@

Add-Type -TypeDefinition @"
public enum snagDocumentType
{
   sdtImage	= 0,
   sdtVideo	= 1
};
"@
