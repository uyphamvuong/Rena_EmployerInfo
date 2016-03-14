using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class ProcessBarEdit : UserControl
    {
        public ProcessBarEdit()
        {
            InitializeComponent();
        }

        #region Properties

        private Color _colorUnderBar = Color.AliceBlue,
            _colorProcess = Color.DeepSkyBlue;
        private int _valuePercent = 20;

        [Description("Màu của nền process")]
        public Color ColorUnderBar
        {
            get
            {
                return _colorUnderBar;
            }

            set
            {
                _colorUnderBar = value;
                pUnderBar.BackColor = value;
            }
        }

        [Description("Màu của thanh process")]
        public Color ColorProcess
        {
            get
            {
                return _colorProcess;
            }

            set
            {
                _colorProcess = value;
                pProcess.BackColor = value;
            }
        }

        [Description("Giá trị processbar")]
        public int ValuePercent
        {
            get
            {
                return _valuePercent;
            }

            set
            {
                _valuePercent = value;
                SetPercent(value);
            }
        }

        #endregion

        
        #region Function

        void SetPercent(int val)
        {
            if (val > 100) { val = 100; }
            double s1 = (double)pUnderBar.Width / 100;
            double s2 = s1 * val;
            pProcess.Width = Convert.ToInt32(s2);
        }

        #endregion

        private void ProcessBarEdit_Load(object sender, EventArgs e)
        {

        }

    }


}
