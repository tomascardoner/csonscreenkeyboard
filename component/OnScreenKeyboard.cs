using System.Windows.Forms;

namespace CardonerSistemas
{
    public partial class OnScreenKeyboard : UserControl
    {

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
            { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "DEL" },
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
        private TextBox _DestinationTextBox;

        public KeyboardLayoutEnums KeyboardLayout
        {
            get => _KeyboardLayout;
            set
            {
                _KeyboardLayout = value;
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
            }
        }

        public TextBox DestinationTextBox { get => _DestinationTextBox; set => _DestinationTextBox = value; }

        #endregion

        #region Initialization

        public ControlsOnScreenKeyboard()
        {
            InitializeComponent();
            KeyboardLayout = KeyboardLayoutEnums.AlphanumericSpanish;
        }

        #endregion

        #region Layout creation

        private void CreateKeyboard()
        {
            InitializeKeyboard();
            CreatePanel();
            CreateButtons();
        }

        private void InitializeKeyboard()
        {
            // Clean old keyboard keys
            foreach (Control button in panelKeyboard.Controls)
            {
                panelKeyboard.Controls.Remove(button);
                button.Dispose();
            }
        }

        private void CreatePanel()
        {
            panelKeyboard.RowCount = _KeyboardRows;
            panelKeyboard.ColumnCount = _KeyboardColumns;
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
                    if (previousButton != null && previousButton.Text == _KeyboardKeys[row, column])
                    {
                        previousColumnSpan++;
                        panelKeyboard.SetColumnSpan(previousButton, previousColumnSpan);
                    }
                    else
                    {
                        Button button = new Button();
                        button.Name = string.Format("{0}{1}{2}{3}{4}", KeyButtonNamePrefix, row, KeyButtonNameRowPrefix , column, KeyButtonNameColumnPrefix);
                        button.Text = _KeyboardKeys[row, column];
                        panelKeyboard.Controls.Add(button, column, row);
                        button.Dock = DockStyle.Fill;
                        previousButton = button;
                        previousColumnSpan = 1;
                    }
                }
            }
        }

        #endregion

        #region Events

        private void KeyMouseUp(object sender, MouseEventArgs e)
        {
            ConvertButtonClickedToKeyOrder(sender as Button);

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

        private void ConvertButtonClickedToKeyOrder(Button buttonKey)
        {
            string keyNameSuffix;
            char keyCharValue;

            keyNameSuffix = buttonKey.Name.Substring(KeyButtonNamePrefix.Length);
            switch (keyNameSuffix)
            {
                case ConstantsKeys.BACKSPACE:
                    SendKeyOrder(CardonerSistemas.ConstantsKeys.BACKSPACE);
                    break;

                case ConstantsKeys.UserDefinedCLEAR:
                    SendKeyOrder(CardonerSistemas.ConstantsKeys.UserDefinedCLEAR);
                    break;

                default:
                    SendKeyOrder(keyNameSuffix);
                    break;
            }
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

                        case CardonerSistemas.ConstantsKeys.ESC:
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

        #endregion

    }
}
