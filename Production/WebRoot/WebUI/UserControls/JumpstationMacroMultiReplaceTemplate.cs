using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public class JumpstationMacroMultiReplaceTemplate : ITemplate
	{
		public const string ID = "ID";
		public const string DESCRIPTION = "Description";
        public const string NAME = "Name";

		public const string PREFIX_DDL = "ddl";
		public const string PREFIX_TXT = "txt";
		public const string PREFIX_LBL = "lbl";

		public static readonly string LBL_ID = PREFIX_LBL + ID;
		public static readonly string LBL_NAME = PREFIX_LBL + NAME;
        public static readonly string TXT_DESCRIPTION = PREFIX_TXT + DESCRIPTION;

		private DataControlRowType _templateType;
		private string _columnName;
		private int _id;

        public JumpstationMacroMultiReplaceTemplate(DataControlRowType type, string colname, int id)
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
					break;

				case DataControlRowType.DataRow:
					switch (_columnName)
					{
						case ID:
							Label lbl = new Label();
							lbl.ID = PREFIX_LBL + _columnName;
							container.Controls.Add(lbl);
							break;

                        case NAME:
                            Label lblName = new Label();
                            lblName.ID = PREFIX_LBL + _columnName;
					        lblName.Width = 200;
                            container.Controls.Add(lblName);
                            break;

						case DESCRIPTION:
							TextBox txtDesc = new TextBox();
							txtDesc.ID = PREFIX_TXT + _columnName;
							txtDesc.Width = 300;
							container.Controls.Add(txtDesc);
							break;

						default:
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
