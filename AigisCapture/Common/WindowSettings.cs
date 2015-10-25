using System.Windows;
using System.Configuration;

namespace AigisCapture.Common
{
    public interface IWindowSettings
    {
        WINDOWPLACEMENT? Placement { get; set; }
        void Reload();
        void Save();
    }

    public class WindowSettings : ApplicationSettingsBase, IWindowSettings
    {
        public WindowSettings(Window window) : base(window.GetType().FullName) { }

        [UserScopedSetting]
        public WINDOWPLACEMENT? Placement
        {
            get { return this["Placement"] != null ? (WINDOWPLACEMENT?)(WINDOWPLACEMENT)this["Placement"] : null; }
            set { this["Placement"] = value; }
        }
    }
}

