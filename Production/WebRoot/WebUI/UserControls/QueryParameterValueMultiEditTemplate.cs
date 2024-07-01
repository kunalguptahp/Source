using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public class QueryParameterValueMultiEditTemplate : ITemplate
	{
		public const string REMOVE_FLAG = "RemoveFlag";
		public const string ID = "ID";
		public const string TARGET_URL = "TargetUrl";
		public const string HIDDEN = "Hidden";
		public const string ERROR = "Error";

		public const string PREFIX_DDL = "ddl";
		public const string PREFIX_TXT = "txt";
		public const string PREFIX_LBL = "lbl";
		public const string PREFIX_CHK = "chk";

		public static readonly string CHK_REMOVE_FLAG = PREFIX_CHK + REMOVE_FLAG;
		public static readonly string LBL_ID = PREFIX_LBL + ID;
		public static readonly string LBL_ERROR = PREFIX_LBL + ERROR;
		public static readonly string LBL_HIDDEN = PREFIX_LBL + HIDDEN;
		public static readonly string TXT_TARGET_URL = PREFIX_TXT + TARGET_URL;

		private DataControlRowType _templateType;
		private string _columnName;
		private int _id;

		public QueryParameterValueMultiEditTemplate(DataControlRowType type, string colname, int id)
		{
			_templateType = type;
			_columnName = colname;
			this._id = id;
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
					if (_columnName == HIDDEN)
						lblHead.Visible = false;
					break;

				case DataControlRowType.DataRow:
					switch (_columnName)
					{
						case REMOVE_FLAG:
							CheckBox chk = new CheckBox();
							chk.ID = PREFIX_CHK + _columnName;
							container.Controls.Add(chk);
							break;

						case ID:
						case HIDDEN:
						case ERROR:
							Label lbl = new Label();
							lbl.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lbl);
							break;

						case TARGET_URL:
							TextBox txt = new TextBox();
							txt.ID = PREFIX_TXT + _columnName;
							txt.Width = 300;
							container.Controls.Add(txt);
							break;

						default:
							DropDownList ddlParam = new DropDownList();
							ddlParam.ID = PREFIX_DDL + _columnName;
							container.Controls.Add(ddlParam);
							BindListControl(ddlParam);
							break;
					}
					break;

				default:
					//Do nothing;
					break;
			}
		}

		private void BindListControl(DropDownList ddl)
		{
			//QueryParameterValueQuerySpecification qs = new QueryParameterValueQuerySpecification();
			//qs.QueryParameterId = _id;
			Global.BindQueryParameterValueListControl(ddl, _id, RowStatus.RowStatusId.Active);
			ddl.InsertItem(0, "", Global.GetSelectListText());
		}

		#endregion
	}
}
