using Microsoft.AspNetCore.Components;
using BlazorUI.Services.Toast;

namespace BlazorUI.Components.BaseComponents.ComponentMain
{
    public class cErrorFunctions : ComponentBase
    {
        #region Class Declarations
        //Protected access modifier lets inherited classes access the below variables
        internal structAssemblyInformation msruAssemblyInformation = new structAssemblyInformation("", "", "", "", "");
        internal bool mblnShowMethodName = true;
        internal string mstrTracePrefix = " - ";
        internal string mstrLastError = "";
        internal enumTraceType menmTraceType = enumTraceType.typNone;
        #endregion

        #region Enumerations
        public enum enumTraceType
        {
            typNone = 0,
            typTextFile = 1,
            typEventLog = 2
        }
        #endregion

        #region Structures
        public struct structAssemblyInformation
        {
            public structAssemblyInformation(string pstrRootPath, string pstrName, string pstrTitle, string pstrDescription, string pstrVersion)
            {
                strRootPath = pstrRootPath;
                strName = pstrName;
                strTitle = pstrTitle;
                strDescription = pstrDescription;
                strVersion = pstrVersion;
            }
            public string strRootPath;
            public string strName;
            public string strTitle;
            public string strDescription;
            public string strVersion;
        }
        #endregion

        #region Properties
        public structAssemblyInformation prpAssemblyInformation
        {
            get
            {
                return msruAssemblyInformation;
            }
        }

        public string prpLastError
        {
            get
            {
                return mstrLastError;
            }
            set
            {
                mstrLastError = value;
            }
        }

        public bool prpShowMethodName
        {
            get
            {
                return mblnShowMethodName;
            }
            set
            {
                mblnShowMethodName = value;
            }
        }

        private enumTraceType prpTraceType
        {
            get
            {
                return menmTraceType;
            }
            set
            {
                fncTraceSet(value);
            }
        }
        #endregion

        #region subSetLastError
        public void subSetLastError(System.String pstrMethodName, System.String pstrErrorMessage)
        {
            System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod()?.Name + " Public Function.");
            mstrLastError = (mblnShowMethodName ? pstrMethodName + " " : "") + pstrErrorMessage;
            System.Diagnostics.Trace.WriteLine("ERROR: " + mstrLastError);
            System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod()?.Name + " END");

            subToast(enumToastType.typError, pstrErrorMessage, pstrMethodName);
        }
        #endregion

        #region fncToast
        public virtual void subToast(enumToastType penmToastType, string pstrTitle, string pstrContent = "", cToastAction? pobjToastAction = null)
        {
            throw new Exception("Must override function");
        }
        #endregion

        #region fncAssemblyInformationSet
        public bool fncAssemblyInformationSet(System.Reflection.Assembly pobjAssembly)
        {
            System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " Public Function.");
            return fncAssemblyInformationSetPrivate(pobjAssembly, null, menmTraceType);
        }

        public bool fncAssemblyInformationSet(System.Reflection.Assembly pobjAssembly, string pstrRootPath)
        {
            System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " Public Function.");
            return fncAssemblyInformationSetPrivate(pobjAssembly, pstrRootPath, menmTraceType);
        }

        public bool fncAssemblyInformationSet(System.Reflection.Assembly pobjAssembly, enumTraceType penmTraceType)
        {
            System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " Public Function.");
            return fncAssemblyInformationSetPrivate(pobjAssembly, null, penmTraceType);
        }

        public bool fncAssemblyInformationSet(System.Reflection.Assembly pobjAssembly, string pstrRootPath, enumTraceType penmTraceType)
        {
            System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " Public Function.");
            return fncAssemblyInformationSetPrivate(pobjAssembly, pstrRootPath, penmTraceType);
        }

        private bool fncAssemblyInformationSetPrivate(System.Reflection.Assembly pobjAssembly, string pstrRootPath, enumTraceType penmTraceType)
        {
            try
            {
                prpTraceType = penmTraceType;
                System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " Private Function.");
                System.Diagnostics.Trace.WriteLine("Setting Assembly Information.");
                //Retrieve the assembly information to use in logging procedures
                string strAssemblyName = System.Diagnostics.FileVersionInfo.GetVersionInfo(pobjAssembly.Location).InternalName;
                //Root Path
                if (string.IsNullOrWhiteSpace(pstrRootPath))
                {
                    System.Diagnostics.Trace.WriteLine(mstrTracePrefix + "Getting root path from Assembly...");
                    msruAssemblyInformation.strRootPath = pobjAssembly.Location.ToString().Remove(pobjAssembly.Location.ToString().LastIndexOf("\\") + 1, strAssemblyName.Length);
                }
                else
                {
                    System.Diagnostics.Trace.WriteLine(mstrTracePrefix + "Getting root path from parameter...");
                    msruAssemblyInformation.strRootPath = pstrRootPath.EndsWith("\\") ? pstrRootPath : pstrRootPath + "\\";
                }
                System.Diagnostics.Trace.WriteLine(mstrTracePrefix + "Assembly root path: " + msruAssemblyInformation.strRootPath);
                //Name
                msruAssemblyInformation.strName = strAssemblyName.Remove(strAssemblyName.LastIndexOf("."), strAssemblyName.Length - strAssemblyName.LastIndexOf("."));
                System.Diagnostics.Trace.WriteLine(mstrTracePrefix + "Assembly executable name without extension: " + msruAssemblyInformation.strName);
                //Title
                msruAssemblyInformation.strTitle = ((System.Reflection.AssemblyTitleAttribute)pobjAssembly.GetCustomAttributes(new System.Reflection.AssemblyTitleAttribute("xx").GetType(), false)[0]).Title;
                System.Diagnostics.Trace.WriteLine(mstrTracePrefix + "Assembly title: " + msruAssemblyInformation.strTitle);
                //Description
                msruAssemblyInformation.strDescription = ((System.Reflection.AssemblyDescriptionAttribute)pobjAssembly.GetCustomAttributes(new System.Reflection.AssemblyDescriptionAttribute("xx").GetType(), false)[0]).Description;
                System.Diagnostics.Trace.WriteLine(mstrTracePrefix + "Assembly description: " + msruAssemblyInformation.strDescription);
                //Version
                msruAssemblyInformation.strVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(pobjAssembly.Location).FileVersion;
                System.Diagnostics.Trace.WriteLine(mstrTracePrefix + "Assembly version: " + msruAssemblyInformation.strVersion);
                System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " END");
                System.Diagnostics.Trace.WriteLine("");
                return true;
            }
            catch (Exception ex)
            {
                subSetLastError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return false;
            }
        }
        #endregion

        #region fncTraceSet
        private bool fncTraceSet(enumTraceType penmTraceType)
        {
            try
            {

                System.Diagnostics.Trace.Listeners.Clear();
                menmTraceType = penmTraceType;
                if (penmTraceType == enumTraceType.typNone)
                {
                    return true;
                }
                string strTraceLog = "Application";
                if (penmTraceType == enumTraceType.typTextFile)
                {
                    strTraceLog = msruAssemblyInformation.strRootPath + msruAssemblyInformation.strName + "Trace" + Guid.NewGuid().ToString() + ".txt";
                    try
                    {
                        if (File.Exists(strTraceLog))
                        {
                            File.Delete(strTraceLog);
                        }
                    }
                    catch { }
                    System.Diagnostics.TextWriterTraceListener objListener = new System.Diagnostics.TextWriterTraceListener(strTraceLog);
                    System.Diagnostics.Trace.AutoFlush = true;
                    System.Diagnostics.Trace.Listeners.Add(objListener);
                }
                else //if (penmTraceType == enumTraceType.typEventLog)
                {
                    System.Diagnostics.EventLog objEventLog = new System.Diagnostics.EventLog();
                    objEventLog.Log = strTraceLog;
                    objEventLog.Source = msruAssemblyInformation.strName;
                    System.Diagnostics.EventLogTraceListener objListener = new System.Diagnostics.EventLogTraceListener();
                    objListener.EventLog = objEventLog;
                    System.Diagnostics.Trace.AutoFlush = true;
                    System.Diagnostics.Trace.Listeners.Add(objListener);
                }
                System.Diagnostics.Trace.WriteLine("Tracing is now active " + DateTime.Now.ToString() + ".");
                System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " Private Function.");
                System.Diagnostics.Trace.WriteLine(string.Format("{0}Trace {1}: '{2}'", mstrTracePrefix, penmTraceType == enumTraceType.typTextFile ? "File" : "EventLog", strTraceLog));
                System.Diagnostics.Trace.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " END");
                System.Diagnostics.Trace.WriteLine("");
                return true;
            }
            catch (Exception ex)
            {
                subSetLastError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return false;
            }
        }
        #endregion
    }
}
