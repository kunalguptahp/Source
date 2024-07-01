using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Xml;


using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Data.CmService
{
	public class ElementsPublisher
	{
		private const string RD_CLIENT_SYSTEM_ID = "redirectClientSystemId";
		//private const string RD_CONTENT_NAME = "redirectContentName";
		private const string RD_SERVICE_URL = "redirectServiceUrl";
		private const string RD_APP_NAME = "redirectAppName";

		private const string CFG_CLIENT_SYSTEM_ID = "configurationServiceClientSystemId";
		//private const string CFG_CONTENT_NAME = "configurationServiceContentName";
		private const string CFG_SERVICE_URL = "configurationServiceServiceUrl";
		private const string CFG_APP_NAME = "configurationServiceAppName";
		private const string CFG_TAG_TRACKINGSERVERDOMAIN = "{TrackingServerDomain}";

        private const string JMP_CLIENT_SYSTEM_ID = "jumpstationClientSystemId";
        //private const string JMP_CONTENT_NAME = "jumpstationContentName";
        private const string JMP_SERVICE_URL = "jumpstationServiceUrl";
        private const string JMP_APP_NAME = "jumpstationAppName";
        //private const string JMP_TAG_TRACKINGSERVERDOMAIN = "{TrackingServerDomain}";

        private const string WRK_CLIENT_SYSTEM_ID = "workflowClientSystemId";
        //private const string WRK_CONTENT_NAME = "workflowContentName";
        private const string WRK_SERVICE_URL = "workflowServiceUrl";
        private const string WRK_APP_NAME = "workflowAppName";

		private static object _lock = new object();
		public static ElementsPublisher Instance = new ElementsPublisher();

		private ElementsPublisher() { }

		private string GetConfigValue(string itemName, Environment env)
		{
			string settingItem = itemName + env.ToString();
			string val = ConfigurationManager.AppSettings[settingItem.Trim()];
			if (string.IsNullOrEmpty(val))
			{
				throw new InvalidOperationException(string.Format("The system's current configuration does not support Elements integration, [{0}] not found.", settingItem));
			}
			return val;
		}

		public int PublishProxyURL(ProxyURL proxyUrl, Environment env)
		{
			ElementsDataPublishingService.ElementsDataPublishingService svc = new HP.ElementsCPS.Data.ElementsDataPublishingService.ElementsDataPublishingService();
			svc.Url = GetConfigValue(RD_SERVICE_URL, env);

			var target = new HP.ElementsCPS.Data.ElementsDataPublishingService.T_redirecttarget();
			var selectorItems = new List<ElementsDataPublishingService.T_redirectselectoritem>();
			foreach (var record in proxyUrl.GetValidQueryParameterValues())
			{
				var selectorItem = new ElementsDataPublishingService.T_redirectselectoritem();
				selectorItem.att_key = record.QueryParameterValue.QueryParameter.ElementsKey;
				selectorItem.att_value = record.QueryParameterValue.Name;
				selectorItems.Add(selectorItem);
			}
			target.redirectselectorgroup = selectorItems.ToArray();
			target.description = proxyUrl.Description ?? string.Empty;
			if (env == Environment.Publication)
			{
                target.site = proxyUrl.ProxyURLType.ProxyURLDomain.ProductionDomain;
				target.target_id = proxyUrl.ProductionId ?? 0;
			}
			if (env == Environment.Validation)
			{
				target.site = proxyUrl.ProxyURLType.ProxyURLDomain.ValidationDomain;
				target.target_id = proxyUrl.ValidationId ?? 0;
			}
			target.last_update_date = proxyUrl.ModifiedOn;
			target.creation_date = proxyUrl.CreatedOn;
			target.target_type = proxyUrl.ProxyURLType.ProxyURLGroupType.Name;
			//TODO:
			target.message_flag_id = 1;

			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("url", proxyUrl.Url);
			var contents = new List<ElementsDataPublishingService.T_redirectcontentitem>();
			foreach (var de in dict)
			{
				var content = new ElementsDataPublishingService.T_redirectcontentitem();
				content.content_key = de.Key;
				content.content_value = de.Value;
				contents.Add(content);
			}
			target.redirectcontent = contents.ToArray();

			long id = target.target_id;

			try
			{
				lock (_lock)
				{
					//Note: double check if the url have id, maybe a concurrent user update it.
					var pu = ProxyURL.FetchByID(proxyUrl.Id);
					if (env == Environment.Validation)
					{
						if (pu.ValidationId != proxyUrl.ValidationId)
						{
							throw new PublishException("Another user has validated this record.", null);
						}
					}
					if (env == Environment.Publication)
					{
						if (pu.ProductionId != proxyUrl.ProductionId)
						{
							throw new PublishException("Another user has published this record.", null);
						}
					}

					var request = new ElementsDataPublishingService.updateRedirectTargetRequest();
					request.appName = GetConfigValue(RD_APP_NAME, env);
					request.clientSystemCode = GetConfigValue(RD_CLIENT_SYSTEM_ID, env);
					//request.contentName = GetConfigValue(RD_CONTENT_NAME, env);
					request.contentKeyID = id.ToString();
					request.redirectTarget = target;
					var response = svc.updateRedirectTarget(request);
					return response.contentKeyID;
				}
			}
			catch (SoapException ex)
			{
				PublishException pex = new PublishException("Error when calling publish service.", ex.Detail, ex);
				throw pex;
			}
			catch (Exception ex)
			{
				PublishException pex = new PublishException("Unknown error when calling publish service.", ex);
				throw pex;
			}
		}

		public void UnPublishProxyURL(ProxyURL proxyUrl, Environment env)
		{
			if (env == Environment.Publication)
			{
				if (!proxyUrl.ProductionId.HasValue)
					throw new InvalidOperationException("Record doesn't have a valid Elements publish Id.");
			}

			if (env == Environment.Validation)
			{
				if (!proxyUrl.ValidationId.HasValue)
					throw new InvalidOperationException("Record doesn't have a valid Elements validation Id.");
			}

			ElementsDataPublishingService.ElementsDataPublishingService svc = new ElementsDataPublishingService.ElementsDataPublishingService();
			svc.Url = GetConfigValue(RD_SERVICE_URL, env);

			try
			{
				lock (_lock)
				{
					var request = new ElementsDataPublishingService.deleteRedirectTargetByIDRequest();
					request.clientSystemCode = GetConfigValue(RD_CLIENT_SYSTEM_ID, env);
					request.appName = GetConfigValue(RD_APP_NAME, env);
					//request.contentName = GetConfigValue(RD_CONTENT_NAME, env);

					if (env == Environment.Publication)
						request.contentKeyID = proxyUrl.ProductionId.Value.ToString();
					if (env == Environment.Validation)
						request.contentKeyID = proxyUrl.ValidationId.Value.ToString();

					svc.deleteRedirectTargetByID(request);
				}
			}
			catch (SoapException ex)
			{
				PublishException pex = new PublishException("Error when calling un-publish service.", ex.Detail, ex);
				throw pex;
			}
			catch (Exception ex)
			{
				PublishException pex = new PublishException("Unknown error when calling un-publish service.", ex);
				throw pex;
			}
		}

		public int PublishConfigGroup(ConfigurationServiceGroup cfgGroup, Environment env)
		{
			ElementsDataPublishingService.ElementsDataPublishingService svc = new HP.ElementsCPS.Data.ElementsDataPublishingService.ElementsDataPublishingService();
			svc.Url = GetConfigValue(CFG_SERVICE_URL, env);

			var target = new HP.ElementsCPS.Data.ElementsDataPublishingService.CfgGroup();

			target.name = cfgGroup.Name;
			target.type = cfgGroup.ConfigurationServiceGroupType.Name;
			target.appVersion = cfgGroup.ConfigurationServiceApplication.Version;
			target.appName = cfgGroup.ConfigurationServiceApplication.ElementsKey;

			var labelValueRecords = cfgGroup.ConfigurationServiceLabelValueRecords();
			var groupLabelRecords = labelValueRecords.Where(x => x.ConfigurationServiceLabel.ConfigurationServiceItem.Parent);
			var itemLabelRecords = labelValueRecords.Where(x => !x.ConfigurationServiceLabel.ConfigurationServiceItem.Parent);

			//Note:Fill target.CfgGroupLabel[]
			var labels = new List<ElementsDataPublishingService.CfgGroupLabel>();
			foreach (var record in groupLabelRecords)
			{
				var label = new ElementsDataPublishingService.CfgGroupLabel();
				label.labelValue = this.PerformTagSubstitution(env, record.ValueX);
				label.labelName = record.ConfigurationServiceLabel.ElementsKey;
				labels.Add(label);
			}
			target.cfgGroupLabel = labels.ToArray();

			//Note:Fill target.cfgItem's CfgItemLabel[]
			var items = new List<ElementsDataPublishingService.CfgItem>();
			var itemRecords = cfgGroup.ConfigurationServiceGroupType.ConfigurationServiceItemConfigurationServiceGroupTypeRecords();
			foreach (var r in itemRecords)
			{
				if (!r.ConfigurationServiceItem.Parent)
				{
					var item = new ElementsDataPublishingService.CfgItem();
					item.type = r.ConfigurationServiceItem.ElementsKey;

					var cfgItemLabels = new List<ElementsDataPublishingService.CfgItemLabel>();
					foreach (var rl in itemLabelRecords)
					{
						if (rl.ConfigurationServiceLabel.ConfigurationServiceItemId == r.ConfigurationServiceItemId)
						{
							if (rl.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId == (int)ConfigurationServiceLabelTypeId.LabelTypeTextMultiple)
							{
								//Note:Multiline text is treat different:each line as a value
								var labelValueLines = rl.ValueX.Split('\n');
								foreach (string line in labelValueLines)
								{
									if (string.IsNullOrEmpty(line))
										continue;
									var cfgItemLabel = new ElementsDataPublishingService.CfgItemLabel();
									cfgItemLabel.labelName = rl.ConfigurationServiceLabel.ElementsKey;
									cfgItemLabel.labelValue = this.PerformTagSubstitution(env, line);
									cfgItemLabels.Add(cfgItemLabel);
								}
							}
							else
							{
								var cfgItemLabel = new ElementsDataPublishingService.CfgItemLabel();
								cfgItemLabel.labelName = rl.ConfigurationServiceLabel.ElementsKey;
								cfgItemLabel.labelValue = this.PerformTagSubstitution(env, rl.ValueX);
								cfgItemLabels.Add(cfgItemLabel);
							}							
						}
					}

                    // only add the item if we have labels values.
					if (cfgItemLabels.Count > 0)
                    {
                        item.cfgItemLabel = cfgItemLabels.ToArray();
	    				items.Add(item);
                    }
				}
			}
			target.cfgItem = items.ToArray();

			//Note:Fill target.selectorItem[]
			var selectors = new List<ElementsDataPublishingService.SelectorItem>();
			var selRecords = cfgGroup.ConfigurationServiceGroupSelectorRecords();

			foreach (var r in selRecords)
			{
				var values = r.ConfigurationServiceGroupSelectorQueryParameterValueRecords();
				var groups = values.GroupBy(x => x.QueryParameterValue.QueryParameter.ElementsKey);

				foreach (var g in groups)
				{
					var negValues = g.Where(x => x.Negation);
					var posValues = g.Where(x => !x.Negation);

					if (negValues.Count() > 0)
					{
						var sel = new ElementsDataPublishingService.SelectorItem();
						sel.negation = true;
						sel.negationSpecified = true;//Note:auto generated code, always set to true.
						sel.selectorGroupName = r.Name;
						sel.selectorName = g.Key;
						sel.selectorValue = negValues.Select(x => x.QueryParameterValue.Name).ToArray();
						selectors.Add(sel);
					}

					if (posValues.Count() > 0)
					{
						var sel = new ElementsDataPublishingService.SelectorItem();
						sel.negation = false;
						sel.negationSpecified = true;//Note:auto generated code, always set to true.
						sel.selectorGroupName = r.Name;
						sel.selectorName = g.Key;
						sel.selectorValue = posValues.Select(x => x.QueryParameterValue.Name).ToArray();
						selectors.Add(sel);
					}
				}
			}
			target.selectorItem = selectors.ToArray();

			if (env == Environment.Publication)
			{
				target.id = cfgGroup.ProductionId ?? 0;
			}
			if (env == Environment.Validation)
			{
				target.id = cfgGroup.ValidationId ?? 0;
			}
			target.idSpecified = true;//Note:auto generated code, always set to true.

			try
			{
				lock (_lock)
				{
					//Note: double check if the url have id, maybe a concurrent user update it.
					var cg = ConfigurationServiceGroup.FetchByID(cfgGroup.Id);
					if (env == Environment.Validation)
					{
						if (cg.ValidationId != cfgGroup.ValidationId)
						{
							throw new PublishException("Another user has validated this record.", null);
						}
					}
					if (env == Environment.Publication)
					{
						if (cg.ProductionId != cfgGroup.ProductionId)
						{
							throw new PublishException("Another user has published this record.", null);
						}
					}

					var request = new ElementsDataPublishingService.updateCfgGroupRequest();
					request.appName = GetConfigValue(CFG_APP_NAME, env);
					request.clientSystemCode = GetConfigValue(CFG_CLIENT_SYSTEM_ID, env);
					//request.contentName = GetConfigValue(CFG_CONTENT_NAME, env);
					request.contentKeyID = target.id.ToString();
					request.cfgGroup = target;
					var resp = svc.updateCfgGroup(request);
					return (int)resp.contentKeyID;
				}
			}
			catch (SoapException ex)
			{
				PublishException pex = new PublishException("Error when calling publish service.", ex.Detail, ex);
				throw pex;
			}
			catch (Exception ex)
			{
				PublishException pex = new PublishException("Unknown error when calling publish service.", ex);
				throw pex;
			}
		}

		public void UnPublishConfigGroup(ConfigurationServiceGroup cfgGroup, Environment env)
		{
			if (env == Environment.Publication)
			{
				if (!cfgGroup.ProductionId.HasValue)
					throw new InvalidOperationException("Record doesn't have a valid elements publish Id.");
			}

			if (env == Environment.Validation)
			{
				if (!cfgGroup.ValidationId.HasValue)
					throw new InvalidOperationException("Record doesn't have a valid elements validation Id.");
			}

			ElementsDataPublishingService.ElementsDataPublishingService svc = new ElementsDataPublishingService.ElementsDataPublishingService();
			svc.Url = GetConfigValue(CFG_SERVICE_URL, env);

			try
			{
				lock (_lock)
				{
					var request = new ElementsDataPublishingService.deleteCfgGroupByIDRequest();
					request.clientSystemCode = GetConfigValue(CFG_CLIENT_SYSTEM_ID, env);
					request.appName = GetConfigValue(CFG_APP_NAME, env);
					//request.contentName = GetConfigValue(CFG_CONTENT_NAME, env);

					if (env == Environment.Publication)
						request.contentKeyID = cfgGroup.ProductionId.Value.ToString();
					if (env == Environment.Validation)
						request.contentKeyID = cfgGroup.ValidationId.Value.ToString();

					svc.deleteCfgGroupByID(request);
				}
			}
			catch (SoapException ex)
			{
				PublishException pex = new PublishException("Error when calling un-publish service.", ex.Detail, ex);
				throw pex;
			}
			catch (Exception ex)
			{
				PublishException pex = new PublishException("Unknown error when calling un-publish service.", ex);
				throw pex;
			}
		}

		public int PublishJumpstationGroup(JumpstationGroup jsGroup, Environment env)
		{
		    ElementsDataPublishingService.ElementsDataPublishingService svc = new HP.ElementsCPS.Data.ElementsDataPublishingService.ElementsDataPublishingService();
		    svc.Url = GetConfigValue(JMP_SERVICE_URL, env);

            var target = new HP.ElementsCPS.Data.ElementsDataPublishingService.JumpstationRedirector();
           
            // Populate the 'header' portion of the Elements update request
            target.targetUrl = jsGroup.TargetURL ;
            target.site = jsGroup.JumpstationApplication.ElementsKey;
		    target.id = jsGroup.Id;
		    target.order = (int) jsGroup.Order;
		    target.orderSpecified = true;

		    //Now fill in the set of selectors and selector values
		    var selectors = new List<ElementsDataPublishingService.SelectorItem1>();
		    var selRecords = jsGroup.JumpstationGroupSelectorRecords();
            var selGroupCount = 0; // sequentially number the selector groups in elements
		    foreach (var r in selRecords)
		    {
			   var values = r.JumpstationGroupSelectorQueryParameterValueRecords();
			   var groups = values.GroupBy(x => x.QueryParameterValue.QueryParameter.ElementsKey);

               foreach (var g in groups)
			   {
				  var negValues = g.Where(x => x.Negation);
				  var posValues = g.Where(x => !x.Negation);


				  if (negValues.Count() > 0)
				  {
					 var sel = new ElementsDataPublishingService.SelectorItem1();
                     sel.negation = true; 
                     sel.groupNumber = selGroupCount;
					 sel.selectorName = g.Key;
					 sel.selectorValue = negValues.Select(x => x.QueryParameterValue.Name).ToArray();
					 selectors.Add(sel);
				  }

				  if (posValues.Count() > 0)
				  {
					 var sel = new ElementsDataPublishingService.SelectorItem1();
					 sel.negation = false;
                     sel.groupNumber = selGroupCount;
					 sel.selectorName = g.Key;
					 sel.selectorValue = posValues.Select(x => x.QueryParameterValue.Name).ToArray();
					 selectors.Add(sel);
				  }
               }
		        selGroupCount++;
		    }

		    target.selectionCriteria = selectors.ToArray();

		    if (env == Environment.Publication)
		    {
			   target.id = jsGroup.ProductionId ?? 0;
		    }
		    if (env == Environment.Validation)
		    {
			   target.id = jsGroup.ValidationId ?? 0;
		    }
	
		    try
		    {
			   lock (_lock)
			   {
				  //Note: double check if the url have id, maybe a concurrent user update it.
				  var cg = JumpstationGroup.FetchByID(jsGroup.Id);
				  if (env == Environment.Validation)
				  {
					 if (cg.ValidationId != jsGroup.ValidationId)
					 {
						throw new PublishException("Another user has validated this record.", null);
					 }
				  }
				  if (env == Environment.Publication)
				  {
					 if (cg.ProductionId != jsGroup.ProductionId)
					 {
						throw new PublishException("Another user has published this record.", null);
					 }
				  }

                   // Now populate the full request and publish to Elements for validation
				  var request = new ElementsDataPublishingService.updateJumpstationRedirectRequest();
				  request.appName = GetConfigValue(JMP_APP_NAME, env);
				  request.clientSystemCode = GetConfigValue(JMP_CLIENT_SYSTEM_ID, env);
				  //request.contentName = GetConfigValue(JMP_CONTENT_NAME, env);
                  request.contentKeyID = target.id.ToString();
			      request.jumpstationRedirect = target;
				  var resp = svc.updateJumpstationRedirect(request);
				  return (int)resp.contentKeyID;
			   }
		    }
		    catch (SoapException ex)
		    {
			   PublishException pex = new PublishException("Error when call publishing service.", ex.Detail, ex);
			   throw pex;
		    }
		    catch (Exception ex)
		    {
			   PublishException pex = new PublishException("Unknown error when call publishing service.", ex);
			   throw pex;
		    }
		}

		public void UnPublishJumpstationGroup(JumpstationGroup cfgGroup, Environment env)
		{
		    if (env == Environment.Publication)
		    {
			   if (!cfgGroup.ProductionId.HasValue)
				  throw new InvalidOperationException("Record doesn't have a valid elements publish Id.");
		    }

		    if (env == Environment.Validation)
		    {
			   if (!cfgGroup.ValidationId.HasValue)
				  throw new InvalidOperationException("Record doesn't have a valid elements validation Id.");
		    }

		    ElementsDataPublishingService.ElementsDataPublishingService svc = new ElementsDataPublishingService.ElementsDataPublishingService();
		    svc.Url = GetConfigValue(JMP_SERVICE_URL, env);

		    try
		    {
			   lock (_lock)
			   {
				  var request = new ElementsDataPublishingService.deleteJumpstationRedirectByIDRequest();
				  request.clientSystemCode = GetConfigValue(JMP_CLIENT_SYSTEM_ID, env);
				  request.appName = GetConfigValue(JMP_APP_NAME, env);
				  //request.contentName = GetConfigValue(JMP_CONTENT_NAME, env);

				  if (env == Environment.Publication)
					 request.contentKeyID = cfgGroup.ProductionId.Value.ToString();
				  if (env == Environment.Validation)
					 request.contentKeyID = cfgGroup.ValidationId.Value.ToString();

				  svc.deleteJumpstationRedirectByID(request);
			   }
		    }
		    catch (SoapException ex)
		    {
			   PublishException pex = new PublishException("Error when calling un-publish service.", ex.Detail, ex);
			   throw pex;
		    }
		    catch (Exception ex)
		    {
			   PublishException pex = new PublishException("Unknown error when calling un-publish service.", ex);
			   throw pex;
		    }
		}

        public int PublishJumpstationMacro(JumpstationMacro jmpMacro, Environment env)
        {
            ElementsDataPublishingService.ElementsDataPublishingService svc = new HP.ElementsCPS.Data.ElementsDataPublishingService.ElementsDataPublishingService();
            svc.Url = GetConfigValue(JMP_SERVICE_URL, env);

            // Populate the 'header' portion of the Elements update request
            var target = new HP.ElementsCPS.Data.ElementsDataPublishingService.Macro();
            target.name = jmpMacro.Name;
            target.description = jmpMacro.Description;
            target.defaultOut = jmpMacro.DefaultResultValue;

            //Note:Fill target.matchPattern[]
            var matchPatternRecords = jmpMacro.JumpstationMacroValueRecords();
            var matchPatterns = new List<ElementsDataPublishingService.MatchPattern>();
            foreach (var record in matchPatternRecords)
            {
                if (record.RowStatusId == 1)
                {
                    var matchPattern = new ElementsDataPublishingService.MatchPattern();
                    matchPattern.@in = record.MatchName;
                    matchPattern.@out = record.ResultValue;
                    matchPatterns.Add(matchPattern);
                }
                
            }
            target.matchPattern = matchPatterns.ToArray();

            if (env == Environment.Publication)
            {
                target.id = jmpMacro.ProductionId ?? 0;
            }
            if (env == Environment.Validation)
            {
                target.id = jmpMacro.ValidationId ?? 0;
            }

            try
            {
                lock (_lock)
                {
                    //Note: double check if the url have id, maybe a concurrent user update it.
                    var macro = JumpstationMacro.FetchByID(jmpMacro.Id);
                    if (env == Environment.Validation)
                    {
                        if (macro.ValidationId != jmpMacro.ValidationId)
                        {
                            throw new PublishException("Another user has validate this record.", null);
                        }
                    }
                    if (env == Environment.Publication)
                    {
                        if (macro.ProductionId != jmpMacro.ProductionId)
                        {
                            throw new PublishException("Another user has publish this record.", null);
                        }
                    }

                    // Now populate the full request and publish to Elements for validation
                    var request = new ElementsDataPublishingService.updateJumpstationMacroRequest();
                    request.appName = GetConfigValue(JMP_APP_NAME, env);
                    request.clientSystemCode = GetConfigValue(JMP_CLIENT_SYSTEM_ID, env);
                    //request.contentName = GetConfigValue(JMP_CONTENT_NAME, env);
                    request.contentKeyID = target.id.ToString();
                    request.jumpstationMacro = target;
                    var resp = svc.updateJumpstationMacro(request);
                    return (int)resp.contentKeyID;
                }
            }
            catch (SoapException ex)
            {
                PublishException pex = new PublishException("Error when call publishing service.", ex.Detail, ex);
                throw pex;
            }
            catch (Exception ex)
            {
                PublishException pex = new PublishException("Unknown error when call publishing service.", ex);
                throw pex;
            }
        }

        public void UnPublishJumpstationMacro(JumpstationMacro jmpMacro, Environment env)
        {
            if (env == Environment.Publication)
            {
                if (!jmpMacro.ProductionId.HasValue)
                    throw new InvalidOperationException("Record doesn't have a valid elements publish Id.");
            }

            if (env == Environment.Validation)
            {
                if (!jmpMacro.ValidationId.HasValue)
                    throw new InvalidOperationException("Record doesn't have a valid elements validation Id.");
            }

            ElementsDataPublishingService.ElementsDataPublishingService svc = new ElementsDataPublishingService.ElementsDataPublishingService();
            svc.Url = GetConfigValue(JMP_SERVICE_URL, env);

            try
            {
                lock (_lock)
                {
                    var request = new ElementsDataPublishingService.deleteJumpstationMacroByIDRequest();
                    request.clientSystemCode = GetConfigValue(JMP_CLIENT_SYSTEM_ID, env);
                    request.appName = GetConfigValue(JMP_APP_NAME, env);
                    if (env == Environment.Publication)
                        request.contentKeyID = jmpMacro.ProductionId.Value.ToString();
                    if (env == Environment.Validation)
                        request.contentKeyID = jmpMacro.ValidationId.Value.ToString();
                    svc.deleteJumpstationMacroByID(request);
                }
            }
            catch (SoapException ex)
            {
                PublishException pex = new PublishException("Error when calling un-publish service.", ex.Detail, ex);
                throw pex;
            }
            catch (Exception ex)
            {
                PublishException pex = new PublishException("Unknown error when calling un-publish service.", ex);
                throw pex;
            }
        }

        public int PublishWorkflow(Workflow wkflow, Environment env)
        {
            string mainUrlValidation = ConfigurationManager.AppSettings["workflowMainUrlValidation"];
            string mainUrlPublication = ConfigurationManager.AppSettings["workflowMainUrlPublication"];

            var target = new HP.ElementsCPS.Data.ElementsDataPublishingService.Workflow();

            //target.name = wkflow.Name;
            //target.type = wkflow.WorkflowType.Name;
            //target.appVersion = wkflow.WorkflowApplication.Version;
            //target.appName = wkflow.WorkflowApplication.ElementsKey;
            target.mainVersion = string.Format("{0}.{1}", wkflow.VersionMajor, wkflow.VersionMinor);
            target.offlineMode = wkflow.Offline ? "Y" : "N";

            //Note:Fill target.modules[]
            var workflowModuleRecords = wkflow.WorkflowWorkflowModuleRecords();
            var modules = new List<ElementsDataPublishingService.WorkflowModule>();
            foreach (var record in workflowModuleRecords)
            {
                var module = new ElementsDataPublishingService.WorkflowModule();
                if (env == Environment.Publication)
                {
                    module.id = record.WorkflowModule.ProductionId ?? 0;
                }
                if (env == Environment.Validation)
                {
                    module.id = record.WorkflowModule.ValidationId ?? 0;
                }
                module.order = record.SortOrder;
                modules.Add(module);
            }
            target.modules = modules.ToArray();

            // Workflow/Reach only supports 1 selector group.
            // These query parameters should be handled more generically.
            var selRecords = wkflow.WorkflowSelectorRecords();
            var values = selRecords[0].WorkflowSelectorQueryParameterValueRecords();

            // parameter values (Reach only supports 1/should be only 1)
            var value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "platform");
            var valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.platform = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "pcbrand");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.brand = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "subbrand");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.subBrand = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "releasestart");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                if (valueArray[0].Contains("*"))
                    // wildcard set to -1
                    target.releaseStart = -1;
                else
                    target.releaseStart = (valueArray == null) ? 0 : Convert.ToInt32(valueArray[0].ToString());

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "releaseend");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                if (valueArray[0].StartsWith("*"))
                    // wildcard set to -1
                    target.releaseEnd = -1;
                else
                    target.releaseEnd = (valueArray == null) ? 0 : Convert.ToInt32(valueArray[0].ToString());

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "cc");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.country = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "modelnumber");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.modelNumber = valueArray[0];

            // parameter values
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "lang");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                target.languages = valueArray;

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "productname");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.productName = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "productnumber");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.productNumber = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "osversion");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.osVersion = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "ostype");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.osType = valueArray[0];

            // parameter values (Reach only supports 1/should be only 1)
            value = values.Where(x => x.QueryParameterValue.QueryParameter.ElementsKey == "osbit");
            valueArray = value.Select(x => x.QueryParameterValue.Name).ToArray();
            if (valueArray.Length > 0)
                // only 1st one (reach doesn't support multiple parameters)
                target.osBit = valueArray[0];

            if (env == Environment.Publication)
            {
                target.mainUrl = string.Format("{0}/{1}.{2}/{3}", mainUrlValidation, wkflow.VersionMajor, wkflow.VersionMinor, wkflow.Filename);
                target.id = wkflow.ProductionId ?? 0;
            }
            if (env == Environment.Validation)
            {
                target.mainUrl = string.Format("{0}/{1}.{2}/{3}", mainUrlPublication, wkflow.VersionMajor, wkflow.VersionMinor, wkflow.Filename);
                target.id = wkflow.ValidationId ?? 0;
            }
            target.idSpecified = true;//Note:auto generated code, always set to true.

            try
            {
                lock (_lock)
                {
                    //Note: double check if the url have id, maybe a concurrent user update it.
                    var cg = Workflow.FetchByID(wkflow.Id);
                    if (env == Environment.Validation)
                    {
                        if (cg.ValidationId != wkflow.ValidationId)
                        {
                            throw new PublishException("Another user has validate this record.", null);
                        }
                    }
                    if (env == Environment.Publication)
                    {
                        if (cg.ProductionId != wkflow.ProductionId)
                        {
                            throw new PublishException("Another user has publish this record.", null);
                        }
                    }

                    var request = new ElementsDataPublishingService.updateReachWorkflowRequest();
                    request.appName = GetConfigValue(WRK_APP_NAME, env);
                    request.clientSystemCode = GetConfigValue(WRK_CLIENT_SYSTEM_ID, env);
                    //request.contentName = GetConfigValue(WRK_CONTENT_NAME, env);
                    request.contentKeyID = target.id.ToString();
                    request.reachWorkflow = target;
                    
                    ElementsDataPublishingService.ElementsDataPublishingService svc = new HP.ElementsCPS.Data.ElementsDataPublishingService.ElementsDataPublishingService();
                    svc.Url = GetConfigValue(WRK_SERVICE_URL, env);
                    var resp = svc.updateReachWorkflow(request);
                    return (int)resp.contentKeyID;
                }
            }
            catch (SoapException ex)
            {
                PublishException pex = new PublishException("Error when call publish service.", ex.Detail, ex);
                throw pex;
            }
            catch (Exception ex)
            {
                PublishException pex = new PublishException("Unknown error when call publish service.", ex);
                throw pex;
            }
        }

        public void UnPublishWorkflow(Workflow workflow, Environment env)
        {
            if (env == Environment.Publication)
            {
                if (!workflow.ProductionId.HasValue)
                    throw new InvalidOperationException("Record don't have a valid elements publish Id.");
            }

            if (env == Environment.Validation)
            {
                if (!workflow.ValidationId.HasValue)
                    throw new InvalidOperationException("Record don't have a valid elements validation Id.");
            }

            try
            {
                lock (_lock)
                {
                    var request = new ElementsDataPublishingService.deleteReachWorkflowByIDRequest();
                    request.clientSystemCode = GetConfigValue(WRK_CLIENT_SYSTEM_ID, env);
                    request.appName = GetConfigValue(WRK_APP_NAME, env);
                    //request.contentName = GetConfigValue(WRK_CONTENT_NAME, env);

                    if (env == Environment.Publication)
                        request.contentKeyID = workflow.ProductionId.Value.ToString();
                    if (env == Environment.Validation)
                        request.contentKeyID = workflow.ValidationId.Value.ToString();

                    ElementsDataPublishingService.ElementsDataPublishingService svc = new ElementsDataPublishingService.ElementsDataPublishingService();
                    svc.Url = GetConfigValue(WRK_SERVICE_URL, env);
                    svc.deleteReachWorkflowByID(request);
                }
            }
            catch (SoapException ex)
            {
                PublishException pex = new PublishException("Error when call un-publish service.", ex.Detail, ex);
                throw pex;
            }
            catch (Exception ex)
            {
                PublishException pex = new PublishException("Unknown error when call un-publish service.", ex);
                throw pex;
            }
        }

        public int PublishWorkflowModule(WorkflowModule workflowModule, Environment env)
        {
            string moduleUrlValidation = ConfigurationManager.AppSettings["workflowModuleUrlValidation"];
            string moduleUrlPublication = ConfigurationManager.AppSettings["workflowModuleUrlPublication"];

            var target = new HP.ElementsCPS.Data.ElementsDataPublishingService.Module();

            target.name = workflowModule.Name;
            target.title = workflowModule.Title;
            target.version = string.Format("{0}.{1}", workflowModule.VersionMajor, workflowModule.VersionMinor);

            if (env == Environment.Publication)
            {
                target.url = string.Format("{0}/{1}/{2}/{3}.{4}/{5}", moduleUrlValidation, workflowModule.WorkflowModuleCategory, workflowModule.WorkflowModuleSubCategory, workflowModule.VersionMajor, workflowModule.VersionMinor, workflowModule.Filename);
                target.id = workflowModule.ProductionId ?? 0;
            }
            if (env == Environment.Validation)
            {
                target.url = string.Format("{0}/{1}/{2}/{3}.{4}/{5}", moduleUrlPublication, workflowModule.WorkflowModuleCategory, workflowModule.WorkflowModuleSubCategory, workflowModule.VersionMajor, workflowModule.VersionMinor, workflowModule.Filename);
                target.id = workflowModule.ValidationId ?? 0;
            }

            //Note:Fill target.conditions[]
            var conditionRecords = workflowModule.WorkflowModuleWorkflowConditionRecords();
            var conditions = new List<ElementsDataPublishingService.Condition>();
            foreach (var record in conditionRecords)
            {
                var condition = new ElementsDataPublishingService.Condition();
                condition.name = record.WorkflowCondition.Name;
                condition.@operator = record.WorkflowCondition.OperatorX;
                condition.value = record.WorkflowCondition.ValueX;
                conditions.Add(condition);
            }
            target.conditions = conditions.ToArray();
            target.idSpecified = true;//Note:auto generated code, always set to true.

            try
            {
                lock (_lock)
                {
                    //Note: double check if the url have id, maybe a concurrent user update it.
                    var cg = WorkflowModule.FetchByID(workflowModule.Id);
                    if (env == Environment.Validation)
                    {
                        if (cg.ValidationId != workflowModule.ValidationId)
                        {
                            throw new PublishException("Another user has validate this record.", null);
                        }
                    }
                    if (env == Environment.Publication)
                    {
                        if (cg.ProductionId != workflowModule.ProductionId)
                        {
                            throw new PublishException("Another user has publish this record.", null);
                        }
                    }

                    var request = new ElementsDataPublishingService.updateReachModuleRequest();
                    request.appName = GetConfigValue(WRK_APP_NAME, env);
                    request.clientSystemCode = GetConfigValue(WRK_CLIENT_SYSTEM_ID, env);
                    //request.contentName = GetConfigValue(WRK_CONTENT_NAME, env);
                    request.contentKeyID = target.id.ToString();
                    request.reachModule = target;

                    ElementsDataPublishingService.ElementsDataPublishingService svc = new HP.ElementsCPS.Data.ElementsDataPublishingService.ElementsDataPublishingService();
                    svc.Url = GetConfigValue(WRK_SERVICE_URL, env);
                    var resp = svc.updateReachModule(request);
                    return (int)resp.contentKeyID;
                }
            }
            catch (SoapException ex)
            {
                PublishException pex = new PublishException("Error when call publish service.", ex.Detail, ex);
                throw pex;
            }
            catch (Exception ex)
            {
                PublishException pex = new PublishException("Unknown error when call publish service.", ex);
                throw pex;
            }
        }

        public void UnPublishWorkflowModule(WorkflowModule workflowModule, Environment env)
        {
            if (env == Environment.Publication)
            {
                if (!workflowModule.ProductionId.HasValue)
                    throw new InvalidOperationException("Record don't have a valid elements publish Id.");
            }

            if (env == Environment.Validation)
            {
                if (!workflowModule.ValidationId.HasValue)
                    throw new InvalidOperationException("Record don't have a valid elements validation Id.");
            }

            try
            {
                lock (_lock)
                {
                    var request = new ElementsDataPublishingService.deleteReachModuleByIDRequest();
                    request.clientSystemCode = GetConfigValue(WRK_CLIENT_SYSTEM_ID, env);
                    request.appName = GetConfigValue(WRK_APP_NAME, env);
                    //request.contentName = GetConfigValue(WRK_CONTENT_NAME, env);

                    if (env == Environment.Publication)
                        request.contentKeyID = workflowModule.ProductionId.Value.ToString();
                    if (env == Environment.Validation)
                        request.contentKeyID = workflowModule.ValidationId.Value.ToString();

                    ElementsDataPublishingService.ElementsDataPublishingService svc = new ElementsDataPublishingService.ElementsDataPublishingService();
                    svc.Url = GetConfigValue(WRK_SERVICE_URL, env);
                    svc.deleteReachModuleByID(request);
                }
            }
            catch (SoapException ex)
            {
                PublishException pex = new PublishException("Error when call un-publish service.", ex.Detail, ex);
                throw pex;
            }
            catch (Exception ex)
            {
                PublishException pex = new PublishException("Unknown error when call un-publish service.", ex);
                throw pex;
            }
        }

		private string PerformTagSubstitution(Environment env, string stringToParse)
		{
			if (stringToParse.Contains(CFG_TAG_TRACKINGSERVERDOMAIN))
			{
				if (env == Environment.Validation)
				{
					string serverDomain = ConfigurationManager.AppSettings["configurationServiceTrackingServerDomainValidation"];
					stringToParse = stringToParse.Replace(CFG_TAG_TRACKINGSERVERDOMAIN, String.IsNullOrEmpty(serverDomain) ? "" : serverDomain);
				}

				if (env == Environment.Publication)
				{
					string serverDomain = ConfigurationManager.AppSettings["configurationServiceTrackingServerDomainPublication"];
					stringToParse = stringToParse.Replace(CFG_TAG_TRACKINGSERVERDOMAIN, String.IsNullOrEmpty(serverDomain) ? "" : serverDomain);
				}
			}	
			return stringToParse;
		}
	}
}
