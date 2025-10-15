namespace WindowsFormsNaidisUL
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    

    public partial class Form1 : Form
    {
        // Defineerime kõik vajalikud elemendid
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pic;
        CheckBox c_btn1, c_btn2;
        RadioButton r_btn1, r_btn2;
        TabControl tabC;
        TabPage tabP1, tabP2, tabP3;
        ListBox lb;
        bool t = true;
        

        public Form1()
        {
            this.Height = 600;
            this.Width = 800;
            this.Text = "Vorm elementidega ülesanne";

            // TreeView ja selle sõlmed
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Ülesanne");
            tn.Nodes.Add(new TreeNode("Silt"));
            tn.Nodes.Add(new TreeNode("PictureBox"));
            tn.Nodes.Add(new TreeNode("Checkbox"));
            tn.Nodes.Add(new TreeNode("RadioButton"));
            tn.Nodes.Add(new TreeNode("MessageBox"));
            tn.Nodes.Add(new TreeNode("TabControl"));
            tn.Nodes.Add(new TreeNode("ListBox"));
            tn.Nodes.Add(new TreeNode("Menu"));



            // Pealkiri
            lbl = new Label();
            lbl.Text = "NASTJA LOH VORM C#";
            lbl.Font = new Font("Cascadia Code", 20);
            lbl.Size = new Size(400, 30);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;


          

            // Pilt
            pic = new PictureBox();
            pic.Size = new Size(50, 50);
            pic.Location = new Point(150, 60);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = Image.FromFile(@"..\..\Images\close_box_red.png");
            pic.DoubleClick += Pic_DoubleClick;


            tree.Nodes.Add(tn);
            this.Controls.Add(tree);
           

        }

        // Topeltklõpsamine PictureBox-is, et vaheldumisi pilte kuvada
        int click = 0;
        private void Pic_DoubleClick(object sender, EventArgs e)
        {   //Double_Click -> carusel (3-4 images) 1-2-3-4-1-2-3-4-... 
            string[] images = { "pervaja.jpg", "vtoraja.jpg", "tri.jpg" };
            string fail = images[click];
            pic.Image = Image.FromFile(@"..\..\Images\" + fail);
            click++;
            if (click == 3) { click = 0; }
        }

        // Pealkirja hover event
        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            lbl.BackColor = Color.Transparent;
            Form1 Form = new Form1();
            Form.Show();
            this.Hide();


        }

        private void Lbl_MouseHover(object sender, EventArgs e)
        {
            lbl.BackColor = Color.FromArgb(200, 10, 20);
        }

        // Nupu click event
        private void Btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nastja kasutab chatgpt irina merkulova tunnis", "Teade");
        }

        // TreeView sõlme valimise sündmus
        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)

        {
            if (e.Node.Text == "PictureBox")
            {
                this.Controls.Add(pic);

            }
            else if (e.Node.Text == "Silt")
            {
                this.Controls.Add(lbl);
            }

            else if (e.Node.Text == "Checkbox")
            {
                c_btn1 = new CheckBox() { Text = "Suurenda akent", Location = new Point(310, 420), AutoSize = true };
                c_btn1.CheckedChanged += C_btn1_CheckedChanged;

                c_btn2 = new CheckBox() { Text = "Näita pilti", Location = new Point(310, 450), AutoSize = true };
                c_btn2.CheckedChanged += (s, ev) => { pic.Visible = c_btn2.Checked; };

                CheckBox c_btn3 = new CheckBox() { Text = "Muuda pealkirja värvi", Location = new Point(310, 480), AutoSize = true };
                c_btn3.CheckedChanged += (s, ev) => { lbl.ForeColor = c_btn3.Checked ? Color.Red : Color.Black; };



                this.Controls.Add(c_btn1);
                this.Controls.Add(c_btn2);
                this.Controls.Add(c_btn3);

            }
            else if (e.Node.Text == "RadioButton")
            {
                r_btn1 = new RadioButton();
                r_btn1.Text = "Must teema";
                r_btn1.Location = new Point(200, 420);
                r_btn2 = new RadioButton();
                r_btn2.Text = "Valge teema";
                r_btn2.Location = new Point(200, 440);
                this.Controls.Add(r_btn1);
                this.Controls.Add(r_btn2);
                r_btn1.CheckedChanged += new EventHandler(R_btn_Checked);
                r_btn2.CheckedChanged += new EventHandler(R_btn_Checked);
            }
            else if (e.Node.Text == "MessageBox")
            {
                MessageBox.Show("nastja kasutab chatgpt merkulova tunnis", "TEADE");
            }
            else if (e.Node.Text == "TabControl")
            {
                tabC = new TabControl();
                tabC.Location = new Point(450, 50);
                tabC.Size = new Size(300, 200);

                tabP1 = new TabPage("moodle");
                tabP2 = new TabPage("TTHK");
                tabP3 = new TabPage("+");
                tabP3.DoubleClick += TabP3_DoubleClick;

                WebBrowser wb1 = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    Url = new Uri("https://moodle.edu.ee")
                };
                tabP1.Controls.Add(wb1);


                WebBrowser wb2 = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    Url = new Uri("https://www.tthk.ee/")
                };
                tabP2.Controls.Add(wb2);

                tabC.Controls.Add(tabP1);
                tabC.Controls.Add(tabP2);
                tabC.Controls.Add(tabP3);
                this.Controls.Add(tabC);
            }

            else if (e.Node.Text == "ListBox")
            {
                AddListBox();
            }
            else if (e.Node.Text == "MainMenu")
            {
                Menu menu = new Menu();
                MenuItem menuFile = new MenuItem("File");
                menuFile.MenuItems.Add("Exit", new EventHandler(menuFile_Exit_Select));

                // Lisa kaks oma punkti
                menuFile.MenuItems.Add("Tere!", (s, ev) => MessageBox.Show("Tere tulemast!"));
                menuFile.MenuItems.Add("Info", (s, ev) => MessageBox.Show("See on info dialoog."));

                menu.MenuItems.Add(menuFile);
                this.Menu = menu;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void menuFile_Exit_Select(object sender, EventArgs e)
        {
            this.Close();
        }

        // Lisab ListBox
        private void AddListBox()
        {
            lb = new ListBox();
            lb.Items.Add("Roheline");
            lb.Items.Add("Punane");
            lb.Items.Add("Sinine");
            lb.Items.Add("Hall");
            lb.Items.Add("Kollane");
            lb.Items.Add("Lilla");
            lb.Items.Add("TumePunane");
            lb.Items.Add("TumeSinine");
            lb.Location = new Point(150, 120);
            lb.SelectedIndexChanged += new EventHandler(Lb_SelectedIndexChanged);
            this.Controls.Add(lb);
        }

        // ListBox valiku muutmine
        private void Lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lb.SelectedItem.ToString())
            {
                case "Sinine": this.BackColor = Color.Blue; break;
                case "Kollane": this.BackColor = Color.Yellow; break;
                case "Punane": this.BackColor = Color.Red; break;
                case "Hall": this.BackColor = Color.Gray; break;
                case "Roheline": this.BackColor = Color.Green; break;
                case "Lilla": this.BackColor = Color.Purple; break;
                case "TumePunane": this.BackColor = Color.DarkRed; break;
                case "TumeSinine": this.BackColor = Color.DarkCyan; break;
            }
        }

        // Vahelehe lisamine (topeltklõps TabControl-l)
        private void TabP3_DoubleClick(object sender, EventArgs e)
        {
            string title = "tabP" + (tabC.TabCount + 1).ToString();
            TabPage tb = new TabPage(title);
            tabC.TabPages.Add(tb);
        }

        // Tumedale ja heledale teemale üleminek raadionuppude kaudu
        private void R_btn_Checked(object sender, EventArgs e)
        {
            if (r_btn1.Checked)
            {
                this.BackColor = Color.Black;
                r_btn2.ForeColor = Color.White;
                r_btn1.ForeColor = Color.White;
            }
            else if (r_btn2.Checked)
            {
                this.BackColor = Color.White;
                r_btn2.ForeColor = Color.Black;
                r_btn1.ForeColor = Color.Black;
            }
        }

        // Kontrollib, kas märkeruut on märgitud või mitte
        private void C_btn1_CheckedChanged(object sender, EventArgs e)
        {
            if (t)
            {
                this.Size = new Size(1000, 1000);
                pic.BorderStyle = BorderStyle.Fixed3D;
                c_btn1.Text = "Tee aken väiksemaks";
                c_btn1.Font = new Font("Arial", 10, FontStyle.Bold);
                t = false;
            }
            else
            {
                this.Size = new Size(800, 700);
                c_btn1.Text = "Suurenda akent";
                c_btn1.Font = new Font("Arial", 10, FontStyle.Bold);
                t = true;

            }
        }
    }
}