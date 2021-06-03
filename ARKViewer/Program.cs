using ArkSavegameToolkitNet.Domain;
using ArkSavegameToolkitNet.Structs;
using ARKViewer.Configuration;
using ARKViewer.CustomNameMaps;
using ARKViewer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    static class Program
    {
        static Dictionary<string, int> ItemImageKeyMap = new Dictionary<string, int>();
        static Dictionary<string, int> MarkerImageKeyMap = new Dictionary<string, int>();
        public static ImageList ItemImageList { get; set; } = new ImageList();
        public static ImageList MarkerImageList { get; set; } = new ImageList();
        public static string ItemImageFolder { get; set; } = "";
        public static string MarkerImageFolder { get; set; } = "";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //param setup
            string appFolder = AppContext.BaseDirectory;
            string logFilename = Path.Combine(appFolder, @"ASV.log");

            ItemImageFolder = Path.Combine(appFolder, @"images\icons\");
            MarkerImageFolder = Path.Combine(appFolder, @"images\");

            if (!Directory.Exists(MarkerImageFolder)) Directory.CreateDirectory(MarkerImageFolder);
            if (!Directory.Exists(ItemImageFolder)) Directory.CreateDirectory(ItemImageFolder);


            //load config
            ProgramConfig = new ViewerConfiguration();


            //support quoted command line arguments which doesn't seem to be supported with Environment.GetCommandLineArgs() 
            string[] commandArguments = Regex.Split(Environment.CommandLine.Trim(), " (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            
            //remove empty argument entries
            commandArguments = commandArguments.Where(a => string.IsNullOrEmpty(a) == false).ToArray();
            if (commandArguments != null && commandArguments.Length > 1)
            {
                //arguments provided, export and exit
                using (TextWriter logWriter = new StreamWriter(logFilename))
                {
                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - ASV Command Line Started: {commandArguments.Length}");

                    int argIndex = 0;
                    foreach (string arg in commandArguments)
                    {
                        logWriter.WriteLine($"\tArg-{argIndex} = {arg}");
                        argIndex++;
                    }

                    //command line, load save game data for export
                    string saveFullFilename = "";

                    if (commandArguments.Length > 3)
                    {
                        //ark save game specified
                        saveFullFilename = commandArguments[3].ToString().Trim().Replace("\"", "");
                    }
                    else
                    {
                        //use configured
                        saveFullFilename = ARKViewer.Program.ProgramConfig.SelectedFile;
                    }

                    if (File.Exists(saveFullFilename))
                    {
                        ContentPack loadedPack = null;
                        switch (Path.GetExtension(saveFullFilename).ToLower())
                        {
                            case "asvpack":
                                loadedPack = new ContentPack(File.ReadAllBytes(saveFullFilename));
                                loadedPack.ContentDate = File.GetLastWriteTimeUtc(saveFullFilename);
                                break;
                            default:
                                //non asv pack, load from toolkit
                                ArkGameData gd = new ArkGameData(saveFullFilename, loadOnlyPropertiesInDomain: false);

                                if (gd.Update(CancellationToken.None, null, true)?.Success == true)
                                {
                                    gd.ApplyPreviousUpdate();
                                    loadedPack = new ContentPack(gd, 0, 0, 50, 100, 100);
                                    loadedPack.ContentDate = File.GetLastWriteTimeUtc(saveFullFilename);
                                }

                                gd = null;
                                break;
                        }

                        ContentManager contentManager = new ContentManager(loadedPack);

                        string commandOptionCheck = commandArguments[1].ToString().Trim().ToLower();
                        string exportFilePath = AppContext.BaseDirectory;
                        string exportFilename = Path.Combine(exportFilePath,"");

                        if (commandArguments.Length > 2)
                        {
                            //export filename
                            exportFilename = commandArguments[2].ToString().Trim().Replace("\"", "");
                            exportFilePath = Path.GetDirectoryName(exportFilename);
                        }
                        if (!Directory.Exists(exportFilePath)) Directory.CreateDirectory(exportFilePath);

                        switch (commandOptionCheck)
                        {
                            case "all":
                                contentManager.ExportAll(exportFilePath);
                                break;
                            case "wild":
                                exportFilename = exportFilename.Length > 0 ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Wild.json");
                                contentManager.ExportWild(exportFilename);
                                break;
                            case "tamed":
                                exportFilename = exportFilename.Length > 0 ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Tamed.json");
                                contentManager.ExportTamed(exportFilename);
                                break;
                            case "structures":
                                exportFilename = exportFilename.Length > 0 ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Structures.json");
                                contentManager.ExportPlayerStructures(exportFilename);
                                break;
                            case "tribes":
                                exportFilename = exportFilename.Length > 0 ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Tribes.json");
                                contentManager.ExportPlayerTribes(exportFilename);
                                break;
                            case "players":
                                exportFilename = exportFilename.Length > 0 ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Players.json");
                                contentManager.ExportPlayers(exportFilename);
                                break;
                        }

                        //success
                        Environment.ExitCode = 0;
                        return;
                    }
                    else
                    {
                        //input file not found
                        Environment.ExitCode = -1;
                    }


                    logWriter.Flush();

                    Application.Exit();
                };

            }
            else
            {
                frmViewer mainForm = new frmViewer();
                PopulateImageLists();


                if (!File.Exists(ProgramConfig.SelectedFile))
                {
                    frmSettings checkSettings = new frmSettings(new ContentManager(LoadContentPack("")));
                    if(checkSettings.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Unable to continue without a valid map file.", "ASV Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                mainForm.Show();
                mainForm.Cursor = Cursors.WaitCursor;
                mainForm.UpdateProgress("Loading content pack...");
                Application.DoEvents();

                
                ContentPack loadedPack = null;
                string saveFullFilename = ProgramConfig.SelectedFile;

                loadedPack = LoadContentPack(saveFullFilename);

                ContentManager contentManager = new ContentManager(loadedPack);
                mainForm.LoadContent(contentManager);
                mainForm.Cursor = Cursors.Default;
                //run viewer

                Application.Run(mainForm);
            }
        }


        public static ArkGameData LoadArkData(string contentFilename)
        {
            if (File.Exists(contentFilename))
            {
                ArkGameData gd = new ArkGameData(contentFilename, loadOnlyPropertiesInDomain: false);

                if (gd.Update(CancellationToken.None, null, true)?.Success == true)
                {
                    gd.ApplyPreviousUpdate();
                }

                return gd;
            }

            return null;
        }

        public static ContentPack LoadContentPack(string contentFilename)
        {
            ContentPack loadedPack = null;
            switch (Path.GetExtension(contentFilename).ToLower())
            {
                case ".asv":

                    loadedPack = new ContentPack(File.ReadAllBytes(contentFilename));
                    loadedPack.ContentDate = File.GetLastWriteTimeUtc(contentFilename);
                    break;
                default:
                    //non asv pack, load from toolkit

                    ArkGameData gd = LoadArkData(contentFilename);
                    if (gd!=null)
                    {
                        if (gd.Update(CancellationToken.None, null, true)?.Success == true)
                        {
                            Application.DoEvents();
                            gd.ApplyPreviousUpdate();
                            loadedPack = new ContentPack(gd, 0, 0, 50, 50, 250);
                            loadedPack.ContentDate = File.GetLastWriteTimeUtc(contentFilename);
                        }
                        gd = null;
                    }
                    else
                    {
                        loadedPack = new ContentPack()
                        {
                            MapFilename = Path.GetFileNameWithoutExtension(contentFilename),
                            ContentDate = new DateTime()                          
                        };
                    }

                    break;
            }
            return loadedPack;
        }

        internal static void AddItemImageMap(string imageName)
        {
            ItemImageKeyMap.Add(imageName, ItemImageKeyMap.Count + 1);
        }

        internal static void AddMarkerImageMap(string imageName)
        {
            MarkerImageKeyMap.Add(imageName, MarkerImageKeyMap.Count + 1);
        }


        private static void PopulateImageLists()
        {
            ItemImageKeyMap.Clear();
            ItemImageList.Images.Clear();

            if (Directory.Exists(ItemImageFolder))
            {

                var itemImageFiles = Directory.GetFiles(ItemImageFolder, "*.png");
                int imageIndex = 1;
                foreach (string fullFilename in itemImageFiles)
                {
                    if (File.Exists(fullFilename))
                    {
                        Image itemImage = Image.FromFile(fullFilename);
                        ItemImageList.Images.Add(itemImage);

                        ItemImageKeyMap.Add(Path.GetFileName(fullFilename), imageIndex);
                        imageIndex++;
                    }
                }

            }

            MarkerImageKeyMap.Clear();
            MarkerImageList.Images.Clear();

            if (Directory.Exists(MarkerImageFolder))
            {
                var markerImageFiles = Directory.GetFiles(MarkerImageFolder, "*.png");
                int markerIndex = 1;
                foreach (string fullFilename in markerImageFiles)
                {
                    if (File.Exists(fullFilename))
                    {
                        Image itemImage = Image.FromFile(fullFilename);
                        MarkerImageList.Images.Add(itemImage);
                        MarkerImageKeyMap.Add(Path.GetFileName(fullFilename), markerIndex);
                        markerIndex++;
                    }
                }


            }

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            string traceLog = ex.StackTrace;
            string errorMessage = ex.Message;

            frmErrorReport report = new frmErrorReport(errorMessage, traceLog);
            
            report.ShowDialog();


        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            string traceLog = ex.StackTrace;
            string errorMessage = ex.Message;

            frmErrorReport report = new frmErrorReport(errorMessage, traceLog);
            report.ShowDialog();

        }

        public static ViewerConfiguration ProgramConfig { get; set; }



        public static int GetItemImageIndex(string itemImageFilename)
        {
            ItemImageKeyMap.TryGetValue(itemImageFilename, out int imageIndex);
            return imageIndex;
        }

        public static int GetMarkerImageIndex(string markerImageFilename)
        {
            MarkerImageKeyMap.TryGetValue(markerImageFilename, out int imageIndex);
            return imageIndex;
        }
    }
}
