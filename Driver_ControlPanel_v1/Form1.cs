using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.IO.Ports;
using AHRSInterface;

namespace Driver_ControlPanel_v1
{
    public partial class Form1 : Form
    {

        int channel;
        //AHRS[] motor = new AHRS[7];
        AHRS motor = new AHRS();
        delegate void AppendTextCallback(string text, Color text_color, int channel);
        delegate void AppendLabel1TextCallback(string text, int channel);
        delegate void AppendLabel2TextCallback(string text, int channel);
        delegate void AppendLabel3TextCallback(string text, int channel);
        delegate void ShowTextCallback(uint Position_Target, uint Position_Now, short Velocity_External, short Velocity_Internal, short Velocity_QEI, short Torque_External, short Torque_Internal, short Motor_Current);
        Timer renderTimer;
        public Form1()
        {
            InitializeComponent();
            initializeSerialPort();
            /*
            for (int i = 0; i < 2; i++) {
            motor[i] = new AHRS();
            motor[i].mChBox_CH = Convert.ToByte(i.ToString());
            motor[i].PacketTimeoutEvent += new StateDelegate(TimeoutEventHandler);
            motor[i].PacketReceivedEvent += new PacketDelegate(PacketReceivedEventHandler);
            motor[i].DataReceivedEvent += new DataReceivedDelegate(DataReceivedEventHandler);
            motor[i].PacketSentEvent += new PacketDelegate(PacketSentEventHandler);
            motor[i].COMFailedEvent += new COMFailedDelegate(COMFailedEventHandler);
            motor[i].PacketLabelEvent += new PacketLabel(PacketLabelHandler);
            renderTimer = new Timer();
            }
            */

            //motor.mChBox_CH = Convert.ToByte(channel.ToString());
            motor.PacketTimeoutEvent += new StateDelegate(TimeoutEventHandler);
            motor.PacketReceivedEvent += new PacketDelegate(PacketReceivedEventHandler);
            motor.DataReceivedEvent += new DataReceivedDelegate(DataReceivedEventHandler);
            motor.PacketSentEvent += new PacketDelegate(PacketSentEventHandler);
            motor.COMFailedEvent += new COMFailedDelegate(COMFailedEventHandler);
            motor.PacketLabelEvent += new PacketLabel(PacketLabelHandler);
            //motor.Kick_Off();
            motor.mChBox_CH = Convert.ToByte(0);
            //motor.synch();
            //motor.Kick_Off();
            /*
            for (int i = 0; i < 4; i++) {
                motor.mChBox_CH = Convert.ToByte(i);
                //motor.synch();
                //System.Threading.Thread.Sleep(5000);
                //motor.Kick_Off();
                //motor.updateAHRS(STATE_RESET_CONTROLLER).sendPacket();
                }
             */
             renderTimer = new Timer();

            renderTimer.Interval = 20;
            renderTimer.Enabled = true;
            //renderTimer.Tick += new System.EventHandler(OnRenderTimerTick);
            //int i;  // index in for loop
            //for ( i = 0; i < 7; i++)
            // Event handlers
        }
        
        public void OnRenderTimerTick(object source, EventArgs e)
        {
            //cube.Invalidate();
        }
        private void initializeSerialPort()
        {
            serialPort = new SerialPort();

            foreach (string s in SerialPort.GetPortNames())
            {
                serialPortCOMBox.Items.Add(s);
            }
            if (serialPortCOMBox.Items.Count == 0)
            {
                serialPortCOMBox.Items.Add("No Ports Avaliable");
                serialPortCOMBox.Enabled = false;
                serialConnectButton.Enabled = false;
            }

            serialPortCOMBox.SelectedIndex = 0;
          
            baudSelectBox.Items.Add(4900000);
            baudSelectBox.Items.Add(5000000);

            baudSelectBox.SelectedIndex = 0;
        }

        //Appen label text function
        private void AppendLabel1Text(string text, int channel)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                switch (channel)
                {
                    case 0:
                        if (this.Motor1_Label1.InvokeRequired)
                        {
                            AppendLabel1TextCallback d = new AppendLabel1TextCallback(AppendLabel1Text);
                            this.Invoke(d, new object[] { text });
                        }
                        else
                        {
                            this.Motor1_Label1.Text = text;
                        }
                        break;
                    case 1:
                        if (this.Motor2_Label1.InvokeRequired)
                        {
                            AppendLabel1TextCallback d = new AppendLabel1TextCallback(AppendLabel1Text);
                            this.Invoke(d, new object[] { text });
                        }
                        else
                        {
                            this.Motor2_Label1.Text = text;
                        }
                        break;
                }
            }
            catch { }
        }

        private void AppendLabel2Text(string text, int channel)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                switch (channel)
                {
                    case 0:
                        if (this.Motor1_Label2.InvokeRequired)
                        {
                            AppendLabel2TextCallback d = new AppendLabel2TextCallback(AppendLabel2Text);
                            this.Invoke(d, new object[] { text, channel });
                        }
                        else
                        {
                            this.Motor1_Label2.Text = text;
                        }
                        break;
                    case 1:
                        if (this.Motor2_Label2.InvokeRequired)
                        {
                            AppendLabel2TextCallback d = new AppendLabel2TextCallback(AppendLabel2Text);
                            this.Invoke(d, new object[] { text, channel });
                        }
                        else
                        {
                            this.Motor2_Label2.Text = text;
                        }
                        break;
                }
            }
            catch { }
        }

        private void AppendLabel3Text(string text, int channel)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                switch (channel)
                {
                    case 0:
                        if (this.Motor1_Label3.InvokeRequired)
                        {
                            AppendLabel3TextCallback d = new AppendLabel3TextCallback(AppendLabel3Text);
                            this.Invoke(d, new object[] { text, channel });
                        }
                        else
                        {
                            this.Motor1_Label3.Text = text;
                        }
                        break;
                    case 1:
                        if (this.Motor2_Label3.InvokeRequired)
                        {
                            AppendLabel3TextCallback d = new AppendLabel3TextCallback(AppendLabel3Text);
                            this.Invoke(d, new object[] { text, channel });
                        }
                        else
                        {
                            this.Motor2_Label3.Text = text;
                        }
                        break;
                }
            }
            catch { }
        }

        private void AppendStatusText(string text, Color text_color , int channel)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                switch (channel)
                {
                    case 0:
                        if (this.Motor1_Statusbox.InvokeRequired)
                        {
                            AppendTextCallback d = new AppendTextCallback(AppendStatusText);
                            this.Invoke(d, new object[] { text, text_color ,channel});
                        }
                        else
                        {
                            this.Motor1_Statusbox.SelectionColor = text_color;
                            this.Motor1_Statusbox.AppendText(text);
                            this.Motor1_Statusbox.ScrollToCaret();
                        }
                        break;
                    case 1:
                        if (this.Motor2_Statusbox.InvokeRequired)
                        {
                            AppendTextCallback d = new AppendTextCallback(AppendStatusText);
                            this.Invoke(d, new object[] { text, text_color, channel });
                        }
                        else
                        {
                            this.Motor2_Statusbox.SelectionColor = text_color;
                            this.Motor2_Statusbox.AppendText(text);
                            this.Motor2_Statusbox.ScrollToCaret();
                        }
                        break;
                    case 2:
                        if (this.Motor3_Statusbox.InvokeRequired)
                        {
                            AppendTextCallback d = new AppendTextCallback(AppendStatusText);
                            this.Invoke(d, new object[] { text, text_color, channel });
                        }
                        else
                        {
                            this.Motor3_Statusbox.SelectionColor = text_color;
                            this.Motor3_Statusbox.AppendText(text);
                            this.Motor3_Statusbox.ScrollToCaret();
                        }
                        break;
                    case 3:
                        if (this.Motor4_Statusbox.InvokeRequired)
                        {
                            AppendTextCallback d = new AppendTextCallback(AppendStatusText);
                            this.Invoke(d, new object[] { text, text_color, channel });
                        }
                        else
                        {
                            this.Motor4_Statusbox.SelectionColor = text_color;
                            this.Motor4_Statusbox.AppendText(text);
                            this.Motor4_Statusbox.ScrollToCaret();
                        }
                        break;
                    case 4:
                        if (this.Motor5_Statusbox.InvokeRequired)
                        {
                            AppendTextCallback d = new AppendTextCallback(AppendStatusText);
                            this.Invoke(d, new object[] { text, text_color, channel });
                        }
                        else
                        {
                            this.Motor5_Statusbox.SelectionColor = text_color;
                            this.Motor5_Statusbox.AppendText(text);
                            this.Motor5_Statusbox.ScrollToCaret();
                        }
                        break;
                    case 5:
                        if (this.Motor6_Statusbox.InvokeRequired)
                        {
                            AppendTextCallback d = new AppendTextCallback(AppendStatusText);
                            this.Invoke(d, new object[] { text, text_color, channel });
                        }
                        else
                        {
                            this.Motor6_Statusbox.SelectionColor = text_color;
                            this.Motor6_Statusbox.AppendText(text);
                            this.Motor6_Statusbox.ScrollToCaret();
                        }
                        break;
                    case 6:
                        if (this.Motor7_Statusbox.InvokeRequired)
                        {
                            AppendTextCallback d = new AppendTextCallback(AppendStatusText);
                            this.Invoke(d, new object[] { text, text_color, channel });
                        }
                        else
                        {
                            this.Motor7_Statusbox.SelectionColor = text_color;
                            this.Motor7_Statusbox.AppendText(text);
                            this.Motor7_Statusbox.ScrollToCaret();
                        }
                        break;
                }


            }
            catch { }
        }

        private void ShowText(uint Position_Target, uint Position_Now, short Velocity_External, short Velocity_Internal, short Velocity_QEI, short Torque_External, short Torque_Internal, short Motor_Current)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                switch (motor.ch_tmp)
                {
                    case 0:
                        if (this.Motor1_Status1.InvokeRequired)
                        {
                            ShowTextCallback d = new ShowTextCallback(ShowText);
                            this.Invoke(d, new object[] {Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal,Motor_Current});
                        }
                        else
                        {
                            this.Motor1_Status1.Text = Position_Target.ToString();
                            this.Motor1_Status2.Text = Position_Now.ToString();
                            this.Motor1_Status3.Text = Velocity_External.ToString();
                            this.Motor1_Status4.Text = Velocity_Internal.ToString();
                            this.Motor1_Status5.Text = Velocity_QEI.ToString();
                            this.Motor1_Status6.Text = Torque_External.ToString();
                            this.Motor1_Status7.Text = Torque_Internal.ToString();
                            this.Motor1_Status8.Text = Motor_Current.ToString();
                        }
                        break;
                    case 1:
                        if (this.Motor2_Status1.InvokeRequired)
                        {
                            ShowTextCallback d = new ShowTextCallback(ShowText);
                            this.Invoke(d, new object[] { Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal, Motor_Current });
                        }
                        else
                        {
                            this.Motor2_Status1.Text = Position_Target.ToString();
                            this.Motor2_Status2.Text = Position_Now.ToString();
                            this.Motor2_Status3.Text = Velocity_External.ToString();
                            this.Motor2_Status4.Text = Velocity_Internal.ToString();
                            this.Motor2_Status5.Text = Velocity_QEI.ToString();
                            this.Motor2_Status6.Text = Torque_External.ToString();
                            this.Motor2_Status7.Text = Torque_Internal.ToString();
                            this.Motor2_Status8.Text = Motor_Current.ToString();
                        }
                        break;
                    case 2:
                        if (this.Motor3_Status1.InvokeRequired)
                        {
                            ShowTextCallback d = new ShowTextCallback(ShowText);
                            this.Invoke(d, new object[] { Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal, Motor_Current });
                        }
                        else
                        {
                            this.Motor3_Status1.Text = Position_Target.ToString();
                            this.Motor3_Status2.Text = Position_Now.ToString();
                            this.Motor3_Status3.Text = Velocity_External.ToString();
                            this.Motor3_Status4.Text = Velocity_Internal.ToString();
                            this.Motor3_Status5.Text = Velocity_QEI.ToString();
                            this.Motor3_Status6.Text = Torque_External.ToString();
                            this.Motor3_Status7.Text = Torque_Internal.ToString();
                            this.Motor3_Status8.Text = Motor_Current.ToString();
                        }
                        break;
                    case 3:
                        if (this.Motor4_Status1.InvokeRequired)
                        {
                            ShowTextCallback d = new ShowTextCallback(ShowText);
                            this.Invoke(d, new object[] { Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal, Motor_Current });
                        }
                        else
                        {
                            this.Motor4_Status1.Text = Position_Target.ToString();
                            this.Motor4_Status2.Text = Position_Now.ToString();
                            this.Motor4_Status3.Text = Velocity_External.ToString();
                            this.Motor4_Status4.Text = Velocity_Internal.ToString();
                            this.Motor4_Status5.Text = Velocity_QEI.ToString();
                            this.Motor4_Status6.Text = Torque_External.ToString();
                            this.Motor4_Status7.Text = Torque_Internal.ToString();
                            this.Motor4_Status8.Text = Motor_Current.ToString();
                        }
                        break;
                    case 4:
                        if (this.Motor5_Status1.InvokeRequired)
                        {
                            ShowTextCallback d = new ShowTextCallback(ShowText);
                            this.Invoke(d, new object[] { Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal, Motor_Current });
                        }
                        else
                        {
                            this.Motor5_Status1.Text = Position_Target.ToString();
                            this.Motor5_Status2.Text = Position_Now.ToString();
                            this.Motor5_Status3.Text = Velocity_External.ToString();
                            this.Motor5_Status4.Text = Velocity_Internal.ToString();
                            this.Motor5_Status5.Text = Velocity_QEI.ToString();
                            this.Motor5_Status6.Text = Torque_External.ToString();
                            this.Motor5_Status7.Text = Torque_Internal.ToString();
                            this.Motor5_Status8.Text = Motor_Current.ToString();
                        }
                        break;
                    case 5:
                        if (this.Motor6_Status1.InvokeRequired)
                        {
                            ShowTextCallback d = new ShowTextCallback(ShowText);
                            this.Invoke(d, new object[] { Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal, Motor_Current });
                        }
                        else
                        {
                            this.Motor6_Status1.Text = Position_Target.ToString();
                            this.Motor6_Status2.Text = Position_Now.ToString();
                            this.Motor6_Status3.Text = Velocity_External.ToString();
                            this.Motor6_Status4.Text = Velocity_Internal.ToString();
                            this.Motor6_Status5.Text = Velocity_QEI.ToString();
                            this.Motor6_Status6.Text = Torque_External.ToString();
                            this.Motor6_Status7.Text = Torque_Internal.ToString();
                            this.Motor6_Status8.Text = Motor_Current.ToString();
                        }
                        break;
                    case 6:
                        if (this.Motor7_Status1.InvokeRequired)
                        {
                            ShowTextCallback d = new ShowTextCallback(ShowText);
                            this.Invoke(d, new object[] { Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal, Motor_Current });
                        }
                        else
                        {
                            this.Motor7_Status1.Text = Position_Target.ToString();
                            this.Motor7_Status2.Text = Position_Now.ToString();
                            this.Motor7_Status3.Text = Velocity_External.ToString();
                            this.Motor7_Status4.Text = Velocity_Internal.ToString();
                            this.Motor7_Status5.Text = Velocity_QEI.ToString();
                            this.Motor7_Status6.Text = Torque_External.ToString();
                            this.Motor7_Status7.Text = Torque_Internal.ToString();
                            this.Motor7_Status8.Text = Motor_Current.ToString();
                        }
                        break;
                }


            }
            catch { }
        }
        // Variables Declaration

        /* **********************************************************************************
         * 
         * Function: void TimeoutEventHandler
         * Inputs: PName packet_type, int flags
         * Outputs: None
         * Return Value: None
         * Dependencies: None
         * Description: 
         * 
         * Handles timeout events generated by the AHRS class - a timeout event occurs if the
         * AHRS class attempts to communicate with the AHRS device and receives no response.
         * 
         * *********************************************************************************/
        
        void TimeoutEventHandler(StateName packet_type, int flags)
        {
            string message;

            message = "Timeout: ";
            message += System.Enum.Format(typeof(StateName), packet_type, "G");
            message += "\r\n";

            AppendStatusText(message, Color.Red, channel);
           
           }
        
        void COMFailedEventHandler()
        {
            //            AppendStatusText("Serial COM failed\r\n", Color.Red);
        }
        
        /* **********************************************************************************
        * 
        * Function: void PacketReceivedEventHandler
        * Inputs: PName packet_type, int flags
        * Outputs: None
        * Return Value: None
        * Dependencies: None
        * Description: 
        * 
        * Handles PacketReceived events generated by the AHRS.
        * 
        * *********************************************************************************/
        void PacketReceivedEventHandler(PName packet_type, int flags , int motor_channel)
        {
            string message;

            if (packet_type == PName.CMD_NO_SUPPORT)
            {
                message = "Command Failed: ";
                message += System.Enum.Format(typeof(PName), flags, "G");
                message += "\r\n";
            }
            else if (packet_type == PName.CMD_COMPLETE)
            {
                message = "Command Complete: ";
                message += System.Enum.Format(typeof(PName), flags, "G");
                message += "\r\n";
            }
            else if (packet_type == PName.CMD_CRC_FAILED)
            {
                if (flags == -1)
                {
                    message = "Device CRC Failed:: ";
                }
                else
                {
                    message = "Command CRC Failed:: ";
                }
                message += System.Enum.Format(typeof(PName), 0, "G");
                message += "\r\n";

            }
            else if (packet_type == PName.CMD_OVER_DATA_LENGTH)
            {
                if (flags == -1)
                {
                    message = "Device Over Data Length: ";
                }
                else
                {
                    message = "Command Over Data Length: ";
                }
                message += System.Enum.Format(typeof(PName), 0, "G");
                message += "\r\n";
            }
            else
            {
                message = "Received ";
                message += System.Enum.Format(typeof(PName), packet_type, "G");
                message += " packet\r\n";
                
                // sync會佔去大量時間
                //motor.synch();
                 
            }

            AppendStatusText(message, Color.Green , channel);
        }

        /* **********************************************************************************
        * 
        * Function: void PacketSentEventHandler
        * Inputs: PName packet_type, int flags
        * Outputs: None
        * Return Value: None
        * Dependencies: None
        * Description: 
        * 
        * Handles PacketReceived events generated by the AHRS.
        * 
        * *********************************************************************************/

        void PacketSentEventHandler(PName packet_type, int flags , int motor_channel)
        {
            channel = motor_channel;
            string message;

            message = "Sent ";
            message += System.Enum.Format(typeof(PName), packet_type, "G");
            message += " packet\r\n";
            
            AppendStatusText(message, Color.Blue,channel);
        }

        /* **********************************************************************************
        * 
        * Function: void PacketLabelHandler
        * Inputs: byte[] data, int flags
        * Outputs: None
        * Return Value: None
        * Dependencies: None
        * Description: 
        * 
        * Handles PacketReceived events generated by the AHRS.
        * 
        * *********************************************************************************/
        
        void PacketLabelHandler(byte[] data, int flags)
        {
            
            string message;
            int i = data.Length;


            if (flags == 1)
            {

                message = "Model Name: ";
                for (i = 0; i < data.Length; i++)
                {
                    message += Convert.ToChar(data[i]);
                }
                AppendLabel1Text(message,channel);
            }
            else if (flags == 2)
            {
                message = "F/W Version: ";
                for (i = 0; i < data.Length; i++)
                {
                    message += Convert.ToChar(data[i]);
                }

                AppendLabel2Text(message, channel);
            }
            else if (flags == 23)
            {
                message = "Sensor ADC: 0x";
                for (i = 0; i < (data.Length - 1); i++)
                {
                    message += data[i].ToString("X2");
                }

                AppendLabel3Text(message, channel);
            }

            else
            {
                // to do something
            }
            
            
             //motor.synch();
        }
        
        /* **********************************************************************************
        * 
        * Function: void DataReceivedEventHandler
        * Inputs: None
        * Outputs: None
        * Return Value: None
        * Dependencies: None
        * Description: 
        * 
        * Handles DataReceived events generated by the AHRS object
        * 
        * *********************************************************************************/
       
        void DataReceivedEventHandler(int active_channels)
        {
            channel = motor.ch_tmp;
            uint Position_Target = motor.Motor_Member[channel].Position_Target;
            uint Position_Now = motor.Motor_Member[channel].QEI32;

            short Velocity_External = motor.Motor_Member[channel].Velocity_External;
            short Velocity_Internal = motor.Motor_Member[channel].Velocity_Internal;
            short Velocity_QEI = motor.Motor_Member[channel].QEI_Diff16;

            short Torque_External = motor.Motor_Member[channel].Torque_External;
            short Torque_Internal = motor.Motor_Member[channel].Torque_Internal;
            short Motor_Current = motor.Motor_Member[channel].Motor_Current;

            ShowText(Position_Target, Position_Now, Velocity_External, Velocity_Internal, Velocity_QEI, Torque_External, Torque_Internal,Motor_Current);
            /*
            int channel_test=motor.mChBox_CH;
            if (channel < 3)
                channel += 1;
            else
                channel = 0;
            motor.mChBox_CH = Convert.ToByte(channel);
            motor.synch();
            motor.Kick_Off();
            */
            //System.Threading.Thread.Sleep(1000);
        }

        private void DataReceivedbitStatus(byte chstatus)
        {
            /*
            bool ptbit4, ptbit5, ptbit6, ptbit7;
            
            ptbit4 = Convert.ToBoolean(Convert.ToByte(sensor.Bit_Statis) & 0x10);
            ptbit5 = Convert.ToBoolean(Convert.ToByte(sensor.Bit_Statis) & 0x20);
            ptbit6 = Convert.ToBoolean(Convert.ToByte(sensor.Bit_Statis) & 0x40);
            ptbit7 = Convert.ToBoolean(Convert.ToByte(sensor.Bit_Statis) & 0x80);
            if (ptbit4)
                picbit4.BackColor = Color.GreenYellow;
            else
                picbit4.BackColor = Color.Red;
            if (ptbit5)
                picbit5.BackColor = Color.GreenYellow;
            else
                picbit5.BackColor = Color.Red;
            if (ptbit6)
                picbit6.BackColor = Color.GreenYellow;
            else
                picbit6.BackColor = Color.Red;
            if (ptbit7)
                picbit7.BackColor = Color.GreenYellow;
            else
                picbit7.BackColor = Color.Red;
             * */
             
        }

        private void serialConnectButton_Click(object sender, EventArgs e)
        {
            try{
                    // Connect to the serial port
                    if (!motor.connect(serialPortCOMBox.SelectedItem.ToString(), (int)baudSelectBox.SelectedItem))
                    {
                        AppendStatusText("Failed to connect to serial port\r\n", Color.Red, channel);
                    }
                    else
                    {
                        AppendStatusText("Connected to " + serialPortCOMBox.SelectedItem.ToString() + "\r\n", Color.Blue, channel);

                        serialDisconnectButton.Enabled = true;
                        serialConnectButton.Enabled = false;
                        //magCalibrationToolStripMenuItem.Enabled = true;
                        //configToolStripMenuItem.Enabled = true;
                        //logDataToolstripItem.Enabled = true;
                        // sensor.Graph_Sketch = true;

                        //motor[0].synch();
                        motor.synch();
                        //motor.Kick_Off();
                    }
                /*
                    while (true) { 
                    for (int i = 0; i < 2; i++)
                    {
                        channel = i;
                        motor.mChBox_CH = Convert.ToByte(i.ToString());
                        motor.synch();
                        System.Threading.Thread.Sleep(500);
                        //Sleep(1000);
                    }
                }
                */ 
            }
            catch { }
        }

        private void serialDisconnectButton_Click(object sender, EventArgs e)
        {

            try
            {

                //Pos_Pane.CurveList.Clear();
                //Pos_Pane.GraphObjList.Clear();

                //Vel_Pane.CurveList.Clear();
                //Vel_Pane.GraphObjList.Clear();

                //Cur_Pane.CurveList.Clear();
                //Cur_Pane.GraphObjList.Clear();
                //refreshGraphs();
                
                //sensor.Graph_Sketch = false;
                motor.Disconnect();
                motor.Invalidate();

                serialDisconnectButton.Enabled = false;
                serialConnectButton.Enabled = true;
                /*
                magCalibrationToolStripMenuItem.Enabled = false;
                configToolStripMenuItem.Enabled = false;
                logDataToolstripItem.Enabled = false;
                */
                AppendStatusText("Disconnected from serial port\r\n", Color.Blue,channel);
            }
            catch { }
        }

        private void KickOff_Click(object sender, EventArgs e)
        {
            motor.Kick_Off();
        }



 
    }
}
