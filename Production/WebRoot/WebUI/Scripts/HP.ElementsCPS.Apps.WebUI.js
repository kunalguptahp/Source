//<script language="javascript" type="text/javascript">

	/// <summary>
	/// Indicates whether a specified object/value is "undefined".
	/// </summary>
	/// <remarks>
	/// NOTE: This function was adapted from a similar one found here:
	/// http://constc.blogspot.com/2008/07/undeclared-undefined-null-in-javascript.html
	/// </remarks>
	/// <param name="x">The value to examine.</param>
	/// <returns><c>true</c> if the specified value is undefined.</returns>
	function isUndefined(x)
	{
		//return (typeof(x) == "undefined");
		return ((x == null) && (x !== null)); //is it equal (but not strictly equal) to null
	}

	//Example usage: "<body onload='adjustIFrameSize(''myFrameId'');' ..."
	function adjustIFrameSizeById(id)
	{
		var objIframe = document.getElementById(id);
		if (objIframe)
		{
			adjustIFrameSize(objIframe);
		}
	}

	//Example usage: "<iframe onload='adjustIFrameSize(this);' ..."
	function adjustIFrameSize(objIframe)
	{
		if (objIframe)
		{
			adjustIFrameHeightToContent(objIframe);
			//NOTE: adjusting the width isn't working as well as I would like yet (due to width/height autosizing interactions).
			//adjustIFrameWidthToContent(objIframe);

			// bind onload events to iframe
			if (objIframe.addEventListener)
			{
				objIframe.addEventListener("load", resizeIframe, false);
			}
			else
			{
				objIframe.attachEvent("onload", resizeIframe);
			}
		}
	}

	function resizeIframe(evt)
	{
		evt = (evt) ? evt : event;
		var target = (evt.target) ? evt.target : evt.srcElement;
		// take care of W3C event processing from iframe's root document
		if (target.nodeType == 9)
		{
			if (evt.currentTarget && evt.currentTarget.tagName.toLowerCase( ) == "iframe")
			{
				target = evt.currentTarget;
			}
		}
		if (target)
		{
			adjustIFrameSize(target.id);
		}
	}

	//Example usage: "<body onload='adjustIFrameHeight(''myFrameId'');' ..."
	function adjustIFrameHeightById(id)
	{
		var objIframe = document.getElementById(id);
		if (objIframe)
		{
			adjustIFrameHeight(objIframe);
		}
	}

	//Example usage: "<iframe onload='adjustIFrameHeight(this);' ..."
	function adjustIFrameHeight(objIframe)
	{
		if (objIframe)
		{
			adjustIFrameHeightToContent(objIframe);

			// bind onload events to iframe
			if (objIframe.addEventListener)
			{
				objIframe.addEventListener("load", resizeIframeHeight, false);
			}
			else
			{
				objIframe.attachEvent("onload", resizeIframeHeight);
			}
		}
	}

	function adjustIFrameHeightToContent(objIframe)
	{
		if (objIframe)
		{
			var maxHeight = Number.POSITIVE_INFINITY;
			if (objIframe.style.maxHeight)
			{
				maxHeight = objIframe.style.maxHeight;
			}
			else if (objIframe.maxHeight)
			{
				maxHeight = objIframe.maxHeight;
			}

			if (objIframe.contentDocument && objIframe.contentDocument.body.offsetHeight)
			{
				// W3C DOM (and Mozilla) syntax
				objIframe.height = Math.min(maxHeight, objIframe.contentDocument.body.offsetHeight);
			}
			else if (objIframe.Document && objIframe.Document.body.scrollHeight)
			{
				// IE DOM syntax
				objIframe.height = Math.min(maxHeight, objIframe.Document.body.scrollHeight);
			}
		}
	}

	function resizeIframeHeight(evt)
	{
		evt = (evt) ? evt : event;
		var target = (evt.target) ? evt.target : evt.srcElement;
		// take care of W3C event processing from iframe's root document
		if (target.nodeType == 9)
		{
			if (evt.currentTarget && evt.currentTarget.tagName.toLowerCase( ) == "iframe")
			{
				target = evt.currentTarget;
			}
		}
		if (target)
		{
			adjustIFrameHeight(target.id);
		}
	}

	//Example usage: "<body onload='adjustIFrameWidth(''myFrameId'');' ..."
	function adjustIFrameWidthById(id)
	{
		var objIframe = document.getElementById(id);
		if (objIframe)
		{
			adjustIFrameWidth(objIframe);
		}
	}

	//Example usage: "<iframe onload='adjustIFrameWidth(this);' ..."
	function adjustIFrameWidth(objIframe)
	{
		if (objIframe)
		{
			adjustIFrameWidthToContent(objIframe);

			// bind onload events to iframe
			if (objIframe.addEventListener)
			{
				objIframe.addEventListener("load", resizeIframeWidth, false);
			}
			else
			{
				objIframe.attachEvent("onload", resizeIframeWidth);
			}
		}
	}

	//Example usage: "<iframe onload='adjustIFrameWidth(this);' ..."
	function adjustIFrameWidthToContent(objIframe)
	{
		if (objIframe)
		{
			var maxWidth = Number.POSITIVE_INFINITY;
			if (objIframe.style.maxWidth)
			{
				maxWidth = objIframe.style.maxWidth;
			}
			else if (objIframe.maxWidth)
			{
				maxWidth = objIframe.maxWidth;
			}

			if (objIframe.contentDocument && objIframe.contentDocument.body.offsetWidth)
			{
				// W3C DOM (and Mozilla) syntax
				objIframe.width = Math.min(maxWidth, objIframe.contentDocument.body.offsetWidth);
			}
			else if (objIframe.Document && objIframe.Document.body.scrollWidth)
			{
				// IE DOM syntax
				objIframe.width = Math.min(maxWidth, objIframe.Document.body.scrollWidth);
			}
		}
	}

	function resizeIframeWidth(evt)
	{
		evt = (evt) ? evt : event;
		var target = (evt.target) ? evt.target : evt.srcElement;
		// take care of W3C event processing from iframe's root document
		if (target.nodeType == 9)
		{
			if (evt.currentTarget && evt.currentTarget.tagName.toLowerCase( ) == "iframe")
			{
				target = evt.currentTarget;
			}
		}
		if (target)
		{
			adjustIFrameWidth(target.id);
		}
	}

//	function setIFrameWidth(objIframe, width)
//	{
//		if (objIframe)
//		{
//			if (objIframe.contentDocument && objIframe.contentDocument.body.offsetWidth)
//			{
//				// W3C DOM (and Mozilla) syntax
//				objIframe.width = width;
//			}
//			else if (objIframe.Document && objIframe.Document.body.scrollWidth)
//			{
//				// IE DOM syntax
//				objIframe.width = width;
//			}
//		}
//	}

	//</script>
