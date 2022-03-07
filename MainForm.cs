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
            gridView.Columns[0].HeaderText = "�����";
            gridView.Columns[1].HeaderText = "����Ȗ�";
            gridView.Columns[2].HeaderText = "�⏕�Ȗ�";
            gridView.Columns[3].HeaderText = "�������z";
            gridView.Columns[4].HeaderText = "�x�o���z";
            
            gridView.Rows.Add("2022/04/01", "��ʔ�", "�V����", 0, 20000);
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
            dlg.Filter = "CSV�t�@�C��(*.csv)|*.csv|���ׂẴt�@�C��(*.*)|*.*";
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            gridView.Rows.Clear();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 0.1");
        }

        private void �I��XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void �ۑ�SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnSave_Click(sender, e);
        }

        private void �J��OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void �V�KNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnClear_Click(sender, e);
        }
    }
}

