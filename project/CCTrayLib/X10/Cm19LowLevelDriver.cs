using System;
using System.Windows.Forms;
using ActiveHomeScriptLib;

namespace ThoughtWorks.CruiseControl.CCTrayLib.X10
{
    public class Cm19LowLevelDriver : IX10LowLevelDriver
    {
        /// <summary>
        /// Very basic platform wrapper provided for the CM19 device.
        /// </summary>
        private ActiveHomeClass _activeHome; 

        /// <summary>
        /// X10 House code.
        /// </summary>
        private string _housecode;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="houseCode">x10 house code</param>
        public Cm19LowLevelDriver(string houseCode)
        {
            _housecode = houseCode;
            EnsureActiveHomeInitalised();
        }

        /// <summary>
        /// Call into the active home SDK for the USB connectivity. 
        /// </summary>
        public void ControlDevice(int deviceCode, Function deviceCommand, int lightLevel)
        {
            if (_activeHome != null)
            {
                try
                {
                    switch (deviceCommand)
                    {
                        case Function.AllUnitsOff:
                            break;
                        case Function.AllLightsOn:
                            _activeHome.SendAction("SendPLC", _housecode + deviceCode + " On", null, null);
                            _activeHome.SendAction("SendPLC", _housecode + deviceCode + " On", null, null);
                            break;
                        case Function.On:
                            _activeHome.SendAction("SendPLC", _housecode + deviceCode + " On", null, null);
                            break;
                        case Function.Off:
                            _activeHome.SendAction("SendPLC", _housecode + deviceCode + " Off", null, null);
                            break;
                        case Function.Dim:
                            break;
                        case Function.Bright:
                            break;
                        case Function.AllLightsOff:
                            _activeHome.SendAction("SendPLC", _housecode + deviceCode + " Off", null, null);
                            _activeHome.SendAction("SendPLC", _housecode + deviceCode + " Off", null, null);
                            break;
                        case Function.ExtendedCode:
                            break;
                        case Function.HailRequest:
                            break;
                        case Function.HailAcknowledge:
                            break;
                        case Function.PresetDim1:
                            break;
                        case Function.PresetDim2:
                            break;
                        case Function.ExtededDataTransfer:
                            break;
                        case Function.StatusOn:
                            break;
                        case Function.StatusOff:
                            break;
                        case Function.StatusRequest:
                            break;
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("Problem with the CM19 X10 'active home API' ", exception);
                }
            }
        }

        /// <summary>
        /// Not implemented 
        /// </summary>
        public void ResetStatus(Label statusLabel){}

        /// <summary>
        /// Not implemented 
        /// </summary>
        public void CloseDriver(){}

        /// <summary>
        /// To avoid problems where users don't have the correct drivers installed. 
        /// Safely initialise the platform API
        /// </summary>
        private void EnsureActiveHomeInitalised()
        {
            try
            {
                if (_activeHome == null)
                {
                    _activeHome = new ActiveHomeClass();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Can't locate CM19 Driver... Please ensure you have the correct CM19 driver installed.", "CM19 Driver not found",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
