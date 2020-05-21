Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports ASPNET.StarterKit.Portal
Imports System.Web.Caching
Imports System.Web

Namespace ASPNET.StarterKit.Portal

	'*********************************************************************
	'
	' Configuration Class
	'
	' Class that encapsulates all data logic necessary to add/query/delete
	' tab configuration settings, module configuration settings and module 
	' definition configuration settings from the PortalCfg.xml file.
	'
	'*********************************************************************
	Public Class Configuration


		'
		' PORTAL
		'
		'*********************************************************************
		'
		' UpdatePortalInfo() Method <a name="UpdatePortalInfo"></a>
		'
		' The UpdatePortalInfo method updates the name and access settings for the portal.
		' These settings are stored in the Xml file PortalCfg.xml.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Sub UpdatePortalInfo(ByVal portalId As Integer, ByVal portalName As String, ByVal alwaysShow As Boolean)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Get first record of the "Global" element 
			With siteSettings.Global.FindByPortalId(portalId)

				' Update the values
				.PortalId = portalId
				.PortalName = portalName
				.AlwaysShowEditButton = alwaysShow

			End With

			' Save the changes 
			SaveSiteSettings()

		End Sub


		'
		' TABS
		'
		'*********************************************************************
		'
		' AddTab Method <a name="AddTab"></a>
		'
		' The AddTab method adds a new tab to the portal.  These settings are 
		' stored in the Xml file PortalCfg.xml.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Function AddTab(ByVal portalId As Integer, ByVal tabName As String, ByVal tabOrder As Integer) As Integer

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Create a new TabRow from the Tab table
			Dim newRow As SiteConfiguration.TabRow = siteSettings.Tab.NewTabRow()

			' Set the properties on the new row
			With newRow

				.TabName = tabName
				.TabOrder = tabOrder
				.MobileTabName = ""
				.ShowMobile = True
				.AccessRoles = "All Users;"

			End With

			' Add the new TabRow to the Tab table
			siteSettings.Tab.AddTabRow(newRow)

			' Save the changes 
			SaveSiteSettings()

			' Return the new TabID
			Return newRow.TabId

		End Function

		'*********************************************************************
		'
		' UpdateTab Method <a name="UpdateTab"></a>
		'
		' The UpdateTab method updates the settings for the specified tab. 
		' These settings are stored in the Xml file PortalCfg.xml.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Sub UpdateTab(ByVal portalId As Integer, ByVal tabId As Integer, ByVal tabName As String, ByVal tabOrder As Integer, ByVal authorizedRoles As String, ByVal mobileTabName As String, ByVal showMobile As Boolean)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate tab in the Tab table and set the properties
			With siteSettings.Tab.FindByTabId(tabId)

				.TabName = tabName
				.TabOrder = tabOrder
				.AccessRoles = authorizedRoles
				.MobileTabName = mobileTabName
				.ShowMobile = showMobile

			End With

			' Save the changes 
			SaveSiteSettings()

		End Sub


		'*********************************************************************
		'
		' UpdateTabOrder Method <a name="UpdateTabOrder"></a>
		'
		' The UpdateTabOrder method changes the position of the tab with respect
		' to other tabs in the portal.  These settings are stored in the Xml 
		' file PortalCfg.xml.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Sub UpdateTabOrder(ByVal tabId As Integer, ByVal tabOrder As Integer)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate tab in the Tab table and set the property
			With siteSettings.Tab.FindByTabId(tabId)

				.TabOrder = tabOrder

			End With

			' Save the changes 
			SaveSiteSettings()

		End Sub

		'*********************************************************************
		'
		' DeleteTab Method <a name="DeleteTab"></a>
		'
		' The DeleteTab method deletes the selected tab and its modules from 
		' the settings which are stored in the Xml file PortalCfg.xml.  This 
		' method also deletes any data from the database associated with all 
		' modules within this tab.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'	  + <a href="DeleteModule.htm" style="color:green">DeleteModule stored procedure</a>
		'
		'*********************************************************************
		Public Sub DeleteTab(ByVal tabId As Integer)
			'
			' Delete the Tab in the XML file
			'

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate tab in the Tab table
			Dim tabTable As SiteConfiguration.TabDataTable = siteSettings.Tab
			Dim tabRow As SiteConfiguration.TabRow = siteSettings.Tab.FindByTabId(tabId)

			'
			' Delete information in the Database relating to each Module being deleted
			'

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_DeleteModule", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterModuleID As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
			myConnection.Open()

			Dim moduleRow As SiteConfiguration._ModuleRow
			For Each moduleRow In tabRow.GetModuleRows()
				myCommand.Parameters.Clear()
				parameterModuleID.Value = moduleRow.ModuleId
				myCommand.Parameters.Add(parameterModuleID)

				' Open the database connection and execute the command
				myCommand.ExecuteNonQuery()
			Next

			' Close the connection
			myConnection.Close()

			' Finish removing the Tab row from the Xml file
			tabTable.RemoveTabRow(tabRow)

			' Save the changes 
			SaveSiteSettings()

		End Sub


		'
		' MODULES
		'
		'*********************************************************************
		'
		' UpdateModuleOrder Method  <a name="UpdateModuleOrder"></a>
		'
		' The UpdateModuleOrder method updates the order in which the modules
		' in a tab are displayed.  These settings are stored in the Xml file 
		' PortalCfg.xml.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Sub UpdateModuleOrder(ByVal ModuleId As Integer, ByVal ModuleOrder As Integer, ByVal pane As String)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate Module in the Module table and update the properties
			With siteSettings._Module.FindByModuleId(ModuleId)

				.ModuleOrder = ModuleOrder
				.PaneName = pane

			End With

			' Save the changes 
			SaveSiteSettings()

		End Sub

		'*********************************************************************
		'
		' AddModule Method  <a name="AddModule"></a>
		'
		' The AddModule method adds Portal Settings for a new Module within
		' a Tab.  These settings are stored in the Xml file PortalCfg.xml.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Function AddModule(ByVal tabId As Integer, ByVal moduleOrder As Integer, ByVal paneName As String, ByVal title As String, ByVal moduleDefId As Integer, ByVal cacheTime As Integer, ByVal editRoles As String, ByVal showMobile As Boolean) As Integer

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Create a new ModuleRow from the Module table
			Dim newModule As SiteConfiguration._ModuleRow = siteSettings._Module.New_ModuleRow()

			' Set the properties on the new Module
			With newModule
                .ModuleDefId = moduleDefId
                .ModuleOrder = moduleOrder
                .ModuleTitle = title
                .PaneName = paneName
                .EditRoles = editRoles
                .CacheTimeout = cacheTime
                .ShowMobile = showMobile
                .TabRow = siteSettings.Tab.FindByTabId(tabId)

			End With

			' Add the new ModuleRow to the Module table
			siteSettings._Module.Add_ModuleRow(newModule)

			' Save the changes
			SaveSiteSettings()

			' Return the new Module ID
			Return newModule.ModuleId

		End Function

		'*********************************************************************
		'
		' UpdateModule Method  <a name="UpdateModule"></a>
		'
		' The UpdateModule method updates the Portal Settings for an existing 
		' Module within a Tab.  These settings are stored in the Xml file
		' PortalCfg.xml.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Function UpdateModule(ByVal moduleId As Integer, ByVal moduleOrder As Integer, ByVal paneName As String, ByVal title As String, ByVal cacheTime As Integer, ByVal editRoles As String, ByVal showMobile As Boolean) As Integer

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate Module in the Module table and update the properties
			With siteSettings._Module.FindByModuleId(moduleId)

				.ModuleOrder = moduleOrder
				.ModuleTitle = title
				.PaneName = paneName
				.CacheTimeout = cacheTime
				.EditRoles = editRoles
				.ShowMobile = showMobile

			End With

			' Save the changes 
			SaveSiteSettings()

			' Return the existing Module ID
			Return moduleId

		End Function

		'*********************************************************************
		'
		' DeleteModule Method  <a name="DeleteModule"></a>
		'
		' The DeleteModule method deletes a specified Module from the settings
		' stored in the Xml file PortalCfg.xml.  This method also deletes any 
		' data from the database associated with this module.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'	  + <a href="DeleteModule.htm" style="color:green">DeleteModule stored procedure</a>
		'
		'*********************************************************************
		Public Sub DeleteModule(ByVal moduleId As Integer)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			'
			' Delete information in the Database relating to Module being deleted
			'

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_DeleteModule", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterModuleID As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
			myConnection.Open()

			parameterModuleID.Value = moduleId
			myCommand.Parameters.Add(parameterModuleID)

			' Open the database connection and execute the command
			myCommand.ExecuteNonQuery()
			myConnection.Close()


			' Finish removing Module
			siteSettings._Module.Remove_ModuleRow(siteSettings._Module.FindByModuleId(moduleId))

			' Save the changes 
			SaveSiteSettings()

		End Sub


		'*********************************************************************
		'
		' UpdateModuleSetting Method  <a name="UpdateModuleSetting"></a>
		'
		' The UpdateModuleSetting Method updates a single module setting 
		' in the configuration file.  If the value passed in is String.Empty,
		' the Setting element is deleted if it exists.  If not, either a 
		' matching Setting element is updated, or a new Setting element is 
		' created.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Sub UpdateModuleSetting(ByVal moduleId As Integer, ByVal key As String, ByVal value As String)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate Module in the Module table
			Dim moduleRow As SiteConfiguration._ModuleRow = siteSettings._Module.FindByModuleId(moduleId)

			' Find the first (only) settings element
			Dim settingsRow As SiteConfiguration.SettingsRow

			If moduleRow.GetSettingsRows().Length > 0 Then
				settingsRow = moduleRow.GetSettingsRows()(0)
			Else
				' Add new settings element
				settingsRow = siteSettings.Settings.NewSettingsRow()

				' Set the parent relationship
				settingsRow._ModuleRow = moduleRow

				siteSettings.Settings.AddSettingsRow(settingsRow)
			End If

			' Find the child setting elements
			Dim settingRow As SiteConfiguration.SettingRow

			Dim settingRows() As SiteConfiguration.SettingRow = settingsRow.GetSettingRows()

			If settingRows.Length = 0 Then
				' If there are no Setting elements at all, add one with the new name and value,
				' but only if the value is not empty
				If value <> String.Empty Then
					settingRow = siteSettings.Setting.NewSettingRow()

					With settingRow
						' Set the parent relationship and data
						.SettingsRow = settingsRow
						.Name = key
						.Setting_Text = value
					End With

					siteSettings.Setting.AddSettingRow(settingRow)
				End If
			Else
				' Update existing setting element if it matches
				Dim found As Boolean = False
				Dim i As Int32

				' Find which row matches the input parameter "key" and update the
				' value.  If the value is String.Empty, however, delete the row.
				For i = 0 To settingRows.Length - 1
					If settingRows(i).Name = key Then
						If value = String.Empty Then

							' Delete the row
							siteSettings.Setting.RemoveSettingRow(settingRows(i))

						Else

							' Update the value
							settingRows(i).Setting_Text = value

						End If

						found = True
					End If
				Next

				If found = False Then
					' Setting elements exist, however, there is no matching Setting element.
					' Add one with new name and value, but only if the value is not empty
					If value <> String.Empty Then
						settingRow = siteSettings.Setting.NewSettingRow()

						With settingRow
							' Set the parent relationship and data
							.SettingsRow = settingsRow
							.Name = key
							.Setting_Text = value
						End With

						siteSettings.Setting.AddSettingRow(settingRow)
					End If
				End If
			End If

			' Save the changes 
			SaveSiteSettings()

		End Sub

		'*********************************************************************
		'
		' GetModuleSettings Method  <a name="GetModuleSettings"></a>
		'
		' The GetModuleSettings Method returns a hashtable of custom,
		' module-specific settings from the configuration file.  This method is
		' used by some user control modules (Xml, Image, etc) to access misc
		' settings.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Shared Function GetModuleSettings(ByVal moduleId As Integer) As Hashtable

			' Create a new Hashtable
			Dim _settingsHT As New Hashtable()

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate Module in the Module table
			Dim moduleRow As SiteConfiguration._ModuleRow = siteSettings._Module.FindByModuleId(moduleId)

			' Find the first (only) settings element
			If moduleRow.GetSettingsRows().Length > 0 Then
				Dim settingsRow As SiteConfiguration.SettingsRow = moduleRow.GetSettingsRows()(0)

				If Not settingsRow Is Nothing Then

					' Find the child setting elements and add to the hashtable
					Dim sRow As SiteConfiguration.SettingRow

					For Each sRow In settingsRow.GetSettingRows()
						_settingsHT(sRow.Name) = sRow.Setting_Text
					Next

				End If

			End If

			Return _settingsHT

		End Function


		'
		' MODULE DEFINITIONS
		'
		'*********************************************************************
		'
		' GetModuleDefinitions() Method <a name="GetModuleDefinitions"></a>
		'
		' The GetModuleDefinitions method returns a list of all module type 
		' definitions for the portal.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Function GetModuleDefinitions(ByVal portalId As Integer) As DataRow()

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate Module in the Module table
			Return siteSettings.ModuleDefinition.Select()

		End Function

		'*********************************************************************
		'
		' AddModuleDefinition() Method <a name="AddModuleDefinition"></a>
		'
		' The AddModuleDefinition add the definition for a new module type
		' to the portal.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************

		Public Function AddModuleDefinition(ByVal portalId As Integer, ByVal name As String, ByVal desktopSrc As String, ByVal mobileSrc As String) As Integer

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Create new ModuleDefinitionRow
			Dim newModuleDef As SiteConfiguration.ModuleDefinitionRow = siteSettings.ModuleDefinition.NewModuleDefinitionRow()

			' Set the parameter values
			With newModuleDef

				.FriendlyName = name
				.DesktopSourceFile = desktopSrc
				.MobileSourceFile = mobileSrc

			End With

			' Add the new ModuleDefinitionRow to the ModuleDefinition table
			siteSettings.ModuleDefinition.AddModuleDefinitionRow(newModuleDef)

			' Save the changes
			SaveSiteSettings()

			' Return the new ModuleDefID
			Return newModuleDef.ModuleDefId

		End Function

		'*********************************************************************
		'
		' DeleteModuleDefinition() Method <a name="DeleteModuleDefinition"></a>
		'
		' The DeleteModuleDefinition method deletes the specified module type 
		' definition from the portal.  Each module which is related to the
		' ModuleDefinition is deleted from each tab in the configuration
		' file, and all data relating to each module is deleted from the
		' database.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'     + <a href="DeleteModule.htm" style="color:green">DeleteModule Stored Procedure</a>
		'
		'*********************************************************************
		Public Sub DeleteModuleDefinition(ByVal defId As Integer)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			'
			' Delete information in the Database relating to each Module being deleted
			'

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_DeleteModule", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterModuleID As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
			myConnection.Open()

			Dim moduleRow As SiteConfiguration._ModuleRow
			For Each moduleRow In siteSettings._Module.Select()
				If moduleRow.ModuleDefId = defId Then

					myCommand.Parameters.Clear()
					parameterModuleID.Value = moduleRow.ModuleId
					myCommand.Parameters.Add(parameterModuleID)

					' Delete the xml module associated with the ModuleDef
					' in the configuration file
					siteSettings._Module.Remove_ModuleRow(moduleRow)

					' Open the database connection and execute the command
					myCommand.ExecuteNonQuery()
				End If

			Next

			myConnection.Close()

			' Finish removing Module Definition
			siteSettings.ModuleDefinition.RemoveModuleDefinitionRow(siteSettings.ModuleDefinition.FindByModuleDefId(defId))

			' Save the changes 
			SaveSiteSettings()

		End Sub

		'*********************************************************************
		'
		' UpdateModuleDefinition() Method <a name="UpdateModuleDefinition"></a>
		'
		' The UpdateModuleDefinition method updates the settings for the 
		' specified module type definition.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Sub UpdateModuleDefinition(ByVal defId As Integer, ByVal name As String, ByVal desktopSrc As String, ByVal mobileSrc As String)

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate Module in the Module table and update the properties
			With siteSettings.ModuleDefinition.FindByModuleDefId(defId)

				.FriendlyName = name
				.DesktopSourceFile = desktopSrc
				.MobileSourceFile = mobileSrc

			End With

			' Save the changes 
			SaveSiteSettings()

		End Sub

		'*********************************************************************
		'
		' GetSingleModuleDefinition Method
		'
		' The GetSingleModuleDefinition method returns a ModuleDefinitionRow
		' object containing details about a specific module definition in the
		' configuration file.
		'
		' Other relevant sources:
		'     + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
		'	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
		'
		'*********************************************************************
		Public Function GetSingleModuleDefinition(ByVal defId As Integer) As SiteConfiguration.ModuleDefinitionRow

			' Obtain SiteSettings from Current Context
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

			' Find the appropriate Module in the Module table
			Return siteSettings.ModuleDefinition.FindByModuleDefId(defId)

		End Function

		'*********************************************************************
		'
		' GetSiteSettings Method
		'
		' The Configuration.GetSiteSettings Method returns a typed
		' dataset of the all of the site configuration settings from the
		' XML configuration file.  This method is used in Global.asax to
		' push the settings into the current HttpContext, so that all of the 
		' pages, content modules and classes throughout the rest of the request
		' may access them.
		'
		' The SiteConfiguration object is cached using the ASP.NET Cache API,
		' with a file-change dependency on the XML configuration file.  Normallly,
		' this method just returns a copy of the object in the cache.  When the
		' configuration is updated and changes are saved to the the XML file,
		' the SiteConfiguration object is evicted from the cache.  The next time 
		' this method runs, it will read from the XML file again and insert a
		' fresh copy of the SiteConfiguration into the cache.
		'
		'*********************************************************************
		Public Function GetSiteSettings() As SiteConfiguration
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Cache("SiteSettings"), SiteConfiguration)

			' If the SiteConfiguration isn't cached, load it from the XML file and add it into the cache.
			If siteSettings Is Nothing Then

				' Create the dataset
				siteSettings = New SiteConfiguration()

				' Retrieve the location of the XML configuration file
				Dim configFile As String = HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings("configFile"))

				With siteSettings
					' Set the AutoIncrement property to true for easier adding of rows
					.Tab.TabIdColumn.AutoIncrement = True
					._Module.ModuleIdColumn.AutoIncrement = True
					.ModuleDefinition.ModuleDefIdColumn.AutoIncrement = True

					' Load the XML data into the DataSet
					siteSettings.ReadXml(configFile)
				End With

				' Store the dataset in the cache
				HttpContext.Current.Cache.Insert("SiteSettings", siteSettings, New CacheDependency(configFile))

			End If

			Return siteSettings

		End Function

		'*********************************************************************
		'
		' SaveSiteSettings Method <a name="SaveSiteSettings"></a>
		'
		' The Configuration.SaveSiteSettings overwrites the the XML file with the
		' settings in the SiteConfiguration object in context.  The object will in 
		' turn be evicted from the cache and be reloaded from the XML file the next
		' time GetSiteSettings() is called.
		'
		'*********************************************************************
		Public Sub SaveSiteSettings()

			' Obtain SiteSettings from the Cache
			Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Cache("SiteSettings"), SiteConfiguration)

			' Check the object
			If siteSettings Is Nothing Then
				' If SaveSiteSettings() is called once, the cache is cleared.  If it is
				' then called again before Global.Application_BeginRequest is called, 
				' which reloads the cache, the siteSettings object will be Null (Nothing)
				siteSettings = GetSiteSettings()
			End If

			Dim configFile As String = HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings("configFile"))

			' Object is evicted from the Cache here.  
			siteSettings.WriteXml(configFile)

		End Sub

	End Class


	'*********************************************************************
	'
	' PortalSettings Class
	'
	' This class encapsulates all of the settings for the Portal, as well
	' as the configuration settings required to execute the current tab
	' view within the portal.
	'
	'*********************************************************************

	Public Class PortalSettings

		Public PortalId As Integer
		Public PortalName As String
		Public AlwaysShowEditButton As Boolean
		Public DesktopTabs As New ArrayList()
		Public MobileTabs As New ArrayList()
		Public ActiveTab As New TabSettings()

		'*********************************************************************
		'
		' PortalSettings Constructor
		'
		' The PortalSettings Constructor encapsulates all of the logic
		' necessary to obtain configuration settings necessary to render
		' a Portal Tab view for a given request.
		'
        ' These Portal Settings are stored within PortalCFG.xml, and are
        ' fetched below by calling config.GetSiteSettings().
        ' The method config.GetSiteSettings() fills the SiteConfiguration
        ' class, derived from a DataSet, which PortalSettings accesses.
		'       
		'*********************************************************************
        Public Sub New(ByVal tabIndex As Integer, ByVal tabId As Integer)

            ' Get the configuration data
            Dim config As Configuration = New Configuration()
            Dim siteSettings As SiteConfiguration = config.GetSiteSettings()


            ' Read the Desktop Tab Information, and sort by Tab Order
            Dim tRow As SiteConfiguration.TabRow
            For Each tRow In siteSettings.Tab.Select("", "TabOrder")
                Dim tabDetails As New TabStripDetails()

                With tabDetails
                    .TabId = tRow.TabId
                    .TabName = tRow.TabName
                    .TabOrder = tRow.TabOrder
                    .AuthorizedRoles = tRow.AccessRoles
                End With

                Me.DesktopTabs.Add(tabDetails)
            Next

            ' If the PortalSettings.ActiveTab property is set to 0, change it to  
            ' the TabID of the first tab in the DesktopTabs collection
            If Me.ActiveTab.TabId = 0 Then
                Me.ActiveTab.TabId = CType(Me.DesktopTabs(0), TabStripDetails).TabId
            End If


            ' Read the Mobile Tab Information, and sort by Tab Order
            Dim mRow As SiteConfiguration.TabRow
            For Each mRow In siteSettings.Tab.Select("ShowMobile='true'", "TabOrder")
                Dim tabDetails As New TabStripDetails()

                With tabdetails
                    .TabId = mRow.TabId
                    .TabName = mRow.MobileTabName
                    .AuthorizedRoles = mRow.AccessRoles
                End With

                Me.MobileTabs.Add(tabDetails)
            Next

            ' Read the Module Information for the current (Active) tab
            Dim activeTab As SiteConfiguration.TabRow = siteSettings.Tab.FindByTabId(tabId)
            Dim moduleRow As SiteConfiguration._ModuleRow

            ' Get Modules for this Tab based on the Data Relation
            For Each moduleRow In activeTab.GetModuleRows()
                Dim moduleSettings As New moduleSettings()

                With moduleSettings

                    .ModuleTitle = moduleRow.ModuleTitle
                    .ModuleId = moduleRow.ModuleId
                    .ModuleDefId = moduleRow.ModuleDefId
                    .ModuleOrder = moduleRow.ModuleOrder
                    .TabId = tabId
                    .PaneName = moduleRow.PaneName
                    .AuthorizedEditRoles = moduleRow.EditRoles
                    .CacheTime = moduleRow.CacheTimeout
                    .ShowMobile = moduleRow.ShowMobile

                    ' ModuleDefinition data
                    Dim modDefRow As SiteConfiguration.ModuleDefinitionRow = siteSettings.ModuleDefinition.FindByModuleDefId(.ModuleDefId)

                    .DesktopSrc = modDefRow.DesktopSourceFile
                    .MobileSrc = modDefRow.MobileSourceFile
                End With

                Me.ActiveTab.Modules.Add(moduleSettings)
            Next

            ' Sort the modules in order of ModuleOrder
            Me.ActiveTab.Modules.Sort()

            ' Get the first row in the Global table
            Dim globalSettings As SiteConfiguration.GlobalRow = siteSettings.Global.Rows(0)

            ' Read Portal global settings 
            Me.PortalId = globalSettings.PortalId
            Me.PortalName = globalSettings.PortalName
            Me.AlwaysShowEditButton = globalSettings.AlwaysShowEditButton
            Me.ActiveTab.TabIndex = tabIndex
            Me.ActiveTab.TabId = tabId
            Me.ActiveTab.TabOrder = activeTab.TabOrder
            Me.ActiveTab.MobileTabName = activeTab.MobileTabName
            Me.ActiveTab.AuthorizedRoles = activeTab.AccessRoles
            Me.ActiveTab.TabName = activeTab.TabName
            Me.ActiveTab.ShowMobile = activeTab.ShowMobile

        End Sub

    End Class

    '*********************************************************************
    '
    ' TabStripDetails Class
    '
    ' Class that encapsulates the just the tabstrip details -- TabName, TabId and TabOrder 
    ' -- for a specific Tab in the Portal
    '
    '*********************************************************************

    Public Class TabStripDetails

        Public TabId As Integer
        Public TabName As String
        Public TabOrder As Integer
        Public AuthorizedRoles As String

    End Class


    '*********************************************************************
    '
    ' TabSettings Class
    '
    ' Class that encapsulates the detailed settings for a specific Tab 
    ' in the Portal
    '
    '*********************************************************************

    Public Class TabSettings

        Public TabIndex As Integer
        Public TabId As Integer
        Public TabName As String
        Public TabOrder As Integer
        Public MobileTabName As String
        Public AuthorizedRoles As String
        Public ShowMobile As Boolean
        Public Modules As New ArrayList()

    End Class


    '*********************************************************************
    '
    ' ModuleSettings Class
    '
    ' Class that encapsulates the detailed settings for a specific Tab 
    ' in the Portal.  ModuleSettings implements the IComparable interface 
    ' so that an ArrayList of ModuleSettings objects may be sorted by
    ' ModuleOrder, using the ArrayList's Sort() method.
    '
    '*********************************************************************

    Public Class ModuleSettings
        Implements IComparable

        Public ModuleId As Integer
        Public ModuleDefId As Integer
        Public TabId As Integer
        Public CacheTime As Integer
        Public ModuleOrder As Integer
        Public PaneName As String
        Public ModuleTitle As String
        Public AuthorizedEditRoles As String
        Public ShowMobile As Boolean
        Public DesktopSrc As String
        Public MobileSrc As String

        Protected Overridable Function CompareTo(ByVal value As Object) As Integer Implements IComparable.CompareTo

            If value Is Nothing Then
                Return 1
            End If

            Dim compareOrder As Integer = CType(value, ModuleSettings).ModuleOrder

            If Me.ModuleOrder = compareOrder Then Return 0
            If Me.ModuleOrder < compareOrder Then Return -1
            If Me.ModuleOrder > compareOrder Then Return 1
            Return 0

        End Function

    End Class

    '*********************************************************************
    '
    ' ModuleItem Class
    '
    ' This class encapsulates the basic attributes of a Module, and is used
    ' by the administration pages when manipulating modules.  ModuleItem implements 
    ' the IComparable interface so that an ArrayList of ModuleItems may be sorted
    ' by ModuleOrder, using the ArrayList's Sort() method.
    '
    '*********************************************************************

    Public Class ModuleItem
        Implements IComparable


        Private _moduleOrder As Integer
        Private _title As String
        Private _pane As String
        Private _id As Integer
        Private _defId As Integer


        Public Property ModuleOrder() As Integer

            Get
                Return _moduleOrder
            End Get
            Set(ByVal Value As Integer)
                _moduleOrder = Value
            End Set

        End Property


        Public Property ModuleTitle() As String

            Get
                Return _title
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set

        End Property


        Public Property PaneName() As String

            Get
                Return _pane
            End Get
            Set(ByVal Value As String)
                _pane = Value
            End Set

        End Property


        Public Property ModuleId() As Integer

            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = Value
            End Set

        End Property


        Public Property ModuleDefId() As Integer

            Get
                Return _defId
            End Get
            Set(ByVal Value As Integer)
                _defId = Value
            End Set

        End Property


        Protected Overridable Function CompareTo(ByVal value As Object) As Integer Implements IComparable.CompareTo

            If value Is Nothing Then
                Return 1
            End If

            Dim compareOrder As Integer = CType(value, ModuleItem).ModuleOrder

            If Me.ModuleOrder = compareOrder Then Return 0
            If Me.ModuleOrder < compareOrder Then Return -1
            If Me.ModuleOrder > compareOrder Then Return 1
            Return 0

        End Function

    End Class


    '*********************************************************************
    '
    ' TabItem Class
    '
    ' This class encapsulates the basic attributes of a Tab, and is used
    ' by the administration pages when manipulating tabs.  TabItem implements 
    ' the IComparable interface so that an ArrayList of TabItems may be sorted
    ' by TabOrder, using the ArrayList's Sort() method.
    '
    '*********************************************************************

    Public Class TabItem
        Implements IComparable

        Private _tabOrder As Integer
        Private _name As String
        Private _id As Integer


        Public Property TabOrder() As Integer

            Get
                Return _tabOrder
            End Get
            Set(ByVal Value As Integer)
                _tabOrder = Value
            End Set
        End Property


        Public Property TabName() As String

            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property


        Public Property TabId() As Integer

            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = Value
            End Set
        End Property

        Public Overridable Function CompareTo(ByVal value As Object) As Integer Implements IComparable.CompareTo

            If value Is Nothing Then
                Return 1
            End If

            Dim compareOrder As Integer = CType(value, TabItem).TabOrder

            If Me.TabOrder = compareOrder Then Return 0
            If Me.TabOrder < compareOrder Then Return -1
            If Me.TabOrder > compareOrder Then Return 1
            Return 0

        End Function

    End Class

End Namespace