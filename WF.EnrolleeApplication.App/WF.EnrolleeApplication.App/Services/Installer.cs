using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using NLog.Config;

namespace WF.EnrolleeApplication.App.Services
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            CreateConnectionString();
            
        }

        private void CreateConnectionString()
        {
            string dataSource = Context.Parameters["DATASOURCE"];
            string initialCatalog = Context.Parameters["INITIALCATALOG"];
            string userId = Context.Parameters["USERID"];
            string password = Context.Parameters["PASSWORD"];
            string connectionString = $"data source={dataSource};initial catalog={initialCatalog};persist security info=True;user id={userId};password={password};MultipleActiveResultSets=True;App=EntityFramework";
            // Файл конфигурации проекта
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            string configFile = string.Concat(Assembly.GetExecutingAssembly().Location, ".config");
            map.ExeConfigFilename = configFile;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["EnrolleeContext"].ConnectionString = connectionString;
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("connectionStrings");
            // Файл конфигурации логирования
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = assemblyFolder + "\\NLog.config";
            string configNlog = File.ReadAllText(path);
            configNlog = configNlog.Replace("@Server", $"\\\\{dataSource}");
            File.WriteAllText(path,configNlog);
        }
    }
}
