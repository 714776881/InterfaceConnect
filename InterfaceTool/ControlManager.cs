using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceConnect
{
    public class ControlManager
    {
        private static readonly Lazy<ControlManager> lazy = new Lazy<ControlManager>(() => new ControlManager());
        public static ControlManager Instance => lazy.Value;
        private Dictionary<string, UserControl> controls = new Dictionary<string, UserControl>();
        private ControlManager() { }
        public UserControl GetControl(InterfaceConfig config, bool isNew = false)
        {
            if (controls.TryGetValue(config.Action, out UserControl control) && isNew == false)
            {
                return control;
            }
            control = Produce(config.Type, config);
            controls[config.Action] = control;
            return control;
        }
        public bool Remove(string action)
        {
            return controls.Remove(action);
        }
        public void Update(string action,string newAction)
        {
            controls.TryGetValue(action, out UserControl control);
            if(control != null)
            {
                controls.Remove(action);
                controls[newAction] = control;
            }
        }
        private static UserControl Produce(ConfigType type, InterfaceConfig config)
        {
            if (type == ConfigType.HTTP)
            {
                 return new HttpControl(config);
            }
            else if (type == ConfigType.DB)
            {
                return new DBControl(config);
            }
            else if(type == ConfigType.DATA)
            {
                //return new DataControl(config);
                return null;
            }
            else if(type == ConfigType.SCRIPT)
            {
                return new ScriptControl(config);
            }
            else
            {
                throw new NotSupportedException($"API Type {config.Type} not supported.");
            }
        }
    }
}