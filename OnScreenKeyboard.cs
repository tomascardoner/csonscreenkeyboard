using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CardonerSistemas
{
    public partial class OnScreenKeyboard : UserControl
    {

        #region Declarations

        private TableLayoutPanel _panelKeyboard;

        #endregion

        #region Layout declarations

        private const int KeyboardRowsAlphanumericES = 4;
        private const int KeyboardColumnsAlphanumericES = 11;

        private const int KeyboardRowsNumericPhone = 4;
        private const int KeyboardColumnsNumericPhone = 3;

        private const int KeyboardRowsNumericCalculator = 4;
        private const int KeyboardColumnsNumericCalculator = 3;

        private const string KeyButtonNamePrefix = "buttonKey";
        private const string KeyButtonNameRowPrefix = "R";
        private const string KeyButtonNameColumnPrefix = "C";

        private string[,] KeyboardKeysAlphanumericES = new string[,]
        {
            { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ConstantsKeys.BACKSPACE },
            { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", ConstantsKeys.DELETE },
            { "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ñ", "Ç" },
            { "Z", "X", "C", "V", "B", "N", "M", ConstantsKeys.UserDefinedSPACE, ConstantsKeys.UserDefinedSPACE, "'", "Ü" }
        };

        private string[,] KeyboardKeysNumericPhone = new string[,]
        {
            { "1", "2", "3" },
            { "4", "5", "6" },
            { "7", "8", "9" },
            { ConstantsKeys.UserDefinedCLEAR, "0", ConstantsKeys.BACKSPACE }
        };

        private string[,] KeyboardKeysNumericCalculator = new string[,]
        {
            { "7", "8", "9" },
            { "4", "5", "6" },
            { "1", "2", "3" },
            { ConstantsKeys.UserDefinedCLEAR, "0", ConstantsKeys.BACKSPACE }
        };

        public enum KeyboardLayoutEnums
        {
            AlphanumericSpanish,
            NumericPhone,
            NumericCalculator
        }

        private int _KeyboardRows;
        private int _KeyboardColumns;
        private string[,] _KeyboardKeys;

        #endregion

        #region Properties

        private KeyboardLayoutEnums _KeyboardLayout;
        private TextBox _DestinationTextBox = null;
        private Color _KeyBackColor = SystemColors.Control;

        [Description("The keyboard layout and distribution"), Category("Appearance")]
        public KeyboardLayoutEnums KeyboardLayout
        {
            get => _KeyboardLayout;
            set
            {
                _KeyboardLayout = value;
                KeyboardLayoutChange();
            }
        }

        [Description("The textbox in wich the keys pressed appears"), Category("Data")]
        public TextBox DestinationTextBox { get => _DestinationTextBox; set => _DestinationTextBox = value; }

        [Description("The background color of the keys."), Category("Appearance")]
        public Color KeyBackColor
        {
            get => _KeyBackColor;
            set
            {
                _KeyBackColor = value;

                _panelKeyboard.SuspendLayout();

                foreach (Control button in _panelKeyboard.Controls)
                {
                    button.BackColor = _KeyBackColor;
                }

                _panelKeyboard.ResumeLayout();
            }
        }

        #endregion

        #region Initialization

        public OnScreenKeyboard()
        {
            InitializeComponent();
            KeyboardLayout = KeyboardLayoutEnums.AlphanumericSpanish;
        }

        #endregion

        #region Layout creation

        private void CreateKeyboard()
        {
            this.SuspendLayout();

            DestroyPreviousKeyboard();
            CreatePanel();
            CreateButtons();

            this.ResumeLayout();
        }

        private void DestroyPreviousKeyboard()
        {
            if (_panelKeyboard != null)
            {
                // Clean old keyboard keys
                foreach (Control button in _panelKeyboard.Controls)
                {
                    _panelKeyboard.Controls.Remove(button);
                    button.Dispose();
                }

                _panelKeyboard.Dispose();
            }
        }

        private void CreatePanel()
        {
            // Create the TableLayoutPanel
            _panelKeyboard = new TableLayoutPanel();
            _panelKeyboard.Name = "panelKeyboard";
            _panelKeyboard.Dock = DockStyle.Fill;
            _panelKeyboard.Location = new System.Drawing.Point(0, 0);
            _panelKeyboard.TabIndex = 0;
            _panelKeyboard.BackColor = this.BackColor;
            this.Controls.Add(_panelKeyboard);

            // Prepare rows
            _panelKeyboard.RowCount = _KeyboardRows;
            Single height = Convert.ToSingle(100) / Convert.ToSingle(_KeyboardRows);
            for (int row = 0; row < _KeyboardRows; row++)
            {
                _panelKeyboard.RowStyles.Add(new RowStyle(SizeType.Percent, height));
            }

            // Prepare columns
            _panelKeyboard.ColumnCount = _KeyboardColumns;
            Single width = Convert.ToSingle(100) / Convert.ToSingle(_KeyboardColumns);
            for (int column = 0; column < _KeyboardColumns; column++)
            {
                _panelKeyboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
            }
        }

        private void CreateButtons()
        {
            // Rows
            for (int row = 0; row < _KeyboardRows; row++)
            {
                Button previousButton = null;
                int previousColumnSpan = 1;

                // Columns
                for (int column = 0; column < _KeyboardColumns; column++)
                {
                    if (previousButton != null && previousButton.Tag.ToString() == _KeyboardKeys[row, column])
                    {
                        // Span adjacent equal keys
                        previousColumnSpan++;
                        _panelKeyboard.SetColumnSpan(previousButton, previousColumnSpan);
                    }
                    else
                    {
                        // Button creation
                        Button button = new Button();
                        button.Name = string.Format("{0}{1}{2}{3}{4}", KeyButtonNamePrefix, KeyButtonNameRowPrefix, row, KeyButtonNameColumnPrefix, column);
                        button.Text = TranslateKeyText(_KeyboardKeys[row, column]);
                        button.Tag = _KeyboardKeys[row, column];
                        _panelKeyboard.Controls.Add(button, column, row);
                        button.Dock = DockStyle.Fill;

                        // Appearance
                        button.BackColor = _KeyBackColor;
                        button.ForeColor = this.ForeColor;
                        button.Font = this.Font;

                        // Events
                        button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KeyMouseUp);

                        // Variables for key span
                        previousButton = button;
                        previousColumnSpan = 1;
                    }
                }
            }
        }

        private string TranslateKeyText(string keyText)
        {
            switch (keyText)
            {
                case ConstantsKeys.DELETE:
                    return CSOnScreenKeyboard.Properties.Resources.KEYTEXT_DELETE_0;
                case ConstantsKeys.BACKSPACE:
                    return CSOnScreenKeyboard.Properties.Resources.KEYTEXT_BACKSPACE_0;
                case ConstantsKeys.UserDefinedCLEAR:
                    return CSOnScreenKeyboard.Properties.Resources.KEYTEXT_CLEAR_0;
                case ConstantsKeys.UserDefinedSPACE:
                    return "";
                default:
                    return keyText;
            }
        }

        #endregion

        #region Events

        public event EventHandler KeyboardLayoutChanged;

        private void KeyboardLayoutChange()
        {
            switch (_KeyboardLayout)
            {
                case KeyboardLayoutEnums.AlphanumericSpanish:
                    _KeyboardRows = KeyboardRowsAlphanumericES;
                    _KeyboardColumns = KeyboardColumnsAlphanumericES;
                    _KeyboardKeys = KeyboardKeysAlphanumericES;
                    break;
                case KeyboardLayoutEnums.NumericPhone:
                    _KeyboardRows = KeyboardRowsNumericPhone;
                    _KeyboardColumns = KeyboardColumnsNumericPhone;
                    _KeyboardKeys = KeyboardKeysNumericPhone;
                    break;
                case KeyboardLayoutEnums.NumericCalculator:
                    _KeyboardRows = KeyboardRowsNumericCalculator;
                    _KeyboardColumns = KeyboardColumnsNumericCalculator;
                    _KeyboardKeys = KeyboardKeysNumericCalculator;
                    break;
                default:
                    _KeyboardRows = 0;
                    _KeyboardColumns = 0;
                    _KeyboardKeys = new string[,] { { } };
                    break;
            }
            CreateKeyboard();
            // this.KeyboardLayoutChanged(this, new EventArgs());
        }

        private void KeyMouseUp(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            SendKeyOrder(button.Tag.ToString());

            if (_DestinationTextBox != null && _DestinationTextBox.Enabled && !_DestinationTextBox.ReadOnly)
            {
                _DestinationTextBox.Focus();
            }
            else
            {
                this.Parent.Focus();
            }
            this.OnClick(e);
        }

        private void SendKeyOrder(string keysValue)
        {
            if (_DestinationTextBox != null)
            {
                if (_DestinationTextBox.Enabled && !_DestinationTextBox.ReadOnly)
                {
                    // Textbox is enabled for typing

                    // ensure that the textbox control has the focus
                    _DestinationTextBox.Focus();
                    // ensure that the insertion point is at the end
                    _DestinationTextBox.SelectionStart = _DestinationTextBox.TextLength;
                    // send the key pressed
                    SendKeys.Send(keysValue);
                }
                else
                {
                    // textbox is disabled or in read-only mode
                    switch (keysValue)
                    {
                        case CardonerSistemas.ConstantsKeys.BACKSPACE:
                            if (_DestinationTextBox.TextLength > 0)
                            {
                                _DestinationTextBox.Text = _DestinationTextBox.Text.Remove(_DestinationTextBox.TextLength - 1);
                            }
                            break;

                        case CardonerSistemas.ConstantsKeys.UserDefinedCLEAR:
                            _DestinationTextBox.Text = "";
                            break;

                        default:
                            if (_DestinationTextBox.TextLength < _DestinationTextBox.MaxLength)
                            {
                                _DestinationTextBox.Text += keysValue;
                            }
                            break;
                    }
                }
            }
        }

        private void FontChangedEvent(object sender, EventArgs e)
        {
            _panelKeyboard.SuspendLayout();

            foreach (Control button in _panelKeyboard.Controls)
            {
                button.Font = this.Font;
            }

            _panelKeyboard.ResumeLayout();
        }

        private void ForeColorChangedEvent(object sender, EventArgs e)
        {
            _panelKeyboard.SuspendLayout();

            foreach (Control button in _panelKeyboard.Controls)
            {
                button.ForeColor = this.ForeColor;
            }

            _panelKeyboard.ResumeLayout();
        }

        #endregion

    }
}
