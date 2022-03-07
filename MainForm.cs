namespace VirtualKaikei
{
    using System.Text;

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            gridView.ColumnCount = 5;
            gridView.Columns[0].HeaderText = "取引日";
            gridView.Columns[1].HeaderText = "勘定科目";
            gridView.Columns[2].HeaderText = "補助科目";
            gridView.Columns[3].HeaderText = "収入金額";
            gridView.Columns[4].HeaderText = "支出金額";
            
            gridView.Rows.Add("2022/04/01", "交通費", "新幹線", 0, 20000);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gridView.Rows.Add(
                dateTran.Text,
                txtTitle.Text,
                txtSub.Text,
                txtInPrice.Text,
                txtOutPrice.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                saveToCsv(dlg.FileName);
            }
        }

        private void saveToCsv(String filename)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding enc = Encoding.GetEncoding("shift_Jis");
            if (enc == null) enc = Encoding.Unicode;
            using (StreamWriter sw = new StreamWriter(filename, false, enc))
            {
                string s = "";
                for (int iCol = 0; iCol < gridView.Columns.Count; iCol++)
                {
                    if (iCol > 0)
                    {
                        s += ",";
                    }
                    object o = gridView.Columns[iCol].HeaderCell.Value;
                    string c = o.ToString() ?? "";
                    if (c is not null) s += quoteCell(c);
                }
                sw.WriteLine(s);
                for (int iRow = 0; iRow < gridView.Rows.Count; iRow++)
                {
                    s = "";
                    for (int iCol = 0; iCol < gridView.Columns.Count; iCol++)
                    {
                        if (iCol > 0)
                        {
                            s += ",";
                        }
                        object o = gridView[iCol, iRow].Value;
                        string c = (o is not null) ? o.ToString() ?? "" : "";
                        s += quoteCell(c);
                    }
                    sw.WriteLine(s);
                }
            }
        }
        private string quoteCell(String cell)
        {
            cell = cell.Trim();
            cell = cell.Replace("\"", "'");
            return "\"" + cell + "\"";
        }
    }
}

