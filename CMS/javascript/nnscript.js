var timerId = null; var previousMenuItemId = null; var clicked = false; var previousControlId = null; var ROOT_ID = "00"; var POSITIONING_CHANGE = "1.2";  function displayMouseOver(menuItemId, orientation, expandDelay, itemOverClass, status, offsetX, offsetY) { 	var menuItem = document.getElementById(menuItemId); 	 	displayCommonMouseOver(menuItem, itemOverClass, status); 	 	refreshMenu(menuItem, orientation, expandDelay, offsetX, offsetY); }  function displayRootMouseOver(rootItemId, openOnClick, orientation, expandDelay, itemOverClass, status, offsetX, offsetY) {	 	var rootItem = document.getElementById(rootItemId); 	displayCommonMouseOver(rootItem , itemOverClass , status); 	checkPreviousControl(rootItem); 	 	if((openOnClick && clicked) || !openOnClick) 	{ 		refreshMenu(rootItem, orientation, expandDelay, offsetX, offsetY); 	} }  function openLink(href, target) { 	open(href, target); }  function openMenu(rootItemId, orientation, expandDelay, itemOverClass, status, offsetX, offsetY) { 	if (!clicked) 	{ 		var rootItem = document.getElementById(rootItemId); 		clicked = true; 		refreshMenu(rootItem, orientation, expandDelay, offsetX, offsetY); 	} }  function displayMouseOut(menuItemId, normalItemClass) { 	var menuItem = document.getElementById(menuItemId); 	menuItem.className = normalItemClass; 	window.status = ""; }  function refreshMenu(menuItem, orientation, expandDelay, offsetX, offsetY) { 	if(previousMenuItemId != menuItem.id) 	{ 		var parentGroup = findParentGroup(menuItem); 		hideMenu(parentGroup, menuItem); 		 		previousMenuItemId = menuItem.id; 		 		var childGroup= findChildGroup(menuItem); 			 		if(childGroup != null) 		{ 			if(childGroup.style.visibility == "hidden") 			{ 				displayMenu(menuItem.id, childGroup.id, orientation, offsetX, offsetY, expandDelay); 			} 		} 	} }  function displayMenu(menuItemId, menuGroupId, orientation, offsetX, offsetY, expandDelay) {		 	var menuGroup = document.getElementById(menuGroupId); 	 	positionLeft(menuGroup, orientation, offsetX); 	positionTop(menuGroup, orientation, offsetY); 	 	setTimeout("makeMenuVisible('" + menuGroupId + "', '" + menuItemId + "')", expandDelay);	 }  function makeMenuVisible(menuGroupId, menuItemId) { 	if(menuItemId == previousMenuItemId) 	{ 		document.getElementById(menuGroupId).style.visibility = "visible"; 	} }  function positionLeft(menuGroup, orientation, offsetX) { 	var proposedOffsetLeft; 	var previousGroup = findPreviousGroup(menuGroup); 	 	var rootContainerLeft = 0; 	 	if(!rootContainerIsControl()) 	{ 		rootContainerLeft = findRootContainerLeft(); 	} 		 	var menuWidth = menuGroup.offsetWidth; 	 	if(((orientation == 0) && isRootGroup(previousGroup)) || (orientation == 2)) 	{ 		var parentItem = findParentItem(menuGroup); 		var parentItemLeft = findParentItemLeft(previousGroup, parentItem, rootContainerLeft); 		 		proposedOffsetLeft = parentItemLeft; 		 		if(orientation == 2) 		{ 			proposedOffsetLeft += offsetX; 		} 	} 	else 	{		 		var previousWidth = previousGroup.offsetWidth; 		var previousOffsetLeft = previousGroup.offsetLeft; 	 		if(isRootGroup(previousGroup)) 		{ 			previousOffsetLeft += rootContainerLeft; 		} 			 		proposedOffsetLeft = previousOffsetLeft + previousWidth + offsetX; 	} 	menuGroup.style.left = proposedOffsetLeft; }  function findParentItemLeft(previousGroup, parentItem, rootContainerLeft) { 	var parentItemLeft = previousGroup.offsetLeft; 	 	if(isRootGroup(previousGroup)) 	{ 		parentItemLeft += rootContainerLeft; 	} 	 	var menuItems = findItems(previousGroup);  	for(var i = 0; i < menuItems.length; i++) 	{ 		if(menuItems[i] == parentItem) 		{ 			break; 		} 		else 		{ 			var currentItemWidth = parseInt(menuItems[i].offsetWidth); 			parentItemLeft += currentItemWidth; 		} 	} 	 	return parentItemLeft; }  function positionTop(menuGroup, orientation, offsetY) { 	var parentItem = findParentItem(menuGroup); 	var previousGroup = findPreviousGroup(menuGroup); 	 	var rootContainerTop=0; 	 	if(!rootContainerIsControl()) 	{ 		rootContainerTop = findRootContainerTop(); 	} 	 	var proposedOffsetTop; 	 	var menuHeight = menuGroup.offsetHeight; 	 	if(((orientation == 0) && isRootGroup(previousGroup)) || (orientation == 2)) 	{ 		var previousGroupHeight = previousGroup.offsetHeight; 		var previousGroupTop = previousGroup.offsetTop; 		 		if(isRootGroup(previousGroup)) 		{ 			previousGroupTop += rootContainerTop; 		} 		 		proposedOffsetTop = previousGroupHeight + previousGroupTop; 		 		if(orientation == 2) 		{ 			proposedOffsetTop += offsetY; 		} 	} 	else 	{ 		var parentItemTop = findParentItemTop(previousGroup, parentItem, rootContainerTop); 		 		proposedOffsetTop = parentItemTop + offsetY; 	} 	 	menuGroup.style.top = proposedOffsetTop; }  function findParentItemTop(previousGroup, parentItem, rootContainerTop) { 	var parentItemTop = previousGroup.offsetTop; 	 	if(isRootGroup(previousGroup)) 	{ 		parentItemTop += rootContainerTop; 	} 	 	var menuItems = findItems(previousGroup);  	for(var i = 0; i < menuItems.length; i++) 	{		 		if(menuItems[i] == parentItem) 		{ 			break; 		} 		else 		{ 			var currentItemHeight = menuItems[i].offsetHeight; 			parentItemTop += currentItemHeight; 		} 	} 	 	return parentItemTop; }  function rootContainerIsControl() { 	var rootGroup = findRootGroup(); 	var rootContainer = rootGroup.offsetParent; 	return(rootContainer.id == previousControlId); }  function findRootContainerLeft() { 	var rootGroup = findRootGroup(); 	var rootContainer = rootGroup.offsetParent; 	var rootContainerLeft = 0; 		 	var browserVersion = getBrowserVersion(); 	 	while(rootContainer != null) 	{		 		rootContainerLeft += rootContainer.offsetLeft; 		 		if(browserVersion < POSITIONING_CHANGE && rootContainer.border != null) 		{ 			rootContainerLeft += parseInt(rootContainer.border); 		} 			 		rootContainer = rootContainer.offsetParent; 	} 		 	return rootContainerLeft; }  function findRootContainerTop() { 	var rootGroup = findRootGroup(); 	var rootContainer = rootGroup.offsetParent; 	var rootContainerTop =0; 	 	var browserVersion = getBrowserVersion(); 	 	while(rootContainer != null) 	{		 		rootContainerTop += rootContainer.offsetTop; 		 		if(browserVersion < POSITIONING_CHANGE && rootContainer.border != null) 		{ 			rootContainerTop += parseInt(rootContainer.border); 		} 			 		rootContainer = rootContainer.offsetParent; 	} 		 	return rootContainerTop; }   function hideMenu(currentGroup, currentItem) { 	if(previousMenuItemId != null) 	{ 		var previousMenuItem = document.getElementById(previousMenuItemId); 		var previousMenuItemParent = findParentGroup(previousMenuItem); 		 		if(previousMenuItemParent == currentGroup) 		{ 			hideChildGroup(previousMenuItem); 		} 		else 		{ 			var parentItem = findParentItem(currentGroup); 			 			if(parentItem != previousMenuItem) 			{ 				hideChildGroup(previousMenuItem); 				 				hideParentGroups(previousMenuItem, currentGroup, currentItem); 			} 		} 	} }  function cancelHideAllMenus() { 	if(timerId != null) 	{ 		clearTimeout(timerId); 	} 		 	timerId = null; }  function hideAllMenus() { 	var rootGroup = findRootGroup(); 	clicked = false; 	hideMenu(rootGroup); 	previousMenuItemId = null; }  function setHideMenuTimeout(hideDelay) { 	if(timerId == null) 	{ 		timerId = setTimeout("hideAllMenus()", hideDelay); 	} }  function currentItemIsHeader(currentItem, currentGroup) { 	if(currentItem != null) 	{ 		var childGroup = findChildGroup(currentItem); 		return(childGroup == currentGroup); 	} 	else 	{ 		return false; 	} }  function hideChildGroup(previousMenuItem) { 	var childGroup = findChildGroup(previousMenuItem); 			 	if(childGroup != null) 	{ 		childGroup.style.visibility = "hidden"; 	} }  function hideParentGroups(previousMenuItem, currentGroup, currentItem) { 	var parentGroup = findParentGroup(previousMenuItem); 					 	while(parentGroup != currentGroup) 	{ 		if(!currentItemIsHeader(currentItem, parentGroup)) 		{ 			parentGroup.style.visibility = "hidden"; 		} 		 		parentGroup = findPreviousGroup(parentGroup); 	} }  function findPreviousGroup(menuGroup) { 	var menuGroupId = new String(menuGroup.id); 	var previousGroupId = menuGroupId.substring(0, menuGroupId.length - 2); 	var previousGroup = document.getElementById(previousGroupId); 	return previousGroup; }  function findParentItem(menuGroup) { 	var menuGroupId = new String(menuGroup.id); 	var parentItemId = menuGroupId.substring(0, menuGroupId.length - 2) + "_" + menuGroupId.substring(menuGroupId.length - 2, menuGroupId.length); 	var parentItem = document.getElementById(parentItemId); 	return parentItem; }  function findParentGroup(menuItem) { 	var menuItemId = new String(menuItem.id); 	var underscorePosition = menuItemId.lastIndexOf("_"); 	var parentId = menuItemId.substring(0, underscorePosition); 	var parentGroup = document.getElementById(parentId); 	return parentGroup;  }  function findChildGroup(menuItem) { 	var menuItemId = new String(menuItem.id); 	var underscorePosition = menuItemId.lastIndexOf("_"); 	var childGroupId = menuItemId.substring(0, underscorePosition) + menuItemId.substring(underscorePosition + 1, menuItemId.length); 	var childGroup = document.getElementById(childGroupId); 	return childGroup; }  function isRootGroup(menuGroup) { 	var rootGroup = findRootGroup(); 	return (menuGroup == rootGroup); }  function findRootGroup() { 	var rootGroup = document.getElementById(previousControlId + "_" + ROOT_ID); 	return rootGroup; }  function findControl(menuItem) { 	var parentGroup = findParentGroup(menuItem); 	var parentGroupId = new String(parentGroup.id); 	var underscorePosition = parentGroupId.lastIndexOf("_"); 	var controlId = parentGroupId.substring(0, underscorePosition); 	var control = document.getElementById(controlId); 	return control; }  function findItems(menuGroup) { 	var menuGroupId = menuGroup.id; 	var count = 0; 	var menuItems = new Array(); 	var currItem = document.getElementById(menuGroupId + "_00"); 	 	while(currItem != null) 	{ 		menuItems[count] = currItem; 		count ++; 		 		if(count < 10) 		{ 			currItem = document.getElementById(menuGroupId + "_0" + count); 		} 		else 		{ 			currItem = document.getElementById(menuGroupId + "_" + count); 		} 	} 	 	return menuItems; }  function resetGlobals() { 	if(previousControlId != null) 	{	 		var rootGroup = findRootGroup(); 		var control = document.getElementById(previousControlId); 		 		clicked = false; 		cancelHideAllMenus(); 		hideMenu(rootGroup); 		previousMenuItemId = null; 		 		control.style.zIndex = "998"; 	} }  function checkPreviousControl(menuItem) { 	var control = findControl(menuItem); 	 	if(previousControlId != control.id) 	{ 		resetGlobals(); 		previousControlId = control.id; 		 		control.style.zIndex="999"; 	} }  function displayCommonMouseOver(menuItem, itemOverClass, status) { 	menuItem.className = itemOverClass; 			 	if(status != null && status != "") 	{ 		window.status = status; 	} }  function getBrowserVersion() { 	var targetString = "rv:";  	var userAgent = navigator.userAgent.toLowerCase(); 	var temp = userAgent.substring(userAgent.indexOf(targetString) + targetString.length, userAgent.length); 	var rightBracketPosition = temp.indexOf(')'); 	var browserVersion = temp.substring(0, rightBracketPosition); 	return browserVersion;  }  function setupSpan(controlId) { 	var rootGroup = document.getElementById(controlId + "_" + ROOT_ID); 	 	if(rootGroup != null){	rootGroup.style.visibility = "visible"; } }