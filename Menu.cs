namespace External_ESP_base
{
    using ECHELON;
    using External_ESP_Base;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class Menu : Form
    {
        public static Color angryColor;
        private Button angryColorBtn;
        private ColorDialog angryColorDialog;
        private TrackBar angryColorSlide;
        private CheckBox angryToggle;
        private IContainer components;
        public static Color cupboardColor;
        private TrackBar cupboardColorAlpha;
        private Button cupboardColorBtn;
        private ColorDialog cupboardColorDialog;
        private CheckBox cupboardToggle;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox ipField;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button listenBtn;
        public static Color niceColor;
        private Button niceColorBtn;
        private ColorDialog niceColorDialog;
        private TrackBar niceColorSlide;
        private CheckBox niceToggle;
        public static Color playerColor;
        private TrackBar playerColorAlpha;
        private Button playerColorBtn;
        private ColorDialog playerColorDialog;
        private CheckBox playerNameToggle;
        private CheckBox playerTog;
        private TextBox portField;
        private TrackBar radarBackAlpha;
        private CheckBox radarBackTog;
        private TrackBar radarColorAlpha;
        private Button radarPrimaryBtn;
        public static Color radarPrimaryColor;
        public static int radarPrimaryColorA;
        private ColorDialog radarPrimaryDlg;
        private CheckBox radarPrimaryTog;
        private Button radarSecondaryBtn;
        public static Color radarSecondaryColor;
        public static int radarSecondaryColorA;
        private ColorDialog radarSecondaryDlg;
        private const int RGBMAX = 0xff;
        public static Color rockColor;
        private TrackBar rockColorAlpha;
        private Button rockColorBtn;
        private ColorDialog rocksColorDialog;
        private CheckBox rockToggle;
        public string serverIP;
        public string serverPort;

        public Menu()
        {
            this.InitializeComponent();
        }

        private void angryColorBtn_Click(object sender, EventArgs e)
        {
            if (this.angryColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.angryColorBtn.BackColor = this.angryColorDialog.Color;
                angryColor = this.angryColorDialog.Color;
                Radar.Default.angryColor = angryColor;
                this.angryColorBtn.ForeColor = this.InvertMeAColour(angryColor);
                Radar.Default.Save();
            }
        }

        private void angryColorSlide_Scroll(object sender, EventArgs e)
        {
            Radar.Default.angryColorA = this.angryColorSlide.Value;
            Radar.Default.Save();
        }

        private void angryToggle_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.drawAngry = this.angryToggle.Checked;
            Radar.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (this.radarSecondaryDlg.ShowDialog() == DialogResult.OK)
            {
                this.radarSecondaryBtn.BackColor = this.radarSecondaryDlg.Color;
                radarSecondaryColor = this.radarSecondaryDlg.Color;
                Radar.Default.radarSecondaryC = radarSecondaryColor;
                this.radarSecondaryBtn.ForeColor = this.InvertMeAColour(radarSecondaryColor);
                Radar.Default.Save();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (this.rocksColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.rockColorBtn.BackColor = this.rocksColorDialog.Color;
                rockColor = this.rocksColorDialog.Color;
                Radar.Default.rocksColor = rockColor;
                this.rockColorBtn.ForeColor = this.InvertMeAColour(rockColor);
                Radar.Default.Save();
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            if (this.niceColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.niceColorBtn.BackColor = this.niceColorDialog.Color;
                niceColor = this.niceColorDialog.Color;
                Radar.Default.niceColor = niceColor;
                this.niceColorBtn.ForeColor = this.InvertMeAColour(niceColor);
                Radar.Default.Save();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.drawNice = this.niceToggle.Checked;
            Radar.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.drawPlayerNames = this.playerNameToggle.Checked;
            Radar.Default.Save();
        }

        private void cupboardColorAlpha_Scroll(object sender, EventArgs e)
        {
            Radar.Default.cupboardColorA = this.cupboardColorAlpha.Value;
            Radar.Default.Save();
        }

        private void cupboardColorBtn_Click(object sender, EventArgs e)
        {
            if (this.cupboardColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.cupboardColorBtn.BackColor = this.cupboardColorDialog.Color;
                cupboardColor = this.cupboardColorDialog.Color;
                Radar.Default.cupboardColor = cupboardColor;
                this.cupboardColorBtn.ForeColor = this.InvertMeAColour(cupboardColor);
                Radar.Default.Save();
            }
        }

        private void cupboardToggle_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.drawCupboards = this.cupboardToggle.Checked;
            Radar.Default.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Radar.Default.Save();
            this.serverIP = Radar.Default.adress;
            this.serverPort = Radar.Default.port;
            radarPrimaryColor = Radar.Default.radarPrimaryC;
            radarPrimaryColorA = Radar.Default.radarPrimaryA;
            radarSecondaryColor = Radar.Default.radarSecondaryC;
            radarSecondaryColorA = Radar.Default.radarSecondaryA;
            playerColor = Radar.Default.playerColor;
            rockColor = Radar.Default.rocksColor;
            cupboardColor = Radar.Default.cupboardColor;
            angryColor = Radar.Default.angryColor;
            niceColor = Radar.Default.niceColor;
            this.ipField.Text = this.serverIP;
            this.portField.Text = this.serverPort;
            this.radarPrimaryBtn.BackColor = radarPrimaryColor;
            this.radarPrimaryBtn.ForeColor = this.InvertMeAColour(radarPrimaryColor);
            this.radarColorAlpha.Value = Radar.Default.radarPrimaryA;
            this.radarSecondaryBtn.BackColor = radarSecondaryColor;
            this.radarSecondaryBtn.ForeColor = this.InvertMeAColour(radarSecondaryColor);
            this.radarBackAlpha.Value = Radar.Default.radarSecondaryA;
            this.playerColorBtn.BackColor = playerColor;
            this.playerColorBtn.ForeColor = this.InvertMeAColour(playerColor);
            this.playerColorAlpha.Value = Radar.Default.rocksColorA;
            this.rockColorBtn.BackColor = rockColor;
            this.rockColorBtn.ForeColor = this.InvertMeAColour(rockColor);
            this.rockColorAlpha.Value = Radar.Default.rocksColorA;
            this.cupboardColorBtn.BackColor = cupboardColor;
            this.cupboardColorBtn.ForeColor = this.InvertMeAColour(cupboardColor);
            this.cupboardColorAlpha.Value = Radar.Default.cupboardColorA;
            this.angryColorBtn.BackColor = angryColor;
            this.angryColorBtn.ForeColor = this.InvertMeAColour(angryColor);
            this.angryColorSlide.Value = Radar.Default.angryColorA;
            this.niceColorBtn.BackColor = niceColor;
            this.niceColorBtn.ForeColor = this.InvertMeAColour(niceColor);
            this.niceColorSlide.Value = Radar.Default.niceColorA;
            this.radarPrimaryTog.Checked = Radar.Default.radarPrimaryT;
            this.radarBackTog.Checked = Radar.Default.radarSecondaryT;
            this.playerTog.Checked = Radar.Default.drawPlayers;
            this.playerNameToggle.Checked = Radar.Default.drawPlayerNames;
            this.rockToggle.Checked = Radar.Default.drawRocks;
            this.cupboardToggle.Checked = Radar.Default.drawCupboards;
            this.angryToggle.Checked = Radar.Default.drawAngry;
            this.niceToggle.Checked = Radar.Default.drawNice;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(External_ESP_base.Menu));
            this.listenBtn = new Button();
            this.label1 = new Label();
            this.ipField = new TextBox();
            this.portField = new TextBox();
            this.radarPrimaryDlg = new ColorDialog();
            this.radarPrimaryTog = new CheckBox();
            this.radarPrimaryBtn = new Button();
            this.radarColorAlpha = new TrackBar();
            this.groupBox1 = new GroupBox();
            this.radarBackAlpha = new TrackBar();
            this.radarSecondaryBtn = new Button();
            this.label3 = new Label();
            this.radarBackTog = new CheckBox();
            this.label2 = new Label();
            this.radarSecondaryDlg = new ColorDialog();
            this.groupBox2 = new GroupBox();
            this.playerNameToggle = new CheckBox();
            this.playerColorAlpha = new TrackBar();
            this.playerColorBtn = new Button();
            this.label4 = new Label();
            this.playerTog = new CheckBox();
            this.playerColorDialog = new ColorDialog();
            this.groupBox3 = new GroupBox();
            this.cupboardColorAlpha = new TrackBar();
            this.cupboardColorBtn = new Button();
            this.cupboardToggle = new CheckBox();
            this.rockColorBtn = new Button();
            this.rockColorAlpha = new TrackBar();
            this.rockToggle = new CheckBox();
            this.angryColorSlide = new TrackBar();
            this.angryColorBtn = new Button();
            this.angryToggle = new CheckBox();
            this.rocksColorDialog = new ColorDialog();
            this.cupboardColorDialog = new ColorDialog();
            this.niceColorSlide = new TrackBar();
            this.niceColorBtn = new Button();
            this.niceToggle = new CheckBox();
            this.angryColorDialog = new ColorDialog();
            this.niceColorDialog = new ColorDialog();
            this.groupBox4 = new GroupBox();
            this.radarColorAlpha.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radarBackAlpha.BeginInit();
            this.groupBox2.SuspendLayout();
            this.playerColorAlpha.BeginInit();
            this.groupBox3.SuspendLayout();
            this.cupboardColorAlpha.BeginInit();
            this.rockColorAlpha.BeginInit();
            this.angryColorSlide.BeginInit();
            this.niceColorSlide.BeginInit();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.listenBtn.Location = new Point(0x15f, 9);
            this.listenBtn.Name = "listenBtn";
            this.listenBtn.Size = new Size(0x5c, 0x17);
            this.listenBtn.TabIndex = 0;
            this.listenBtn.Text = "Listen";
            this.listenBtn.UseVisualStyleBackColor = true;
            this.listenBtn.Click += new EventHandler(this.listenBtn_Click);
            this.label1.AutoSize = true;
            this.label1.ForeColor = Color.Red;
            this.label1.Location = new Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Info";
            this.ipField.Location = new Point(0x4d, 9);
            this.ipField.Name = "ipField";
            this.ipField.Size = new Size(0x95, 20);
            this.ipField.TabIndex = 2;
            this.ipField.TextChanged += new EventHandler(this.ipField_TextChanged);
            this.portField.Location = new Point(0xe8, 9);
            this.portField.Name = "portField";
            this.portField.Size = new Size(0x71, 20);
            this.portField.TabIndex = 3;
            this.portField.TextChanged += new EventHandler(this.portField_TextChanged);
            this.radarPrimaryTog.AutoSize = true;
            this.radarPrimaryTog.Location = new Point(6, 0x13);
            this.radarPrimaryTog.Name = "radarPrimaryTog";
            this.radarPrimaryTog.Size = new Size(0x55, 0x11);
            this.radarPrimaryTog.TabIndex = 5;
            this.radarPrimaryTog.Text = "Center Lines";
            this.radarPrimaryTog.UseVisualStyleBackColor = true;
            this.radarPrimaryTog.CheckedChanged += new EventHandler(this.radarPrimaryTog_CheckedChanged);
            this.radarPrimaryBtn.BackColor = SystemColors.ControlText;
            this.radarPrimaryBtn.FlatStyle = FlatStyle.Flat;
            this.radarPrimaryBtn.Location = new Point(0x45, 0x27);
            this.radarPrimaryBtn.Name = "radarPrimaryBtn";
            this.radarPrimaryBtn.Size = new Size(20, 20);
            this.radarPrimaryBtn.TabIndex = 6;
            this.radarPrimaryBtn.Text = "C";
            this.radarPrimaryBtn.UseVisualStyleBackColor = false;
            this.radarPrimaryBtn.Click += new EventHandler(this.radarPrimaryBtn_Click);
            this.radarColorAlpha.Location = new Point(0x5f, 0x27);
            this.radarColorAlpha.Maximum = 0xff;
            this.radarColorAlpha.Name = "radarColorAlpha";
            this.radarColorAlpha.Size = new Size(0x68, 0x2d);
            this.radarColorAlpha.TabIndex = 7;
            this.radarColorAlpha.Value = 0xff;
            this.radarColorAlpha.Scroll += new EventHandler(this.radarColorAlpha_Scroll);
            this.groupBox1.Controls.Add(this.radarBackAlpha);
            this.groupBox1.Controls.Add(this.radarSecondaryBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radarBackTog);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radarPrimaryTog);
            this.groupBox1.Controls.Add(this.radarColorAlpha);
            this.groupBox1.Controls.Add(this.radarPrimaryBtn);
            this.groupBox1.Location = new Point(15, 0x23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd3, 130);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Radar Settings";
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.radarBackAlpha.Location = new Point(0x5f, 0x4d);
            this.radarBackAlpha.Maximum = 0xff;
            this.radarBackAlpha.Name = "radarBackAlpha";
            this.radarBackAlpha.Size = new Size(0x68, 0x2d);
            this.radarBackAlpha.TabIndex = 12;
            this.radarBackAlpha.Value = 0xff;
            this.radarBackAlpha.Scroll += new EventHandler(this.radarBackAlpha_Scroll);
            this.radarSecondaryBtn.BackColor = SystemColors.ControlText;
            this.radarSecondaryBtn.FlatStyle = FlatStyle.Flat;
            this.radarSecondaryBtn.Location = new Point(0x45, 0x49);
            this.radarSecondaryBtn.Name = "radarSecondaryBtn";
            this.radarSecondaryBtn.Size = new Size(20, 20);
            this.radarSecondaryBtn.TabIndex = 11;
            this.radarSecondaryBtn.Text = "C";
            this.radarSecondaryBtn.UseVisualStyleBackColor = false;
            this.radarSecondaryBtn.Click += new EventHandler(this.button1_Click_1);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 0x4d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Back Color";
            this.radarBackTog.AutoSize = true;
            this.radarBackTog.Location = new Point(0x73, 0x13);
            this.radarBackTog.Name = "radarBackTog";
            this.radarBackTog.Size = new Size(0x54, 0x11);
            this.radarBackTog.TabIndex = 9;
            this.radarBackTog.Text = "Background";
            this.radarBackTog.UseVisualStyleBackColor = true;
            this.radarBackTog.CheckedChanged += new EventHandler(this.radarBackTog_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x2a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Line Colors";
            this.groupBox2.Controls.Add(this.playerNameToggle);
            this.groupBox2.Controls.Add(this.playerColorAlpha);
            this.groupBox2.Controls.Add(this.playerColorBtn);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.playerTog);
            this.groupBox2.Location = new Point(15, 0xab);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd3, 0x55);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player Settings";
            this.playerNameToggle.AutoSize = true;
            this.playerNameToggle.Location = new Point(0x73, 0x13);
            this.playerNameToggle.Name = "playerNameToggle";
            this.playerNameToggle.Size = new Size(0x5b, 0x11);
            this.playerNameToggle.TabIndex = 14;
            this.playerNameToggle.Text = "Player Names";
            this.playerNameToggle.UseVisualStyleBackColor = true;
            this.playerNameToggle.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
            this.playerColorAlpha.Location = new Point(0x5f, 0x2a);
            this.playerColorAlpha.Maximum = 0xff;
            this.playerColorAlpha.Name = "playerColorAlpha";
            this.playerColorAlpha.Size = new Size(0x68, 0x2d);
            this.playerColorAlpha.TabIndex = 13;
            this.playerColorAlpha.Value = 0xff;
            this.playerColorAlpha.Scroll += new EventHandler(this.playerColorAlpha_Scroll);
            this.playerColorBtn.BackColor = SystemColors.ControlText;
            this.playerColorBtn.FlatStyle = FlatStyle.Flat;
            this.playerColorBtn.Location = new Point(0x45, 0x2a);
            this.playerColorBtn.Name = "playerColorBtn";
            this.playerColorBtn.Size = new Size(20, 20);
            this.playerColorBtn.TabIndex = 13;
            this.playerColorBtn.Text = "C";
            this.playerColorBtn.UseVisualStyleBackColor = false;
            this.playerColorBtn.Click += new EventHandler(this.playerColorBtn_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 0x2a);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x24, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Player";
            this.playerTog.AutoSize = true;
            this.playerTog.Location = new Point(6, 0x13);
            this.playerTog.Name = "playerTog";
            this.playerTog.Size = new Size(80, 0x11);
            this.playerTog.TabIndex = 0;
            this.playerTog.Text = "Player Dots";
            this.playerTog.UseVisualStyleBackColor = true;
            this.playerTog.CheckedChanged += new EventHandler(this.playerTog_CheckedChanged);
            this.groupBox3.Controls.Add(this.cupboardColorAlpha);
            this.groupBox3.Controls.Add(this.cupboardColorBtn);
            this.groupBox3.Controls.Add(this.cupboardToggle);
            this.groupBox3.Controls.Add(this.rockColorBtn);
            this.groupBox3.Controls.Add(this.rockColorAlpha);
            this.groupBox3.Controls.Add(this.rockToggle);
            this.groupBox3.Location = new Point(0xe8, 0x24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xd3, 0x5c);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Entities";
            this.cupboardColorAlpha.Location = new Point(0x67, 0x36);
            this.cupboardColorAlpha.Maximum = 0xff;
            this.cupboardColorAlpha.Name = "cupboardColorAlpha";
            this.cupboardColorAlpha.Size = new Size(0x68, 0x2d);
            this.cupboardColorAlpha.TabIndex = 0x12;
            this.cupboardColorAlpha.Value = 0xff;
            this.cupboardColorAlpha.Scroll += new EventHandler(this.cupboardColorAlpha_Scroll);
            this.cupboardColorBtn.BackColor = SystemColors.ControlText;
            this.cupboardColorBtn.FlatStyle = FlatStyle.Flat;
            this.cupboardColorBtn.Location = new Point(0x4d, 0x36);
            this.cupboardColorBtn.Name = "cupboardColorBtn";
            this.cupboardColorBtn.Size = new Size(20, 20);
            this.cupboardColorBtn.TabIndex = 0x11;
            this.cupboardColorBtn.Text = "C";
            this.cupboardColorBtn.UseVisualStyleBackColor = false;
            this.cupboardColorBtn.Click += new EventHandler(this.cupboardColorBtn_Click);
            this.cupboardToggle.AutoSize = true;
            this.cupboardToggle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cupboardToggle.Location = new Point(14, 0x39);
            this.cupboardToggle.Name = "cupboardToggle";
            this.cupboardToggle.Size = new Size(40, 0x11);
            this.cupboardToggle.TabIndex = 0x10;
            this.cupboardToggle.Text = "TC";
            this.cupboardToggle.UseVisualStyleBackColor = true;
            this.cupboardToggle.CheckedChanged += new EventHandler(this.cupboardToggle_CheckedChanged);
            this.rockColorBtn.BackColor = SystemColors.ControlText;
            this.rockColorBtn.FlatStyle = FlatStyle.Flat;
            this.rockColorBtn.Location = new Point(0x4d, 0x12);
            this.rockColorBtn.Name = "rockColorBtn";
            this.rockColorBtn.Size = new Size(20, 20);
            this.rockColorBtn.TabIndex = 15;
            this.rockColorBtn.Text = "C";
            this.rockColorBtn.UseVisualStyleBackColor = false;
            this.rockColorBtn.Click += new EventHandler(this.button1_Click_2);
            this.rockColorAlpha.Location = new Point(0x67, 0x13);
            this.rockColorAlpha.Maximum = 0xff;
            this.rockColorAlpha.Name = "rockColorAlpha";
            this.rockColorAlpha.Size = new Size(0x68, 0x2d);
            this.rockColorAlpha.TabIndex = 15;
            this.rockColorAlpha.Value = 0xff;
            this.rockColorAlpha.Scroll += new EventHandler(this.rockColorAlpha_Scroll);
            this.rockToggle.AutoSize = true;
            this.rockToggle.Location = new Point(14, 0x16);
            this.rockToggle.Name = "rockToggle";
            this.rockToggle.Size = new Size(0x39, 0x11);
            this.rockToggle.TabIndex = 15;
            this.rockToggle.Text = "Rocks";
            this.rockToggle.UseVisualStyleBackColor = true;
            this.rockToggle.CheckedChanged += new EventHandler(this.rockToggle_CheckedChanged);
            this.angryColorSlide.Location = new Point(0x67, 0x1c);
            this.angryColorSlide.Maximum = 0xff;
            this.angryColorSlide.Name = "angryColorSlide";
            this.angryColorSlide.Size = new Size(0x68, 0x2d);
            this.angryColorSlide.TabIndex = 0x15;
            this.angryColorSlide.Value = 0xff;
            this.angryColorSlide.Scroll += new EventHandler(this.angryColorSlide_Scroll);
            this.angryColorBtn.BackColor = SystemColors.ControlText;
            this.angryColorBtn.FlatStyle = FlatStyle.Flat;
            this.angryColorBtn.Location = new Point(0x4d, 0x1c);
            this.angryColorBtn.Name = "angryColorBtn";
            this.angryColorBtn.Size = new Size(20, 20);
            this.angryColorBtn.TabIndex = 20;
            this.angryColorBtn.Text = "C";
            this.angryColorBtn.UseVisualStyleBackColor = false;
            this.angryColorBtn.Click += new EventHandler(this.angryColorBtn_Click);
            this.angryToggle.AutoSize = true;
            this.angryToggle.Location = new Point(14, 0x1f);
            this.angryToggle.Name = "angryToggle";
            this.angryToggle.Size = new Size(0x36, 0x11);
            this.angryToggle.TabIndex = 0x13;
            this.angryToggle.Text = "Aggro";
            this.angryToggle.UseVisualStyleBackColor = true;
            this.angryToggle.CheckedChanged += new EventHandler(this.angryToggle_CheckedChanged);
            this.niceColorSlide.Location = new Point(0x67, 70);
            this.niceColorSlide.Maximum = 0xff;
            this.niceColorSlide.Name = "niceColorSlide";
            this.niceColorSlide.Size = new Size(0x68, 0x2d);
            this.niceColorSlide.TabIndex = 0x18;
            this.niceColorSlide.Value = 0xff;
            this.niceColorSlide.Scroll += new EventHandler(this.trackBar1_Scroll);
            this.niceColorBtn.BackColor = SystemColors.ControlText;
            this.niceColorBtn.FlatStyle = FlatStyle.Flat;
            this.niceColorBtn.Location = new Point(0x4d, 70);
            this.niceColorBtn.Name = "niceColorBtn";
            this.niceColorBtn.Size = new Size(20, 20);
            this.niceColorBtn.TabIndex = 0x17;
            this.niceColorBtn.Text = "C";
            this.niceColorBtn.UseVisualStyleBackColor = false;
            this.niceColorBtn.Click += new EventHandler(this.button1_Click_3);
            this.niceToggle.AutoSize = true;
            this.niceToggle.Location = new Point(14, 0x49);
            this.niceToggle.Name = "niceToggle";
            this.niceToggle.Size = new Size(0x3f, 0x11);
            this.niceToggle.TabIndex = 0x16;
            this.niceToggle.Text = "Passive";
            this.niceToggle.UseVisualStyleBackColor = true;
            this.niceToggle.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.groupBox4.Controls.Add(this.angryColorBtn);
            this.groupBox4.Controls.Add(this.niceColorSlide);
            this.groupBox4.Controls.Add(this.angryToggle);
            this.groupBox4.Controls.Add(this.angryColorSlide);
            this.groupBox4.Controls.Add(this.niceColorBtn);
            this.groupBox4.Controls.Add(this.niceToggle);
            this.groupBox4.Location = new Point(0xe8, 0x85);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xd3, 0x7d);
            this.groupBox4.TabIndex = 0x19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Animals";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(460, 0x112);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.portField);
            base.Controls.Add(this.ipField);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listenBtn);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MaximumSize = new Size(0x1dc, 0x138);
            this.MinimumSize = new Size(0x1dc, 0x138);
            base.Name = "Menu";
            this.Text = " ";
            base.Load += new EventHandler(this.Form1_Load);
            this.radarColorAlpha.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.radarBackAlpha.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.playerColorAlpha.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.cupboardColorAlpha.EndInit();
            this.rockColorAlpha.EndInit();
            this.angryColorSlide.EndInit();
            this.niceColorSlide.EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private Color InvertMeAColour(Color ColourToInvert) => 
            Color.FromArgb(0xff - ColourToInvert.R, 0xff - ColourToInvert.G, 0xff - ColourToInvert.B);

        private void ipField_TextChanged(object sender, EventArgs e)
        {
        }

        private void listen()
        {
        }

        private void listenBtn_Click(object sender, EventArgs e)
        {
            Radar.Default.adress = this.ipField.Text;
            Radar.Default.port = this.portField.Text;
            Radar.Default.Save();
            int porty = int.Parse(this.portField.Text);
            new Thread(() => Overlay.ReadRustPackets(this.ipField.Text, porty)).Start();
            this.listenBtn.Enabled = false;
        }

        private void playerColorAlpha_Scroll(object sender, EventArgs e)
        {
            Radar.Default.playerColorA = this.playerColorAlpha.Value;
            Radar.Default.Save();
        }

        private void playerColorBtn_Click(object sender, EventArgs e)
        {
            if (this.playerColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.playerColorBtn.BackColor = this.playerColorDialog.Color;
                playerColor = this.playerColorDialog.Color;
                Radar.Default.playerColor = playerColor;
                this.playerColorBtn.ForeColor = this.InvertMeAColour(playerColor);
                Radar.Default.Save();
            }
        }

        private void playerTog_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.drawPlayers = this.playerTog.Checked;
            Radar.Default.Save();
        }

        private void portField_TextChanged(object sender, EventArgs e)
        {
        }

        private void radarBackAlpha_Scroll(object sender, EventArgs e)
        {
            Radar.Default.radarSecondaryA = this.radarBackAlpha.Value;
            Radar.Default.Save();
        }

        private void radarBackTog_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.radarSecondaryT = this.radarBackTog.Checked;
            Radar.Default.Save();
        }

        private void radarColorAlpha_Scroll(object sender, EventArgs e)
        {
            Radar.Default.radarPrimaryA = this.radarColorAlpha.Value;
            Radar.Default.Save();
        }

        private void radarPrimaryBtn_Click(object sender, EventArgs e)
        {
            if (this.radarPrimaryDlg.ShowDialog() == DialogResult.OK)
            {
                this.radarPrimaryBtn.BackColor = this.radarPrimaryDlg.Color;
                radarPrimaryColor = this.radarPrimaryDlg.Color;
                Radar.Default.radarPrimaryC = radarPrimaryColor;
                this.radarPrimaryBtn.ForeColor = this.InvertMeAColour(radarPrimaryColor);
                Radar.Default.Save();
            }
        }

        private void radarPrimaryTog_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.radarPrimaryT = this.radarPrimaryTog.Checked;
            Radar.Default.Save();
        }

        private void rockColorAlpha_Scroll(object sender, EventArgs e)
        {
            Radar.Default.rocksColorA = this.rockColorAlpha.Value;
            Radar.Default.Save();
        }

        private void rockToggle_CheckedChanged(object sender, EventArgs e)
        {
            Radar.Default.drawRocks = this.rockToggle.Checked;
            Radar.Default.Save();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Radar.Default.niceColorA = this.niceColorSlide.Value;
            Radar.Default.Save();
        }
    }
}

