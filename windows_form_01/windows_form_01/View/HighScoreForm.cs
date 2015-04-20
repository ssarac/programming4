using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_01.Model;

namespace windows_form_01.View
{
    public partial class HighScoreForm : Form
    {
        public HighScoreForm()
        {
            InitializeComponent();

            listView.Columns.Add("Name", 100, HorizontalAlignment.Left);
            listView.Columns.Add("Time", 100, HorizontalAlignment.Left);

            List<GameScore> gameScores = GameScore.Scores;

            
            string[] listItemData = new string[2];
            ListViewItem listViewItem;
            foreach (GameScore gameScore in gameScores)
            {
                listItemData[0] = gameScore.Name;
                listItemData[1] = gameScore.TimeString;

                listViewItem = new ListViewItem(listItemData);
                listView.Items.Add(listViewItem);
            }
        }
    }
}
