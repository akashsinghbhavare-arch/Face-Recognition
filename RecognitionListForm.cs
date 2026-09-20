using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MultiFaceRec
{
    public class EditPersonDialog : Form
    {
        private TextBox txtName;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnCancel;

        public string UpdatedName { get; private set; }
        public bool DeleteRequested { get; private set; }

        public EditPersonDialog(string currentName, int sampleCount)
        {
            this.Text = "Update / Delete Registered Person";
            this.Size = new Size(420, 230);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            Label lblInfo = new Label
            {
                Text = string.Format("Person: \"{0}\"", currentName),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(22, 16),
                AutoSize = true
            };

            Label lblSamples = new Label
            {
                Text = string.Format("Trained Samples: {0} {1} registered", sampleCount, sampleCount == 1 ? "shot" : "shots (Burst training)"),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(23, 40),
                AutoSize = true
            };

            Label lblPrompt = new Label
            {
                Text = "Edit the name below to rename, or delete to remove this person entirely:",
                ForeColor = Color.FromArgb(73, 80, 87),
                Location = new Point(23, 64),
                AutoSize = true
            };

            txtName = new TextBox
            {
                Text = currentName,
                Location = new Point(24, 88),
                Width = 356,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)))
            };
            txtName.SelectAll();

            btnUpdate = new Button
            {
                Text = "Update Name",
                Location = new Point(24, 134),
                Size = new Size(115, 34),
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnUpdate.Click += delegate(object sender, EventArgs e)
            {
                string trimmed = txtName.Text.Trim();
                if (string.IsNullOrEmpty(trimmed))
                {
                    MessageBox.Show("Name cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UpdatedName = trimmed;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            btnDelete = new Button
            {
                Text = "Delete Person",
                Location = new Point(148, 134),
                Size = new Size(115, 34),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDelete.Click += delegate(object sender, EventArgs e)
            {
                var res = MessageBox.Show(
                    string.Format("Are you sure you want to delete person \"{0}\" and all {1} trained sample(s)?\n\nThis will remove this person from recognition entirely.", currentName, sampleCount),
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    DeleteRequested = true;
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                }
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(272, 134),
                Size = new Size(108, 34),
                UseVisualStyleBackColor = true,
                Cursor = Cursors.Hand
            };
            btnCancel.Click += delegate(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.Add(lblInfo);
            this.Controls.Add(lblSamples);
            this.Controls.Add(lblPrompt);
            this.Controls.Add(txtName);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnUpdate;
            this.CancelButton = btnCancel;
        }
    }

    public partial class RecognitionListForm : Form
    {
        private FrmPrincipal mainForm;
        private DataGridView gridFaces;
        private Label lblHeaderTitle;
        private Label lblPrivacyNotice;
        private Label lblStatusSummary;
        private TextBox txtSearch;
        private Button btnUpdateSelected;
        private Button btnDeleteSelected;
        private Button btnRefresh;
        private Button btnClose;
        private ContextMenuStrip contextMenu;

        public RecognitionListForm(FrmPrincipal form)
        {
            this.mainForm = form;
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "List of Recognition";
            this.Size = new Size(780, 500);
            this.MinimumSize = new Size(650, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            // Header Panel
            Panel panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 76,
                BackColor = Color.FromArgb(34, 45, 50),
                Padding = new Padding(16, 10, 16, 10)
            };

            lblHeaderTitle = new Label
            {
                Text = "List of Recognition",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                AutoSize = true,
                Location = new Point(15, 10)
            };

            lblPrivacyNotice = new Label
            {
                Text = "Each enrolled person is registered once regardless of burst shots. Face photos are hidden for privacy.",
                ForeColor = Color.FromArgb(180, 205, 220),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                AutoSize = true,
                Location = new Point(16, 39)
            };

            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Controls.Add(lblPrivacyNotice);

            // Search and filter toolbar
            Panel panelToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Color.FromArgb(238, 242, 246),
                Padding = new Padding(12, 6, 12, 6)
            };

            Label lblSearch = new Label
            {
                Text = "Search Person:",
                AutoSize = true,
                Location = new Point(12, 13),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)))
            };

            txtSearch = new TextBox
            {
                Location = new Point(115, 10),
                Width = 240,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)))
            };
            txtSearch.TextChanged += delegate(object sender, EventArgs e) { FilterData(); };

            panelToolbar.Controls.Add(lblSearch);
            panelToolbar.Controls.Add(txtSearch);

            // Bottom action panel
            Panel panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 55,
                BackColor = Color.FromArgb(235, 238, 242),
                Padding = new Padding(12, 10, 12, 10)
            };

            lblStatusSummary = new Label
            {
                Text = "Total Registered Persons: 0",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(12, 18)
            };

            btnUpdateSelected = new Button
            {
                Text = "Update Name",
                Size = new Size(110, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(370, 11),
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnUpdateSelected.Click += delegate(object sender, EventArgs e)
            {
                string person = GetSelectedPersonName();
                if (!string.IsNullOrEmpty(person)) PromptEditPerson(person);
            };

            btnDeleteSelected = new Button
            {
                Text = "Delete Person",
                Size = new Size(110, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(488, 11),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDeleteSelected.Click += delegate(object sender, EventArgs e)
            {
                string person = GetSelectedPersonName();
                if (!string.IsNullOrEmpty(person)) PromptDeletePerson(person);
            };

            btnRefresh = new Button
            {
                Text = "Refresh",
                Size = new Size(75, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(606, 11),
                UseVisualStyleBackColor = true,
                Cursor = Cursors.Hand
            };
            btnRefresh.Click += delegate(object sender, EventArgs e) { LoadData(); };

            btnClose = new Button
            {
                Text = "Close",
                Size = new Size(72, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(688, 11),
                UseVisualStyleBackColor = true,
                Cursor = Cursors.Hand
            };
            btnClose.Click += delegate(object sender, EventArgs e) { this.Close(); };

            panelBottom.Controls.Add(lblStatusSummary);
            panelBottom.Controls.Add(btnUpdateSelected);
            panelBottom.Controls.Add(btnDeleteSelected);
            panelBottom.Controls.Add(btnRefresh);
            panelBottom.Controls.Add(btnClose);

            // DataGridView
            gridFaces = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(230, 230, 230),
                EnableHeadersVisualStyles = false
            };

            gridFaces.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 246);
            gridFaces.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            gridFaces.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            gridFaces.ColumnHeadersHeight = 34;
            gridFaces.RowTemplate.Height = 32;
            gridFaces.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 255);

            // Columns
            gridFaces.Columns.Add("colNo", "#");
            gridFaces.Columns.Add("colName", "Recognized Face Name (Click to Edit)");
            gridFaces.Columns.Add("colSamples", "Trained Angles / Shots");
            gridFaces.Columns.Add("colStatus", "Status");

            // Action button columns
            DataGridViewButtonColumn btnColUpdate = new DataGridViewButtonColumn
            {
                Name = "colBtnUpdate",
                HeaderText = "Update",
                Text = "Update",
                UseColumnTextForButtonValue = true,
                Width = 85
            };
            btnColUpdate.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);

            DataGridViewButtonColumn btnColDelete = new DataGridViewButtonColumn
            {
                Name = "colBtnDelete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 85
            };
            btnColDelete.DefaultCellStyle.ForeColor = Color.FromArgb(220, 53, 69);

            gridFaces.Columns.Add(btnColUpdate);
            gridFaces.Columns.Add(btnColDelete);

            gridFaces.Columns["colNo"].Width = 45;
            gridFaces.Columns["colNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFaces.Columns["colSamples"].Width = 160;
            gridFaces.Columns["colSamples"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFaces.Columns["colStatus"].Width = 90;
            gridFaces.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFaces.Columns["colName"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            gridFaces.Columns["colName"].DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);

            // Grid Events
            gridFaces.CellContentClick += GridFaces_CellContentClick;
            gridFaces.CellClick += GridFaces_CellClick;
            gridFaces.CellDoubleClick += GridFaces_CellDoubleClick;

            // Context Menu
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem menuUpdate = new ToolStripMenuItem("Update Name...", null, delegate(object sender, EventArgs e)
            {
                string person = GetSelectedPersonName();
                if (!string.IsNullOrEmpty(person)) PromptEditPerson(person);
            });
            ToolStripMenuItem menuDelete = new ToolStripMenuItem("Delete Person...", null, delegate(object sender, EventArgs e)
            {
                string person = GetSelectedPersonName();
                if (!string.IsNullOrEmpty(person)) PromptDeletePerson(person);
            });
            contextMenu.Items.Add(menuUpdate);
            contextMenu.Items.Add(menuDelete);
            gridFaces.ContextMenuStrip = contextMenu;

            this.Controls.Add(gridFaces);
            this.Controls.Add(panelToolbar);
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelBottom);
        }

        private void GridFaces_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = gridFaces.Columns[e.ColumnIndex].Name;
            string personName = Convert.ToString(gridFaces.Rows[e.RowIndex].Tag);

            if (colName == "colBtnUpdate")
            {
                PromptEditPerson(personName);
            }
            else if (colName == "colBtnDelete")
            {
                PromptDeletePerson(personName);
            }
        }

        private void GridFaces_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // When clicked on the person name column
            if (gridFaces.Columns[e.ColumnIndex].Name == "colName")
            {
                string personName = Convert.ToString(gridFaces.Rows[e.RowIndex].Tag);
                PromptEditPerson(personName);
            }
        }

        private void GridFaces_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string personName = Convert.ToString(gridFaces.Rows[e.RowIndex].Tag);
            PromptEditPerson(personName);
        }

        private string GetSelectedPersonName()
        {
            if (gridFaces.SelectedRows.Count > 0)
            {
                return Convert.ToString(gridFaces.SelectedRows[0].Tag);
            }
            if (gridFaces.CurrentRow != null)
            {
                return Convert.ToString(gridFaces.CurrentRow.Tag);
            }
            return null;
        }

        public void LoadData()
        {
            FilterData();
        }

        private void FilterData()
        {
            if (mainForm == null) return;

            List<FrmPrincipal.RegisteredPerson> registeredPersons = mainForm.GetRegisteredPersons();
            string filter = txtSearch.Text.Trim();

            gridFaces.Rows.Clear();
            int displayNo = 1;
            int totalShots = 0;

            for (int i = 0; i < registeredPersons.Count; i++)
            {
                FrmPrincipal.RegisteredPerson person = registeredPersons[i];
                totalShots += person.SampleCount;

                if (!string.IsNullOrEmpty(filter) && person.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    continue;
                }

                string sampleDesc = person.SampleCount > 1
                    ? string.Format("{0} shots (Burst)", person.SampleCount)
                    : "1 shot (Single)";

                int rowIndex = gridFaces.Rows.Add(
                    displayNo++,
                    person.Name,
                    sampleDesc,
                    "Enrolled"
                );

                // Store person name in row Tag for actions
                gridFaces.Rows[rowIndex].Tag = person.Name;
            }

            lblStatusSummary.Text = string.Format("Total Enrolled Persons: {0}   |   Total Trained Samples: {1}", registeredPersons.Count, totalShots);
        }

        private void PromptEditPerson(string personName)
        {
            if (mainForm == null || string.IsNullOrEmpty(personName)) return;

            List<FrmPrincipal.RegisteredPerson> persons = mainForm.GetRegisteredPersons();
            FrmPrincipal.RegisteredPerson match = persons.Find(p => string.Equals(p.Name, personName, StringComparison.OrdinalIgnoreCase));
            int sampleCount = match != null ? match.SampleCount : 1;

            using (EditPersonDialog dlg = new EditPersonDialog(personName, sampleCount))
            {
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(dlg.UpdatedName) && !string.Equals(dlg.UpdatedName, personName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (mainForm.UpdatePersonName(personName, dlg.UpdatedName))
                        {
                            MessageBox.Show(
                                string.Format("Person \"{0}\" updated to \"{1}\" across all {2} sample(s) successfully.", personName, dlg.UpdatedName, sampleCount),
                                "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                    }
                }
                else if (result == DialogResult.Abort && dlg.DeleteRequested)
                {
                    if (mainForm.DeletePerson(personName))
                    {
                        MessageBox.Show(
                            string.Format("Person \"{0}\" and all {1} sample(s) deleted successfully.", personName, sampleCount),
                            "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
            }
        }

        private void PromptDeletePerson(string personName)
        {
            if (mainForm == null || string.IsNullOrEmpty(personName)) return;

            List<FrmPrincipal.RegisteredPerson> persons = mainForm.GetRegisteredPersons();
            FrmPrincipal.RegisteredPerson match = persons.Find(p => string.Equals(p.Name, personName, StringComparison.OrdinalIgnoreCase));
            int sampleCount = match != null ? match.SampleCount : 1;

            var res = MessageBox.Show(
                string.Format("Are you sure you want to permanently delete \"{0}\" and all {1} trained burst sample(s)?\n\nThis action cannot be undone.", personName, sampleCount),
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
                if (mainForm.DeletePerson(personName))
                {
                    MessageBox.Show(
                        string.Format("Person \"{0}\" was removed successfully.", personName),
                        "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
        }
    }
}
