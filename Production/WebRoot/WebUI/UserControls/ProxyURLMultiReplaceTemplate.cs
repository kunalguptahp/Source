using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public class ProxyURLMultiReplaceTemplate : ITemplate
	{
		public const string ID = "ID";
		public const string TARGET_URL = "TargetUrl";
		public const string DESCRIPTION = "Description";
		public const string REDIRECTOR_TYPE = "Redirector Type";
		public const string BRAND_PARAMETER_VALUE = "Brand";
		public const string CYCLE_PARAMETER_VALUE = "Cycle";
		public const string LOCALE_PARAMETER_VALUE = "Locale";
		public const string PARTNER_CATEGORY_PARAMETER_VALUE = "Partner Category";
		public const string PLATFORM_PARAMETER_VALUE = "Platform";
		public const string TOUCHPOINT_PARAMETER_VALUE = "Touchpoint";

		public const string PREFIX_DDL = "ddl";
		public const string PREFIX_TXT = "txt";
		public const string PREFIX_LBL = "lbl";

		public static readonly string LBL_ID = PREFIX_LBL + ID;
		public static readonly string TXT_TARGET_URL = PREFIX_TXT + TARGET_URL;
		public static readonly string TXT_DESCRIPTION = PREFIX_TXT + DESCRIPTION;
		public static readonly string LBL_REDIRECTOR_TYPE = PREFIX_LBL + REDIRECTOR_TYPE;
		public static readonly string LBL_BRAND_PARAMETER_VALUE = PREFIX_LBL + BRAND_PARAMETER_VALUE;
		public static readonly string LBL_CYCLE_PARAMETER_VALUE = PREFIX_LBL + CYCLE_PARAMETER_VALUE;
		public static readonly string LBL_LOCALE_PARAMETER_VALUE = PREFIX_LBL + LOCALE_PARAMETER_VALUE;
		public static readonly string LBL_PARTNER_CATEGORY_PARAMETER_VALUE = PREFIX_LBL + PARTNER_CATEGORY_PARAMETER_VALUE;
		public static readonly string LBL_PLATFORM_PARAMETER_VALUE = PREFIX_LBL + PLATFORM_PARAMETER_VALUE;
		public static readonly string LBL_TOUCHPOINT_PARAMETER_VALUE = PREFIX_LBL + TOUCHPOINT_PARAMETER_VALUE;

		private DataControlRowType _templateType;
		private string _columnName;

		public ProxyURLMultiReplaceTemplate(DataControlRowType type, string colname)
		{
			_templateType = type;
			_columnName = colname;
		}

		#region ITemplate Members

		public void InstantiateIn(Control container)
		{
			switch (_templateType)
			{
				case DataControlRowType.Header:
					Label lblHead = new Label();
					lblHead.Text = _columnName;
					container.Controls.Add(lblHead);
					break;

				case DataControlRowType.DataRow:
					switch (_columnName)
					{
						case ID:
							Label lblId = new Label();
							lblId.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblId);
							break;

						case TARGET_URL:
							TextBox txtTargetURL = new TextBox();
							txtTargetURL.ID = PREFIX_TXT + _columnName;
							txtTargetURL.Width = 300;
							container.Controls.Add(txtTargetURL);
							break;

						case DESCRIPTION:
							TextBox txtDesc = new TextBox();
							txtDesc.ID = PREFIX_TXT + _columnName;
							txtDesc.Width = 300;
							container.Controls.Add(txtDesc);
							break;

						case REDIRECTOR_TYPE:
							Label lblRedirectorType = new Label();
							lblRedirectorType.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblRedirectorType);
							break;

						case BRAND_PARAMETER_VALUE:
							Label lblBrand = new Label();
							lblBrand.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblBrand);
							break;

						case CYCLE_PARAMETER_VALUE:
							Label lblCycle = new Label();
							lblCycle.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblCycle);
							break;

						case LOCALE_PARAMETER_VALUE:
							Label lblLocale = new Label();
							lblLocale.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblLocale);
							break;

						case PARTNER_CATEGORY_PARAMETER_VALUE:
							Label lblPartnerCategory = new Label();
							lblPartnerCategory.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblPartnerCategory);
							break;

						case PLATFORM_PARAMETER_VALUE:
							Label lblPlatform = new Label();
							lblPlatform.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblPlatform);
							break;

						case TOUCHPOINT_PARAMETER_VALUE:
							Label lblTouchpoint = new Label();
							lblTouchpoint.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lblTouchpoint);
							break;

						default:
							//Label lbl3 = new Label();
							//lbl3.ID = PREFIX_LBL + _id.ToString();
							//container.Controls.Add(lbl3);
							break;
					}
					break;

				default:
					//Do nothing;
					break;
			}
		}

		#endregion
	}
}
