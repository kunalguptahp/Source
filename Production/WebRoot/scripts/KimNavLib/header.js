var goHPEP;
if (goHPEP==null)
{
    var newLineChar=String.fromCharCode(10);
    var ie4=document.all&&navigator.userAgent.indexOf("Opera")==-1;
    var ns6=(document.getElementById && !document.all) || (navigator.userAgent.indexOf("Opera") !=-1);
    var ToolBar_Supported=(ie4 || ns6)? true : false;
    var ToolbarMenu=1;
    var goHPSearchMenu=new Array();;
    var goHPMenu;
    var goNav;
    var oNav2Menu=null;
    var oNav1Menu=null;
    var g_Host="localhost"; //"vdccstpron.houston.hp.com";
    var g_Path="/Sunergos/scripts/KimNavLib/"; //"/lib/navigation/";
    var g_LogOffURL="http://localhost/portal/site/athp/template.LOGOUT/"; //"http://athp.hp.com/portal/site/athp/template.LOGOUT/";
    var g_LogOnURL="http://localhost/portal/site/athp/template.LOGIN/"; //"http://athp.hp.com/portal/site/athp/template.LOGIN/";
    var g_Build="Version 7.0.0";
    var g_MyLinks="";
    var g_DevFeedBack="";
    var questFieldConfig={};
    var isHeader=true;
    var arrBreadCrumbMapItem=null;
    var printable=false;
    var globalNavItemsFlag="";
    var quickLinksFlag="";
    var menuHTMLItems="";
    var quickLinksItems="";
    var g_BannerServerName="";
    var parentFontStyle;
    var g_SearchCharSet="utf-8";
    var hide_kLinks_pFinder=false;
    if( !ie4)
    {
        parentFontStyle="small";
    }
    else
    {
        parentFontStyle="x-small";
    }
    _HPEPInitHPMenu();_HPEPInitHPSearchMenu();_HPEPInitGlobals();goNav.menuString1=_HPEPInitHorizontalNav();goNav.leftNav=_HPEPInitLeftNav();_HPEPInitQuestField();
}

function _HPEPInitQuestField()
    {
    var flashWasDetected=_HPEPIsFlashInstalled();
    var os=navigator.platform.split(" ")[0];
    if ((os=="HP-UX") || (os=="OSF1"))
    {
    flashWasDetected=false;
    }
    if (flashWasDetected)
    {
    goHPEP.usePeopleFinderQuestField=true;document.write('<script type="text/javascript" language="javascript" src="'+goHPEP.libPath+'qo/client/scripts/v10/mo-common.js"><\/script>');document.write('<script type="text/javascript" language="javascript" src="'+goHPEP.libPath+'qo/client/scripts/v10/qo-common.js"><\/script>');document.write('<script type="text/javascript" language="javascript" src="'+goHPEP.libPath+'qo/client/scripts/v10/qo-questlet.js"><\/script>');
    }

    }

function _HPEPIsFlashInstalled()
    {
    var flashWasDetected=false;
    if (navigator.plugins.length)
    {
    var i;for (i=0; i < navigator.plugins.length; i++)
    {
    var pluginIdent=navigator.plugins[i].description.split(" ");
    if (pluginIdent[0]=="Shockwave" && pluginIdent[1]=="Flash")
    {
    var versionArray=pluginIdent[2].split(".");
    if (versionArray[0] > 6)
    {
    flashWasDetected=true;
    }
    else if (versionArray[0] > 5)
    {
    if ((pluginIdent[3] !=null) && (pluginIdent[3] !=""))
    {
    versionArray=pluginIdent[3].split("r");
    }
    else
    {
    versionArray=pluginIdent[4].split("r");
    }
    if (versionArray[1] > 64)
    {
    flashWasDetected=true;
    }

    }
    break;
    }

    }

    }
    else
    {
    flashWasDetected=true;try
    {
    var Flash=new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
    }
    catch(e)
    {
    flashWasDetected=false;
    }

    }
    return (flashWasDetected);
    }

function _HPEPInitHPMenu()
    {
    goHPMenu=new Array();goHPMenu.menuString="<div id='idMenuPane' style='position:absolute;top:0;left:0;height:0;' nowrap><!--HP_MENU_TITLES--></div>";
    }

function _HPEPInitHPSearchMenu()
    {
    goHPSearchMenu.siteSearchEnabled=false;goHPSearchMenu.siteSearchSID="";goHPSearchMenu.siteSearchURL="";goHPSearchMenu.siteSearchCaption="Site Search";
    }

function _HPEPInitGlobals()
    {
    goHPEP=new Array();goHPEP.enableNav1Menu=false;goHPEP.enableLeftNavMenu=false;goHPEP.menuDropDown=true;goHPEP.lockMenu=false;goHPEP.versionNumber="";goHPEP.pageRevision="";goHPEP.bannerName="Smarter<br>Faster<br>Connected";setBanner(goHPEP.bannerName);goHPEP.bannerDrawn=false;goHPEP.loginEnabled=false;goHPEP.loggedIn=_HPEPIsLoggedIn();goHPEP.enablePortalImage=false;goHPEP.enableMyLinks=true;goHPEP.enableFullCSS=true;
    if (location.protocol=="file:")
    {
    goHPEP.pageProtocol="http://";
    }
    else
    {
    goHPEP.pageProtocol=location.protocol+"//";
    }
    goHPEP.libPath=goHPEP.pageProtocol+g_Host+g_Path;goHPEP.images=goHPEP.libPath+"images/";goHPEP.warningMsg="";goHPEP.warningMsgGlobal="";goHPEP.greetingMsg="";document.write("<link rel='stylesheet' type='text/css' href='"+goHPEP.libPath+"css/header.css'>");document.write("<link rel='stylesheet' type='text/css' href='"+goHPEP.libPath+"css/banner_color.css'>");document.write("<link rel='stylesheet' type='text/css' href='"+goHPEP.libPath+"css/GlobalNavigation.css'>");goHPEP.printLabel="Printable Version";goHPEP.printableTarget="";goHPEP.printPageURL="";goHPEP.footerDrawn=false;goHPEP.privacyLabel="Privacy Statement";goHPEP.feedbackLabel="Feedback";goHPEP.termsOfUseLabel="Terms of Use"
    goHPEP.feedbackTarget="";goHPEP.copyright="&copy; Copyright "+new Date().getFullYear()+" Hewlett-Packard Development Company, L.P.";goHPEP.securityLabel="HP Restricted";goHPEP.supportLabel="Support";goHPEP.supportTarget="";goHPEP.webMetrics=false;goHPEP.usePeopleFinderQuestField=false;goNav=new Array();goNav.breadCrumbLinkMenuID=""
    goNav.breadCrumbMenuID=""
    goNav.breadCrumbNav1="";goNav.breadCrumbNav2="";goNav.breadCrumbNav3="";goNav.breadCrumbLabel="";goNav.menuString="";goNav.leftNav="";goNav.leftNavString="";goMenu=new Array();document.write("<script language='javascript' src='"+goHPEP.libPath+"global.js'></script>");document.write("<script language='javascript' src='"+goHPEP.libPath+"ServerName.js'></script>");
    }

function _HPEPInitHorizontalNav()
    {
    var obj=new Array()
    obj.menuString1="<table border=0 cellpadding=0 cellspacing=0 width='100%'> <tr><td width=5><img src='"+goHPEP.images+"spacer.gif' height=20 width=5 border=0></td><!--LD_MENU_TITLES--><td class='nav1Filler' width='100%'></td><td width=5><img src='"+goHPEP.images+"spacer.gif' height=20 width=5 border=0></td></tr></table>";return (obj.menuString1);
    }

function _HPEPInitLeftNav()
    {
    var aLeftNav=new Array();
    var aOpenLeftItems=new Array();aLeftNav.width=151;aLeftNav.enabled=true;aLeftNav.openItems=aOpenLeftItems;plus=new Image();plus.src=goHPEP.images+"plus.gif";minus=new Image();minus.src=goHPEP.images+"minus.gif";return aLeftNav;
    }

function _HPEPGetCookieVal(iOffset)
    {
    var iEnd=document.cookie.indexOf (";", iOffset);
    if (iEnd==-1)
    {
    iEnd=document.cookie.length;
    }
    return unescape(document.cookie.substring(iOffset, iEnd));
    }

function _HPEPGetCookie(sCookieName)
    {
    var sArg=sCookieName+"=";
    var iArgLen=sArg.length;
    var iCookieLen=document.cookie.length;
    var iCookieIndx=0;
    var iCookie=null;
    while (iCookieIndx < iCookieLen)
    {
    iCookie=iCookieIndx+iArgLen;
    if (document.cookie.substring(iCookieIndx, iCookie)==sArg)
    {
    return _HPEPGetCookieVal(iCookie);
    }
    iCookieIndx=document.cookie.indexOf(" ", iCookieIndx)+1;
    if (iCookieIndx==0)
    break;
    }
    return null;
    }

function _HPEPSetSessionCookie(sCookieName, sValue)
    {
    document.cookie=sCookieName+"="+sValue;
    }

function _HPEPSetCookie(name, value, domain, path, maxage, delim)
    {
    var today=new Date();
    var expires=new Date();
    if (maxage==null || maxage==0)
    maxage=1;expires.setTime(today.getTime()+3600*24*maxage);value=_HPEPEscapeCookieVal(value, delim);
    var curCookie=name+"="+value+
    ((expires) ? "; expires="+expires.toGMTString() : "")+
    ((path) ? "; path="+path : "")+
    ((domain) ? "; domain="+domain : "");
    }

function _HPEPEscapeCookieVal(value, delim)
    {
    var tmpStr=value;
    var returnVal='';
    var length=tmpStr.length;
    var begin=0;
    var end=0;do
    {
    var index=tmpStr.indexOf(delim);
    if ( index >=0)
    returnVal=returnVal.concat(escape(tmpStr.substring(begin, index)));else
    returnVal=returnVal.concat(escape(tmpStr.substring(begin, length)));
    if (index >=0)
    returnVal=returnVal.concat(delim);tmpStr=tmpStr.substring(index+1, length);
    }
    while ( index >=0)
    return returnVal;
    }

function _HPEPIsLoggedIn()
    {
    var sCode=_HPEPGetCookie("SMSESSION");
    if (sCode !=null && sCode !="LOGGEDOFF")
    {
    return true;
    }
    return false;
    }

function _HPEPOpenWindow(sURL, sName)
    {
    var sLoc="width=600,height=500,left=350,top=0";
    var bReturn=false;
    var sOpts;
    var oRemote;
    if (_HPEPOpenWindow.arguments.length > 2)
    {
    if (sLoc !="")
    {
    sLoc=_HPEPOpenWindow.arguments[2];
    }

    }
    if (_HPEPOpenWindow.arguments.length > 3)
    {
    bReturn=true;
    }
    sOpts=sLoc+",toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1";oRemote=window.open('','sName', sOpts);oRemote.location.href=sURL;
    if (!oRemote.opener)
    {
    oRemote.opener=self;
    }
    if (bReturn)
    return oRemote;
    }

function _HPEPGoButton(sSearchType)
    {
    if (sSearchType=="frmSiteSearch")
    {
    if (goHPSearchMenu.siteSearchEnabled)
    {
    if (document.frmSiteSearch.siteSearch1.checked)
    {
    if (goHPSearchMenu.siteSearchURL !="")
    {
    document.frmSiteSearch.sid.disabled=true;document.frmSiteSearch.sType.siteSearch1.disabled=true;document.frmSiteSearch.action=goHPSearchMenu.siteSearchURL;
    }
    else if (document.frmSiteSearch.sid !="")
    {
    document.frmSiteSearch.action=goHPSearchMenu.siteSearchPage;
    }

    }
    else
    {
    document.frmSiteSearch.sid.disabled=true;document.frmSiteSearch.action=goHPSearchMenu.searchHPPage;
    }

    }

    }
    return true;
    }

function _HPEPCreateMenu(sMenuID, sMenuDefObject, sMenuDisplay, sMenuURL, sTarget)
    {
    var obj=eval(sMenuDefObject);
    var sTag="<!--LD_MENU_TITLES-->";
    var sMenu=newLineChar;
    var sLinkMenuID="AM_"+sMenuID;
    var sClassName;goHPEP.enableNav1Menu=true;
    if (sMenuDisplay==obj.breadCrumbNav1)
    {
    obj.breadCrumbMenuID=sMenuID;obj.breadCrumbLinkMenuID=sLinkMenuID;sClassName="nav1Down";
    }
    else
    {
    if (sMenuDisplay=="Help")
    {
    sClassName="nav1UpHelp";
    }
    else
    {
    sClassName="nav1Up";
    }

    }
    sMenu+="<td><div style='position:relative' ><table border=0 cellspacing=0 cellpadding=0 height=20><tr><td  class='"+
    sClassName+"' ID='"+sLinkMenuID+"' nowrap valign='middle'><a id='idLocalNav' onmouseout=\"_HPEPDelayHideMenu();\" onmouseover=\"_HPEPDoMenu(event, '"+sLinkMenuID+"', '"+sMenuID+"','"+sMenuURL+"','"+sMenuDefObject+
    "');\"";
    if (sMenuURL !="")
    {
    sMenu+=" target='"+sTarget+"' href='"+sMenuURL+"'>";
    }
    else
    {
    sMenu+=" style='cursor:default' href=\"javascript:void();\" onclick=\"window.event.returnValue=false;\">";
    }
    sMenu+="&nbsp;&nbsp;"+sMenuDisplay+"&nbsp;&nbsp;</a></td></tr></table></div></td><td><img src='"+goHPEP.images+"spacer.gif' height=20 width=2 border=0></td>"+
    sTag;obj.menuString1=obj.menuString1.replace(sTag, sMenu);
    }

function _HPEPHideMenu(sMenuDefObject, sNav1MenuObject)
    {
    var obj=eval(sMenuDefObject);
    var objNav1Menu=eval(sNav1MenuObject);
    var oBreadCrumbNav2Menu;
    if (oNav2Menu)
    {
    oBreadCrumbNav2Menu=document.getElementById(obj.breadCrumbMenuID);
    if (goHPEP.menuDropDown)
    {
    oBreadCrumbNav2Menu=null;
    if (ie4)
    {
    _HPEPShowElement("SELECT");_HPEPShowElement("OBJECT");_HPEPShowElement("IFRAME");
    }
    else
    {
    _HPEPShowMozillaElements();
    }

    }
    if (oBreadCrumbNav2Menu==null)
    {
    oNav2Menu.thestyle.visibility=(ie4||ns6)? "hidden" : "hide";oNav2Menu.innerHTML="";
    }
    else
    {
    oNav2Menu.innerHTML=oBreadCrumbNav2Menu.innerHTML;
    }
    if (objNav1Menu)
    {
    var oBreadCrumbNav1Menu;
    if ((obj.breadCrumbLinkMenuID !=null) && (obj.breadCrumbLinkMenuID !=""))
    oBreadCrumbNav1Menu=document.getElementById(obj.breadCrumbLinkMenuID);
    if (oBreadCrumbNav1Menu !=null)
    {
    oBreadCrumbNav1Menu.className="nav1Down";
    }
    if (objNav1Menu !=oBreadCrumbNav1Menu)
    {
    if (objNav1Menu.innerText=="  Help  ")
    objNav1Menu.className="nav1UpHelp";else
    objNav1Menu.className="nav1Up";
    }
    else
    {
    objNav1Menu.className="nav1Down";
    }

    }

    }

    }

function _HPEPContainsNS6(a, b)
    {
    while (b.parentNode)
    {
    if ((b=b.parentNode)==a)
    {
    return true;
    }

    }
    return false;
    }

function _HPEPDynamicHide(e)
    {
    if (ie4&&!oNav2Menu.contains(e.toElement))
    _HPEPDelayHideMenu();else if (ns6&&e.currentTarget!=e.relatedTarget&& !_HPEPContainsNS6(e.currentTarget, e.relatedTarget))
    _HPEPDelayHideMenu();
    }

function _HPEPDelayHideMenu()
    {
    var iDelay=1500;
    if (goHPEP.menuDropDown)
    iDelay=250;
    if (ie4||ns6)
    delayhide=setTimeout("_HPEPHideMenu(goNav,oNav1Menu)", iDelay);
    }

function _HPEPClearHideMenuTimeout()
    {
    if (window.delayhide)
    clearTimeout(delayhide);
    }

function _HPEPHighlightNav2Menu(e, state, sMenuDefObject)
    {
    var obj=eval(sMenuDefObject);
    var source_el;
    if (document.all)
    {
    source_el=event.srcElement;
    }
    else if (document.getElementById)
    source_el=e.target;
    if (source_el.className=="nav2menuitem")
    {
    if (state=="on")
    {
    source_el.id="nav2DownHover";
    }
    else
    {
    if (source_el.innerText=="  "+obj.breadCrumbNav2+"  ")
    {
    source_el.id="nav2Down";
    }
    else
    {
    source_el.id="";
    }

    }

    }
    else
    {
    while(source_el.id!="popmenu")
    {
    source_el=document.getElementById? source_el.parentNode : source_el.parentElement;
    if (source_el.className=="nav2menuitem")
    {
    if (state=="on")
    {
    source_el.id="nav2DownHover";
    var level2text=_getInnerText(source_el);
    if (level2text==obj.breadCrumbNav2)
    {
    source_el.id="nav2DownSelectedHover";
    }

    }
    else
    {
    if (source_el.innerText=="  "+obj.breadCrumbNav2+"  ")
    {
    source_el.id="nav2Down";
    }
    else
    {
    source_el.id="";
    }

    }

    }

    }

    }

    }

function _getInnerText(level2Item)
    {
    var finalStr=level2Item.innerHTML.replace(/<[^>]+>/g,"");
    while (finalStr.indexOf("&nbsp;") !=-1)
    {
    finalStr=finalStr.replace("&nbsp;", "");
    }
    escapedFinalStr=escape(finalStr);
    while(escapedFinalStr.indexOf("%0D") !=-1)
    {
    escapedFinalStr=escapedFinalStr.replace("%0D", "");
    }
    while(escapedFinalStr.indexOf("%0A") !=-1)
    {
    escapedFinalStr=escapedFinalStr.replace("%0A", "");
    }
    finalStr=unescape(escapedFinalStr);return finalStr;
    }

function _HPEPDoMenu(e, sLinkMenuID, sMenuID, sMenuURL, sMenuDefObject)
    {
    var eventX;
    var eventY;
    var nav1Height=20;
    var obj=eval(sMenuDefObject);
    var oMenu=document.getElementById(sMenuID);
    var oSelectedNav1Menu=document.getElementById(sLinkMenuID);
    var oBreadCrumbNav1Menu;
    if ((obj.breadCrumbLinkMenuID !=null) && (obj.breadCrumbLinkMenuID !=""))
    {
    oBreadCrumbNav1Menu=document.getElementById(obj.breadCrumbLinkMenuID);
    }
    oSelectedNav1Menu.className="nav1DownHover";
    if (oNav1Menu)
    {
    if (oNav1Menu !=oSelectedNav1Menu)
    {
    if (oNav1Menu !=oBreadCrumbNav1Menu)
    {
    if (oNav1Menu.innerText=="  Help  ")
    oNav1Menu.className="nav1UpHelp";else
    oNav1Menu.className="nav1Up";
    }

    }

    }
    if ((oBreadCrumbNav1Menu !=null) && (oSelectedNav1Menu !=oBreadCrumbNav1Menu))
    {
    oBreadCrumbNav1Menu.className="nav1Up";
    }
    if (sMenuURL !="") oSelectedNav1Menu.className="nav1LinkHover";oNav1Menu=oSelectedNav1Menu;
    if (oMenu==null)
    {
    if ((ns6) && (Event.stopPropagation))
    {
    Event.stopPropagation();
    }
    else if (window.event)
    {
    window.event.cancelbubble=true;
    }
    oMenu=new Array();oMenu.innerHTML="<table></table>";
    }
    if (!document.all&&!document.getElementById&&!document.layers)
    return;_HPEPClearHideMenuTimeout();oNav2Menu=ie4? document.all.popmenu : document.getElementById("popmenu");oNav2Menu.thestyle=oNav2Menu.style;oNav2Menu.innerHTML=oMenu.innerHTML;
    if (goHPEP.menuDropDown)
    {
    if (ie4)
    {
    _HPEPShowElement("SELECT");_HPEPShowElement("OBJECT");_HPEPShowElement("IFRAME");
    }
    else
    {
    _HPEPShowMozillaElements();
    }
    oNav2Menu.contentwidth=oNav2Menu.offsetWidth;oNav2Menu.contentheight=oNav2Menu.offsetHeight;
    if (ie4)
    {
    eventX=event.clientX - event.offsetX - 2;eventY=(event.clientY+nav1Height) - event.offsetY - 1;
    }
    else if (ns6)
    {
    eventX=e.pageX - e.layerX;eventY=e.pageY - e.layerY+21;
    }
    var rightedge=ie4? document.body.clientWidth-eventX : window.innerWidth-eventX;
    var bottomedge=ie4? document.body.clientHeight-eventY : window.innerHeight-eventY;
    if (rightedge < oNav2Menu.contentwidth)
    {
    oNav2Menu.thestyle.left=ie4? document.body.scrollLeft+eventX+event.srcElement.offsetWidth-oNav2Menu.contentwidth : window.pageXOffset+eventX+e.currentTarget.offsetWidth-oNav2Menu.contentwidth;
    }
    else
    {
    oNav2Menu.thestyle.left=ie4? document.body.scrollLeft+eventX : window.pageXOffset+eventX;
    }
    if (bottomedge < oNav2Menu.contentheight)
    {
    oNav2Menu.thestyle.top=ie4? ( document.body.scrollTop+eventY-oNav2Menu.contentheight ) : window.pageYOffset+eventY-oNav2Menu.contentheight;
    if ( (oNav2Menu.thestyle.top).replace("px","") <=35)
    {
    hide_kLinks_pFinder=true;
    }
    else
    {
    hide_kLinks_pFinder=false;
    }

    }
    else
    {
    oNav2Menu.thestyle.top=ie4? document.body.scrollTop+eventY : window.pageYOffset+eventY;
    }
    if (ie4)
    {
    _HPEPHideElement(oNav2Menu, "SELECT");_HPEPHideElement(oNav2Menu, "OBJECT");_HPEPHideElement(oNav2Menu, "IFRAME");
    }
    else
    {
    _HPEPHideMozillaElements(oNav2Menu);
    }
    hide_kLinks_pFinder=false;
    }
    if (ie4)
    {
    if ((oNav2Menu.innerText=="") && (goHPEP.menuDropDown))
    oNav2Menu.thestyle.visibility=(ie4||ns6)? "hidden" : "hide";else
    oNav2Menu.thestyle.visibility="visible";
    }
    else
    {
    //Mozilla
    if ((_getInnerText(oNav2Menu)=="") && (goHPEP.menuDropDown))
    oNav2Menu.thestyle.visibility=(ie4||ns6)? "hidden" : "hide";else
    oNav2Menu.thestyle.visibility="visible";
    }
    return false;
    }

function _HPEPHideElement(oNav2Menu, elmID)
    {
    var i=0, obj, objLeft, objTop, objParent;
    var iNavTop, iNavLeft;iNavTop=oNav2Menu.thestyle.pixelTop;iNavLeft=oNav2Menu.thestyle.pixelLeft;for (i=0; i < document.all.tags(elmID).length; i++)
    {
    obj=document.all.tags(elmID)[i];
    if (!obj || !obj.offsetParent || (obj.id=='gNQuickLinks' && hide_kLinks_pFinder==false) || (obj.id=='PeopleFinderNameSearch' && hide_kLinks_pFinder==false) || (obj.id=='PeopleFinderNameSearchPopUp') )
    {
    //some conditions added for Global Nav
    continue;
    }
    objLeft=obj.offsetLeft;objTop=obj.offsetTop;objParent=obj.offsetParent;
    while (objParent !=null)
    {
    if (objParent.tagName.toUpperCase() !="BODY")
    {
    objLeft+=objParent.offsetLeft;objTop+=objParent.offsetTop;objParent=objParent.offsetParent;
    }
    else
    {
    break;
    }

    }
    if ((objTop < (iNavTop+oNav2Menu.contentheight)) &&
    ((objTop+obj.clientHeight) > iNavTop) &&
    (objLeft > iNavLeft || objLeft < iNavLeft) && //One more 'or' condition added for e-quote Administration Site issue (CQ 2913).
    (objLeft < (iNavLeft+oNav2Menu.contentwidth)))
    {
    obj.style.visibility=(ie4||ns6)? "hidden" : "hide";obj.style.HPEPHide=true;
    }
    else if ((objTop < (iNavTop+oNav2Menu.contentheight)) &&
    ((objLeft+obj.clientWidth) > iNavLeft) &&
    ((objLeft+obj.clientWidth) < (iNavLeft+oNav2Menu.contentwidth)))
    {
    obj.style.visibility=(ie4||ns6)? "hidden" : "hide";obj.style.HPEPHide=true;
    }
    else if (objTop > (iNavTop+oNav2Menu.contentheight))
    {
    return ;
    }

    }

    }

function _HPEPShowElement(elmID)
    {
    var i=0, obj;for (i=0; i < document.all.tags(elmID).length; i++)
    {
    obj=document.all.tags(elmID)[i];
    if (!obj)
    continue;
    if (obj.style.HPEPHide)
    {
    obj.style.visibility="visible";obj.style.HPEPHide=false;
    }

    }

    }

function _HPEPHideMozillaElements(elm)
    {

function getCoordinates(theObject)
    {
    if( theObject.offsetParent )
    {
    for (var theX=0, theY=0; theObject.offsetParent; theObject=theObject.offsetParent)
    {
    theX+=theObject.offsetLeft;theY+=theObject.offsetTop;
    }
    return [theX, theY];
    }
    else
    {
    return [theObject.x, theObject.y];
    }

    }
    var field=document.getElementById("PeopleFinderNameSearch");
    var fieldCoordinates=getCoordinates(field);
    var fieldX=fieldCoordinates[0];
    var fieldY=fieldCoordinates[1];
    var fieldW=field.offsetWidth;
    var fieldH=field.offsetHeight;
    var menuCoordinates=getCoordinates(elm);
    var theXMin=menuCoordinates[0];
    var theYMin=menuCoordinates[1];
    var theXMax=theXMin+elm.offsetWidth;
    var theYMax=theYMin+elm.offsetHeight;
    if ( (fieldX < theXMax) && (fieldY < theYMax) &&
    ((fieldX+fieldW) > theXMin) && ((fieldY+fieldH) > theYMin)
    )
    {
    field.style.visibility="hidden";
    }

    }

function _HPEPShowMozillaElements()
    {
    var field=document.getElementById("PeopleFinderNameSearch");field.style.visibility="visible";
    }

function _HPEPAddMenuItem(sMenuID, sMenuDefObject, sSubMenu, sSubMenuURL, sTarget)
    {
    var obj=eval(sMenuDefObject);
    var sMenuString=obj.menuString;
    var sURL=sSubMenuURL;
    var sMenuType="idLocalNav";
    var sLookUpTag="<!--"+sMenuID+"-->";
    var iPos=sMenuString.indexOf(sLookUpTag);
    var sTemp=newLineChar;
    if (iPos <=0)
    {
    sMenuString+=newLineChar+
    newLineChar+
    "<div id='"+sMenuID+"'  style='display:none;'>";
    }
    if (sURL)
    {
    sTemp+="<div class='nav2menuitem'><a id=\""+sMenuType+"\" target=\""+sTarget+"\" ";sTemp+=" href='"+sURL+"' ";sTemp+=">"+sSubMenu+"</a></div>";
    }
    else
    {
    sTemp+="<label>"+sSubMenu+"</label>";
    }
    sTemp+=sLookUpTag;
    if (iPos <=0)
    {
    sMenuString+=sTemp+"</div>";
    }
    else
    {
    sMenuString=sMenuString.replace(sLookUpTag, sTemp);
    }
    obj.menuString=sMenuString;
    }

function _HPEPAddHorMenuItem(sMenuID, sMenuDefObject, sSubMenu, sSubMenuURL, sTarget)
    {
    var obj=eval(sMenuDefObject);
    var sMenuString=obj.menuString;
    var sURL=sSubMenuURL;
    var sMenuType="idLocalNav";
    var sLookUpTag="<!--"+sMenuID+"-->";
    var iPos=sMenuString.indexOf(sLookUpTag);
    var sTemp=newLineChar;
    if (sURL)
    {
    if (iPos <=0)
    {
    sMenuString+=newLineChar+
    newLineChar+
    "<div id='"+sMenuID+"'  style='display:none;'><table border=0 width='100%' cellspacing=0 cellpadding=0><tr>";
    }
    if ((sMenuID==obj.breadCrumbMenuID) & (sSubMenu==obj.breadCrumbNav2))
    {
    sTemp+="<td><div class='nav2menuitem' id='nav2Down'><table border=0 cellpadding=0 cellspacing=0><tr><td nowrap height=20><a id='"+sMenuType+"' target=\""+sTarget+"\" ";
    }
    else
    {
    sTemp+="<td><div class='nav2menuitem'><table border=0 cellpadding=0 cellspacing=0><tr><td nowrap height=20><a id='"+sMenuType+"' target=\""+sTarget+"\" ";
    }
    sTemp+=" href='"+sURL+"' ";sTemp+=">&nbsp;&nbsp;"+sSubMenu+"&nbsp;&nbsp;</a></td></tr></table></div></td><td bgcolor='#FFFFFF'><img src='"+goHPEP.images+"spacer.gif' height=0 width=2 border=0></td>"+
    sLookUpTag;
    if (iPos <=0)
    {
    sMenuString+=sTemp+"<td width='100%'>&nbsp;</td></tr></table></div>";
    }
    else
    {
    sMenuString=sMenuString.replace(sLookUpTag, sTemp);
    }
    obj.menuString=sMenuString;
    }

    }

function _HPEPAddMenuSeparator(sMenuID, sMenuDefObject)
    {
    var obj=eval(sMenuDefObject);
    var sLookUpTag="<!--"+sMenuID+"-->";
    var sMenuString, sTemp;sMenuString=obj.menuString
    var iPos=sMenuString.indexOf(sLookUpTag);
    if (iPos > 0)
    {
    sTemp=newLineChar+
    "<div style='margin-top:4px;margin-bottom:4px;width:100%;height:1px;background-color:white'><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></div>"+sLookUpTag;sMenuString=sMenuString.replace(sLookUpTag, sTemp);
    }
    obj.menuString=sMenuString;
    }

function _HPEPAddNavLink(sSubMenu, sSubMenuURL, sTarget, sMenuDefObject)
    {
    var obj=eval(sMenuDefObject);
    var bSelected;
    if (unescape(location.href)==sSubMenuURL || unescape(location.pathname)==sSubMenuURL || sSubMenu==obj.breadCrumbNav3)
    bSelected=true;obj.leftNavString+=
    "<tr><td align=center valign=top width=16 nowrap><img align=center src='"+goHPEP.images+"bullet.gif'  vspace=3 height=4 width=4 hspace=3 border=0></td><td valign=top width=100%>";
    if (bSelected)
    {
    obj.leftNavString+="<div ID=listItemSelected class='leftNavDownSelected'>"+sSubMenu+"</div>";
    }
    else
    {
    obj.leftNavString+="<a class='leftNavDown' target='"+sTarget+"' href='"+sSubMenuURL+"'>"+sSubMenu+"</a>";
    }
    obj.leftNavString+="</td></tr>";
    }

function _HPEPCreateNavExpandingList(sMenuID, sSubMenu, sSubMenuURL, bDefOpen, sTarget, sMenuDefObject)
    {
    var obj=eval(sMenuDefObject);
    var sLookUpTag="<!--"+sMenuID+"-->";
    var sPos=obj.leftNavString.indexOf(sLookUpTag);
    if (sPos <=0)
    {
    obj.leftNavString+=
    "<tr><td align=center valign=top width=16 nowrap><img src='"+goHPEP.images+(bDefOpen? 'minus.gif':'plus.gif')+"' align='center' height='9' width='9' border=0 id='BTN_"+sMenuID+"' vspace=3 style='cursor:hand;' onclick=\"return _HPEPToggleNavExpandingList('BTN_"+sMenuID+"', 'ITEMS_"+sMenuID+"');\"></td><td valign=top width=100%>";
    if (sSubMenuURL)
    {
    var bSelected=false;
    if (unescape(location.href)==sSubMenuURL || unescape(location.pathname)==sSubMenuURL || sSubMenu==obj.breadCrumbNav3)
    {
    bSelected=true;
    }
    if(bSelected)
    {
    obj.leftNavString+=
    "<div id=listItemSelected><div class='leftNavDownSelected'>"+sSubMenu+"</div></div>"
    }
    else
    {
    obj.leftNavString+=
    "<a class='leftNavDown' target='"+sTarget+"' href='"+sSubMenuURL+"' id='"+sMenuID+"' class=clsTocHead >"+sSubMenu+"</a>";
    }

    }
    else
    {
    obj.leftNavString+="<div class='leftNavDown' id='idNotALink' >"+sSubMenu+"</div>";
    }
    obj.leftNavString+="<div id=ITEMS_"+sMenuID+" style='display:"+(bDefOpen? 'block':'none')+"';'><!--"+sMenuID+"--></div></td></tr>";
    if (_HPEPCreateNavExpandingList.arguments.length > 3)
    {
    if (bDefOpen)
    {
    obj.leftNav.openItems[obj.leftNav.openItems.length]=sMenuID;
    }

    }

    }

    }

function _HPEPAddNavExpandingListItem(sMenuID, sSubMenu, sSubMenuURL, sTarget, sMenuDefObject)
    {
    var bSelected;
    var obj=eval(sMenuDefObject);
    if (unescape(location.href)==sSubMenuURL || unescape(location.pathname+location.search)==sSubMenuURL || sSubMenu==obj.breadCrumbNav3)
    {
    bSelected=true;
    var i=0, bFound=false;for (i=0; i < obj.leftNav.openItems.length; i++)
    {
    if (obj.leftNav.openItems[i]==sMenuID)
    {
    bFound=true;break;
    }

    }
    if (!bFound)
    obj.leftNav.openItems[obj.leftNav.openItems.length]=sMenuID;
    }
    var sLookUpTag="<!--"+sMenuID+"-->";
    var sTemp=newLineChar+"<div "+(bSelected? ' id=listItemSelected':'')+" >";
    if (bSelected)
    {
    sTemp+="<div class='leftNavSelected'>"+sSubMenu+"</div></div>"+sLookUpTag;
    }
    else
    {
    sTemp+="<a class='leftNav' target='"+sTarget+"' href='"+sSubMenuURL+"'>"+sSubMenu+"</a></div>"+sLookUpTag;
    }
    obj.leftNavString=obj.leftNavString.replace(sLookUpTag, sTemp);
    }

function _HPEPOpenNavExpandingLists(sMenuDefObject)
    {
    var obj=eval(sMenuDefObject);
    var iCntr;
    var oList;
    var oBtn;for (iCntr=0; iCntr < obj.leftNav.openItems.length; iCntr++)
    {
    oList=document.getElementById("ITEMS_"+obj.leftNav.openItems[iCntr]);oBtn=document.getElementById("BTN_"+obj.leftNav.openItems[iCntr]);oList.style.display="block";oBtn.src=eval("minus.src");
    }

    }

function _HPEPAddNavImageLink(sSubImage, sSubMenuURL, sSubMenu, sTarget, sMenuDefObject)
    {
    var bSelected;
    var obj=eval(sMenuDefObject);
    if (unescape(location.href)==sSubMenuURL || unescape(location.pathname)==sSubMenuURL || sSubMenu==obj.breadCrumbNav3)
    {
    bSelected=true;
    }
    obj.leftNavString+="<tr><td colspan=2><table border=0 cellpadding=0 cellspacing=0><tr>";
    if (bSelected)
    {
    obj.leftNavString+="<td><img align=left src='"+sSubImage+"' height=16 width=16 border=0></td><td id=\"listItemSelected\" width=100% class=\"clsTocHead\" id=\"listItemSelected\" style='font-family:Verdana;font-size:10px;font-weight:bold;'>"+sSubMenu+"</td>";
    }
    else
    {
    obj.leftNavString+="<td><a target='"+sTarget+"' href='"+sSubMenuURL+"' ><img align=left src='"+sSubImage+"' height=16 width=16 border=0></a></td><td class=\"leftNavMenuItem\" width=100%><a style='font-family:Verdana;font-size:10px;font-weight:bold;text-decoration:none;' target='"+sTarget+"' href='"+sSubMenuURL+"' class=clsTocHead>"+sSubMenu+"</a></td>";
    }
    obj.leftNavString+="</tr></table></td></tr>";
    }

function _HPEPAddNavImage(sSubImage, sSubMenuURL, sSubMenu, sTarget, sMenuDefObject)
    {
    var obj=eval(sMenuDefObject);obj.leftNavString+=
    "<tr><td style=margin-left-width=10px; colspan=2 align=center><a target='"+sTarget+"' href='"+sSubMenuURL+"'><img src='"+sSubImage+"' border=0 ALT='"+sSubMenu+"'></td></a></td></tr>";
    }

function _HPEPCreateFooter()
    {
    if (printable)
    {
    var printFoot="<table border='0' cellpadding='0' cellspacing='0' width='740'><tr><td align='center' valign='bottom' width='170'><img src='"+goHPEP.images+"hpweb_1-2_prnt_icn.gif' width='19' height='13 alt='' border='0'><b><a target='"+goHPEP.printableTarget+"' href='"+goHPEP.printPageURL+"'>"+goHPEP.printLabel+"</a></b></td><td width='10'></td><td width='560'><img src='"+goHPEP.images+"s.gif' width='1' height=13' alt=''border='0'></td></tr></table>";document.writeln(printFoot);
    }
    var sFooter="<table width='100%' border=0 cellspacing=0 cellpadding=2><tr><td><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td><td width='100%'><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td><td><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td></tr><tr><td><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td><td class='footerSkin' width='100%' valign='top' align='middle'>";sFooter+="<a href='javascript:_HPEPOpenWindow(\""+goHPEP.privacyPage+"\",\""+goHPEP.privacyLabel+"\");'>Privacy Statement</a>&nbsp;|&nbsp;<a href='javascript:_HPEPOpenWindow(\""+goHPEP.termsOfUsagePage+"\",\""+goHPEP.termsOfUseLabel+"\");'>Terms of Use</a>&nbsp;|&nbsp;";
    if (goHPEP.feedbackPage.length > 0)
    {
    sFooter+="<a href='"+goHPEP.feedbackPage+"'";
    if(goHPEP.feedbackTarget.length > 0)
    {
    sFooter+=" target='"+goHPEP.feedbackTarget+"'";
    }
    if (goHPEP.enablePortalImage)
    {
    sFooter+="onClick=\"siteCatalyst_sendLinkMetrics('"
    +goHPEP.feedbackLabel+"','"
    +goHPEP.feedbackLabel+"','"
    +goHPEP.feedbackPage+"');\""
    }
    sFooter+=">"+goHPEP.feedbackLabel+"</a>&nbsp;|&nbsp;";
    }
    if (goHPEP.supportPage.length > 0)
    {
    sFooter+="<a href='"+goHPEP.supportPage+"'";
    if(goHPEP.supportTarget.length > 0)
    {
    sFooter+=" target='"+goHPEP.supportTarget+"'";
    }
    if (goHPEP.enablePortalImage)
    {
    sFooter+="onClick=\"siteCatalyst_sendLinkMetrics('"
    +goHPEP.supportLabel+"','"
    +goHPEP.supportLabel+"','"
    +goHPEP.supportPage+"');\""
    }
    sFooter+=">"+goHPEP.supportLabel+"</a>&nbsp;|&nbsp;";
    }
    if (goHPEP.versionNumber !="" )
    sFooter+=goHPEP.versionNumber+"&nbsp;|&nbsp;"
    if (goHPEP.pageRevision !="" )
    sFooter+=goHPEP.pageRevision+"&nbsp;|&nbsp;"
    sFooter+=goHPEP.securityLabel+"<br><div class='copyright'>"+goHPEP.copyright+"</div>";sFooter+="</td><td><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td></tr><tr><td><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td><td width='100%'><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td><td><img src='"+goHPEP.images+"spacer.gif' width=1 height=1></td></tr></table>";document.writeln(sFooter);
    }

function _HPEPToggleNavExpandingList(sButton, sItem)
    {
    var oButton=document.getElementById(sButton);
    var oItem=document.getElementById(sItem);
    if ((oItem.style.display=="") || (oItem.style.display=="none"))
    {
    oItem.style.display="block";oButton.src=eval("minus.src");
    }
    else
    {
    oItem.style.display="none";oButton.src=eval("plus.src");
    }
    return false;
    }

function _HPEPBuildMenus()
    {
    var aMenu;for(var i=0; i < goMenu.length; i++)
    {
    aMenu=goMenu[i];switch (aMenu[0])
    {
    case "Level1":
    _HPEPCreateMenu(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5]);break;case "Level2":
    if (goHPEP.menuDropDown)
    _HPEPAddMenuItem(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5]);else
    _HPEPAddHorMenuItem(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5]);break;case "Separator":
    if (goHPEP.menuDropDown)
    _HPEPAddMenuSeparator(aMenu[1], aMenu[2]);break;case "Level3 - AddNavLink":
    _HPEPAddNavLink(aMenu[1], aMenu[2], aMenu[3], aMenu[4]);break;case "Level3 - AddNavSpacer":
    goNav.leftNavString+="<tr><td colspan=2 height="+aMenu[1]+">&nbsp;</td></tr>";break;case "Level3 - AddNavImage":
    _HPEPAddNavImage(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5]);break;case "Level3 - addNavExpandingListSubItem":
    _HPEPAddNavExpandingListItem(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5]);break;case "Level3 - addNavExpandingListItem":
    _HPEPAddNavExpandingListItem(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5]);break;case "Level3 - addNavLabel":
    goNav.leftNavString+=
    "<tr><td valign=top width=100% colspan=2><div class=clsTocHead style='cursor:default;color:"+goNav.foreground+";margin-left:2px;margin-right:2px;font-family:Verdana;font-size:10px;font-weight:bold;'>"+aMenu[1]+"</div></td></tr>";break;case "Level3 - addNavImageLink":
    _HPEPAddNavImageLink(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5]);break;case "Level3 - addNavSeparator":
    goNav.leftNavString+=newLineChar+"<tr><td colspan=2><hr style='margin-left:1px;margin-right:1px;width:100%;height:2px;border-style:outset;border-right-width:0px;border-left-width:0px;background-color:"+goNav.hiliteBackground+";color:"+goNav.foreground+"'></td></tr>";break;case "Level3 - createNavExpandingList":
    _HPEPCreateNavExpandingList(aMenu[1], aMenu[2], aMenu[3], aMenu[4], aMenu[5], aMenu[6]);break;
    }

    }
    goMenu=new Array();
    }

function _HPEPDrawHorizontalNavigation()
    {
    var sHTML;sHTML=goNav.menuString1;
    if (goHPEP.menuDropDown)
    {
    sHTML+="<div id='popmenu' class='nav2Skin' onMouseover=\"_HPEPClearHideMenuTimeout();_HPEPHighlightNav2Menu(event,'on', goNav);\" onMouseout=\"_HPEPHighlightNav2Menu(event,'off', goNav);_HPEPDynamicHide(event);\"></div><table width='100%' border=0 cellspacing=0 cellpadding=0><tr><td width=5><img src='"+goHPEP.images+"spacer.gif' height=10 width=5 border=0></td><td class='greetingMsg' align='right'>"+goNav.breadCrumbLabel+"</td><td width=5><img src='"+goHPEP.images+"spacer.gif' height=10 width=5 border=0></td></tr></table>"+goNav.menuString;
    }
    else
    {
    sHTML+="<table width='100%' cellspacing=0 cellpadding=0 border=0><tr><td><img src='"+goHPEP.images+"spacer.gif' height=21 width=5 border=0></td><td width='100%'><div id='popmenu' class='hNav2Skin' onMouseover=\"_HPEPClearHideMenuTimeout();_HPEPHighlightNav2Menu(event,'on', goNav);\" onMouseout=\"_HPEPHighlightNav2Menu(event,'off', goNav);_HPEPDynamicHide(event);\"></div></td><td><img src='"+goHPEP.images+"spacer.gif' height=21 width=5 border=0></td></tr></table><table width='100%' border=0 cellspacing=0 cellpadding=0><tr><td width=5><img src='"+goHPEP.images+"spacer.gif' height=10 width=5 border=0></td><td width=5><img src='"+goHPEP.images+"spacer.gif' height=10 width=5 border=0></td></tr></table>"+goNav.menuString;
    }
    document.write(sHTML);
    if (!goHPEP.menuDropDown)
    {
    var oBreadCrumbNav2Menu=document.getElementById(goNav.breadCrumbMenuID);
    if (oBreadCrumbNav2Menu)
    {
    oNav2Menu=ie4? document.all.popmenu : document.getElementById("popmenu");oNav2Menu.thestyle=oNav2Menu.style;oNav2Menu.innerHTML=oBreadCrumbNav2Menu.innerHTML;oNav2Menu.thestyle.visibility="visible";
    }

    }
    goHPEP.bannerDrawn=true;
    }

function _HPEPDrawHeader()
    {
    var sHTML;sHTML="";goHPEP.bannerDrawn=true;
    if (goHPEP.enableFullCSS)
    {
    document.write("<link rel='stylesheet' type='text/css' href='"+goHPEP.libPath+"css/header_body.css'>");
    }
    sHTML+="<table class='TopTableProps' border='0' cellspacing='0' cellpadding='0'><tr valign='top' class='FirstRowColor'><td colspan='3'><table border='0' cellpadding='0' cellspacing='0' class='Row1TableProps'><tr><td valign='top' style='padding-left:10px;font-size:"+parentFontStyle+";'><a href='"+goHPEP.homePage+"' style='color:#FFFFFF;font-size:0.9em' class='WhiteHeaderMediumGN' target='_top' onclick='_HPEPSymbolClick(event,this);'>@hp&nbsp;Home</a></td><td align='right' style='padding-right:11px;'>";sHTML+=_HPEPCreateQuickLinksMenu();sHTML+="</td><td class='BG_White_FF' width='1'><img src='"+goHPEP.images+"spacer.gif' width='1'></td></tr></table><td align='right' valign='middle' style='padding-right:10px;'>";
    if (goHPEP.enableMyLinks)
    {
    sHTML+="<a href='javascript:doMyLinks();' style='color:#FFFFFF;' class='WhiteTextSmall'>Add My Link</a>";
    }
    if ( goHPEP.enableMyLinks && ( goHPEP.loggedIn || goHPEP.loginEnabled ))
    {
    sHTML+=" <span style='color:#FFFFFF;' class='WhiteTextSmall' >|</span> ";
    }
    else
    {
    sHTML+="";
    }
    if (goHPEP.loggedIn)
    {
    sHTML+="<a href='"+g_LogOffURL+"' target='_top' style='color:#FFFFFF;' class='WhiteTextSmall'>Log Off</a>";
    }
    else if (goHPEP.loginEnabled)
    {
    sHTML+="<a href='"+g_LogOnURL+"' target='_top' style='color:#FFFFFF;' class='WhiteTextSmall'>Log On</a>";
    }
    else
    {
    sHTML+="";
    }
    sHTML+="</td></tr><tr><td colspan='4' width='100%' class='BG_White_FF'><img src='"+goHPEP.images+"spacer.gif' width='1px'></td></tr><tr><td valign='top' width='645px' class='bannerSkin'>";sHTML+=_HPEPCreateGlobalNavMenu();sHTML+="</td><td width='121px' valign='top' align='right' style='padding-top:7px' class='bannerSkin'><a href='"+goHPEP.hpComPage+"' target='_top' onClick='_HPEPImageClick(this)'><img src='"+goHPEP.images+"hp_logo.gif' border=0 alt='hp.com' title='hp.com' height='28' width='47'></a></td><td  width='1px' class='BG_White_FF'><img src='"+goHPEP.images+"spacer.gif' width='1' height='1'></td><td width='225px' valign='middle' align='right' style='padding-right:10px;padding-top:3px;padding-left:10px' class='bannerSkin'><table border='0' cellspacing='0' cellpadding='0'><tr><td valign='bottom' align='right' style='padding-right:4px;'><a href='"+goHPSearchMenu.searchPeopleFinderPage+"' target='_top' style='color:#FFFFFF; font-size:"+parentFontStyle+";' class='LinkWhite'>PeopleFinder:</a></td><form name='pfsearch' target='_top' id='PeopleFinderForm' action='"+goHPSearchMenu.searchPeopleFinderPage+"'><input type='hidden' name='pf_hp' value=1><input type='hidden' name='pf_detectsearch' value=1><input type='hidden' name='pf_searchoption' value=0><input type='hidden' name='pf_searchtype' value=0>";
    if (goHPEP.usePeopleFinderQuestField)
    {
    sHTML+="<td valign='bottom'><input type='hidden' name='pf_searchval' id='InputPF' value=''>"+createPeopleFinderQuestField(140,19,'#333333')+"</td>";
    }
    else
    {
    sHTML+="<td valign='top'><table border=0 cellspacing=0 cellpadding=0><tr><td valign='top'><input class='TextFields' type='text' maxlength='255' size=15 autocomplete='on' name='pf_searchval'></td><td><img src='"+goHPEP.images+"spacer.gif' width='3px' border=0></td><td valign='center' align='right'><input type='image' src='"+goHPEP.images+"btn_go_hpr.gif' width='17' height='18' alt='Submit PeopleFinder Search'></td></tr></table></td>";
    }
    sHTML+="</form></tr><tr><td valign='top' align='right' style='padding-top:5px;padding-right:4px;'><a href='"+goHPSearchMenu.searchHPPage+"' target='_top' style='color:#FFFFFF; font-size:"+parentFontStyle+";' class='LinkWhite'>Search:</a></td><td style='padding-top:2px'><table border='0' cellspacing=0 cellpadding=0 ><form name='frmSiteSearch' id='FormSiteSearch' target='_top' action='"+goHPSearchMenu.searchHPPage+"' onsubmit='_HPEPGoButton(\"frmSiteSearch\")' accept-charset='"+g_SearchCharSet+"'><tr><td valign='top' style='padding-bottom:1px'><input class='TextFields' type='text' maxlength=255 size=15 autocomplete='on' name='query'><input type='hidden' name='charset' value='"+g_SearchCharSet+"'><input type='hidden' name='sid' value='"+goHPSearchMenu.siteSearchSID+"'></td><td style='padding-bottom:1px'><img src='"+goHPEP.images+"spacer.gif' width='3px' border=0></td><td valign='center' align='right' style='padding-bottom:1px'><input id='searchSubmitImage' type=image src='"+goHPEP.images+"btn_go_hpr.gif' width='17' height='18' align='middle' title='Submit Intranet Search' ALT='Submit Intranet Search' value='>>'></td></tr><tr><td colspan='3' valign='middle' style='text-align:left' class='WhiteTextSmall'>";
    if (goHPSearchMenu.siteSearchEnabled)
    {
    sHTML+="<input id='siteSearch1' name='sType' type='radio' value=1 class='RadioButton' onclick='_HPEPChangeSubmitButtonAltValue()'><label for='siteSearch1'>"+
    goHPSearchMenu.siteSearchCaption+"</label>&nbsp;<input id='siteSearch2' name='sType' type='radio' checked value=2 class='RadioButton' onclick='_HPEPChangeSubmitButtonAltValue()'><label for='siteSearch2'>Intranet</label>";
    }
    else
    {
    sHTML+="&nbsp;";
    }
    sHTML+="</td></tr></form></table></td></tr></table></td></tr></table>";document.write(sHTML);
    }

function _HPEPSymbolClick(e, anchorTagObject)
    {
    if(ie4)
    {
    if (window.event.altKey)
    {
    alert("Banner Server Name: "+g_BannerServerName+"\n Banner Version: "+g_Build);anchorTagObject.href=goHPEP.libPath+"release_notes.htm";
    }

    }
    else
    {
    if ( e.altKey || e.shiftKey )
    {
    alert("Banner Server Name: "+g_BannerServerName+"\n Banner Version: "+g_Build);_HPEPNewLocation( goHPEP.libPath+"release_notes.htm");
    }

    }

    }

function _HPEPImageClick()
    {
    if (window.event.altKey)
    {
    var sPath=goHPEP.pageProtocol+window.location.hostname+"/portal/athp_showheader.jsp"
    window.location.href=sPath;
    }

    }

function _HPEPDrawMessages()
    {
    var sHTML;
    var sMsg="";
    if ((goHPEP.warningMsgGlobal !="") || (goHPEP.warningMsg !="") || (goHPEP.greetingMsg !="") || (goHPEP.bannerName !=""))
    {
    if (goHPEP.warningMsgGlobal !="")
    {
    sMsg=goHPEP.warningMsgGlobal;
    }
    if (goHPEP.warningMsg !="")
    {
    if (sMsg !="")
    {
    sMsg+=" | ";
    }
    sMsg+=goHPEP.warningMsg;
    }
    sHTML="<table width='100%' border=0 cellpadding='0' cellspacing='0' style='margin-left:17px;width:98%'><tr><td colspan='2'><img src='"+goHPEP.images+"spacer.gif' height=3 width=1 border=0></td></tr><tr><td colspan='2' style='font-size:"+parentFontStyle+";'>"+goHPEP.bannerName+"</td></tr><tr><td colspan='2'><img src='"+goHPEP.images+"spacer.gif' height=3 width=1 border=0></td></tr><tr><td valign='top' class='warningMsg' style='padding-bottom:5px;padding-top:4px;font-size:"+parentFontStyle+";'>"+sMsg+"</td><td valign='bottom' align='right' class='greetingMsg' style='padding-right:12px;padding-bottom:5px;padding-top:4px;font-size:"+parentFontStyle+";'>"+goHPEP.greetingMsg+"</td></tr></table>";document.write(sHTML);
    }

    }

function _HPEPSetBreadCrumbMappedItem( sMenuDefObject )
    {
    var sSubMenuURL;
    var nIndex;
    var nLength;
    var bFound=false;
    var arrBreadCrumbItem;
    var obj=eval(sMenuDefObject);
    if ((obj.breadCrumbNav1=="") &  (arrBreadCrumbMapItem !=null))
    {
    nLength=arrBreadCrumbMapItem.length;for(nIndex=0; nIndex < nLength; nIndex++)
    {
    arrBreadCrumbItem=arrBreadCrumbMapItem[nIndex];sSubMenuURL=arrBreadCrumbItem[0];
    if (unescape(location.href)==sSubMenuURL || unescape(location.pathname)==sSubMenuURL)
    {
    bFound=true;break;
    }

    }

    }
    if (bFound)
    {
    nLength=arrBreadCrumbItem.length;for (nIndex=1; nIndex < nLength; nIndex++)
    {
    if (nIndex==1)
    {
    obj.breadCrumbNav1=arrBreadCrumbItem[nIndex];obj.breadCrumbLabel=obj.breadCrumbNav1;
    }
    else
    {
    obj.breadCrumbLabel+=" > "+arrBreadCrumbItem[nIndex];
    }
    if (nIndex==2)
    {
    obj.breadCrumbNav2=arrBreadCrumbItem[nIndex];
    }

    }

    }

    }

function _HPEPStripStyleTags(sBanner)
    {
    var regExp1=new RegExp("<[/]*[ ]*span[^>]*>","g");
    var regExp2=new RegExp("<[/]*[ ]*font[^>]*>","g");
    var regExp3=new RegExp("<[/]*[ ]*div[^>]*>","g");sBanner=sBanner.replace(regExp1,"");sBanner=sBanner.replace(regExp2,"");sBanner=sBanner.replace(regExp3,"");return sBanner;
    }

function _HPEPChangeSubmitButtonAltValue()
    {
    if ( document.getElementById( 'siteSearch2' ).checked )
    {
    document.getElementById('searchSubmitImage').alt="Submit Intranet Search";document.getElementById('searchSubmitImage').title="Submit Intranet Search";
    }
    else
    {
    document.getElementById('searchSubmitImage').alt="Search This Site";document.getElementById('searchSubmitImage').title="Search This Site";
    }

    }

function _HPEPForceReload()
    {
    if (_HPEPGetCookie("ATHP_FORCE_PAGE_RELOAD")==1)
    {
    _HPEPSetSessionCookie("ATHP_FORCE_PAGE_RELOAD", 0);location.reload(true);
    }

    }

function _HPEPIsRemembered()
    {
    var sCode=_HPEPGetCookie("ATHP_COOKIE");
    if (sCode !=null)
    {
    sCode=decodeURI(sCode);
    var location=sCode.indexOf("userID=");
    if (location !=-1 )
    {
    return true;
    }

    }
    return false;
    }

function _HPEPGetAthpCookieAttribute(sCookieAttr)
    {
    var sCookieVal=_HPEPGetCookie("ATHP_COOKIE");
    if (sCookieVal !=null)
    {
    sCookieVal=decodeURI(sCookieVal);
    var location=sCookieVal.indexOf(sCookieAttr+"=");
    var sVal=sCookieVal.substring(location, sCookieVal.length);
    var start=sCookieAttr.length+1;
    var end=-1;end=sVal.indexOf("|");
    if ( end <=0 )
    {
    //end of string
    end=sCookieVal.length;
    }
    if (location !=-1 )
    {
    return sVal.substring(start, end);
    }

    }
    return null;
    }

function _HPEPCreateGlobalNavMenu()
    {
    var menuHTML="";
    var menuHTMLStart="";
    var menuHTMLEnd="";
    if ( globalNavItemsFlag )
    {
    menuHTMLStart="<table  width='100%' border='0' cellspacing='0' cellpadding='0'><form name='NavigationLinksForm' method='post' action=''><tr><td valign='top' class='GlobalNavButtonMargin'>";menuHTMLEnd="</td></tr></form></table>";
    }
    menuHTML=menuHTMLStart+menuHTMLItems+menuHTMLEnd;return menuHTML;
    }

function _HPEPCreateQuickLinksMenu()
    {
    var quickLinksHTML="";
    var quickLinksHTMLStart="";
    var quickLinksEnd="";
    if ( quickLinksFlag )
    {
    quickLinksHTMLStart="<select id='gNQuickLinks' name='gNSelect' style='font-size:7pt' class='QuickLinks' onchange='_HPEPNewLocation(this.options[selectedIndex].value)'><option selected>Key Links</option>";quickLinksEnd="</select>";
    }
    quickLinksHTML=quickLinksHTMLStart+quickLinksItems+quickLinksEnd;return quickLinksHTML;
    }

function _HPEPAddGlobalNavMenuItem(itemName, itemValue, itemWidth, itemURL)
    {
    var menuHTMLItemsCode="";menuHTMLItemsCode+="<input name='"+itemName+"' type='button' style='font-size:"+parentFontStyle+";' class='NavigationButtons' value='"+itemValue+"' style='width:"+itemWidth+"em' onclick=_HPEPNewLocation('"+itemURL+"')>&nbsp;&nbsp;";menuHTMLItems+=menuHTMLItemsCode;
    }

function _HPEPAddQuickLinksMenuItem( optionID, optionName, optionURL)
    {
    var qLinksItemsCode="";qLinksItemsCode+="<option id='"+optionID+"' value='"+optionURL+"'>"+optionName+"</option>";quickLinksItems+=qLinksItemsCode;
    }

function _HPEPNewLocation(url)
    {
    if ( url=='')
    {

    }
    else
    {
    window.open(url,"_top");
    }

    }

function drawPrintLink(statement)
    {
    if(statement==null)
    {
    statement="";
    }
    if (printable)
    {
    var printLink="<table align='center' border='0' cellpadding='0' cellspacing='0' width='960' height='20'><tr><td width='80'><img src='"+goHPEP.images+"s.gif' width='1' height=20' alt=''border='0'></td><td width='600' align='center'>"+statement+"</td><td align='right' valign='top' width='200'><img src='"+goHPEP.images+"hpweb_1-2_prnt_icn.gif' width='19' height='13 alt='' border='0'><b><a target='"+goHPEP.printableTarget+"' href='"+goHPEP.printPageURL+"'>"+goHPEP.printLabel+"</a></b></td></tr></table>";document.writeln(printLink);
    }

    }

function setBreadCrumbs()
    {
    var nIndex;
    var nLength=setBreadCrumbs.arguments.length;for (nIndex=0; nIndex < nLength; nIndex++)
    {
    if (nIndex==0)
    {
    if((setBreadCrumbs.arguments[nIndex] !=null) && !(setBreadCrumbs.arguments[nIndex]==""))
    {
    goNav.breadCrumbNav1=setBreadCrumbs.arguments[nIndex];goNav.breadCrumbLabel=goNav.breadCrumbNav1;
    }

    }
    else
    {
    if((setBreadCrumbs.arguments[nIndex] !=null) && !(setBreadCrumbs.arguments[nIndex]==""))
    {
    goNav.breadCrumbLabel+=" > "+setBreadCrumbs.arguments[nIndex];
    }

    }
    if (nIndex==1)
    {
    if((setBreadCrumbs.arguments[nIndex] !=null) && !(setBreadCrumbs.arguments[nIndex]==""))
    {
    goNav.breadCrumbNav2=setBreadCrumbs.arguments[nIndex];
    }

    }
    else
    {
    if((setBreadCrumbs.arguments[nIndex] !=null) && !(setBreadCrumbs.arguments[nIndex]==""))
    {
    goNav.breadCrumbNav3=setBreadCrumbs.arguments[nIndex];
    }

    }

    }

    }

function setTargetFrame(sFrame)
    {
    if (!goHPEP.feedbackTarget.length > 0)
    goHPEP.feedbackTarget=sFrame;
    if (!goHPEP.supportTarget.length > 0)
    goHPEP.supportTarget=sFrame;
    }

function setLocalSupportPage(sURL)
    {
    goHPEP.supportPage=sURL;
    }

function setFooterFeedbackPage(sURL)
    {
    goHPEP.feedbackPage=sURL;
    }

function setFooterHelpPage(sURL)
    {
    setLocalSupportPage(sURL);
    }

function setPageConfidential()
    {
    goHPEP.securityLabel="HP Confidential";
    }

function setPagePublic()
    {
    goHPEP.securityLabel="HP Public";
    }

function setPagePrivate()
    {
    goHPEP.securityLabel="HP Private";
    }

function setPrinterLink(printURL, target)
    {
    goHPEP.printPageURL=printURL;
    if ((target==null) || (target==""))
    {
    goHPEP.printableTarget="_top";
    }
    else
    {
    goHPEP.printableTarget=target;
    }
    printable=true;
    }

function drawFooter()
    {
    if (goHPEP.footerDrawn)
    return;goHPEP.footerDrawn=true;_HPEPCreateFooter();
    if(goHPEP.webMetrics)
    siteCatalyst_sendMetrics();
    }

function drawHPFooter()
    {
    drawFooter();
    }

function drawSeparator(iHeight, sColor)
    {
    document.writeln("<table border=0 cellspacing=0 cellpadding=0 width=\"100%\"><tr><td bgcolor=\""+sColor+"\"><table border=0 cellspacing=0 cellpadding=0><tr><td height=\""+iHeight+"\"></td></tr></table></td></tr></table>");
    }

function setVersionNumber(sVersionNumber)
    {
    goHPEP.versionNumber="Site Version "+sVersionNumber;
    }

function setPortalVersionNumber(sVersionNumber)
    {
    goHPEP.versionNumber="@hp Portal Version "+sVersionNumber;
    }

function addNavSpacer( iHeight )
    {
    var aMenu=new Array(2);goHPEP.enableLeftNavMenu=true;aMenu[0]="Level3 - AddNavSpacer";aMenu[1]=iHeight;goMenu.push(aMenu);
    }

function addNavImage(sSubImage, sSubMenuURL, sSubMenu)
    {
    var sTarget="_top";
    var aMenu=new Array(5);goHPEP.enableLeftNavMenu=true;
    if (addNavImage.arguments.length > 3)
    sTarget=addNavImage.arguments[3];aMenu[0]="Level3 - AddNavImage";aMenu[1]=sSubImage;aMenu[2]=sSubMenuURL;aMenu[3]=sSubMenu;aMenu[4]=sTarget;aMenu[5]="goNav";goMenu.push(aMenu);
    }

function addNavExpandingListSubItem(sMenuID, sSubMenu, sSubMenuURL)
    {
    var all=document.getElementsByTagName("*");
    var sTarget="_top";
    var isFirstNav;
    var aMenu=new Array(6);
    if (addNavExpandingListSubItem.arguments.length > 3)
    sTarget=addNavExpandingListSubItem.arguments[3];isFirstNav=eval("all.ITEMS_EL_"+sMenuID);
    if (isFirstNav)
    {
    if(!isFirstNav.length)
    sMenuID+=1;else
    sMenuID+=isFirstNav.length;
    }
    aMenu[0]="Level3 - addNavExpandingListSubItem";aMenu[1]="EL_"+sMenuID;aMenu[2]=sSubMenu;aMenu[3]=sSubMenuURL;aMenu[4]=sTarget;aMenu[5]="goNav";goMenu.push(aMenu);
    }

function addNavLabel(sSubMenu)
    {
    var aMenu=new Array(2);goHPEP.enableLeftNavMenu=true;aMenu[0]="Level3 - addNavLabel";aMenu[1]=sSubMenu;goMenu.push(aMenu);
    }

function addNavImageLink(sSubImage, sSubMenuURL, sSubMenu)
    {
    var sTarget="_top";
    var aMenu=new Array(5);goHPEP.enableLeftNavMenu=true;
    if (addNavImageLink.arguments.length > 3)
    sTarget=addNavImageLink.arguments[3];aMenu[0]="Level3 - addNavImageLink";aMenu[1]=sSubImage;aMenu[2]=sSubMenuURL;aMenu[3]=sSubMenu;aMenu[4]=sTarget;aMenu[5]="goNav";goMenu.push(aMenu);
    }

function drawLeftNavigation()
    {
    if ((!ToolBar_Supported) &&(!goHPEP.enableLeftNavMenu))
    {
    return;
    }
    if(goMenu.length > 0)
    _HPEPBuildMenus();
    var sHTML="";sHTML="<table class='leftNavSkin' cellspacing=0 cellpadding=0 border=0 width='151'><tr><th class='leftNavHeader'><img src='"+goHPEP.images+"spacer.gif' width=1 height=6></th><th class='leftNavHeader'><img src='"+goHPEP.images+"spacer.gif' width=138 height=6></th><th class='leftNavHeader'><img src='"+goHPEP.images+"spacer.gif' width=1 height=6></th></tr><tr><td colspan=3><img src='"+goHPEP.images+"spacer.gif' width=138 height=10></td></tr><tr><td width=1><img src='"+goHPEP.images+"spacer.gif' width=1 height=20></td><td width='138' align='center'><table width='138' border=0 cellspacing=0 cellpadding=0>"+
    goNav.leftNavString+
    "</table></td><td width=1><img src='"+goHPEP.images+"spacer.gif' width=1 height=20></td></tr><tr><td colspan=3><img src='"+goHPEP.images+"spacer.gif' width=138 height=10></td></tr></table>";document.writeln(sHTML);_HPEPOpenNavExpandingLists("goNav");
    }

function addNavExpandingListItem(sMenuID, sSubMenu, sSubMenuURL)
    {
    var sTarget="_top";
    var isFirstNav;
    var all=document.getElementsByTagName("*");
    var aMenu=new Array(5);
    if (addNavExpandingListItem.arguments.length > 3)
    sTarget=addNavExpandingListItem.arguments[3];isFirstNav=eval("all.ITEMS_EL_"+sMenuID);
    if (isFirstNav)
    {
    if(!isFirstNav.length)
    sMenuID+=1;else
    sMenuID+=isFirstNav.length;
    }
    aMenu[0]="Level3 - addNavExpandingListItem";aMenu[1]="EL_"+sMenuID;aMenu[2]=sSubMenu;aMenu[3]=sSubMenuURL;aMenu[4]=sTarget;aMenu[5]="goNav";goMenu.push(aMenu);
    }

function addNavSeparator()
    {
    var aMenu=new Array(1);goHPEP.enableLeftNavMenu=true;aMenu[0]="Level3 - addNavSeparator";goMenu.push(aMenu);
    }

function createNavExpandingList(sMenuID, sMenuDisplay, sMenuURL, bDefOpen)
    {
    var isFirstNav;
    var all=document.getElementsByTagName("*");
    var aMenu=new Array(6);
    var sTarget="_top";goHPEP.enableLeftNavMenu=true;
    if (createNavExpandingList.arguments.length > 4)
    sTarget=createNavExpandingList.arguments[4];isFirstNav=eval("all.ITEMS_EL_"+sMenuID);
    if (isFirstNav)
    {
    if(!isFirstNav.length)
    {
    sMenuID+=1;
    }
    else
    {
    sMenuID+=isFirstNav.length;
    }

    }
    aMenu[0]="Level3 - createNavExpandingList";aMenu[1]="EL_"+sMenuID;aMenu[2]=sMenuDisplay;aMenu[3]=sMenuURL;aMenu[4]=bDefOpen;aMenu[5]=sTarget;aMenu[6]="goNav";goMenu.push(aMenu);
    }

function setWarningMessage(sMsg)
    {
    goHPEP.warningMsg=sMsg;
    }

function setGlobalWarningMessage(sMsg)
    {
    goHPEP.warningMsgGlobal=sMsg;
    }

function setGreetingMessage(sMsg)
    {
    goHPEP.greetingMsg=sMsg;
    }

function addNavLink(sSubMenu, sSubMenuURL)
    {
    var sTarget="_top";
    var aMenu=new Array(4);goHPEP.enableLeftNavMenu=true;
    if (addNavLink.arguments.length > 2)
    {
    sTarget=addNavLink.arguments[2];
    }
    aMenu[0]="Level3 - AddNavLink";aMenu[1]=sSubMenu;aMenu[2]=sSubMenuURL;aMenu[3]=sTarget;aMenu[4]="goNav";goMenu.push(aMenu);
    }

function addMenuSeparator(sMenuID)
    {
    if (goHPEP.menuDropDown)
    {
    var aMenu=new Array(3);aMenu[0]="Separator";aMenu[1]="LD_"+sMenuID+"_MENU";aMenu[2]="goNav";goMenu.push(aMenu);
    }

    }

function addMenuItem(sMenuID, sSubMenu, sSubMenuURL)
    {
    var sTarget="_top";
    var aMenu=new Array(6);
    if (addMenuItem.arguments.length > 3)
    {
    sTarget=addMenuItem.arguments[3];
    }
    aMenu[0]="Level2";aMenu[1]="LD_"+sMenuID+"_MENU";aMenu[2]='goNav';aMenu[3]=sSubMenu;aMenu[4]=sSubMenuURL;aMenu[5]=sTarget;goMenu.push(aMenu);
    }

function addBreadCrumbMapItem()
    {
    var nIndex;
    var nLength=addBreadCrumbMapItem.arguments.length;
    var arrItem=new Array();
    if (arrBreadCrumbMapItem==null)
    {
    arrBreadCrumbMapItem=new Array();
    }
    for (nIndex=0; nIndex < nLength; nIndex++)
    {
    arrItem[nIndex]=addBreadCrumbMapItem.arguments[nIndex];
    }
    nIndex=arrBreadCrumbMapItem.length;arrBreadCrumbMapItem[nIndex]=arrItem;
    }

function drawBanner()
    {
    _HPEPForceReload()
    if (goHPEP.bannerDrawn)
    return;_HPEPSetBreadCrumbMappedItem("goNav");_HPEPDrawHeader();_HPEPDrawMessages();
    }

function drawHeader()
    {
    drawBanner();_HPEPBuildMenus();
    if ((ToolBar_Supported) && (goHPEP.enableNav1Menu))
    {
    _HPEPDrawHorizontalNavigation();
    }

    }

function displayLogin(sURL)
    {
    if (sURL !=null && sURL !="")
    g_LogOnURL=sURL;
    if (displayLogin.arguments.length > 1)
    g_LogOffURL=displayLogin.arguments[1];goHPEP.loginEnabled=true;
    }

function createMenu(sMenuID, sMenuDisplay)
    {
    var sTarget="_top";
    var sMenuURL="";
    var aMenu=new Array(6);goHPEP.enableNav1Menu=true;
    if (createMenu.arguments.length > 2)
    sMenuURL=createMenu.arguments[2];
    if (createMenu.arguments.length > 3)
    sTarget=createMenu.arguments[3];aMenu[0]="Level1";aMenu[1]="LD_"+sMenuID+"_MENU";aMenu[2]='goNav';aMenu[3]=sMenuDisplay;aMenu[4]=sMenuURL;aMenu[5]=sTarget;goMenu.push(aMenu);
    }

function setHorizontalNav()
    {
    goHPEP.menuDropDown=false;
    }

function setBanner(sBanner)
    {
    sBanner=_HPEPStripStyleTags(sBanner)
    var title_array=sBanner.split("<br>");
    if(title_array.length<2)
    title_array=sBanner.split("<br/>");
    if(title_array.length<2)
    title_array=sBanner.split("<BR>");
    if(title_array.length<2)
    title_array=sBanner.split("<BR/>");setMultiLineBanner(title_array[0],title_array[1],title_array[2]);
    }

function setMultiLineBanner(sMjrBanner,sMnrBanner,sOthBanner)
    {
    sMnrBanner=( ( ( sMnrBanner==null ) || (sMnrBanner=='') )?'' :'&nbsp;-&nbsp;'+sMnrBanner );sOthBanner=( ( ( sOthBanner==null ) || (sOthBanner=='') ) ?'' :'&nbsp;-&nbsp;'+sOthBanner );goHPEP.bannerName="<span class='SiteTitle' style='margin-left:0px'>"+sMjrBanner+"</span><span class='SiteTitleMd'>"+sMnrBanner+"</span><span class='SiteTitleSml'>"+sOthBanner+"</span>";
    }

function setSearchCharSet(charset)
    {
    g_SearchCharSet=charset;
    }

function setPortalImage()
    {
    goHPEP.enablePortalImage=true;
    }

function setSiteSearch(sSID)
    {
    if (setSiteSearch.arguments.length > 1)
    {
    goHPSearchMenu.siteSearchCaption=setSiteSearch.arguments[1];
    }
    goHPSearchMenu.siteSearchSID=sSID;goHPSearchMenu.siteSearchEnabled=true;
    }

function setSiteSearchURL(sSrchURL)
    {
    if (setSiteSearchURL.arguments.length > 1)
    {
    goHPSearchMenu.siteSearchCaption=setSiteSearchURL.arguments[1];
    }
    goHPSearchMenu.siteSearchURL=sSrchURL;goHPSearchMenu.siteSearchSID="";goHPSearchMenu.siteSearchEnabled=true;
    }

function getCurrentDate()
    {
    var day;
    var now;now=new Date();day=now.getDay();
    var dayname;
    if (day==0) dayname="Sunday";
    if (day==1) dayname="Monday";
    if (day==2) dayname="Tuesday";
    if (day==3) dayname="Wednesday";
    if (day==4) dayname="Thursday";
    if (day==5) dayname="Friday";
    if (day==6) dayname="Saturday";
    var month
    month=now.getMonth();
    var monthname;
    if (month==0) monthname="January";
    if (month==1) monthname="February";
    if (month==2) monthname="March";
    if (month==3) monthname="April";
    if (month==4) monthname="May";
    if (month==5) monthname="June";
    if (month==6) monthname="July";
    if (month==7) monthname="August";
    if (month==8) monthname="September";
    if (month==9) monthname="October";
    if (month==10) monthname="November";
    if (month==11) monthname="December";date=now.getDate();year=now.getYear();
    if (year < 1000)
    year+=1900;return (monthname+" "+date+", "+year);
    }

function displayPageRevision()
    {
    var months=new Array(13);months[1]="Jan";months[2]="Feb";months[3]="Mar";months[4]="Apr";months[5]="May";months[6]="Jun";months[7]="Jul";months[8]="Aug";months[9]="Sep";months[10]="Oct";months[11]="Nov";months[12]="Dec";
    var oDate=new Date(document.lastModified)
    var iMonth=months[oDate.getMonth()+1]
    var iYear=oDate.getYear()
    var iDate=oDate.getDate()
    if (iYear < 1000)
    iYear=iYear+1900;goHPEP.pageRevision="Page Revision: "+iDate+" "+iMonth+" "+iYear
    }

function disableMyLinks()
    {
    goHPEP.enableMyLinks=false;
    }

function doMyLinks()
    {
    var sTitle, sURL, sKnown;sKnown=(_HPEPIsLoggedIn() || _HPEPIsRemembered()) ;sURL=escape(top.window.location.href);sTitle=escape(top.document.title);
    if ( sKnown )
    {
    _HPEPOpenWindow(g_MyLinks+"?title="+sTitle+"&url="+sURL+"&known="+sKnown, "MyLinks", "width=500,height=235,left=350,top=0");
    }
    else
    {
    _HPEPOpenWindow(g_MyLinks+"?title="+sTitle+"&url="+sURL+"&known="+sKnown, "MyLinks", "width=800,height=535,left=350,top=0");
    }

    }

function athpSetForcePageReload()
    {
    _HPEPSetSessionCookie("ATHP_FORCE_PAGE_RELOAD", 1);
    }

function setBodyStyleDisabled()
    {
    goHPEP.enableFullCSS=false;
    }

function getUserPref(attr)
    {
    if ( attr.match("PreferredLanguage") !=null )
    {
    return _HPEPGetAthpCookieAttribute("prefLang");
    }
    else if ( attr.match("PreferredCountry") !=null )
    {
    return _HPEPGetAthpCookieAttribute("prefCountry");
    }
    else if ( attr.match("PreferredViewGlobalContent") !=null )
    {
    return _HPEPGetAthpCookieAttribute("prefViewGlobalContent");
    }
    else if ( attr.match("UseDigitalBadge") !=null )
    {
    return _HPEPGetAthpCookieAttribute("useDB");
    }
    else if ( attr.match("UserID") !=null )
    {
    return _HPEPGetAthpCookieAttribute("userID");
    }
    else
    return _HPEPGetAthpCookieAttribute(attr);
    }

function createPeopleFinderQuestField(fieldWidth,fieldHeight,fieldColor)
    {
    questFieldConfig.valueForm="PeopleFinderForm";questFieldConfig.valueInput="InputPF";questFieldConfig.keyInput="InputPF";questFieldConfig.helpText="Check the <i>Enable QuestField</i> box to have the name auto-completed as you type.<BR><BR>Check the <i>Pop-Up List Automatically</i> box to have a list of names pop up automatically as you type.<br><br>For searches with a large number of results, only a limited number will be displayed. Please insert additional characters in the input field to refine your results.";questFieldConfig.helpLinkLabel="PeopleFinder Help";questFieldConfig.helpLink=goHPEP.supportPage;questFieldConfig.helpTarget="help";questFieldConfig.helpParameters="top=30,left=30,width=760,height=510";questFieldConfig.aboutText="";questFieldConfig.questFieldName="PeopleFinderNameSearch";questFieldConfig.questFieldWidth=fieldWidth;questFieldConfig.questFieldHeight=fieldHeight;questFieldConfig.submitAreaWidth=19;questFieldConfig.submitButtonWidth=17;questFieldConfig.bgColor=fieldColor;questFieldConfig.popUpWidth=400;questFieldConfig.popUpRows=18;questFieldConfig.popUpToLeft=true;return qoCreatePopUpQuestField (questFieldConfig);
    }

function setTheme(sThemeName)
    {
    return;
    }

function setCSSUseR3()
    {
    return;
    }

function setCSSDisabled()
    {
    return;
    }

function setSearchCollection(sCollection)
    {
    return;
    }

function drawWarningMsg()
    {
    return;
    }

function drawHorizontalNavigation()
    {
    return;
    }

function scriptInTable()
    {
    return;
    }

function openSelectedLink(list)
    {
    var newPage=list.options[list.selectedIndex].value
    if (newPage !="None")
    {
    window.open(newPage)
    }

    }
