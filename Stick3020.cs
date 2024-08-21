using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class Stick3020: Form
{
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new Stick3020());
	}

	private const int fontSize = 16, gridSize = 24;

	private static Size GetSizeOnGrid(float width, float height)
	{
		return new Size((int)(gridSize * width), (int)(gridSize * height));
	}

	private static Point GetPointOnGrid(float x, float y)
	{
		return new Point((int)(gridSize * x), (int)(gridSize * y));
	}

	private static T CreateControl<T>(Control parent, float x, float y, float width, float height)
	where T: Control, new()
	{
		T control = new T();
		control.Location = GetPointOnGrid(x, y);
		control.Size = GetSizeOnGrid(width, height);
		if (parent != null) parent.Controls.Add(control);
		return control;
	}

	private UIText uiText = new JapaneseUIText();

	private MenuStrip mainMenuStrip;
	private ToolStripMenuItem languageMenuItem;
	private ToolStripMenuItem languageJapaneseMenuItem, languageEnglishMenuItem;

	private Panel mainPanel;

	private GroupBox controllerSelectGroupBox;
	private RadioButton[] controllerSelectRadioButtons = new RadioButton[4];

	private GroupBox brakeSelectGroupBox;
	private RadioButton brakeAutoRadioButton;
	private RadioButton brake6StepRadioButton;
	private RadioButton brake7StepRadioButton;
	private RadioButton brake8StepRadioButton;
	private RadioButton brakeAnalogRadioButton;

	private GroupBox operationStatusGroupBox;
	private Label powerTitleLabel, powerNotchLabel, powerDepthLabel;
	private Label brakeTitleLabel, brakeNotchLabel, brakeDepthLabel;
	private Label leftTriggerTitleLabel, leftTriggerDepthLabel;
	private Label rightTriggerTitleLabel, rightTriggerDepthLabel;

	private GroupBox inputConfigurationGroupBox;
	private Label stickThresholdTitleLabel, stickThresholdUnitLabel;
	private NumericUpDown stickThresholdNumericUpDown;
	private Label trigterShallowThresholdTitleLabel, trigterShallowThresholdUnitLabel;
	private NumericUpDown trigterShallowThresholdNumericUpDown;
	private Label trigterDeepThresholdTitleLabel, trigterDeepThresholdUnitLabel;
	private NumericUpDown trigterDeepThresholdNumericUpDown;
	private Label notchHysteresisTitleLabel, notchHysteresisUnitLabel;
	private NumericUpDown notchHysteresisNumericUpDown;
	private CheckBox enableDeadmanCheckBox;

	public Stick3020()
	{
		this.Font = new Font("MS UI Gothic", fontSize, GraphicsUnit.Pixel);
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.MaximizeBox = false;
		this.Text = "Stick 3020";
		SuspendLayout();

		mainMenuStrip = new MenuStrip();
		languageMenuItem = new ToolStripMenuItem();
		languageMenuItem.Text = "言語 / Language (&L)";
		languageJapaneseMenuItem = new ToolStripMenuItem();
		languageJapaneseMenuItem.Text = "日本語 (&J)";
		languageJapaneseMenuItem.Checked = true;
		languageEnglishMenuItem = new ToolStripMenuItem();
		languageEnglishMenuItem.Text = "English (&E)";
		languageMenuItem.DropDownItems.Add(languageJapaneseMenuItem);
		languageMenuItem.DropDownItems.Add(languageEnglishMenuItem);
		mainMenuStrip.Items.Add(languageMenuItem);
		this.Controls.Add(mainMenuStrip);
		this.MainMenuStrip = mainMenuStrip;

		mainPanel = CreateControl<Panel>(this, 0, 0, 23.5f, 13.5f);
		mainPanel.Top += mainMenuStrip.Height;
		this.ClientSize = mainPanel.Size;
		this.Height += mainMenuStrip.Height;
		mainPanel.SuspendLayout();

		controllerSelectGroupBox = CreateControl<GroupBox>(mainPanel, 0.5f, 0.5f, 10, 5.5f);
		controllerSelectGroupBox.SuspendLayout();
		for (int i = 0; i < 4; i++)
		{
			controllerSelectRadioButtons[i] = CreateControl<RadioButton>(
				controllerSelectGroupBox, 0.5f, i + 1, 9, 1
			);
		}
		controllerSelectGroupBox.ResumeLayout();

		brakeSelectGroupBox = CreateControl<GroupBox>(mainPanel, 0.5f, 6.5f, 10, 6.5f);
		brakeSelectGroupBox.SuspendLayout();
		brakeAutoRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 1, 9, 1);
		brake6StepRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 2, 9, 1);
		brake7StepRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 3, 9, 1);
		brake8StepRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 4, 9, 1);
		brakeAnalogRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 5, 9, 1);
		brakeSelectGroupBox.ResumeLayout();

		operationStatusGroupBox = CreateControl<GroupBox>(mainPanel, 11, 0.5f, 12, 5.5f);
		operationStatusGroupBox.SuspendLayout();
		powerTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 1, 5, 1);
		powerNotchLabel = CreateControl<Label>(operationStatusGroupBox, 5.5f, 1, 4, 1);
		powerNotchLabel.Text = "-";
		powerDepthLabel = CreateControl<Label>(operationStatusGroupBox, 9.5f, 1, 2, 1);
		powerDepthLabel.Text = "-";
		brakeTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 2, 5, 1);
		brakeNotchLabel = CreateControl<Label>(operationStatusGroupBox, 5.5f, 2, 4, 1);
		brakeNotchLabel.Text = "-";
		brakeDepthLabel = CreateControl<Label>(operationStatusGroupBox, 9.5f, 2, 2, 1);
		brakeDepthLabel.TextAlign = ContentAlignment.TopRight;
		brakeDepthLabel.Text = "-";
		leftTriggerTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 3, 9, 1);
		leftTriggerDepthLabel = CreateControl<Label>(operationStatusGroupBox, 9.5f, 3, 2, 1);
		leftTriggerDepthLabel.TextAlign = ContentAlignment.TopRight;
		leftTriggerDepthLabel.Text = "-";
		rightTriggerTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 4, 9, 1);
		rightTriggerDepthLabel = CreateControl<Label>(operationStatusGroupBox, 9.5f, 4, 2, 1);
		rightTriggerDepthLabel.TextAlign = ContentAlignment.TopRight;
		rightTriggerDepthLabel.Text = "-";
		operationStatusGroupBox.ResumeLayout();

		inputConfigurationGroupBox = CreateControl<GroupBox>(mainPanel, 11, 6.5f, 12, 6.5f);
		inputConfigurationGroupBox.SuspendLayout();
		stickThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 1, 6, 1);
		stickThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 6.5f, 1, 4, 1);
		stickThresholdNumericUpDown.Maximum = 100;
		stickThresholdNumericUpDown.Minimum = 0;
		stickThresholdNumericUpDown.Value = 70;
		stickThresholdNumericUpDown.Increment = 1;
		stickThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 10.5f, 1, 1, 1);
		stickThresholdUnitLabel.Text = "%";
		trigterShallowThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 2, 6, 1);
		trigterShallowThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 6.5f, 2, 4, 1);
		trigterShallowThresholdNumericUpDown.Maximum = 80;
		trigterShallowThresholdNumericUpDown.Minimum = 0;
		trigterShallowThresholdNumericUpDown.Value = 30;
		trigterShallowThresholdNumericUpDown.Increment = 1;
		trigterShallowThresholdNumericUpDown.ValueChanged += TriggerThresholdChangeHandler;
		trigterShallowThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 10.5f, 2, 1, 1);
		trigterShallowThresholdUnitLabel.Text = "%";
		trigterDeepThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 3, 6, 1);
		trigterDeepThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 6.5f, 3, 4, 1);
		trigterDeepThresholdNumericUpDown.Maximum = 100;
		trigterDeepThresholdNumericUpDown.Minimum = 30;
		trigterDeepThresholdNumericUpDown.Value = 80;
		trigterDeepThresholdNumericUpDown.Increment = 1;
		trigterDeepThresholdNumericUpDown.ValueChanged += TriggerThresholdChangeHandler;
		trigterDeepThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 10.5f, 3, 1, 1);
		trigterDeepThresholdUnitLabel.Text = "%";
		notchHysteresisTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 4, 6, 1);
		notchHysteresisNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 6.5f, 4, 4, 1);
		notchHysteresisNumericUpDown.Maximum = 100;
		notchHysteresisNumericUpDown.Minimum = 0;
		notchHysteresisNumericUpDown.Value = 20;
		notchHysteresisNumericUpDown.Increment = 1;
		notchHysteresisUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 10.5f, 4, 1, 1);
		notchHysteresisUnitLabel.Text = "%";
		enableDeadmanCheckBox = CreateControl<CheckBox>(inputConfigurationGroupBox, 0.5f, 5, 11, 1);
		inputConfigurationGroupBox.ResumeLayout();

		mainPanel.ResumeLayout();
		ResumeLayout();

		languageJapaneseMenuItem.Click += LanguageMenuClickHandler;
		languageEnglishMenuItem.Click += LanguageMenuClickHandler;
		SetControlTexts();

		Load += LoadHandler;
	}

	private void SetControllerStatusTexts()
	{
		for (int i = 0; i < 4; i++)
		{
			controllerSelectRadioButtons[i].Text = controllerConnected[i] ?
				i.ToString() :
				string.Format("{0} ({1})", i, uiText.ControllerDisconnected);
		}
	}

	private void SetControlTexts()
	{
		controllerSelectGroupBox.Text = uiText.ControllerSelect;
		SetControllerStatusTexts();

		brakeSelectGroupBox.Text = uiText.BrakeSelect;
		brakeAutoRadioButton.Text = string.Format("{0}：{1}", uiText.BrakeAuto, uiText.Brake6StepForAuto);
		brake6StepRadioButton.Text = uiText.Brake6Step;
		brake7StepRadioButton.Text = uiText.Brake7Step;
		brake8StepRadioButton.Text = uiText.Brake8Step;
		brakeAnalogRadioButton.Text = uiText.BrakeAnalog;

		operationStatusGroupBox.Text = uiText.OperationStatus;
		powerTitleLabel.Text = uiText.OperationPower;
		powerDepthLabel.TextAlign = ContentAlignment.TopRight;
		brakeTitleLabel.Text = uiText.OperationBrake;
		leftTriggerTitleLabel.Text = uiText.OperationLeftTrigger;
		rightTriggerTitleLabel.Text = uiText.OperationRightTrigger;

		inputConfigurationGroupBox.Text = uiText.InputConfiguration;
		stickThresholdTitleLabel.Text = uiText.InputStickThreshold;
		trigterShallowThresholdTitleLabel.Text = uiText.InputTriggerThresholdShallow;
		trigterDeepThresholdTitleLabel.Text = uiText.InputTriggerThresholdDeep;
		notchHysteresisTitleLabel.Text = uiText.InputNotchHysteresis;
		enableDeadmanCheckBox.Text = uiText.InputDeadman;
	}

	private void LanguageMenuClickHandler(object sender, EventArgs e)
	{
		if (sender == languageJapaneseMenuItem)
		{
			languageJapaneseMenuItem.Checked = true;
			languageEnglishMenuItem.Checked = false;
			uiText = new JapaneseUIText();
		}
		else if (sender == languageEnglishMenuItem)
		{
			languageJapaneseMenuItem.Checked = false;
			languageEnglishMenuItem.Checked = true;
			uiText = new EnglishUIText();
		}
		SetControlTexts();
	}

	private void TriggerThresholdChangeHandler(object sender, EventArgs e)
	{
		trigterDeepThresholdNumericUpDown.Minimum = trigterShallowThresholdNumericUpDown.Value;
		trigterShallowThresholdNumericUpDown.Maximum = trigterDeepThresholdNumericUpDown.Value;
	}

	private Timer inputTimer, controllerCheckTimer;
	private bool[] controllerConnected = new bool[4];

	private void LoadHandler(object sender, EventArgs e)
	{
		for (int i = 0; i < 4; i++)
		{
			controllerConnected[i] = XInputReader.Read(i).HasValue;
		}
		SetControllerStatusTexts();
		inputTimer = new Timer();
		inputTimer.Interval = 15;
		inputTimer.Tick += InputTimerTickHandler;
		inputTimer.Start();
		controllerCheckTimer = new Timer();
		controllerCheckTimer.Interval = 1000;
		controllerCheckTimer.Tick += ControllerCheckTimerTickHandler;
		controllerCheckTimer.Start();
	}

	private void InputTimerTickHandler(object sender, EventArgs e)
	{
		XInputReader.XInputGamepad? inputRaw = null;
		for (int i = 0; i < 4; i++)
		{
			if (controllerSelectRadioButtons[i].Checked && controllerConnected[i])
			{
				inputRaw = XInputReader.Read(i);
				if (inputRaw.HasValue != controllerConnected[i])
				{
					controllerConnected[i] = inputRaw.HasValue;
					SetControllerStatusTexts();
				}
				break;
			}
		}
		int inputButtons = 0;
		// 力行：左が角度0、時計回り
		// ブレーキ：上が角度0、反時計回り
		int accelPower = -1,  brakePower = -1, leftTriggerPower = -1, rightTriggerPower = -1;
		double accelAngle = 0, brakeAngle = 0;
		if (inputRaw.HasValue)
		{
			inputButtons = inputRaw.Value.wButtons;
			double lx = inputRaw.Value.sThumbLX / 32767.0;
			double ly = inputRaw.Value.sThumbLY / 32767.0;
			double rx = inputRaw.Value.sThumbRX / 32767.0;
			double ry = inputRaw.Value.sThumbRY / 32767.0;
			accelPower = (int)Math.Min(Math.Truncate(Math.Sqrt(lx * lx + ly * ly) * 100), 100);
			accelAngle = Math.Atan2(-ly, lx) + Math.PI;
			brakePower = (int)Math.Min(Math.Truncate(Math.Sqrt(rx * rx + ry * ry) * 100), 100);
			brakeAngle = Math.Atan2(rx, -ry) + Math.PI;
			leftTriggerPower = inputRaw.Value.bLeftTrigger * 100 / 255;
			rightTriggerPower = inputRaw.Value.bRightTrigger * 100 / 255;
			powerDepthLabel.Text = string.Format("{0}%", accelPower);
			brakeDepthLabel.Text = string.Format("{0}%", brakePower);
			leftTriggerDepthLabel.Text = string.Format("{0}%", leftTriggerPower);
			rightTriggerDepthLabel.Text = string.Format("{0}%", rightTriggerPower);
		}
		else
		{
			powerDepthLabel.Text = "-";
			brakeDepthLabel.Text = "-";
			leftTriggerDepthLabel.Text = "-";
			rightTriggerDepthLabel.Text = "-";
		}
		if (accelPower >= stickThresholdNumericUpDown.Value)
		{
			powerNotchLabel.Text = ((int)(accelAngle / (Math.PI * 2) * 360)).ToString();
		}
		else
		{
			powerNotchLabel.Text = "-";
		}
		if (brakePower >= stickThresholdNumericUpDown.Value)
		{
			brakeNotchLabel.Text = ((int)(brakeAngle / (Math.PI * 2) * 360)).ToString();
		}
		else
		{
			brakeNotchLabel.Text = "-";
		}
	}

	private void ControllerCheckTimerTickHandler(object sender, EventArgs e)
	{
		bool valueChanged = false;
		for (int i = 0; i < 4; i++)
		{
			if (controllerConnected[i])
			{
				if (!controllerSelectRadioButtons[i].Checked)
				{
					controllerConnected[i] = XInputReader.Read(i).HasValue;
					if (!controllerConnected[i]) valueChanged = true;
				}
			}
			else
			{
				controllerConnected[i] = XInputReader.Read(i).HasValue;
				if (controllerConnected[i]) valueChanged = true;
			}
		}
		if (valueChanged) SetControllerStatusTexts();
	}
}
