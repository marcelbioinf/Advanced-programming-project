
namespace projekt
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fit_txtbox = new System.Windows.Forms.TextBox();
            this.cnt_fitness_btn = new System.Windows.Forms.Button();
            this.dimension_n_textbox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Shuffle_btn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.file_name_textbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.create_handinstance_button = new System.Windows.Forms.Button();
            this.hand_generate_button = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.read_checkb = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.readfile_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.addcol_button = new System.Windows.Forms.Button();
            this.columns_textbox = new System.Windows.Forms.TextBox();
            this.savefile_button = new System.Windows.Forms.Button();
            this.generate_button = new System.Windows.Forms.Button();
            this.difficulty_textbox = new System.Windows.Forms.TextBox();
            this.errors_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dimension_m_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Main_log_txtbox = new System.Windows.Forms.TextBox();
            this.Stop_button = new System.Windows.Forms.Button();
            this.Pause_button = new System.Windows.Forms.Button();
            this.clear_log_button = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.Start_meta_btn = new System.Windows.Forms.Button();
            this.mutprob_textbox = new System.Windows.Forms.TextBox();
            this.crossprob_textbox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Iteration_textbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Populationsize_textbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.time_txtbox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.sol_txt_box = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(964, 515);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.AccessibleName = "Instance_generator";
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(956, 489);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Instance generator";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fit_txtbox);
            this.splitContainer1.Panel2.Controls.Add(this.cnt_fitness_btn);
            this.splitContainer1.Panel2.Controls.Add(this.dimension_n_textbox);
            this.splitContainer1.Panel2.Controls.Add(this.label15);
            this.splitContainer1.Panel2.Controls.Add(this.Shuffle_btn);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.file_name_textbox);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.create_handinstance_button);
            this.splitContainer1.Panel2.Controls.Add(this.hand_generate_button);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.read_checkb);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.readfile_button);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.addcol_button);
            this.splitContainer1.Panel2.Controls.Add(this.columns_textbox);
            this.splitContainer1.Panel2.Controls.Add(this.savefile_button);
            this.splitContainer1.Panel2.Controls.Add(this.generate_button);
            this.splitContainer1.Panel2.Controls.Add(this.difficulty_textbox);
            this.splitContainer1.Panel2.Controls.Add(this.errors_textbox);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.dimension_m_textbox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(956, 489);
            this.splitContainer1.SplitterDistance = 667;
            this.splitContainer1.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(675, 485);
            this.dataGridView1.TabIndex = 0;
            // 
            // fit_txtbox
            // 
            this.fit_txtbox.Location = new System.Drawing.Point(166, 340);
            this.fit_txtbox.Name = "fit_txtbox";
            this.fit_txtbox.Size = new System.Drawing.Size(77, 20);
            this.fit_txtbox.TabIndex = 27;
            // 
            // cnt_fitness_btn
            // 
            this.cnt_fitness_btn.Location = new System.Drawing.Point(38, 337);
            this.cnt_fitness_btn.Name = "cnt_fitness_btn";
            this.cnt_fitness_btn.Size = new System.Drawing.Size(84, 25);
            this.cnt_fitness_btn.TabIndex = 26;
            this.cnt_fitness_btn.Text = "Count fitness";
            this.cnt_fitness_btn.UseVisualStyleBackColor = true;
            this.cnt_fitness_btn.Click += new System.EventHandler(this.cnt_fitness_btn_Click);
            // 
            // dimension_n_textbox
            // 
            this.dimension_n_textbox.Location = new System.Drawing.Point(141, 199);
            this.dimension_n_textbox.Name = "dimension_n_textbox";
            this.dimension_n_textbox.Size = new System.Drawing.Size(100, 20);
            this.dimension_n_textbox.TabIndex = 25;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label15.Location = new System.Drawing.Point(21, 200);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 15);
            this.label15.TabIndex = 24;
            this.label15.Text = "Dimension n";
            // 
            // Shuffle_btn
            // 
            this.Shuffle_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Shuffle_btn.Location = new System.Drawing.Point(166, 296);
            this.Shuffle_btn.Name = "Shuffle_btn";
            this.Shuffle_btn.Size = new System.Drawing.Size(84, 32);
            this.Shuffle_btn.TabIndex = 23;
            this.Shuffle_btn.Text = "Shuffle";
            this.Shuffle_btn.UseVisualStyleBackColor = true;
            this.Shuffle_btn.Click += new System.EventHandler(this.Shuffle_btn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 3.05F);
            this.label7.Location = new System.Drawing.Point(2, 365);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(286, 6);
            this.label7.TabIndex = 22;
            this.label7.Text = resources.GetString("label7.Text");
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // file_name_textbox
            // 
            this.file_name_textbox.Location = new System.Drawing.Point(78, 419);
            this.file_name_textbox.Name = "file_name_textbox";
            this.file_name_textbox.Size = new System.Drawing.Size(69, 20);
            this.file_name_textbox.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "File name";
            // 
            // create_handinstance_button
            // 
            this.create_handinstance_button.Location = new System.Drawing.Point(166, 33);
            this.create_handinstance_button.Name = "create_handinstance_button";
            this.create_handinstance_button.Size = new System.Drawing.Size(75, 23);
            this.create_handinstance_button.TabIndex = 19;
            this.create_handinstance_button.Text = "Create";
            this.create_handinstance_button.UseVisualStyleBackColor = true;
            this.create_handinstance_button.Click += new System.EventHandler(this.create_handinstance_button_Click);
            // 
            // hand_generate_button
            // 
            this.hand_generate_button.BackColor = System.Drawing.Color.Transparent;
            this.hand_generate_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.hand_generate_button.ForeColor = System.Drawing.Color.Black;
            this.hand_generate_button.Location = new System.Drawing.Point(96, 374);
            this.hand_generate_button.Name = "hand_generate_button";
            this.hand_generate_button.Size = new System.Drawing.Size(95, 33);
            this.hand_generate_button.TabIndex = 18;
            this.hand_generate_button.Text = "Generate";
            this.hand_generate_button.UseVisualStyleBackColor = false;
            this.hand_generate_button.Click += new System.EventHandler(this.hand_generate_button_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 3.05F);
            this.label8.Location = new System.Drawing.Point(2, 452);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(286, 6);
            this.label8.TabIndex = 17;
            this.label8.Text = resources.GetString("label8.Text");
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // read_checkb
            // 
            this.read_checkb.AutoSize = true;
            this.read_checkb.BackColor = System.Drawing.Color.Transparent;
            this.read_checkb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.read_checkb.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.read_checkb.Location = new System.Drawing.Point(96, 461);
            this.read_checkb.Name = "read_checkb";
            this.read_checkb.Size = new System.Drawing.Size(122, 22);
            this.read_checkb.TabIndex = 16;
            this.read_checkb.Text = "Instance ready";
            this.read_checkb.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label6.Location = new System.Drawing.Point(1, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(281, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "                      Hand instance generator                    ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label5.Location = new System.Drawing.Point(1, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(282, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "                  Random instance generator                  ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // readfile_button
            // 
            this.readfile_button.ForeColor = System.Drawing.Color.Black;
            this.readfile_button.Location = new System.Drawing.Point(107, 102);
            this.readfile_button.Name = "readfile_button";
            this.readfile_button.Size = new System.Drawing.Size(65, 27);
            this.readfile_button.TabIndex = 12;
            this.readfile_button.Text = "Read file";
            this.readfile_button.UseVisualStyleBackColor = true;
            this.readfile_button.Click += new System.EventHandler(this.readfile_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label4.Location = new System.Drawing.Point(21, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Number of columns";
            // 
            // addcol_button
            // 
            this.addcol_button.Location = new System.Drawing.Point(166, 62);
            this.addcol_button.Name = "addcol_button";
            this.addcol_button.Size = new System.Drawing.Size(75, 23);
            this.addcol_button.TabIndex = 10;
            this.addcol_button.Text = "Add column";
            this.addcol_button.UseVisualStyleBackColor = true;
            this.addcol_button.Click += new System.EventHandler(this.addcol_button_Click);
            // 
            // columns_textbox
            // 
            this.columns_textbox.Location = new System.Drawing.Point(37, 54);
            this.columns_textbox.Name = "columns_textbox";
            this.columns_textbox.Size = new System.Drawing.Size(85, 20);
            this.columns_textbox.TabIndex = 9;
            // 
            // savefile_button
            // 
            this.savefile_button.ForeColor = System.Drawing.Color.Black;
            this.savefile_button.Location = new System.Drawing.Point(166, 413);
            this.savefile_button.Name = "savefile_button";
            this.savefile_button.Size = new System.Drawing.Size(75, 30);
            this.savefile_button.TabIndex = 8;
            this.savefile_button.Text = "Save to file";
            this.savefile_button.UseVisualStyleBackColor = true;
            this.savefile_button.Click += new System.EventHandler(this.savefile_button_Click);
            // 
            // generate_button
            // 
            this.generate_button.BackColor = System.Drawing.Color.Transparent;
            this.generate_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.generate_button.ForeColor = System.Drawing.Color.Black;
            this.generate_button.Location = new System.Drawing.Point(38, 296);
            this.generate_button.Name = "generate_button";
            this.generate_button.Size = new System.Drawing.Size(84, 32);
            this.generate_button.TabIndex = 7;
            this.generate_button.Text = "Create";
            this.generate_button.UseVisualStyleBackColor = false;
            this.generate_button.Click += new System.EventHandler(this.generate_button_Click);
            // 
            // difficulty_textbox
            // 
            this.difficulty_textbox.Location = new System.Drawing.Point(141, 231);
            this.difficulty_textbox.Name = "difficulty_textbox";
            this.difficulty_textbox.Size = new System.Drawing.Size(100, 20);
            this.difficulty_textbox.TabIndex = 5;
            // 
            // errors_textbox
            // 
            this.errors_textbox.Location = new System.Drawing.Point(141, 264);
            this.errors_textbox.Name = "errors_textbox";
            this.errors_textbox.Size = new System.Drawing.Size(100, 20);
            this.errors_textbox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label3.Location = new System.Drawing.Point(21, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dificulty level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label2.Location = new System.Drawing.Point(21, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of errors";
            // 
            // dimension_m_textbox
            // 
            this.dimension_m_textbox.Location = new System.Drawing.Point(141, 169);
            this.dimension_m_textbox.Name = "dimension_m_textbox";
            this.dimension_m_textbox.Size = new System.Drawing.Size(100, 20);
            this.dimension_m_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label1.Location = new System.Drawing.Point(21, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dimension m";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(956, 489);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Metaheuristic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer2.Panel1.Controls.Add(this.Main_log_txtbox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Panel2.Controls.Add(this.Stop_button);
            this.splitContainer2.Panel2.Controls.Add(this.Pause_button);
            this.splitContainer2.Panel2.Controls.Add(this.clear_log_button);
            this.splitContainer2.Panel2.Controls.Add(this.label14);
            this.splitContainer2.Panel2.Controls.Add(this.Start_meta_btn);
            this.splitContainer2.Panel2.Controls.Add(this.mutprob_textbox);
            this.splitContainer2.Panel2.Controls.Add(this.crossprob_textbox);
            this.splitContainer2.Panel2.Controls.Add(this.label13);
            this.splitContainer2.Panel2.Controls.Add(this.label12);
            this.splitContainer2.Panel2.Controls.Add(this.Iteration_textbox);
            this.splitContainer2.Panel2.Controls.Add(this.label11);
            this.splitContainer2.Panel2.Controls.Add(this.Populationsize_textbox);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Size = new System.Drawing.Size(950, 483);
            this.splitContainer2.SplitterDistance = 661;
            this.splitContainer2.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 456);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(659, 24);
            this.progressBar1.TabIndex = 1;
            // 
            // Main_log_txtbox
            // 
            this.Main_log_txtbox.Location = new System.Drawing.Point(6, 5);
            this.Main_log_txtbox.Multiline = true;
            this.Main_log_txtbox.Name = "Main_log_txtbox";
            this.Main_log_txtbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Main_log_txtbox.Size = new System.Drawing.Size(653, 450);
            this.Main_log_txtbox.TabIndex = 0;
            // 
            // Stop_button
            // 
            this.Stop_button.BackColor = System.Drawing.Color.Red;
            this.Stop_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Stop_button.Location = new System.Drawing.Point(161, 326);
            this.Stop_button.Name = "Stop_button";
            this.Stop_button.Size = new System.Drawing.Size(88, 36);
            this.Stop_button.TabIndex = 24;
            this.Stop_button.Text = "STOP";
            this.Stop_button.UseVisualStyleBackColor = false;
            this.Stop_button.Click += new System.EventHandler(this.Stop_button_Click);
            // 
            // Pause_button
            // 
            this.Pause_button.BackColor = System.Drawing.Color.LightGray;
            this.Pause_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pause_button.ForeColor = System.Drawing.Color.Red;
            this.Pause_button.Location = new System.Drawing.Point(26, 326);
            this.Pause_button.Name = "Pause_button";
            this.Pause_button.Size = new System.Drawing.Size(88, 36);
            this.Pause_button.TabIndex = 23;
            this.Pause_button.Text = "Pause";
            this.Pause_button.UseVisualStyleBackColor = false;
            this.Pause_button.Click += new System.EventHandler(this.Pause_button_Click);
            // 
            // clear_log_button
            // 
            this.clear_log_button.Location = new System.Drawing.Point(20, 435);
            this.clear_log_button.Name = "clear_log_button";
            this.clear_log_button.Size = new System.Drawing.Size(65, 35);
            this.clear_log_button.TabIndex = 22;
            this.clear_log_button.Text = "Clear log";
            this.clear_log_button.UseVisualStyleBackColor = true;
            this.clear_log_button.Click += new System.EventHandler(this.clear_log_button_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label14.Location = new System.Drawing.Point(-1, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(292, 16);
            this.label14.TabIndex = 21;
            this.label14.Text = "                      Metaheuristics parameters                    ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Start_meta_btn
            // 
            this.Start_meta_btn.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Start_meta_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start_meta_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Start_meta_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Start_meta_btn.Location = new System.Drawing.Point(95, 233);
            this.Start_meta_btn.Name = "Start_meta_btn";
            this.Start_meta_btn.Size = new System.Drawing.Size(95, 36);
            this.Start_meta_btn.TabIndex = 20;
            this.Start_meta_btn.Text = "Start";
            this.Start_meta_btn.UseVisualStyleBackColor = false;
            this.Start_meta_btn.Click += new System.EventHandler(this.Start_meta_btn_Click);
            // 
            // mutprob_textbox
            // 
            this.mutprob_textbox.Location = new System.Drawing.Point(161, 184);
            this.mutprob_textbox.Name = "mutprob_textbox";
            this.mutprob_textbox.Size = new System.Drawing.Size(85, 20);
            this.mutprob_textbox.TabIndex = 19;
            // 
            // crossprob_textbox
            // 
            this.crossprob_textbox.Location = new System.Drawing.Point(161, 141);
            this.crossprob_textbox.Name = "crossprob_textbox";
            this.crossprob_textbox.Size = new System.Drawing.Size(85, 20);
            this.crossprob_textbox.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label13.Location = new System.Drawing.Point(17, 185);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 15);
            this.label13.TabIndex = 17;
            this.label13.Text = "Mutation probability";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label12.Location = new System.Drawing.Point(17, 142);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 15);
            this.label12.TabIndex = 16;
            this.label12.Text = "Crossing probability";
            // 
            // Iteration_textbox
            // 
            this.Iteration_textbox.Location = new System.Drawing.Point(161, 101);
            this.Iteration_textbox.Name = "Iteration_textbox";
            this.Iteration_textbox.Size = new System.Drawing.Size(85, 20);
            this.Iteration_textbox.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label11.Location = new System.Drawing.Point(17, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 15);
            this.label11.TabIndex = 14;
            this.label11.Text = "Iteration number";
            // 
            // Populationsize_textbox
            // 
            this.Populationsize_textbox.Location = new System.Drawing.Point(161, 62);
            this.Populationsize_textbox.Name = "Populationsize_textbox";
            this.Populationsize_textbox.Size = new System.Drawing.Size(85, 20);
            this.Populationsize_textbox.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label10.Location = new System.Drawing.Point(17, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 15);
            this.label10.TabIndex = 12;
            this.label10.Text = "Population size";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.time_txtbox);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.sol_txt_box);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(956, 489);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Solution";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // time_txtbox
            // 
            this.time_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.time_txtbox.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.time_txtbox.Location = new System.Drawing.Point(496, 454);
            this.time_txtbox.Name = "time_txtbox";
            this.time_txtbox.Size = new System.Drawing.Size(155, 23);
            this.time_txtbox.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label16.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label16.Location = new System.Drawing.Point(322, 456);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 18);
            this.label16.TabIndex = 2;
            this.label16.Text = "Fitness:";
            // 
            // sol_txt_box
            // 
            this.sol_txt_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.sol_txt_box.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.sol_txt_box.Location = new System.Drawing.Point(402, 454);
            this.sol_txt_box.Name = "sol_txt_box";
            this.sol_txt_box.Size = new System.Drawing.Size(66, 23);
            this.sol_txt_box.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(2, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(953, 432);
            this.dataGridView2.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 517);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Genetic_algorithm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox errors_textbox;
        private System.Windows.Forms.TextBox difficulty_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dimension_m_textbox;
        private System.Windows.Forms.Button generate_button;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addcol_button;
        private System.Windows.Forms.TextBox columns_textbox;
        private System.Windows.Forms.Button savefile_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button readfile_button;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox read_checkb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button create_handinstance_button;
        private System.Windows.Forms.Button hand_generate_button;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox file_name_textbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Shuffle_btn;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button Start_meta_btn;
        private System.Windows.Forms.TextBox mutprob_textbox;
        private System.Windows.Forms.TextBox crossprob_textbox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Iteration_textbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Populationsize_textbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Main_log_txtbox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox dimension_n_textbox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button Stop_button;
        private System.Windows.Forms.Button Pause_button;
        private System.Windows.Forms.Button clear_log_button;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox sol_txt_box;
        private System.Windows.Forms.TextBox fit_txtbox;
        private System.Windows.Forms.Button cnt_fitness_btn;
        private System.Windows.Forms.TextBox time_txtbox;
    }
}

