using ARKViewer.Configuration;
using ARKViewer.CustomNameMaps;
using ARKViewer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ARKViewer
{
    public partial class frmAncestorView : Form
    {

        ColumnHeader SortingColumn_DetailTame = null;
        ContentTamedCreature tame = null;
        ContentManager cm = null;
        bool isLoading = false;
        
        private void LoadWindowSettings()
        {
            var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);


            if (savedWindow != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Left = savedWindow.Left;
                this.Top = savedWindow.Top;
                this.Width = savedWindow.Width;
                this.Height = savedWindow.Height;
            }
        }

        private void UpdateWindowSettings()
        {
            //only save location if normal window, do not save location/size if minimized/maximized
            if (this.WindowState == FormWindowState.Normal)
            {
                var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);
                if (savedWindow == null)
                {
                    savedWindow = new ViewerWindow();
                    savedWindow.Name = this.Name;
                    ARKViewer.Program.ProgramConfig.Windows.Add(savedWindow);
                }

                if (savedWindow != null)
                {
                    savedWindow.Left = this.Left;
                    savedWindow.Top = this.Top;
                    savedWindow.Width = this.Width;
                    savedWindow.Height = this.Height;
                }
            }
        }

        public frmAncestorView(ContentTamedCreature selectedTame, ContentManager manager)
        {
            InitializeComponent();
            LoadWindowSettings();
            tame = selectedTame;
            cm = manager;

            string friendlyName = selectedTame.ClassName;
            var dinoMap = Program.ProgramConfig.DinoMap.FirstOrDefault(d => d.ClassName == selectedTame.ClassName);
            if (dinoMap != null) friendlyName = dinoMap.FriendlyName;
            if(selectedTame.Name!=null && selectedTame.Name.Length > 0) friendlyName = selectedTame.Name;

            string tribeName = "";
            var tribe = cm.GetTribes(selectedTame.TargetingTeam).FirstOrDefault();
            if (tribe != null)
            {
                tribeName = tribe.TribeName; 
            }

            lblPlayerLevel.Text = selectedTame.Level.ToString();
            lblPlayerName.Text = friendlyName;
            lblTribeName.Text = tribeName;

            PopulateTameDetails();
            PopulateAncestorLevels();
        }

        private void PopulateTameDetails()
        {
            var tameDetail = tame;
            ListViewItem item = new ListViewItem(tameDetail.Gender);
            item.Tag = tameDetail;
            item.UseItemStyleForSubItems = false;

            item.SubItems.Add(tameDetail.BaseLevel.ToString());
            item.SubItems.Add(tameDetail.Level.ToString());
            if (tameDetail.Longitude != null && tameDetail.Latitude != null)
            {
                item.SubItems.Add(((decimal)tameDetail.Latitude).ToString("0.00"));
                item.SubItems.Add(((decimal)tameDetail.Longitude).ToString("0.00"));

            }
            else
            {
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
            }

            if (tameDetail.BaseStats != null && tameDetail.BaseStats.Length > 0)
            {
                if (optStatsTamed.Checked)
                {
                    //7
                    item.SubItems.Add(tameDetail.TamedStats[0].ToString());
                    item.SubItems.Add(tameDetail.TamedStats[1].ToString());
                    item.SubItems.Add(tameDetail.TamedStats[8].ToString());
                    item.SubItems.Add(tameDetail.TamedStats[7].ToString());
                    item.SubItems.Add(tameDetail.TamedStats[9].ToString());
                    item.SubItems.Add(tameDetail.TamedStats[4].ToString());
                    item.SubItems.Add(tameDetail.TamedStats[3].ToString());
                    item.SubItems.Add(tameDetail.TamedStats[11].ToString());

                }
                else
                {
                    item.SubItems.Add(tameDetail.BaseStats[0].ToString());
                    item.SubItems.Add(tameDetail.BaseStats[1].ToString());
                    item.SubItems.Add(tameDetail.BaseStats[8].ToString());
                    item.SubItems.Add(tameDetail.BaseStats[7].ToString());
                    item.SubItems.Add(tameDetail.BaseStats[9].ToString());
                    item.SubItems.Add(tameDetail.BaseStats[4].ToString());
                    item.SubItems.Add(tameDetail.BaseStats[3].ToString());
                    item.SubItems.Add(tameDetail.BaseStats[11].ToString());
                }
            }
            else
            {
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
            }


            item.SubItems.Add(tameDetail.TamedOnServerName);

            string tamerName = tameDetail.TamerName != null ? tameDetail.TamerName : "";
            string imprinterName = tameDetail.ImprinterName;
            if (tamerName.Length == 0)
            {
                if (tameDetail.ImprintedPlayerId != 0)
                {
                    var tamer = cm.GetPlayers(0, tameDetail.ImprintedPlayerId).FirstOrDefault<ContentPlayer>();
                    if (tamer != null) tamerName = tamer.CharacterName;
                }
            }



            item.SubItems.Add(tamerName);
            item.SubItems.Add(tameDetail.ImprinterName);
            item.SubItems.Add((tameDetail.ImprintQuality * 100).ToString("f0"));

            bool isStored = tameDetail.IsCryo | tameDetail.IsVivarium;

            item.SubItems.Add(isStored.ToString());
            if (tameDetail.IsCryo)
            {
                item.BackColor = Color.LightSkyBlue;
                item.SubItems[1].BackColor = Color.LightSkyBlue;
                item.SubItems[2].BackColor = Color.LightSkyBlue;
                item.SubItems[3].BackColor = Color.LightSkyBlue;
                item.SubItems[4].BackColor = Color.LightSkyBlue;
                item.SubItems[5].BackColor = Color.LightSkyBlue;
                item.SubItems[6].BackColor = Color.LightSkyBlue;
                item.SubItems[7].BackColor = Color.LightSkyBlue;
                item.SubItems[8].BackColor = Color.LightSkyBlue;
                item.SubItems[9].BackColor = Color.LightSkyBlue;
                item.SubItems[10].BackColor = Color.LightSkyBlue;
                item.SubItems[11].BackColor = Color.LightSkyBlue;
                item.SubItems[12].BackColor = Color.LightSkyBlue;
                item.SubItems[13].BackColor = Color.LightSkyBlue;
                item.SubItems[14].BackColor = Color.LightSkyBlue;
                item.SubItems[15].BackColor = Color.LightSkyBlue;
                item.SubItems[16].BackColor = Color.LightSkyBlue;
                item.SubItems[17].BackColor = Color.LightSkyBlue;
                item.SubItems[18].BackColor = Color.LightSkyBlue;
            }
            if (tameDetail.IsVivarium)
            {
                item.BackColor = Color.LightGreen;
                item.SubItems[1].BackColor = Color.LightGreen;
                item.SubItems[2].BackColor = Color.LightGreen;
                item.SubItems[3].BackColor = Color.LightGreen;
                item.SubItems[4].BackColor = Color.LightGreen;
                item.SubItems[5].BackColor = Color.LightGreen;
                item.SubItems[6].BackColor = Color.LightGreen;
                item.SubItems[7].BackColor = Color.LightGreen;
                item.SubItems[8].BackColor = Color.LightGreen;
                item.SubItems[9].BackColor = Color.LightGreen;
                item.SubItems[10].BackColor = Color.LightGreen;
                item.SubItems[11].BackColor = Color.LightGreen;
                item.SubItems[12].BackColor = Color.LightGreen;
                item.SubItems[13].BackColor = Color.LightGreen;
                item.SubItems[14].BackColor = Color.LightGreen;
                item.SubItems[15].BackColor = Color.LightGreen;
                item.SubItems[16].BackColor = Color.LightGreen;
                item.SubItems[17].BackColor = Color.LightGreen;
                item.SubItems[18].BackColor = Color.LightGreen;
            }
            if (tameDetail.BaseStats == null) //fake tame, used for ancestry only as unable to identify living parent
            {
                item.BackColor = Color.LightPink;
                item.SubItems[1].BackColor = Color.LightPink;
                item.SubItems[2].BackColor = Color.LightPink;
                item.SubItems[3].BackColor = Color.LightPink;
                item.SubItems[4].BackColor = Color.LightPink;
                item.SubItems[5].BackColor = Color.LightPink;
                item.SubItems[6].BackColor = Color.LightPink;
                item.SubItems[7].BackColor = Color.LightPink;
                item.SubItems[8].BackColor = Color.LightPink;
                item.SubItems[9].BackColor = Color.LightPink;
                item.SubItems[10].BackColor = Color.LightPink;
                item.SubItems[11].BackColor = Color.LightPink;
                item.SubItems[12].BackColor = Color.LightPink;
                item.SubItems[13].BackColor = Color.LightPink;
                item.SubItems[14].BackColor = Color.LightPink;
                item.SubItems[15].BackColor = Color.LightPink;
                item.SubItems[16].BackColor = Color.LightPink;
                item.SubItems[17].BackColor = Color.LightPink;
                item.SubItems[18].BackColor = Color.LightPink;
            }

            //Colours
            if (tameDetail.Colors != null)
            {
                int colourCheck = (int)tameDetail.Colors[0];
                item.SubItems.Add(colourCheck == 0 ? "n/a" : tameDetail.Colors[0].ToString()); //14
                ColourMap selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameDetail.Colors[0]).FirstOrDefault();
                if (selectedColor != null && selectedColor.Hex.Length > 0)
                {
                    item.SubItems[18].BackColor = selectedColor.Color;
                    item.SubItems[18].ForeColor = selectedColor.Color;
                }

                colourCheck = (int)tameDetail.Colors[1];
                item.SubItems.Add(colourCheck == 0 ? "n/a" : tameDetail.Colors[1].ToString()); //15
                selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameDetail.Colors[1]).FirstOrDefault();
                if (selectedColor != null && selectedColor.Hex.Length > 0)
                {
                    item.SubItems[19].BackColor = selectedColor.Color;
                    item.SubItems[19].ForeColor = selectedColor.Color;
                }

                colourCheck = (int)tameDetail.Colors[2];
                item.SubItems.Add(colourCheck == 0 ? "n/a" : tameDetail.Colors[2].ToString()); //16
                selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameDetail.Colors[2]).FirstOrDefault();
                if (selectedColor != null && selectedColor.Hex.Length > 0)
                {
                    item.SubItems[20].BackColor = selectedColor.Color;
                    item.SubItems[20].ForeColor = selectedColor.Color;
                }

                colourCheck = (int)tameDetail.Colors[3];
                item.SubItems.Add(colourCheck == 0 ? "n/a" : tameDetail.Colors[3].ToString()); //17
                selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameDetail.Colors[3]).FirstOrDefault();
                if (selectedColor != null && selectedColor.Hex.Length > 0)
                {
                    item.SubItems[21].BackColor = selectedColor.Color;
                    item.SubItems[21].ForeColor = selectedColor.Color;
                }

                colourCheck = (int)tameDetail.Colors[4];
                item.SubItems.Add(colourCheck == 0 ? "n/a" : tameDetail.Colors[4].ToString()); //18
                selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameDetail.Colors[4]).FirstOrDefault();
                if (selectedColor != null && selectedColor.Hex.Length > 0)
                {
                    item.SubItems[22].BackColor = selectedColor.Color;
                    item.SubItems[22].ForeColor = selectedColor.Color;
                }
                colourCheck = (int)tameDetail.Colors[5];
                item.SubItems.Add(colourCheck == 0 ? "n/a" : tameDetail.Colors[5].ToString()); //19
                selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameDetail.Colors[5]).FirstOrDefault();
                if (selectedColor != null && selectedColor.Hex.Length > 0)
                {
                    item.SubItems[23].BackColor = selectedColor.Color;
                    item.SubItems[23].ForeColor = selectedColor.Color;
                }

            }
            else
            {
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
                item.SubItems.Add("n/a");
            }


            //mutations
            item.SubItems.Add(tameDetail.RandomMutationsFemale.ToString());
            item.SubItems.Add(tameDetail.RandomMutationsMale.ToString());
            item.SubItems.Add(tameDetail.Id.ToString());

            lvwTame.Items.Clear();
            lvwTame.Items.Add(item);
        }

        private void PopulateAncestorLevels()
        {
            if (tame == null) return;

            lblStatus.Text = "Finding available generations...";
            lblStatus.Refresh();


            //determine ancestry line
            var currentTame = tame;
            if (tame.FatherId.GetValueOrDefault(0) == 0 || tame.MotherId.GetValueOrDefault(0) == 0) return;

            cboGeneration.Items.Clear();
            cboGeneration.Items.Add(new ContentAncestor(0,0,"All", "",0));

            var ancestors = GetAncestors(1, tame).GroupBy(x => x.Generation).Select(g => new ContentAncestor(g.Key,0,"","",0)).OrderBy(o => o.Generation).ToList();
            ancestors.ForEach(a =>
            {
                cboGeneration.Items.Add(a);                   

            });

            lblStatus.Text = "";
            lblStatus.Refresh();
            cboGeneration.SelectedIndex = 0;
        }

        private void PopulateAncestors()
        {

            lvwTameDetail.BeginUpdate();
            lvwTameDetail.Items.Clear();
            if (cboGeneration.SelectedItem == null) return;

            lblStatus.Text = "Finding available stats...";
            lblStatus.Refresh();

            ContentAncestor ancestorGroup = (ContentAncestor)cboGeneration.SelectedItem;

            var ancestors = GetAncestors(1, tame);
            var filteredAncestors = ancestors.Where(a => a.Generation > 0 && ( ancestorGroup.Generation == 0 || a.Generation == ancestorGroup.Generation)).ToList();
            foreach(var ancestor in filteredAncestors)
            {
                ContentTamedCreature tameAncestor = cm.GetTamedCreature(ancestor.Id);
                if (tameAncestor == null)
                {
                    //parse out level, name and gender
                    tameAncestor = new ContentTamedCreature()
                    {
                        Gender = ancestor.Gender,
                        BaseLevel = ancestor.Level,
                        Level = ancestor.Level,
                        Name = ancestor.Name,
                        ClassName = tame.ClassName
                    };
                }
                
                string creatureName = tameAncestor.ClassName;
                var dinoMap = Program.ProgramConfig.DinoMap.FirstOrDefault(d => d.ClassName == tameAncestor.ClassName);
                if (dinoMap != null)
                {
                    creatureName = dinoMap.FriendlyName;
                }
                if (tameAncestor.Name != null) creatureName = tameAncestor.Name;


                ListViewItem item = new ListViewItem(ancestor.Generation.ToString());
                item.Tag = tameAncestor;
                item.UseItemStyleForSubItems = false;

                item.SubItems.Add(creatureName);
                item.SubItems.Add(tameAncestor.Gender.ToString());
                item.SubItems.Add(tameAncestor.BaseLevel.ToString());
                item.SubItems.Add(tameAncestor.Level.ToString());
                if(tameAncestor.Longitude!=null && tameAncestor.Latitude != null)
                {
                    item.SubItems.Add(((decimal)tameAncestor.Latitude).ToString("0.00"));
                    item.SubItems.Add(((decimal)tameAncestor.Longitude).ToString("0.00"));

                }
                else
                {
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                }

                if(tameAncestor.BaseStats!=null &&tameAncestor.BaseStats.Length > 0)
                {
                    if (optStatsTamed.Checked)
                    {
                        //7
                        item.SubItems.Add(tameAncestor.TamedStats[0].ToString());
                        item.SubItems.Add(tameAncestor.TamedStats[1].ToString());
                        item.SubItems.Add(tameAncestor.TamedStats[8].ToString());
                        item.SubItems.Add(tameAncestor.TamedStats[7].ToString());
                        item.SubItems.Add(tameAncestor.TamedStats[9].ToString());
                        item.SubItems.Add(tameAncestor.TamedStats[4].ToString());
                        item.SubItems.Add(tameAncestor.TamedStats[3].ToString());
                        item.SubItems.Add(tameAncestor.TamedStats[11].ToString());

                    }
                    else
                    {
                        item.SubItems.Add(tameAncestor.BaseStats[0].ToString());
                        item.SubItems.Add(tameAncestor.BaseStats[1].ToString());
                        item.SubItems.Add(tameAncestor.BaseStats[8].ToString());
                        item.SubItems.Add(tameAncestor.BaseStats[7].ToString());
                        item.SubItems.Add(tameAncestor.BaseStats[9].ToString());
                        item.SubItems.Add(tameAncestor.BaseStats[4].ToString());
                        item.SubItems.Add(tameAncestor.BaseStats[3].ToString());
                        item.SubItems.Add(tameAncestor.BaseStats[11].ToString());
                    }
                }
                else
                {
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                }
                

                item.SubItems.Add(tameAncestor.TamedOnServerName);

                string tamerName = tameAncestor.TamerName != null ? tameAncestor.TamerName : "";
                string imprinterName = tameAncestor.ImprinterName;
                if (tamerName.Length == 0)
                {
                    if (tameAncestor.ImprintedPlayerId != 0)
                    {
                        var tamer = cm.GetPlayers(0, tameAncestor.ImprintedPlayerId).FirstOrDefault<ContentPlayer>();
                        if (tamer != null) tamerName = tamer.CharacterName;
                    }
                }



                item.SubItems.Add(tamerName);
                item.SubItems.Add(tameAncestor.ImprinterName);
                item.SubItems.Add((tameAncestor.ImprintQuality * 100).ToString("f0"));

                bool isStored = tameAncestor.IsCryo | tameAncestor.IsVivarium;

                item.SubItems.Add(isStored.ToString());
                if (tameAncestor.IsCryo)
                {
                    item.BackColor = Color.LightSkyBlue;
                    item.SubItems[1].BackColor = Color.LightSkyBlue;
                    item.SubItems[2].BackColor = Color.LightSkyBlue;
                    item.SubItems[3].BackColor = Color.LightSkyBlue;
                    item.SubItems[4].BackColor = Color.LightSkyBlue;
                    item.SubItems[5].BackColor = Color.LightSkyBlue;
                    item.SubItems[6].BackColor = Color.LightSkyBlue;
                    item.SubItems[7].BackColor = Color.LightSkyBlue;
                    item.SubItems[8].BackColor = Color.LightSkyBlue;
                    item.SubItems[9].BackColor = Color.LightSkyBlue;
                    item.SubItems[10].BackColor = Color.LightSkyBlue;
                    item.SubItems[11].BackColor = Color.LightSkyBlue;
                    item.SubItems[12].BackColor = Color.LightSkyBlue;
                    item.SubItems[13].BackColor = Color.LightSkyBlue;
                    item.SubItems[14].BackColor = Color.LightSkyBlue;
                    item.SubItems[15].BackColor = Color.LightSkyBlue;
                    item.SubItems[16].BackColor = Color.LightSkyBlue;
                    item.SubItems[17].BackColor = Color.LightSkyBlue;
                    item.SubItems[18].BackColor = Color.LightSkyBlue;
                }
                if (tameAncestor.IsVivarium)
                {
                    item.BackColor = Color.LightPink;
                    item.SubItems[1].BackColor = Color.LightGreen;
                    item.SubItems[2].BackColor = Color.LightGreen;
                    item.SubItems[3].BackColor = Color.LightGreen;
                    item.SubItems[4].BackColor = Color.LightGreen;
                    item.SubItems[5].BackColor = Color.LightGreen;
                    item.SubItems[6].BackColor = Color.LightGreen;
                    item.SubItems[7].BackColor = Color.LightGreen;
                    item.SubItems[8].BackColor = Color.LightGreen;
                    item.SubItems[9].BackColor = Color.LightGreen;
                    item.SubItems[10].BackColor = Color.LightGreen;
                    item.SubItems[11].BackColor = Color.LightGreen;
                    item.SubItems[12].BackColor = Color.LightGreen;
                    item.SubItems[13].BackColor = Color.LightGreen;
                    item.SubItems[14].BackColor = Color.LightGreen;
                    item.SubItems[15].BackColor = Color.LightGreen;
                    item.SubItems[16].BackColor = Color.LightGreen;
                    item.SubItems[17].BackColor = Color.LightGreen;
                    item.SubItems[18].BackColor = Color.LightGreen;
                }
                if (tameAncestor.BaseStats==null) //fake tame, used for ancestry only as unable to identify living parent
                {
                    item.BackColor = Color.LightPink;
                    item.SubItems[1].BackColor = Color.LightPink;
                    item.SubItems[2].BackColor = Color.LightPink;
                    item.SubItems[3].BackColor = Color.LightPink;
                    item.SubItems[4].BackColor = Color.LightPink;
                    item.SubItems[5].BackColor = Color.LightPink;
                    item.SubItems[6].BackColor = Color.LightPink;
                    item.SubItems[7].BackColor = Color.LightPink;
                    item.SubItems[8].BackColor = Color.LightPink;
                    item.SubItems[9].BackColor = Color.LightPink;
                    item.SubItems[10].BackColor = Color.LightPink;
                    item.SubItems[11].BackColor = Color.LightPink;
                    item.SubItems[12].BackColor = Color.LightPink;
                    item.SubItems[13].BackColor = Color.LightPink;
                    item.SubItems[14].BackColor = Color.LightPink;
                    item.SubItems[15].BackColor = Color.LightPink;
                    item.SubItems[16].BackColor = Color.LightPink;
                    item.SubItems[17].BackColor = Color.LightPink;
                    item.SubItems[18].BackColor = Color.LightPink;
                }

                //Colours
                if (tameAncestor.Colors != null)
                {
                    int colourCheck = (int)tameAncestor.Colors[0];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : tameAncestor.Colors[0].ToString()); //14
                    ColourMap selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameAncestor.Colors[0]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[20].BackColor = selectedColor.Color;
                        item.SubItems[20].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)tameAncestor.Colors[1];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : tameAncestor.Colors[1].ToString()); //15
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameAncestor.Colors[1]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[21].BackColor = selectedColor.Color;
                        item.SubItems[21].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)tameAncestor.Colors[2];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : tameAncestor.Colors[2].ToString()); //16
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameAncestor.Colors[2]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[22].BackColor = selectedColor.Color;
                        item.SubItems[22].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)tameAncestor.Colors[3];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : tameAncestor.Colors[3].ToString()); //17
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameAncestor.Colors[3]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[23].BackColor = selectedColor.Color;
                        item.SubItems[23].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)tameAncestor.Colors[4];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : tameAncestor.Colors[4].ToString()); //18
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameAncestor.Colors[4]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[24].BackColor = selectedColor.Color;
                        item.SubItems[24].ForeColor = selectedColor.Color;
                    }
                    colourCheck = (int)tameAncestor.Colors[5];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : tameAncestor.Colors[5].ToString()); //19
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)tameAncestor.Colors[5]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[25].BackColor = selectedColor.Color;
                        item.SubItems[25].ForeColor = selectedColor.Color;
                    }

                }
                else
                {
                    item.SubItems.Add("n/a"); 
                    item.SubItems.Add("n/a"); 
                    item.SubItems.Add("n/a"); 
                    item.SubItems.Add("n/a"); 
                    item.SubItems.Add("n/a"); 
                    item.SubItems.Add("n/a"); 
                }


                //mutations
                item.SubItems.Add(tameAncestor.RandomMutationsFemale.ToString());
                item.SubItems.Add(tameAncestor.RandomMutationsMale.ToString());
                item.SubItems.Add(tameAncestor.Id.ToString());

                lvwTameDetail.Items.Add(item);
            }


            if (SortingColumn_DetailTame != null)
            {
                lvwTameDetail.ListViewItemSorter =
                    new ListViewComparer(SortingColumn_DetailTame.Index, SortingColumn_DetailTame.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                // Sort.
                lvwTameDetail.Sort();
            }
            else
            {

                SortingColumn_DetailTame = lvwTameDetail.Columns[0]; ;
                SortingColumn_DetailTame.Text = "> " + SortingColumn_DetailTame.Text;

                lvwTameDetail.ListViewItemSorter =
                    new ListViewComparer(0, SortOrder.Ascending);

                // Sort.
                lvwTameDetail.Sort();
            }

            lvwTameDetail.EndUpdate();

            lblStatus.Text = "";
            lblStatus.Refresh();
        }

        private List<ContentAncestor> GetAncestors(long gen, ContentTamedCreature tame)
        {
            List<ContentAncestor> currentAncestors = new List<ContentAncestor>();
            if (tame.FatherId.GetValueOrDefault(0) != 0)
            {

                ContentTamedCreature father = cm.GetTamedCreature(tame.FatherId.Value);
                string fatherName = tame.FatherName;
                int fatherLevel = 0;

                if (father != null)
                {
                    fatherLevel = father.Level;
                    currentAncestors.AddRange(GetAncestors(gen + 1, father));
                }
                else
                {
                    if (fatherName.Contains("- Lvl"))
                    {
                        int.TryParse(fatherName.Substring(fatherName.LastIndexOf("l") + 1), out fatherLevel);
                        fatherName = fatherName.Substring(0, fatherName.LastIndexOf("-") - 1).Trim();
                    }
                }
                currentAncestors.Add(new ContentAncestor(gen, (long)tame.FatherId.GetValueOrDefault(0), fatherName, "Male", fatherLevel));

            }
            if (tame.MotherId.GetValueOrDefault(0) != 0)
            {
                ContentTamedCreature mother = cm.GetTamedCreature(tame.MotherId.Value);
                string motherName = tame.MotherName;
                int motherLevel = 0;

                if (mother != null)
                {
                    motherLevel = mother.Level;
                    currentAncestors.AddRange(GetAncestors(gen+1, mother));
                }
                else
                {
                    if(motherName.Contains("- Lvl"))
                    {
                        int.TryParse(motherName.Substring(motherName.LastIndexOf("l") + 1), out motherLevel);
                        motherName = motherName.Substring(0, motherName.LastIndexOf("-") - 1).Trim();
                    }
                }
                currentAncestors.Add(new ContentAncestor(gen, (long)tame.MotherId.GetValueOrDefault(0), motherName, "Female", motherLevel));

            }

            return currentAncestors;
        }

        private void btnCopyCommandTamed_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsTamed.SelectedItem == null) return;
            if (lvwTameDetail.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwTameDetail.SelectedItems[0];

            var commandText = cboConsoleCommandsTamed.SelectedItem.ToString();
            if (commandText != null)
            {

                ContentTamedCreature selectedCreature = (ContentTamedCreature)selectedItem.Tag;
                commandText = commandText.Replace("<ClassName>", selectedCreature.ClassName);
                commandText = commandText.Replace("<Level>", (selectedCreature.BaseLevel / 1.5).ToString("f0"));
                commandText = commandText.Replace("<TribeID>", selectedCreature.TargetingTeam.ToString("f0"));

                commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedCreature.X:0.00}"));
                commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedCreature.Y:0.00}"));
                commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedCreature.Z + 250:0.00}"));

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = commandText.Replace("<DoTame>", "admincheat DoTame");
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = commandText.Replace("<DoTame>", "cheat DoTame");
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied to clipboard: {commandText}";
                lblStatus.Refresh();
            }
        }

        private void frmAncestorView_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }

        private void cboGeneration_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboGeneration.Enabled = false;
            PopulateAncestors();
            cboGeneration.Enabled = true;
        }

        private void optStatsBase_CheckedChanged(object sender, EventArgs e)
        {
            SwitchStats();
        }

        private void optStatsTamed_CheckedChanged(object sender, EventArgs e)
        {

            SwitchStats();
        }

        private void SwitchStats()
        {
            if (isLoading) return;
            isLoading = true;

            //switch tame stat view
            if (tame.BaseStats != null && tame.BaseStats.Length > 0)
            {
                ListViewItem item = lvwTame.Items[0];

                if (optStatsTamed.Checked)
                {   
                    item.SubItems[5].Text = tame.TamedStats[0].ToString();
                    item.SubItems[6].Text = tame.TamedStats[1].ToString();
                    item.SubItems[7].Text = tame.TamedStats[8].ToString();
                    item.SubItems[8].Text = tame.TamedStats[7].ToString();
                    item.SubItems[9].Text = tame.TamedStats[9].ToString();
                    item.SubItems[10].Text = tame.TamedStats[4].ToString();
                    item.SubItems[11].Text = tame.TamedStats[3].ToString();
                    item.SubItems[12].Text = tame.TamedStats[11].ToString();

                }
                else
                {
                    item.SubItems[5].Text = tame.BaseStats[0].ToString();
                    item.SubItems[6].Text = tame.BaseStats[1].ToString();
                    item.SubItems[7].Text = tame.BaseStats[8].ToString();
                    item.SubItems[8].Text = tame.BaseStats[7].ToString();
                    item.SubItems[9].Text = tame.BaseStats[9].ToString();
                    item.SubItems[10].Text = tame.BaseStats[4].ToString();
                    item.SubItems[11].Text = tame.BaseStats[3].ToString();
                    item.SubItems[12].Text = tame.BaseStats[11].ToString();
                }
            }



            if (lvwTameDetail.Items.Count > 0)
            {
                foreach(ListViewItem item in lvwTameDetail.Items)
                {
                    ContentTamedCreature tame = (ContentTamedCreature)item.Tag;
                    if(tame.BaseStats!=null && tame.BaseStats.Length > 0)
                    {
                        if (optStatsTamed.Checked)
                        {
                            //7
                            item.SubItems[7].Text = tame.TamedStats[0].ToString();
                            item.SubItems[8].Text = tame.TamedStats[1].ToString();
                            item.SubItems[9].Text = tame.TamedStats[8].ToString();
                            item.SubItems[10].Text = tame.TamedStats[7].ToString();
                            item.SubItems[11].Text = tame.TamedStats[9].ToString();
                            item.SubItems[12].Text = tame.TamedStats[4].ToString();
                            item.SubItems[13].Text = tame.TamedStats[3].ToString();
                            item.SubItems[14].Text = tame.TamedStats[11].ToString();

                        }
                        else
                        {
                            item.SubItems[7].Text = tame.BaseStats[0].ToString();
                            item.SubItems[8].Text = tame.BaseStats[1].ToString();
                            item.SubItems[9].Text = tame.BaseStats[8].ToString();
                            item.SubItems[10].Text = tame.BaseStats[7].ToString();
                            item.SubItems[11].Text = tame.BaseStats[9].ToString();
                            item.SubItems[12].Text = tame.BaseStats[4].ToString();
                            item.SubItems[13].Text = tame.BaseStats[3].ToString();
                            item.SubItems[14].Text = tame.BaseStats[11].ToString();
                        }
                    }

                }
            }

            isLoading = false;
        }

        private void lvwTameDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopyCommandTamed.Enabled = lvwTameDetail.SelectedItems.Count == 1;
        }

        private void lvwTameDetail_Click(object sender, EventArgs e)
        {
            btnCopyCommandTamed.Enabled = lvwTameDetail.SelectedItems.Count == 1;
        }
    }
}
