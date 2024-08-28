using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TrainCrew;

class Stick3020: Form
{
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new Stick3020());
	}

	private readonly static string LanguageValueName = "language";
	private readonly static string LanguageJapaneseData = "Japanese";
	private readonly static string LanguageEnglishData = "English";

	private readonly static string ControllerSelectValueName = "controller";

	private readonly static string BrakeSelectValueName = "brake";
	private readonly static string BrakeSelectAutoData = "Auto";
	private readonly static string BrakeSelect6StepData = "6-Step";
	private readonly static string BrakeSelect7StepData = "7-Step";
	private readonly static string BrakeSelect8StepData = "8-Step";
	private readonly static string BrakeSelectAnalogData = "Analog";

	private readonly static string StickThresholdValueName = "stickThreshold";
	private readonly static string TriggerShallowThresholdValueName = "triggerShallowThreshold";
	private readonly static string TriggerDeepThresholdValueName = "triggerDeepThreshold";
	private readonly static string NotchHysteresisValueName = "notchHysteresis";
	private readonly static string DeadmanValueName = "deadman";

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
	private Label triggerShallowThresholdTitleLabel, triggerShallowThresholdUnitLabel;
	private NumericUpDown triggerShallowThresholdNumericUpDown;
	private Label triggerDeepThresholdTitleLabel, triggerDeepThresholdUnitLabel;
	private NumericUpDown triggerDeepThresholdNumericUpDown;
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
		triggerShallowThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 2, 6, 1);
		triggerShallowThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 6.5f, 2, 4, 1);
		triggerShallowThresholdNumericUpDown.Maximum = 100;
		triggerShallowThresholdNumericUpDown.Minimum = 0;
		triggerShallowThresholdNumericUpDown.Value = 30;
		triggerShallowThresholdNumericUpDown.Increment = 1;
		triggerShallowThresholdNumericUpDown.ValueChanged += TriggerThresholdChangeHandler;
		triggerShallowThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 10.5f, 2, 1, 1);
		triggerShallowThresholdUnitLabel.Text = "%";
		triggerDeepThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 3, 6, 1);
		triggerDeepThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 6.5f, 3, 4, 1);
		triggerDeepThresholdNumericUpDown.Maximum = 100;
		triggerDeepThresholdNumericUpDown.Minimum = 0;
		triggerDeepThresholdNumericUpDown.Value = 80;
		triggerDeepThresholdNumericUpDown.Increment = 1;
		triggerDeepThresholdNumericUpDown.ValueChanged += TriggerThresholdChangeHandler;
		triggerDeepThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 10.5f, 3, 1, 1);
		triggerDeepThresholdUnitLabel.Text = "%";
		notchHysteresisTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 4, 6, 1);
		notchHysteresisNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 6.5f, 4, 4, 1);
		notchHysteresisNumericUpDown.Maximum = 100;
		notchHysteresisNumericUpDown.Minimum = 0;
		notchHysteresisNumericUpDown.Value = 20;
		notchHysteresisNumericUpDown.Increment = 1;
		notchHysteresisUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 10.5f, 4, 1, 1);
		notchHysteresisUnitLabel.Text = "%";
		enableDeadmanCheckBox = CreateControl<CheckBox>(inputConfigurationGroupBox, 0.5f, 5, 11, 1);
		enableDeadmanCheckBox.Checked = true;
		inputConfigurationGroupBox.ResumeLayout();

		mainPanel.ResumeLayout();
		ResumeLayout();

		languageJapaneseMenuItem.Click += LanguageMenuClickHandler;
		languageEnglishMenuItem.Click += LanguageMenuClickHandler;
		SetControlTexts();

		Load += LoadHandler;
		FormClosed += FormClosedHandler;
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
		triggerShallowThresholdTitleLabel.Text = uiText.InputTriggerThresholdShallow;
		triggerDeepThresholdTitleLabel.Text = uiText.InputTriggerThresholdDeep;
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
		triggerDeepThresholdNumericUpDown.Minimum = triggerShallowThresholdNumericUpDown.Value;
		triggerShallowThresholdNumericUpDown.Maximum = triggerDeepThresholdNumericUpDown.Value;
	}

	private enum BrakeKind
	{
		Brake6Steps,
		Brake7Steps,
		Brake8Steps,
		BrakeAnalog,
	}

	private bool trainCrewEnabled = false;

	private Timer inputTimer, controllerCheckTimer;
	private bool[] controllerConnected = new bool[4];

	private BrakeKind prevBrakeKind = BrakeKind.Brake6Steps;
	private int? prevPowerNotch = null;
	private int? prevBrakeNotch = null;
	private float? prevBrakePressure = null;
	private bool prevDeadmanIsActive = false; // active = 発報中 (ブレーキをかける状態)
	private bool prevBuzzer = false;

	private void LoadHandler(object sender, EventArgs e)
	{
		RegistryIO regIO = RegistryIO.OpenForRead();
		if (regIO != null)
		{
			string language = regIO.GetStringValue(LanguageValueName);
			if (LanguageEnglishData.Equals(language))
			{
				languageEnglishMenuItem.Checked = true;
				uiText = new EnglishUIText();
			}
			else
			{
				languageJapaneseMenuItem.Checked = true;
			}

			int? controller = regIO.GetIntValue(ControllerSelectValueName);
			if (controller.HasValue && 0 <= controller.Value && controller.Value < 4)
			{
				controllerSelectRadioButtons[controller.Value].Checked = true;
			}
			else
			{
				controllerSelectRadioButtons[0].Checked = true;
			}

			string brake = regIO.GetStringValue(BrakeSelectValueName);
			if (BrakeSelect6StepData.Equals(brake))
			{
				brake6StepRadioButton.Checked = true;
				prevBrakeKind = BrakeKind.Brake6Steps;
			}
			else if (BrakeSelect7StepData.Equals(brake))
			{
				brake7StepRadioButton.Checked = true;
				prevBrakeKind = BrakeKind.Brake7Steps;
			}
			else if (BrakeSelect8StepData.Equals(brake))
			{
				brake8StepRadioButton.Checked = true;
				prevBrakeKind = BrakeKind.Brake8Steps;
			}
			else if (BrakeSelectAnalogData.Equals(brake))
			{
				brakeAnalogRadioButton.Checked = true;
				prevBrakeKind = BrakeKind.BrakeAnalog;
			}
			else
			{
				brakeAutoRadioButton.Checked = true;
			}

			int? stickRaw = regIO.GetIntValue(StickThresholdValueName);
			if (stickRaw.HasValue)
			{
				stickThresholdNumericUpDown.Value = Math.Min(Math.Max(stickRaw.Value, 0), 100);
			}
			int? triggerShallowRaw = regIO.GetIntValue(TriggerShallowThresholdValueName);
			if (triggerShallowRaw.HasValue)
			{
				triggerShallowThresholdNumericUpDown.Value = Math.Min(Math.Max(triggerShallowRaw.Value, 0), 100);
			}
			int? triggerDeepRaw = regIO.GetIntValue(TriggerDeepThresholdValueName);
			if (triggerDeepRaw.HasValue)
			{
				triggerDeepThresholdNumericUpDown.Value = Math.Min(Math.Max(triggerDeepRaw.Value, triggerShallowThresholdNumericUpDown.Value), 100);
			}
			int? notchHysteresisRaw = regIO.GetIntValue(NotchHysteresisValueName);
			if (notchHysteresisRaw.HasValue)
			{
				notchHysteresisNumericUpDown.Value = Math.Min(Math.Max(notchHysteresisRaw.Value, 0), 100);
			}
			int? deadmanRaw = regIO.GetIntValue(DeadmanValueName);
			if (deadmanRaw.HasValue)
			{
				enableDeadmanCheckBox.Checked = deadmanRaw.Value != 0;
			}

			regIO.Close();
		}
		else
		{
			languageJapaneseMenuItem.Checked = true;
			controllerSelectRadioButtons[0].Checked = true;
			brakeAutoRadioButton.Checked = true;
		}
		TriggerThresholdChangeHandler(null, null);

		for (int i = 0; i < 4; i++)
		{
			controllerConnected[i] = XInputReader.Read(i).HasValue;
		}
		SetControlTexts();

		TrainCrewInput.Init();
		trainCrewEnabled = true;

		inputTimer = new Timer();
		inputTimer.Interval = 15;
		inputTimer.Tick += InputTimerTickHandler;
		inputTimer.Start();
		controllerCheckTimer = new Timer();
		controllerCheckTimer.Interval = 1000;
		controllerCheckTimer.Tick += ControllerCheckTimerTickHandler;
		controllerCheckTimer.Start();
	}

	private void FormClosedHandler(object sender, EventArgs e)
	{
		trainCrewEnabled = false;
		TrainCrewInput.SetDeadman(0, true);
		TrainCrewInput.SetDeadman(1, true);
		TrainCrewInput.SetButton(InputAction.Buzzer, false);
		TrainCrewInput.Dispose();

		RegistryIO regIO = RegistryIO.OpenForWrite();
		if (regIO != null)
		{
			if (languageJapaneseMenuItem.Checked)
			{
				regIO.SetValue(LanguageValueName, LanguageJapaneseData);
			}
			else if (languageEnglishMenuItem.Checked)
			{
				regIO.SetValue(LanguageValueName, LanguageEnglishData);
			}

			for (int i = 0; i < 4; i++)
			{
				if (controllerSelectRadioButtons[i].Checked)
				{
					regIO.SetValue(ControllerSelectValueName, i);
					break;
				}
			}

			if (brakeAutoRadioButton.Checked)
			{
				regIO.SetValue(BrakeSelectValueName, BrakeSelectAutoData);
			}
			else if (brake6StepRadioButton.Checked)
			{
				regIO.SetValue(BrakeSelectValueName, BrakeSelect6StepData);
			}
			else if (brake7StepRadioButton.Checked)
			{
				regIO.SetValue(BrakeSelectValueName, BrakeSelect7StepData);
			}
			else if (brake8StepRadioButton.Checked)
			{
				regIO.SetValue(BrakeSelectValueName, BrakeSelect8StepData);
			}
			else if (brakeAnalogRadioButton.Checked)
			{
				regIO.SetValue(BrakeSelectValueName, BrakeSelectAnalogData);
			}

			regIO.SetValue(StickThresholdValueName, (int)stickThresholdNumericUpDown.Value);
			regIO.SetValue(TriggerShallowThresholdValueName, (int)triggerShallowThresholdNumericUpDown.Value);
			regIO.SetValue(TriggerDeepThresholdValueName, (int)triggerDeepThresholdNumericUpDown.Value);
			regIO.SetValue(NotchHysteresisValueName, (int)notchHysteresisNumericUpDown.Value);
			regIO.SetValue(DeadmanValueName, enableDeadmanCheckBox.Checked ? 1 : 0);

			regIO.Close();
		}
	}

	private void InputTimerTickHandler(object sender, EventArgs e)
	{
		if (!trainCrewEnabled) return;

		BrakeKind currentBrakeKind, autoBrakeKind;
		int? currentPowerNotch = null;
		int? currentBrakeNotch = null;
		float? currentBrakePressure = null;
		bool currentDeadmanIsActive = false;
		bool currentBuzzer = false;

		TrainState trainState = TrainCrewInput.GetTrainState();
		string carModel = trainState.CarStates.Count > 0 ? trainState.CarStates[0].CarModel : "";
		if ("4000".Equals(carModel) || "4000R".Equals(carModel))
		{
			autoBrakeKind = BrakeKind.Brake7Steps;
			brakeAutoRadioButton.Text = string.Format("{0}：{1}", uiText.BrakeAuto, uiText.Brake7StepForAuto);
		}
		else if ("3020".Equals(carModel))
		{
			autoBrakeKind = BrakeKind.BrakeAnalog;
			brakeAutoRadioButton.Text = string.Format("{0}：{1}", uiText.BrakeAuto, uiText.BrakeAnalogForAuto);
		}
		else
		{
			autoBrakeKind = BrakeKind.Brake6Steps;
			brakeAutoRadioButton.Text = string.Format("{0}：{1}", uiText.BrakeAuto, uiText.Brake6StepForAuto);
		}

		if (brake6StepRadioButton.Checked)
		{
			currentBrakeKind = BrakeKind.Brake6Steps;
		}
		else if (brake7StepRadioButton.Checked)
		{
			currentBrakeKind = BrakeKind.Brake7Steps;
		}
		else if (brake8StepRadioButton.Checked)
		{
			currentBrakeKind = BrakeKind.Brake8Steps;
		}
		else if (brakeAnalogRadioButton.Checked)
		{
			currentBrakeKind = BrakeKind.BrakeAnalog;
		}
		else
		{
			currentBrakeKind = autoBrakeKind;
		}

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
			// 135度 (0.75*PI) = N
			// 抑速：そこから30度 (1.0/6.0*PI) ずつマイナス
			// P：そこから30度ずつプラス
			// -3 -2 -1 0 1 2 3 4 5 ← ノッチ
			//   0  1  2 3 4 5 6 7  ← 閾値配列
			double[] thresholds = new double[8];
			for (int i = 0; i < 8; i++)
			{
				thresholds[i] = (0.75 + (i * 2 - 5) / 12.0) * Math.PI;
			}
			if (prevBrakeKind == currentBrakeKind && prevPowerNotch.HasValue)
			{
				double hysteresisDelta = 1.0 / 12.0 * Math.PI * (double)(notchHysteresisNumericUpDown.Value / 100);
				for (int i = 0; i < 8; i++)
				{
					if (i <= prevPowerNotch.Value + 2)
					{
						thresholds[i] -= hysteresisDelta;
					}
					else
					{
						thresholds[i] += hysteresisDelta;
					}
				}
			}
			// 閾値に基づいてノッチを読み取る
			currentPowerNotch = 5;
			for (int i = 0; i < 8; i++)
			{
				if (accelAngle < thresholds[i])
				{
					currentPowerNotch = i - 3;
					break;
				}
			}
			// 読み取ったノッチを表示する
			if (currentPowerNotch.Value < 0)
			{
				if (currentBrakeKind == BrakeKind.Brake6Steps)
				{
					powerNotchLabel.Text = uiText.OperationPowerHoldingWithoutNumber;
				}
				else
				{
					powerNotchLabel.Text = string.Format("{0}{1}", uiText.OperationPowerHolding, -currentPowerNotch.Value);
				}
			}
			else if (currentPowerNotch.Value == 0)
			{
				powerNotchLabel.Text = "N";
			}
			else
			{
				powerNotchLabel.Text = string.Format("P{0}", currentPowerNotch.Value);
			}
		}
		else
		{
			powerNotchLabel.Text = "-";
		}
		if (brakePower >= stickThresholdNumericUpDown.Value)
		{
			// 90度 (0.5*PI) = ユルメ
			// 180度 (PI) = 常用最大
			// 225度 (1.25*PI) = 非常
			double ebThreshold = 1.125 * Math.PI;
			double ebHysteresisDelta = 0.125 * Math.PI * (double)(notchHysteresisNumericUpDown.Value / 100);
			if (currentBrakeKind == BrakeKind.BrakeAnalog)
			{
				if (prevBrakeKind == currentBrakeKind && prevBrakePressure.HasValue)
				{
					if (prevBrakePressure.Value > 405)
					{
						ebThreshold -= ebHysteresisDelta;
					}
					else
					{
						ebThreshold += ebHysteresisDelta;
					}
				}
				if (brakeAngle <= 0.5 * Math.PI)
				{
					currentBrakePressure = 0;
				}
				else if (brakeAngle < Math.PI)
				{
					currentBrakePressure = (float)(400 * (brakeAngle - 0.5 * Math.PI) / (0.5 * Math.PI));
				}
				else if (brakeAngle < ebThreshold)
				{
					currentBrakePressure = 400;
				}
				else
				{
					currentBrakePressure = 410;
				}
				// 読み取ったノッチを表示する
				if (currentBrakePressure.Value <= 0)
				{
					brakeNotchLabel.Text = uiText.OperationBrakeRelease;
				}
				else if (currentBrakePressure.Value > 405)
				{
					brakeNotchLabel.Text = "EB";
				}
				else
				{
					brakeNotchLabel.Text = string.Format("B-{0:#}kPa", currentBrakePressure.Value);
				}
			}
			else
			{
				// 0 1 2 3 4 5 6 7 8 ← ノッチ (BrakeKind.Brake7Steps の場合)
				//  0 1 2 3 4 5 6    ← 閾値配列 (常用→非常の閾値は別管理なので、ここには含めない)
				int numBrakeSteps =
					currentBrakeKind == BrakeKind.Brake7Steps ? 7 :
					currentBrakeKind == BrakeKind.Brake8Steps ? 8 : 6;
				double[] thresholds = new double[numBrakeSteps];
				for (int i = 0; i < numBrakeSteps; i++)
				{
					thresholds[i] = (0.5 + 0.5 * (i * 2 + 1) / (numBrakeSteps * 2)) * Math.PI;
				}
				if (prevBrakeKind == currentBrakeKind && prevBrakeNotch.HasValue)
				{
					double hysteresisDelta = 0.5 / (numBrakeSteps * 2) * Math.PI * (double)(notchHysteresisNumericUpDown.Value / 100);
					for (int i = 0; i < numBrakeSteps; i++)
					{
						if (i < prevBrakeNotch.Value)
						{
							thresholds[i] -= hysteresisDelta;
						}
						else
						{
							thresholds[i] += hysteresisDelta;
						}
					}
					if (prevBrakeNotch.Value <= numBrakeSteps)
					{
						ebThreshold += ebHysteresisDelta;
					}
					else
					{
						ebThreshold -= ebHysteresisDelta;
					}
				}
				// 閾値に基づいてノッチを読み取る
				if (brakeAngle >= ebThreshold)
				{
					currentBrakeNotch = numBrakeSteps + 1;
				}
				else
				{
					currentBrakeNotch = numBrakeSteps;
					for (int i = 0; i < numBrakeSteps; i++)
					{
						if (brakeAngle < thresholds[i])
						{
							currentBrakeNotch = i;
							break;
						}
					}
				}
				// 読み取ったノッチを表示する
				if (currentBrakeNotch.Value > numBrakeSteps)
				{
					brakeNotchLabel.Text = "EB";
				}
				else if (currentBrakeNotch.Value == 0)
				{
					brakeNotchLabel.Text = uiText.OperationBrakeRelease;
				}
				else
				{
					brakeNotchLabel.Text = string.Format("B{0}", currentBrakeNotch.Value);
				}
			}
		}
		else
		{
			brakeNotchLabel.Text = "-";
		}
		currentDeadmanIsActive = enableDeadmanCheckBox.Checked &&
			!(currentPowerNotch.HasValue && currentBrakeNotch.HasValue);

		// R1ボタン = 連絡ブザー
		currentBuzzer = (inputButtons & 0x0200) != 0;

		if (currentBrakeKind != prevBrakeKind || currentPowerNotch != prevPowerNotch ||
			(currentBrakeKind == BrakeKind.BrakeAnalog ?
				currentBrakePressure != prevBrakePressure :
				currentBrakeNotch != prevBrakeNotch))
		{
			if (currentPowerNotch.HasValue)
			{
				int power = currentPowerNotch.Value;
				if (currentBrakeKind == BrakeKind.Brake6Steps && power < 0) power = 0;
				TrainCrewInput.SetPowerNotch(power);
			}
			if (currentBrakeNotch.HasValue || currentBrakePressure.HasValue)
			{
				if (currentBrakeKind == BrakeKind.BrakeAnalog)
				{
					TrainCrewInput.SetBrakeSAP(currentBrakePressure.Value);
				}
				else
				{
					int brake = currentBrakeNotch.Value;
					if (currentBrakeKind == BrakeKind.Brake6Steps)
					{
						if (brake == 0)
						{
							if (currentPowerNotch.HasValue && currentPowerNotch < 0) brake = 1;
						}
						else
						{
							brake++;
						}
					}
					TrainCrewInput.SetBrakeNotch(brake);
				}
			}
		}
		if (currentDeadmanIsActive != prevDeadmanIsActive)
		{
			TrainCrewInput.SetDeadman(0, !currentDeadmanIsActive);
			TrainCrewInput.SetDeadman(1, !currentDeadmanIsActive);
		}
		if (currentBuzzer != prevBuzzer)
		{
			TrainCrewInput.SetButton(InputAction.Buzzer, currentBuzzer);
		}

		prevBrakeKind = currentBrakeKind;
		prevPowerNotch = currentPowerNotch;
		prevBrakeNotch = currentBrakeNotch;
		prevBrakePressure = currentBrakePressure;
		prevDeadmanIsActive = currentDeadmanIsActive;
		prevBuzzer = currentBuzzer;
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
