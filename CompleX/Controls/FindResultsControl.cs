using System.Collections.Generic;
using System.Windows.Forms;
using CompleX.Properties;
using CompleX.Services;
using GrepWrap;

namespace CompleX.Controls
{
    public partial class FindResultsControl : UserControl
    {

        public FindResultsControl()
        {
            InitializeComponent();
        }

        public void UpdateData(IEnumerable<Occurence> findResults)
        {
            if (dataSetFindResults.TableFindResults.Count > 0)
            {
                if( MessageService.AskDsa(Resources.ConfirmClearFindResults,Resources.Clear, "CLEAR_OLD_FINDRESULTS"))
                    dataSetFindResults.TableFindResults.Clear();
            }
            foreach (var findResult in findResults)
            {
                dataSetFindResults.TableFindResults.AddTableFindResultsRow(findResult.Filename,
                                                                           findResult.Match,
                                                                           findResult.LineNumber,
                                                                           findResult.StartPosition,
                                                                           findResult.EndPosition);
            }
        }

        private void simpleButtonClear_Click(object sender, System.EventArgs e)
        {
            dataSetFindResults.TableFindResults.Clear();
        }

    }
}
