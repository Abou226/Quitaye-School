using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;

namespace CustomAction1
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult CustomAction1(Session session)
        {
            session.Log("Begin CustomAction1");
//#if DEBUG
//            int processId = Process.GetCurrentProcess().Id;
            MessageBox.Show(session.CustomActionData["assemblypath"], "Repertoire");

            Configuration config = ConfigurationManager.OpenExeConfiguration(session.CustomActionData["assemblypath"]);


            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;
            //MessageBox.Show(section.Settings, "Repertoire");

            if (section.SectionInformation.IsProtected)
                return ActionResult.SkipRemainingActions;

            section.SectionInformation.ProtectSection("DPAPIProtection");
            if (!section.SectionInformation.IsProtected)
                return ActionResult.Failure;
            config.Save();
//            string message = string.Format("Please attach the debugger (elevated on Vista or Win 7) to process [{0}].", processId);
//            MessageBox.Show(message, "Debug");
//#endif
            
            return ActionResult.Success;
        }


        //[CustomAction]
        //public static ActionResult EncryptConnStr(Session session)
        //{
        //    try
        //    {
        //        var config = ConfigurationManager.AppSettings;
        //        var section = config.GetSection("connectionStrings");
        //        var cms;
        //        var connStr = BuildConnStr(session["SHOSTNAME"], session["SDBNAME"], session["SUSERNAME"], session["SPASSWORD"]);


        //            // Update existing Connection String
        //            cms.ConnectionString = connStr;


        //        // Encrypt
        //        section.SectionInformation.ProtectSection(ConnStrEncryptionKey);

        //        // Save the configuration file.
        //        config.Save(ConfigurationSaveMode.Modified);

        //        return ActionResult.Success;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.StackTrace, ex.Message);
        //        throw;
        //    }
        //}

        //[CustomAction()]
        //public static ActionResult Encrypt(Session session)
        //{
        //    System.Configuration config = ConfigurationManager.OpenExeConfiguration(session.CustomActionData("ExecutablePath"));
        //    ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
        //    if (section.SectionInformation.IsProtected)
        //        return ActionResult.SkipRemainingActions;
        //    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
        //    if (!section.SectionInformation.IsProtected)
        //        return ActionResult.Failure;
        //    config.Save();
        //    return ActionResult.Success;
        //}




        [CustomAction()]
        public static ActionResult Encrypt(Session session)
        {
            //#if DEBUG
            //             int processId = Process.GetCurrentProcess().Id;
            //            string message = string.Format("Please attach the debugger (elevated on Vista or Win 7) to process [{0}].", processId);
            //            MessageBox.Show(message, "Debug");
            //#endif
            MessageBox.Show(session.CustomActionData["assemblypath"], "Repertoire");

            Configuration config = ConfigurationManager.OpenExeConfiguration(session.CustomActionData["assemblypath"]);

            
            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;
            //MessageBox.Show(section.Settings, "Repertoire");

            if (section.SectionInformation.IsProtected)
                return ActionResult.SkipRemainingActions;

            section.SectionInformation.ProtectSection("DPAPIProtection");
            if (!section.SectionInformation.IsProtected)
                return ActionResult.Failure;
            config.Save();
            return ActionResult.Success;
        }

    }
}
