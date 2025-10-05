using System;
using System.Globalization;
using System.Windows.Forms;
using WinFormsLabel = System.Windows.Forms.Label;

namespace Hackathlone2025
{
    public class MainForm : Form
    {
        private ComboBox cmbMaterial;
        private TextBox txtKg;
        private Button btnProcess;
        private Button btnRecycle;
        private TextBox txtOutput;
        private WinFormsLabel lblTitle;
        private WinFormsLabel lblMaterial;
        private WinFormsLabel lblKg;
        private GroupBox grpInput;
        private GroupBox grpOutput;

   
        private Organic_Plastic PVDF;
        private Organic_Plastic Nomex;
        private Organic_Plastic Polyethylene;
        private Organic_Plastic Nitrile;
        private Organic_Plastic PLA;
        private Organic_Plastic PHA;
        private Organic_Plastic PBS;

        public MainForm()
        {
            this.Text = "Waste Recycling System";
            this.Width = 900;
            this.Height = 640;
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeMaterials();
            InitializeControls();
        }

        private void InitializeMaterials()
        {
            PVDF = new Organic_Plastic
            {
                thermoprops = "Thermoplastic",
                plasticprops = "Nonbio",
                syngas_val = 0.25
            };

            Nomex = new Organic_Plastic(1, 0, 0, 0, 0, 0, 0.5, 0.5, "Thermosetplastic", "Nonbio");

            Polyethylene = new Organic_Plastic
            {
                thermoprops = "Thermoplastic",
                plasticprops = "Nonbio",
                syngas_val = 0.30
            };

            Nitrile = new Organic_Plastic(1, 0, 0, 0, 0, 0, 0, 0.18, "Thermosetplastic", "Nonbio");

            PLA = new Organic_Plastic(1, 0.52, 0.16, 0.36, 0.68, 0.20, 0.48, 0.00, "Thermoplastic", "Bio");
            PHA = new Organic_Plastic(1, 1.12, 0.40, 0.73, 0.30, 0.20, 0.10, 0.00, "Thermoplastic", "Bio");
            PBS = new Organic_Plastic(1, 0.37, 0.13, 0.24, 0.83, 0.20, 0.63, 0.00, "thermoplastic", "Bio");
        }

        private void InitializeControls()
        {
            lblTitle = new WinFormsLabel();
            lblTitle.Text = "Waste Recycling System";
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.Top = 10;
            lblTitle.Left = 20;
            this.Controls.Add(lblTitle);

            grpInput = new GroupBox();
            grpInput.Text = "Deposit Martian Bio/Non-Bio Waste Material";
            grpInput.Left = 20;
            grpInput.Top = 60;
            grpInput.Width = 830;
            grpInput.Height = 140;
            this.Controls.Add(grpInput);

            lblMaterial = new WinFormsLabel();
            lblMaterial.Text = "Waste Material:";
            lblMaterial.AutoSize = true;
            lblMaterial.Left = 5;
            lblMaterial.Top = 35;
            grpInput.Controls.Add(lblMaterial);

            cmbMaterial = new ComboBox();
            cmbMaterial.Left = 90;
            cmbMaterial.Top = 30;
            cmbMaterial.Width = 280;
            cmbMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterial.Items.Add("PLA (Bio, Thermoplastic)");
            cmbMaterial.Items.Add("PHA (Bio, Thermoplastic)");
            cmbMaterial.Items.Add("PBS (Bio, Thermoplastic)");
            cmbMaterial.Items.Add("PVDF (NonBio, Thermoplastic)");
            cmbMaterial.Items.Add("Nomex (NonBio, Thermosetplastic)");
            cmbMaterial.Items.Add("Polyethylene (NonBio, Thermoplastic)");
            cmbMaterial.Items.Add("Nitrile (NonBio, Thermosetplastic)");
            cmbMaterial.SelectedIndex = 0;
            grpInput.Controls.Add(cmbMaterial);

            lblKg = new WinFormsLabel();
            lblKg.Text = "Amount (kg):";
            lblKg.AutoSize = true;
            lblKg.Left = 400;
            lblKg.Top = 35;
            grpInput.Controls.Add(lblKg);

            txtKg = new TextBox();
            txtKg.Left = 490;
            txtKg.Top = 30;
            txtKg.Width = 120;
            txtKg.Text = "1.0";
            grpInput.Controls.Add(txtKg);

            btnProcess = new Button();
            btnProcess.Text = "Process";
            btnProcess.Left = 640;
            btnProcess.Top = 28;
            btnProcess.Width = 150;
            btnProcess.Height = 32;
            btnProcess.Click += BtnProcess_Click;
            grpInput.Controls.Add(btnProcess);

            btnRecycle = new Button();
            btnRecycle.Text = "Recycle";
            btnRecycle.Left = 640;
            btnRecycle.Top = 84;
            btnRecycle.Width = 150;
            btnRecycle.Height = 32;
            btnRecycle.Click += BtnRecycle_Click;
            grpInput.Controls.Add(btnRecycle);

            grpOutput = new GroupBox();
            grpOutput.Text = "Results";
            grpOutput.Left = 20;
            grpOutput.Top = 210;
            grpOutput.Width = 830;
            grpOutput.Height = 370;
            this.Controls.Add(grpOutput);

            txtOutput = new TextBox();
            txtOutput.Multiline = true;
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Left = 20;
            txtOutput.Top = 25;
            txtOutput.Width = 790;
            txtOutput.Height = 330;
            txtOutput.Font = new System.Drawing.Font("Consolas", 10F);
            grpOutput.Controls.Add(txtOutput);
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            double kg;
            if (!TryParseDouble(txtKg.Text, out kg) || kg <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for kg.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Organic_Plastic selected = GetSelectedMaterial();
            if (selected == null)
            {
                MessageBox.Show("Please select a material.", "Selection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selected.MaterialAmount = kg;
            string report = selected.ProcessToString();
            txtOutput.Text = report;
        }

        private void BtnRecycle_Click(object sender, EventArgs e)
        {
            double kg;
            if (!TryParseDouble(txtKg.Text, out kg) || kg <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for kg.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Organic_Plastic selected = GetSelectedMaterial();
            if (selected == null)
            {
                MessageBox.Show("Please select a material.", "Selection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selected.MaterialAmount = kg;
            string report = selected.BuildRecycleReport();
            txtOutput.Text = report;
        }

        private Organic_Plastic GetSelectedMaterial()
        {
            int idx = cmbMaterial.SelectedIndex;
            Organic_Plastic selected = null;

            if (idx == 0) selected = PLA;
            else if (idx == 1) selected = PHA;
            else if (idx == 2) selected = PBS;
            else if (idx == 3) selected = PVDF;
            else if (idx == 4) selected = Nomex;
            else if (idx == 5) selected = Polyethylene;
            else if (idx == 6) selected = Nitrile;

            return selected;
        }

        private bool TryParseDouble(string text, out double value)
        {
            return double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value
            ) || double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out value
            );
        }
    }
}
