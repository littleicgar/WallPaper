using System;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using System.Windows.Forms;
using System.IO;
namespace WallPaper.Properties
{
    /// <summary>
    /// 创建一个ConfigProvide类，用于应用程序配置储存和读取。与默认的LocalConfigProvider不同的是，AppConfig可以指定存放配置文件的位置。
    /// 实现配置文件自定义存放位置需要完成一下工作：
    /// 1、Settings类声明SettingsProvider属性，使用本应用配置提供类。[SettingsProvider(typeof(AppConfigProvider))]
    /// 2、Settings类增加两个属性，ConfigPath、ConfigFile，这两个属性用于提供配置程序存放的位置和文件名称。
    ///    配置文件名称可以自定义，目前使用的是user.config。
    /// 3、程序修改应用配置文件保存位置是，应询问是否将当前配置保存到新位置，如不保存当前配置，而是加载新位置的配置文件，则需要调用AppConfigProvider
    ///    类的ResetSettingsXml方法，将xml文件对象重置。
    /// 4、配置文件存放位置信息保存在程序目录下"<程序名>.ConfigPath"中。详见Settings.ConfigPath.
    ///    ***************************************************************************************************************
    ///    ****注意，这个配置文件信息xml与属性配置文件xml是两个文件，不要搞混淆了，配置文件信息xml文件固定放在程序目录****
    ///    ***************************************************************************************************************
    /// 5、改变配置文件存放路径使用Settings.ChangeConfigPath方法。
    /// 以上内容对Settings类的新增定义均在本文件内。
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <param name="propvals"></param>
    /// v1.1 更改Settings属性public为internal
    ///      配置文件名称"<程序名>.config"更改为"<程序名>.ConfigPath"，避免混淆。
    ///      修正Settings.ConfigPath 在xml文件不存在,或者xml文件错误时，返回空目录的情况。
    ///      修正了多处设置属性时，XmlNode叠加引用时，node为null报异常的问题。
    public class AppConfigProvider : SettingsProvider
    {
        bool SkipRoamingCheck = true; //if true, all settings will be forcely marked as Roaming ;if false, only settings that has SettingsManageabilityAttribute will be marked as Roaming;
        private XmlDocument m_SettingsXML = null;

        public override void Initialize(string name, NameValueCollection col)
        {
            base.Initialize(ApplicationName, col);
        }

        public override string ApplicationName
        {
            get
            {
                return Application.ProductName;
            }
            set
            {
            }
        }
        /// <summary>
        /// Iterate through the settings to be stored
        /// Only dirty settings are included in propvals, and only ones relevant to this provider
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propvals"></param>
        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals)
        {
            foreach (SettingsPropertyValue propval in propvals)
                SetValue(propval);
            try
            {
                SettingsXML.Save(Path.Combine(Path.Combine(Settings.Default.ConfigPath, Settings.Default.ConfigFile)));
            }
            catch
            {
            }

        }
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection props)
        {
            // Create new collection of values
            SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
            // Iterate through the settings to be retrieved
            foreach (SettingsProperty setting in props)
            {
                SettingsPropertyValue value = new SettingsPropertyValue(setting);
                value.IsDirty = false;
                value.SerializedValue = GetValue(setting);
                values.Add(value);
            }
            return values;
        }
        public void ResetSettingsXml() {
            m_SettingsXML = null;
        }

        /// <summary>
        /// 指定存储配置文件的地址
        /// </summary>
        /// <returns></returns>
        public virtual string GetAppSettingsPath()
        {
            return (new FileInfo(Application.ExecutablePath)).DirectoryName; //Use application path
        }
        /// <summary>
        /// 指定存储文件名
        /// </summary>
        /// <returns></returns>
        public virtual string GetAppSettingsFilename()
        {
            return ApplicationName + ".config";
        }
        /// <summary>
        /// 获得配置文件对象
        /// </summary>
        /// <returns></returns>
        private XmlDocument SettingsXML
        {
            get
            {
                // If we dont hold an xml document, try opening one.  
                // If it doesnt exist then create a new one ready.
                if (m_SettingsXML == null)
                {
                    m_SettingsXML = new XmlDocument();
                    try
                    {
                        m_SettingsXML.Load(Path.Combine(Settings.Default.ConfigPath, Settings.Default.ConfigFile));
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("用户配置文件丢失！");
                        // Create new document
                        XmlDeclaration dec = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
                        m_SettingsXML.AppendChild(dec);
                    }
                }
                return m_SettingsXML;
            }
        }
        /// <summary>
        /// 获得设置的根节点
        /// </summary>
        /// <returns></returns>
        private XmlNode SettingsRootNode
        {
            get
            {
                string xpath = "configuration/userSettings/" + ApplicationName + ".Properties.Settings";
                XmlNode rNode =  SettingsXML.SelectSingleNode(xpath);
                if (rNode is null)
                {
                    rNode = SettingsXML.AppendChild(SettingsXML.CreateNode(XmlNodeType.Element, "configuration", ""))
                                .AppendChild(SettingsXML.CreateNode(XmlNodeType.Element, "userSettings", ""))
                                .AppendChild(SettingsXML.CreateNode(XmlNodeType.Element, ApplicationName + ".Properties.Settings", ""))
                    ;
                }
                return rNode;
            }
        }
        private string GetValue(SettingsProperty setting)
        {
            XmlNode rNode;
            try
            {
                if (IsRoaming(setting))
                    rNode = SettingsRootNode.SelectSingleNode("setting[@name = \"" + setting.Name + "\"]/value");
                else
                    rNode = SettingsRootNode.SelectSingleNode(Environment.MachineName + "/setting[@name = \"" + setting.Name + "\"]/value");
            }
            catch
            {
                //MessageBox.Show("读取属性" + setting.Name + "失败");
                rNode = null;
            }
            if (rNode == null)
                if (setting.DefaultValue != null)
                    return setting.DefaultValue.ToString();
                else
                    return "";
            return rNode.InnerText;
        }
        private void SetValue(SettingsPropertyValue propVal)
        {
            XmlElement MachineNode;
            XmlElement SettingNode;
            XmlElement ValueNode;
            // Determine if the setting is roaming.
            // If roaming then the value is stored as an element under the root
            // Otherwise it is stored under a machine name node 
            if (IsRoaming(propVal.Property)) {
                SettingNode = (XmlElement)SettingsRootNode.SelectSingleNode("setting[@name = \"" + propVal.Name + "\"]/value");
                // Check to see if the node exists, if so then set its new value
                if (SettingNode != null)
                    SettingNode.InnerText = propVal.SerializedValue.ToString();
                else
                {
                    SettingNode = (XmlElement)SettingsRootNode.SelectSingleNode("setting[@name = \"" + propVal.Name + "\"]");
                    if (SettingNode == null)
                    {
                        SettingNode = SettingsXML.CreateElement("setting");
                        SettingNode.SetAttribute("name", propVal.Property.Name);
                        SettingsRootNode.AppendChild(SettingNode);
                    }
                    // Store the value as an element of the Settings Root Node
                    SettingNode.SetAttribute("serializeAs", propVal.Property.PropertyType.Name);
                    ValueNode = SettingsXML.CreateElement("value");
                    ValueNode.InnerText = propVal.SerializedValue.ToString();
                    SettingNode.AppendChild(ValueNode);
                }
            }
            else
            {
                SettingNode = (XmlElement)SettingsRootNode.SelectSingleNode(Environment.MachineName + "/setting[@name = \"" + propVal.Name + "\"]/value");
                if (SettingNode != null)
                    SettingNode.InnerText = propVal.SerializedValue.ToString();
                else
                {
                    // Its machine specific, store as an element of the machine name node,
                    // creating a new machine name node if one doesnt exist.
                    MachineNode = (XmlElement)SettingsRootNode.SelectSingleNode(Environment.MachineName);
                    if (MachineNode == null)
                    {
                        MachineNode = SettingsXML.CreateElement(Environment.MachineName);
                        SettingsRootNode.AppendChild(MachineNode);
                    }
                    SettingNode = (XmlElement)SettingsRootNode.SelectSingleNode(Environment.MachineName + "/setting[@name = \"" + propVal.Name + "\"]");
                    if (SettingNode == null)
                    {
                        SettingNode = SettingsXML.CreateElement("setting");
                        SettingNode.SetAttribute("name", propVal.Property.Name);
                        MachineNode.AppendChild(SettingNode);
                    }
                    // Store the value as an element of the Settings Root Node
                    SettingNode.SetAttribute("serializeAs", propVal.Property.PropertyType.Name);
                    ValueNode = SettingsXML.CreateElement("value");
                    ValueNode.InnerText = propVal.SerializedValue.ToString();
                    SettingNode.AppendChild(ValueNode);
                }
            }
        }
        /// <summary>
        /// Determine if the setting is marked as Roaming
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private bool IsRoaming(SettingsProperty prop)
        {
            if (SkipRoamingCheck) 
                return true;
            foreach(DictionaryEntry d in prop.Attributes)
            {
                Attribute a = (Attribute)d.Value;
                if (a is SettingsManageabilityAttribute)
                    return true;
            }
            return false;
        }
    }

    [SettingsProvider(typeof(AppConfigProvider))]
    internal sealed partial class Settings
    {
        private const string defaultXmlStr ="<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<configuration>" +
            "  <applicationSettings>" +
            "       <setting name =\"ConfigPath\" serializeAs=\"String\">" +
            "          <value></value >" +
            "      </setting>" +
            "  </applicationSettings>" +
            "</configuration>";
        public string ConfigPath
        {
            get
            {
                XmlDocument appSettingsXML = new XmlDocument();
                string appPath = (new FileInfo(Application.ExecutablePath)).DirectoryName;
                string appSettingFile = Path.Combine(appPath, Application.ProductName+".ConfigPath");
                if(!File.Exists(appSettingFile))
                {
                    //在程序所在目录新建.ConfigPath文件
                    appSettingsXML.LoadXml(defaultXmlStr);
                    XmlNode cfgNode = appSettingsXML.SelectSingleNode("configuration/applicationSettings//setting[@name=\"ConfigPath\"]");
                    cfgNode.SelectSingleNode("value").InnerText = appPath;
                    appSettingsXML.Save(appSettingFile);
                }
                try
                {
                    appSettingsXML.Load(appSettingFile);
                    XmlNode cfgNode = appSettingsXML.SelectSingleNode("configuration/applicationSettings//setting[@name=\"ConfigPath\"]");
                    string path = cfgNode.SelectSingleNode("value").InnerText;
                    if (!Directory.Exists(path))
                    {
                        //配置文件目录不存在，默认使用程序所在目录
                        path = appPath;
                        cfgNode.SelectSingleNode("value").InnerText = path;
                        appSettingsXML.Save(appSettingFile);
                    }
                    return path;
                }
                catch (Exception)
                {
                    //MessageBox.Show(".ConfigPath 文件格式错误，无法获取配置文件位置！");
                    //将格式错误的配置文件更改成其他文件名作为备份
                    if (File.Exists(appSettingFile + "_1"))
                        File.Delete(appSettingFile + "_1");
                    File.Move(appSettingFile,appSettingFile+"_1");
                    //新建.ConfigPath文件，默认使用程序所在目录
                    appSettingsXML.LoadXml(defaultXmlStr);
                    XmlNode cfgNode = appSettingsXML.SelectSingleNode("configuration/applicationSettings//setting[@name=\"ConfigPath\"]");
                    cfgNode.SelectSingleNode("value").InnerText = appPath;
                    appSettingsXML.Save(appSettingFile);
                    return appPath;
                }
            }
            set
            {
                XmlDocument appSettingsXML = new XmlDocument();
                string appSettingFile = Path.Combine((new FileInfo(Application.ExecutablePath)).DirectoryName, Application.ProductName + ".ConfigPath");
                try
                {
                    appSettingsXML.Load(appSettingFile);
                }
                catch (Exception)
                {
                    appSettingsXML.LoadXml(defaultXmlStr);
                }
                XmlNode cfgNode = appSettingsXML.SelectSingleNode("configuration/applicationSettings//setting[@name=\"ConfigPath\"]");
                cfgNode.SelectSingleNode("value").InnerText = value;
                appSettingsXML.Save(appSettingFile);
            }
        }
        public string ConfigFile
        {
            get
            {
                return "user.config";
            }

        }
        ///程序修改应用配置文件保存位置是，应询问是否将当前配置保存到新位置，如不保存当前配置，而是加载新位置的配置文件，则需要调用AppConfigProvider
        ///类的ResetSettingsXml方法，将xml文件对象重置。
        public string ChangeConfigPath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            string cofigFilePath = ConfigPath;
            if (cofigFilePath.Length > 0) {
                dialog.SelectedPath = cofigFilePath.Substring(0, cofigFilePath.Length - 1);
            }
            if (dialog.ShowDialog() != DialogResult.OK)
                return "";
            cofigFilePath = dialog.SelectedPath + "\\";
            ConfigPath = cofigFilePath;
            if (MessageBox.Show("是否保存现有配置到新目录", "保存？", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Default.Save();
            else
            {
                foreach (SettingsProvider sp in Default.Providers)
                {
                    if (sp.GetType() == typeof(AppConfigProvider))
                        ((AppConfigProvider)sp).ResetSettingsXml();
                }
                Default.Reload();
                MessageBox.Show("加载新配置文件，程序重启", "重启");
                Application.Restart();
            }
            return ConfigPath;
        }
    }
}