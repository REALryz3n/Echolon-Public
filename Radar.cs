namespace ECHELON
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed class Radar : ApplicationSettingsBase
    {
        private static Radar defaultInstance = ((Radar) SettingsBase.Synchronized(new Radar()));

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("127.0.0.1")]
        public string adress
        {
            get => 
                ((string) this["adress"]);
            set
            {
                this["adress"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("128, 64, 0")]
        public Color angryColor
        {
            get => 
                ((Color) this["angryColor"]);
            set
            {
                this["angryColor"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("255")]
        public int angryColorA
        {
            get => 
                ((int) this["angryColorA"]);
            set
            {
                this["angryColorA"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("Magenta")]
        public Color cupboardColor
        {
            get => 
                ((Color) this["cupboardColor"]);
            set
            {
                this["cupboardColor"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("255")]
        public int cupboardColorA
        {
            get => 
                ((int) this["cupboardColorA"]);
            set
            {
                this["cupboardColorA"] = value;
            }
        }

        public static Radar Default =>
            defaultInstance;

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool drawAngry
        {
            get => 
                ((bool) this["drawAngry"]);
            set
            {
                this["drawAngry"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("False")]
        public bool drawCupboards
        {
            get => 
                ((bool) this["drawCupboards"]);
            set
            {
                this["drawCupboards"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool drawNice
        {
            get => 
                ((bool) this["drawNice"]);
            set
            {
                this["drawNice"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("False")]
        public bool drawPlayerNames
        {
            get => 
                ((bool) this["drawPlayerNames"]);
            set
            {
                this["drawPlayerNames"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool drawPlayers
        {
            get => 
                ((bool) this["drawPlayers"]);
            set
            {
                this["drawPlayers"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("False")]
        public bool drawRocks
        {
            get => 
                ((bool) this["drawRocks"]);
            set
            {
                this["drawRocks"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("LightCoral")]
        public Color niceColor
        {
            get => 
                ((Color) this["niceColor"]);
            set
            {
                this["niceColor"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("255")]
        public int niceColorA
        {
            get => 
                ((int) this["niceColorA"]);
            set
            {
                this["niceColorA"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("Red")]
        public Color playerColor
        {
            get => 
                ((Color) this["playerColor"]);
            set
            {
                this["playerColor"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("255")]
        public int playerColorA
        {
            get => 
                ((int) this["playerColorA"]);
            set
            {
                this["playerColorA"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("5454")]
        public string port
        {
            get => 
                ((string) this["port"]);
            set
            {
                this["port"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("255")]
        public int radarPrimaryA
        {
            get => 
                ((int) this["radarPrimaryA"]);
            set
            {
                this["radarPrimaryA"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("Red")]
        public Color radarPrimaryC
        {
            get => 
                ((Color) this["radarPrimaryC"]);
            set
            {
                this["radarPrimaryC"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool radarPrimaryT
        {
            get => 
                ((bool) this["radarPrimaryT"]);
            set
            {
                this["radarPrimaryT"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("55")]
        public int radarSecondaryA
        {
            get => 
                ((int) this["radarSecondaryA"]);
            set
            {
                this["radarSecondaryA"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("Black")]
        public Color radarSecondaryC
        {
            get => 
                ((Color) this["radarSecondaryC"]);
            set
            {
                this["radarSecondaryC"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool radarSecondaryT
        {
            get => 
                ((bool) this["radarSecondaryT"]);
            set
            {
                this["radarSecondaryT"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("Blue")]
        public Color rocksColor
        {
            get => 
                ((Color) this["rocksColor"]);
            set
            {
                this["rocksColor"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("255")]
        public int rocksColorA
        {
            get => 
                ((int) this["rocksColorA"]);
            set
            {
                this["rocksColorA"] = value;
            }
        }
    }
}

