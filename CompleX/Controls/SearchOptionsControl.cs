using System;
using System.ComponentModel;
using GrepWrap;
using System.Windows.Forms;

namespace CompleX.Controls
{
    /// <summary>
    /// SearchOptionsControl
    /// </summary>
    public partial class SearchOptionsControl : UserControl,INotifyPropertyChanged
    {
        private SearchOptions searchOptions;

        [Browsable(true)]
        public event EventHandler SearchOptionsChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                if (isExpanded)
                {
                    Height = 20;
                    buttonExpand.Text = @"-";
                }
                else
                {
                    Height = 135;
                    buttonExpand.Text = @"+";
                }
                OnPropertyChanged("IsExpanded");
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public SearchOptionsControl()
        {
            InitializeComponent();
            comboBoxRegexOrWildCard.SelectedIndex = 0;
        }

        /// <summary>
        /// GrepWrap SearchOptions
        /// </summary>
        public SearchOptions SearchOptions
        {
            get
            {
                UpdateSearchOptions();
                return searchOptions;
            }
            set
            {
                searchOptions = value;
                UpdateCheckboxes();
                OnSearchOptionsChanged(new EventArgs());
            }
        }

        private void UpdateCheckboxes()
        {
            checkBoxIgnoreCase.Checked = (0 != (searchOptions & SearchOptions.IgnoreCase));
            checkBoxMatchWholeWord.Checked = (0 != (searchOptions & SearchOptions.WholeWord));
            checkBoxInvertSearch.Checked = (0 != (searchOptions & SearchOptions.InvertSearch));
            checkBoxWildCardsOrRegex.Checked = (0 != (searchOptions & SearchOptions.WildCards) || 0 != (searchOptions & SearchOptions.RegularExpression));
            if(checkBoxWildCardsOrRegex.Checked)
                comboBoxRegexOrWildCard.SelectedIndex = 0 != (searchOptions & SearchOptions.RegularExpression) ? 0 : 1;
            
        }

        private void UpdateSearchOptions()
        {
            // TODO: use or (|) instead of XOR(^) 
            var options = new SearchOptions();
            if (checkBoxIgnoreCase.Checked) options = options ^ SearchOptions.IgnoreCase;
            if (checkBoxMatchWholeWord.Checked) options = options ^ SearchOptions.WholeWord;
            if (checkBoxInvertSearch.Checked) options = options ^ SearchOptions.InvertSearch;
            if (checkBoxWildCardsOrRegex.Checked)
            {
                if(comboBoxRegexOrWildCard.SelectedIndex == 0)
                    options = options ^ SearchOptions.RegularExpression;
                else
                    options = options ^ SearchOptions.WildCards;
            }
            searchOptions = options;
        }

        private void CheckBoxWildCardsOrRegexCheckedChanged(object sender, System.EventArgs e)
        {
            comboBoxRegexOrWildCard.Enabled = checkBoxWildCardsOrRegex.Checked;
            OnSearchOptionsChanged(new EventArgs());
        }

        private void ButtonExpandClick(object sender, System.EventArgs e)
        {
            IsExpanded = !IsExpanded;
        }

        private void OnSearchOptionsChanged(EventArgs e)
        {
            EventHandler handler = SearchOptionsChanged;
            if (handler != null) handler(this, e);
            OnPropertyChanged("SearchOptions");
        }

        private void OnPropertyChanged(string propname)
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propname));
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            OnSearchOptionsChanged(new EventArgs());
        }

    }
}
