using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.SqlClient;
using HP.ElementsCPS.Core.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Web.Utility;
using System.Net;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
    public partial class WorkflowURLReportPanel : UserControl
    {

        #region Properties

        public string URLToParse { get; set; }

        #endregion

        #region Structures

        class URLInformation
        {
            public string URL { get; set; }
            public int URLCount { get; set; }
            public int URLTotal { get; set; }
            public string Percent { get { return String.Format("{0:0%}", (decimal)URLCount / (decimal)(URLTotal == 0 ? 1 : URLTotal)); } }
        }

        #endregion

        #region PageEvents

        protected override void OnDataBinding(EventArgs e)
        {
            this.repWorkflowURL.DataSource = ParseURL();
            this.repWorkflowURL.DataBind();
        }

        #endregion

        #region Methods

        private List<URLInformation> ParseURL()
        {
            List<URLInformation> urlInformationList = new List<URLInformation>();
            //this.URLToParse = "http://www.toyota.com/newbee/modules/category/subcategory/major.minor/filename{Aclassic|Bholiday|Bholiday|Choliday|Bholiday}.htm";
            //this.URLToParse = "http://www.toyota.com/newbee/modules/category/subcategory/major.minor/{filename}.htm";
            string urlToParse = this.URLToParse.TrimToNull();
            if (string.IsNullOrEmpty(urlToParse))
            {
                return null;
            }

            int startCurly = urlToParse.IndexOf("{");
            int endCurly = urlToParse.IndexOf("}");
            if (startCurly <= 0 || endCurly <= 0)
            {
                URLInformation urlInformation = new URLInformation();
                urlInformation.URL = urlToParse;
                urlInformation.URLCount = 1;
                urlInformation.URLTotal = 1;
                urlInformationList.Add(urlInformation);
            }
            else
            {
                string urlStart = urlToParse.Substring(0, startCurly);
                string urlEnd = urlToParse.Substring(endCurly + 1);
                string parseContent = urlToParse.Substring(startCurly + 1, endCurly - startCurly - 1);
                if (parseContent.IndexOf("|") > 0)
                {
                    string[] contents = parseContent.Split('|');
                    foreach (string content in contents)
                    {
                        bool urlFound = false;
                        string newURL = urlStart + content + urlEnd;
                        for (int i = 0; i < urlInformationList.Count; i++)
                        {
                            if (urlInformationList[i].URL == newURL)
                            {
                                urlInformationList[i].URLCount = urlInformationList[i].URLCount + 1;
                                urlFound = true;
                            }
                        }

                        // add new
                        if (!urlFound)
                        {
                            URLInformation urlInformation = new URLInformation();
                            urlInformation.URL = newURL;
                            urlInformation.URLCount = 1;
                            urlInformation.URLTotal = contents.Length;
                            urlInformationList.Add(urlInformation);
                        }
                    }
                }
                else
                {
                    URLInformation urlInformation = new URLInformation();
                    urlInformation.URL = urlStart + parseContent + urlEnd;
                    urlInformation.URLCount = 1;
                    urlInformation.URLTotal = 1;
                    urlInformationList.Add(urlInformation);
                }
            }
            return urlInformationList;
        }

        #endregion

    }
}