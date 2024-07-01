/**
 *  mo-common.js
 *
 *  This file is used by MasterObjects-enhanded web pages and scripts.
 *  (c) 2004-2005 MasterObjects (http://www.masterobjects.com) with the exception
 *  of the following portions:
 *
 *  This file incorporates freely available third party browser detection code.
 *  See below for copyright and license information covering that code.
 *  The original browser detection script was modified in the following ways:
 *  1. Variables were prefixed by "mo" and capitalized to avoid naming clashes with customer
 *     scripts.
 *  2. The browser checks were encapsulated into a function to allow local variables.
 *  3. Local variables were prefixed by "var" to limit their scope to the function.
 *  4. moBrow is overridden to contain the browser name even for Mozilla browsers.
 *  5. Netscape 4 document.layers check was changed into typeof document.layers == "object".
 *  6. var patterns variable declaration was added.
 *	If you would like to reuse this script, we strongly recommend that you download and use the
 *  original from the URI mentioned below. Also, if you encounter issues with the JavaScript
 *  below, we encourage you to contact MasterObjects or to send your improved code to the
 *  authors listed below.
 *
 *  Cookie handling code is based on examples from the "JavaScript and DHTML Cookbook"
 *  Published by O'Reilly & Associates, ISBN 0-596-00467-2 and Copyright (c) 2003 Danny Goodman
 *
 *
 *  To use this file, include the following line in the <head> section of the HTML page:
 *		<script type="text/javascript" src="path/mo-common.js"></script>
 *
 *	Note: MasterObjects JavaScript files are ISO Latin 1 encoded using DOS line breaks, allowing
 * 	easy editing and viewing on the Windows platform. Lines were wrapped after 96 characters.
 *
 */


// Only functions and variables starting with "mo" can safely be used outside of this script,
// as well as constants starting with MO_. Variables gMo... should not be used by third parties.

var gMoCommonBuild = 17;  // June 24, 2005
var gMoDisabled = false;  // if true, no more global user warnings are displayed
var MO_NBSP = String.fromCharCode(160);
var MO_NDASH = String.fromCharCode(8211);

/**
 *  Script Name: Full Featured Javascript Browser/OS detection
 *  Authors: Harald Hope, Tapio Markula, Websites: http://techpatterns.com/
 *  http://www.nic.fi/~tapio1/Teaching/index1.php3
 *  Script Source URI: http://techpatterns.com/downloads/javascript_browser_detection.php
 *  Version 4.2.1
 *  Copyright (C) 03 March 2005
 *  
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *  
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *  
 *  Lesser GPL license text:
 *  http://www.gnu.org/licenses/lgpl.txt
 *  
 *  Coding conventions:
 *  http://cvs.sourceforge.net/viewcvs.py/phpbb/phpBB2/docs/codingstandards.htm?rev=1.3
 *
 */

//initialization, browser, os detection
var	moD, moDom, moNu='', moBrow='', moIe, moIe4, moIe5, moIe5x, moIe6, moIe7, moNs4, moMoz,
	moMozRvSub, moReleaseDate='', moMozBrow, moMozBrowNu='', moMozBrowNuSub='', moRvFull='',
	moMac, moWin, moOld, moLin, moIe5Mac, moIe5xWin, moKonq, moSaf, moOp, moOp4, moOp5, moOp6,
	moOp7;

function moBrowserDetectionScript() {
	moD=document;
	// local var declaration added by MasterObjects
	var n=navigator;
	var nav=n.appVersion;
	var nan=n.appName;
	var nua=n.userAgent;
	var str_pos;
	var moz_types;
	var rv_pos;
	var rv_slice;
	var sub_nu_slice;
	
	moOld=(nav.substring(0,1)<4);
	moMac=(nav.indexOf('Mac')!=-1);
	moWin=( ( (nav.indexOf('Win')!=-1) || (nav.indexOf('NT')!=-1) ) && !moMac)?true:false;
	moLin=(nua.indexOf('Linux')!=-1);
	// begin primary moDom/moNs4 test
	// this is the most important test on the page
	
	// *** CHANGED BY MASTEROBJECTS ***
	if ( typeof document.layers == "object" )
	{
		moDom = false; 
		moNs4 = true;// only netscape 4 supports document layers
	}
	else { 
		moDom = ( moD.getElementById ) ? moD.getElementById : false;	
	}
	// end main moDom/moNs4 test
	
	moOp=(nua.indexOf('Opera')!=-1);
	moSaf=(nua.indexOf('Safari')!=-1);
	moKonq=(!moSaf && (nua.indexOf('Konqueror')!=-1) ) ? true : false;
	moMoz=( (!moSaf && !moKonq ) && ( nua.indexOf('Gecko')!=-1 ) ) ? true : false;
	moIe=((nua.indexOf('MSIE')!=-1)&&!moOp);
	if (moOp)
	{
		str_pos=nua.indexOf('Opera');
		moNu=nua.substr((str_pos+6),4);
		moBrow = 'Opera';
	}
	else if (moSaf)
	{
		str_pos=nua.indexOf('Safari');
		moNu=nua.substr((str_pos+7),5);
		moBrow = 'Safari';
	}
	else if (moKonq)
	{
		str_pos=nua.indexOf('Konqueror');
		moNu=nua.substr((str_pos+10),3);
		moBrow = 'Konqueror';
	}
	// this part is complicated a bit, don't mess with it unless you understand regular expressions
	// note, for most comparisons that are practical, compare the 3 digit rv nubmer, that is the output
	// placed into 'moNu'.
	else if (moMoz)
	{
		// regular expression pattern that will be used to extract main version/rv numbers
		// *** CHANGED BY MASTEROBJECTS ***
		var pattern = /[(); \n]/;
		// moMoz type array, add to this if you need to
		moz_types = new Array( 'Firebird', 'Phoenix', 'Firefox', 'Galeon', 'K-Meleon', 'Camino', 'Epiphany', 'Netscape6', 'Netscape', 'MultiZilla', 'Gecko Debian', 'rv' );
		rv_pos = nua.indexOf( 'rv' );// find 'rv' position in nua string
		moRvFull = nua.substr( rv_pos + 3, 6 );// cut out maximum size it can be, eg: 1.8a2, 1.0.0 etc
		// search for occurance of any of characters in pattern, if found get position of that character
		rv_slice = ( moRvFull.search( pattern ) != -1 ) ? moRvFull.search( pattern ) : '';
		//check to make sure there was a result, if not do  nothing
		// otherwise slice out the part that you want if there is a slice position
		( rv_slice ) ? moRvFull = moRvFull.substr( 0, rv_slice ) : '';
		// this is the working id number, 3 digits, you'moD use this for 
		// number comparison, like if moNu >= 1.3 do something
		moNu = moRvFull.substr( 0, 3 );
		// *** CHANGED BY MASTEROBJECTS: added var ***
		for (var i=0; i < moz_types.length; i++)
		{
			if ( nua.indexOf( moz_types[i]) !=-1 )
			{
				moMozBrow = moz_types[i];
				break;
			}
		}
		if ( moMozBrow )// if it was found in the array
		{
			str_pos=nua.indexOf(moMozBrow);// extract string position
			moMozBrowNu = nua.substr( (str_pos + moMozBrow.length + 1 ) ,3);// slice out working number, 3 digit
			// if you got it, use it, else use moNu
			moMozBrowNu = ( isNaN( moMozBrowNu ) ) ? moMozBrowNu = moNu: moMozBrowNu;
			moMozBrowNuSub = nua.substr( (str_pos + moMozBrow.length + 1 ), 8);
			// this makes sure that it's only the id number
			sub_nu_slice = ( moMozBrowNuSub.search( pattern ) != -1 ) ? moMozBrowNuSub.search( pattern ) : '';
			//check to make sure there was a result, if not do  nothing
			( sub_nu_slice ) ? moMozBrowNuSub = moMozBrowNuSub.substr( 0, sub_nu_slice ) : '';
		}
		if ( moMozBrow == 'Netscape6' )
		{
			moMozBrow = 'Netscape';
		}
		// *** CHANGED BY MASTEROBJECTS: comparator === ***
		else if ( moMozBrow == 'rv' || moMozBrow === '' )// default value if no other gecko name fit
		{
			moMozBrow = 'Mozilla';
		} 
		if ( !moMozBrowNu )// use rv number if nothing else is available
		{
			moMozBrowNu = moNu;
			moMozBrowNuSub = moNu;
		}
		if (n.productSub)
		{
			moReleaseDate = n.productSub;
		}
				
	}
	else if (moIe)
	{
		str_pos=nua.indexOf('MSIE');
		moNu=nua.substr((str_pos+5),3);
		moBrow = 'Microsoft Internet Explorer';
	}
	// default to navigator app name
	else 
	{
		moBrow = nan;
	}
	moOp5=(moOp&&(moNu.substring(0,1)==5));
	moOp6=(moOp&&(moNu.substring(0,1)==6));
	moOp7=(moOp&&(moNu.substring(0,1)==7));
	moIe4=(moIe&&!moDom);
	moIe5=(moIe&&(moNu.substring(0,1)==5));
	moIe6=(moIe&&(moNu.substring(0,1)==6));
	moIe7=(moIe&&(moNu.substring(0,1)==7));
	// default to get number from navigator app version.
	if(!moNu) 
	{
		moNu = nav.substring(0,1);
	}
	/*moIe5x tests only for functionality. moDom or moIe5x would be default settings. 
	Opera will register true in this test if set to identify as IE 5*/
	moIe5x=(moD.all&&moDom);
	moIe5Mac=(moMac&&moIe5);
	moIe5xWin=(moWin&&moIe5x);

	// End of standard browser-detection script

}

moBrowserDetectionScript();


/**
 *  ADDITIONAL DETECTION VARIABLES USED BY MASTEROBJECTS
 */

var	moBrowName, moIe50, moIe55, moBoxModelHack, moNsPlugins, moBrowDesc,
	moBrowRec, moBrowRecWin, moBrowRecMac, moBrowRecLin, moCookieEnabled;

if (typeof moMozBrow != "undefined") {
	moBrow = moMozBrow;
	moBrowName = moMozBrow+MO_NBSP+moMozBrowNu;
} else if (moBrow == '') {
	moBrowName = "Undetected Browser (which may not be fully supported by MasterObjects)";
	moNu = ''; // use (!moNu) to detect a non-recognized browser.
} else {
	moBrowName = moBrow+MO_NBSP+moNu;
}

moIe50 = moIe5&&(moNu.substring(2,3)=='0');
moIe55 = moIe5&&(moNu.substring(2,3)>4);
moBoxModelHack = (moIe5 && !moMac) || (moIe6 && moD.compatMode == "BackCompat");
moNsPlugins =
		(navigator.plugins && navigator.mimeTypes.length);  // Netscape plugin architecture

moBrowDesc = moBrowName;
moBrowRecWin = "Firefox"+MO_NBSP+"1.0+, Internet"+MO_NBSP+"Explorer" +MO_NBSP+"6+"+MO_NBSP+
				"(or"+MO_NBSP+"5.5), Mozilla"+MO_NBSP+"1.7+, or Netscape"+MO_NBSP+"7.2+";
moBrowRecMac = "Safari"+MO_NBSP+"1.3+ or Internet"+MO_NBSP+"Explorer" +MO_NBSP+"5.2.3"+
				MO_NBSP+" (version"+MO_NBSP+"5.1.7 for Mac"+MO_NBSP+"OS"+MO_NBSP+"9)";
moBrowRecLin = "Firefox"+MO_NBSP+"1.0+, Mozilla"+MO_NBSP+"1.7+, Galeon"+MO_NBSP+
				"1.3+, or Netscape"+MO_NBSP+"7.2+";

// String containing recommendation for old browsers not supported by any MasterObjects pages.
// Empty if the browser matches minimum criteria.
if (moOld) {
	moBrowDesc = "an old browser (" + moBrowName + ")";
	moBrowRec = "a more recent browser such as Firefox, Internet Explorer"+MO_NBSP+"5.1+, Mozilla"+MO_NBSP+"1.7+, Safari, or Netscape"+MO_NBSP+"7.2+";
} else if (moNs4 || (moMozBrow == "Netscape" && moMozBrowNu.substring(0,1)==6)) {
	moBrowDesc = "an old version of Netscape";
	moBrowRec = "version"+MO_NBSP+"7.2 or higher, or use a different browser such as Firefox, Internet Explorer"+MO_NBSP+"5.1+, Mozilla"+MO_NBSP+"1.7+, or Safari";		
	moOld = true;
} else if (moIe4) {
	moBrowDesc = "an old version of Internet Explorer";
	moBrowRec = "at least IE version"+MO_NBSP+"5.1, or use a different browser such as Firefox, Mozilla"+MO_NBSP+"1.7+, Safari, or Netscape"+MO_NBSP+"7.2+";
	moOld = true;
} else if (moIe50) {
	moBrowRec = "version"+MO_NBSP+"5.1 or higher, or use a different browser such as Firefox, Mozilla"+MO_NBSP+"1.7+, Safari, or Netscape"+MO_NBSP+"7.2+";
	moOld = true;
} else if ((moMozBrow == "Mozilla") && (moMozBrowNu < 1.4)) {
	moBrowDesc = "a Mozilla version older than 1.4 (" + moMozBrowNu + ")";
	moBrowRec = "version"+MO_NBSP+"1.7 or higher, or use a different browser such as Firefox, Internet Explorer"+MO_NBSP+"5.1+, Netscape "+MO_NBSP+"7.2+, or Safari";
	moOld = true;
} else if (moMac) {
	moBrowRec = "(on the Mac) "+moBrowRecMac;
} else if (moWin) {
	moBrowRec = "(on Windows) "+moBrowRecWin;
} else if (moLin) {
	moBrowRec = "(on Linux) "+moBrowRecLin;
} else {
	moBrowRec = moBrowRecWin+" (on Windows), "+moBrowRecMac+" (on Mac"+MO_NBSP+"), or "+
				moBrowRecLin+" (on Linux or Unix platforms)";
}

// Returns an empty string, or a string containing a warning for browsers that do not
// meet minimum requirements for MasterObjects pages.
function moBrowserWarningText() {
	return( (moOld) ? "It seems that you are using " + moBrowDesc + 
			". We recommend that you upgrade to " + moBrowRec + "." : '');
}

moCookieEnabled = false;
if (typeof navigator.cookieEnabled == "undefined") {
	if (typeof(moD.cookie) == "string") {
		if (moD.cookie.length === 0) {
			moD.cookie = "test";
			moCookieEnabled = (moD.cookie == "test");
			moD.cookie = "";
		} else {
			moCookieEnabled = true;
		}
	}
} else {
	moCookieEnabled = navigator.cookieEnabled;
}


/**
 *  COOKIE HANDLING
 */

// utility function to retrieve a future expiration date in proper format;
// pass three integer parameters for the number of days, hours,
// and minutes from now you want the cookie to expire; all three
// parameters required, so use zeros where appropriate
function moGetExpirationDate(d, h, m) {
    var e = new Date();
    if (typeof d == "number" && typeof h == "number" && typeof h == "number") {
        e.setDate(e.getDate() + parseInt(d));
        e.setHours(e.getHours() + parseInt(h));
        e.setMinutes(e.getMinutes() + parseInt(m));
        return e.toGMTString();
    }
}

// primary function to retrieve cookie by name
function moGetCookie(name) {
	function getVal(offset) {
		var endstr = moD.cookie.indexOf (";", offset);
		if (endstr == -1) {
			endstr = moD.cookie.length;
		}
		return unescape(moD.cookie.substring(offset, endstr));
	}
	
    var arg = name + "=";
    var alen = arg.length;
    var clen = moD.cookie.length;
    var i = 0;
    while (i < clen) {
        var j = i + alen;
        if (moD.cookie.substring(i, j) == arg) {
            return (getVal(j));
        }
        i = moD.cookie.indexOf(" ", i) + 1;
        if (i === 0) {
        	break;
        }
    }

	return null;
}

// store cookie value with optional details as needed
function moSetCookie(name, value, expires, path, domain, secure) {
    moD.cookie = name + "=" + escape (value) +
        ((expires) ? "; expires=" + expires : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
}

// remove the cookie by setting ancient expiration date
function moDeleteCookie(name,path,domain) {
    if (moGetCookie(name)) {
        moD.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}


/**
 *  USER MESSAGE HANDLING
 */

// Displays a one-time warning to the user for the given Id. A warning is only shown if Cookies
// were enabled by the user. canceledString is an optional parameter that signifies the string
// to be displayed when the user "backs away". 
function moOneTimeWarning(theId, theString, theCanceledBoolean, theCancelString) {
	if (theString.length>0 && !theCanceledBoolean) {
		if (moCookieEnabled) {
			theCanceledBoolean = moGetCookie(theId);
			if ((theCanceledBoolean === null) || !theCanceledBoolean) {
				if (typeof theCancelString != "undefined" && theCancelString.length > 0) {
					if (confirm(theString)) {
						return(true);
					}
					theString = theCancelString;
				}
				alert(theString +
				'\n\n(This message will not be shown again until you restart your browser.)');
				moSetCookie(theId,theCanceledBoolean);
			}
		}
	}
	return (false);
}

// Function that may be called from a page that wants to ensure that the browser meets
// minimum specifications for all MasterObjects pages.
function moBrowserCheck() {
	gMoDisabled = !moOneTimeWarning('moW1',moBrowserWarningText(),gMoDisabled);
	return (!gMoDisabled);
}

/**
 *  UTILITY FUNCTIONS
 */

// Replaces all occurences of a string in a string by a replacement string
function moReplaceAll(theString,theOriginal,theReplacement) {
	var r = new RegExp(theOriginal,"g");
	return(theString.replace(r,theReplacement));
}

// Jumps to an anchor or URL that corresponds to the current selection item.
function moJumpToSelection(theSelection) {
	var theURL = theSelection.options[theSelection.selectedIndex].value; 
	if (theURL) {
		window.location = theURL;
	}
	theSelection.selectedIndex = 0;
}

// Temporary function -- to be removed because of potential naming collisions w/ third parties
function jumpTo(theSelection) {
	return (moJumpToSelection(theSelection));
}

// Returns the document that corresponds to an IFrame object.
// Based on example code from: http://developer.apple.com/internet/webcontent/iframe.html
function moIFrameDocument(theIFrame) {
	if (theIFrame.contentDocument) {
    	// For NS6
    	return(theIFrame.contentDocument); 
  	} else if (theIFrame.contentWindow) {
    	// For IE5.5 and IE6
    	return(theIFrame.contentWindow.document);
  	} else if (theIFrame.document) {
    	// For IE5
		if (moD.frames) {
			// this is for IE5 Mac, because it will only allow access to the document object
			// of the IFrame if we access it through the document.frames array
			return(moD.frames[theIFrame.name].document);
		} else {
    		return(theIFrame.document);
    	}
  	} else {
    	return(null);
  	}
}

// Returns the value that the browser uses to govern the property whose name is passed
// as a parameter. Credit goes to Danny Goodman and Travis Beckham (squidfingers.com).
function moElementStyle(element, IEStyleProp, CSSStyleProp) {
	if (element.style[IEStyleProp]) {
		// inline style property
		return element.style[IEStyleProp];
    } else if (element.currentStyle) {
    	// external stylesheet for IE
        return element.currentStyle[IEStyleProp];
    } else if (moD.defaultView && moD.defaultView.getComputedStyle) {
    	// external stylesheet for Moz/Saf 1.3+
        return moD.defaultView.getComputedStyle(element, "").getPropertyValue(CSSStyleProp);
    }
    // Safari < 1.3
    return "";
}

// End of mo-common.js