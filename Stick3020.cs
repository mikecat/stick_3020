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

	private const int fontSize = 16, gridSize = 22;

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

		mainPanel = CreateControl<Panel>(this, 0, 0, 26.5f, 13.5f);
		this.ClientSize = mainPanel.Size;
		mainPanel.SuspendLayout();

		controllerSelectGroupBox = CreateControl<GroupBox>(mainPanel, 0.5f, 0.5f, 10, 5.5f);
		controllerSelectGroupBox.Text = "コントローラー選択";
		controllerSelectGroupBox.SuspendLayout();
		for (int i = 0; i < 4; i++)
		{
			controllerSelectRadioButtons[i] = CreateControl<RadioButton>(
				controllerSelectGroupBox, 0.5f, i + 1, 9, 1
			);
			controllerSelectRadioButtons[i].Text = i.ToString();
		}
		controllerSelectGroupBox.ResumeLayout();

		brakeSelectGroupBox = CreateControl<GroupBox>(mainPanel, 0.5f, 6.5f, 10, 6.5f);
		brakeSelectGroupBox.Text = "ブレーキ設定";
		brakeSelectGroupBox.SuspendLayout();
		brakeAutoRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 1, 9, 1);
		brakeAutoRadioButton.Text = "自動：6段";
		brake6StepRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 2, 9, 1);
		brake6StepRadioButton.Text = "6段 (一般)";
		brake7StepRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 3, 9, 1);
		brake7StepRadioButton.Text = "7段 (4000/4000R形)";
		brake8StepRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 4, 9, 1);
		brake8StepRadioButton.Text = "8段 (3020形)";
		brakeAnalogRadioButton = CreateControl<RadioButton>(brakeSelectGroupBox, 0.5f, 5, 9, 1);
		brakeAnalogRadioButton.Text = "アナログ (3020形)";
		brakeSelectGroupBox.ResumeLayout();

		operationStatusGroupBox = CreateControl<GroupBox>(mainPanel, 11, 0.5f, 15, 5.5f);
		operationStatusGroupBox.Text = "操作状態";
		operationStatusGroupBox.SuspendLayout();
		powerTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 1, 5, 1);
		powerTitleLabel.Text = "力行";
		powerNotchLabel = CreateControl<Label>(operationStatusGroupBox, 5.5f, 1, 5, 1);
		powerNotchLabel.Text = "抑速1";
		powerDepthLabel = CreateControl<Label>(operationStatusGroupBox, 10.5f, 1, 4, 1);
		powerDepthLabel.TextAlign = ContentAlignment.TopRight;
		powerDepthLabel.Text = "100%";
		brakeTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 2, 5, 1);
		brakeTitleLabel.Text = "ブレーキ";
		brakeNotchLabel = CreateControl<Label>(operationStatusGroupBox, 5.5f, 2, 5, 1);
		brakeNotchLabel.Text = "400kPa";
		brakeDepthLabel = CreateControl<Label>(operationStatusGroupBox, 10.5f, 2, 4, 1);
		brakeDepthLabel.TextAlign = ContentAlignment.TopRight;
		brakeDepthLabel.Text = "100%";
		leftTriggerTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 3, 10, 1);
		leftTriggerTitleLabel.Text = "左トリガー";
		leftTriggerDepthLabel = CreateControl<Label>(operationStatusGroupBox, 10.5f, 3, 4, 1);
		leftTriggerDepthLabel.TextAlign = ContentAlignment.TopRight;
		leftTriggerDepthLabel.Text = "0%";
		rightTriggerTitleLabel = CreateControl<Label>(operationStatusGroupBox, 0.5f, 4, 10, 1);
		rightTriggerTitleLabel.Text = "右トリガー";
		rightTriggerDepthLabel = CreateControl<Label>(operationStatusGroupBox, 10.5f, 4, 4, 1);
		rightTriggerDepthLabel.TextAlign = ContentAlignment.TopRight;
		rightTriggerDepthLabel.Text = "0%";
		operationStatusGroupBox.ResumeLayout();

		inputConfigurationGroupBox = CreateControl<GroupBox>(mainPanel, 11, 6.5f, 15, 6.5f);
		inputConfigurationGroupBox.Text = "操作設定";
		inputConfigurationGroupBox.SuspendLayout();
		stickThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 1, 9, 1);
		stickThresholdTitleLabel.Text = "スティック閾値";
		stickThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 9.5f, 1, 4, 1);
		stickThresholdNumericUpDown.Maximum = 100;
		stickThresholdNumericUpDown.Minimum = 0;
		stickThresholdNumericUpDown.Value = 70;
		stickThresholdNumericUpDown.Increment = 1;
		stickThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 13.5f, 1, 1, 1);
		stickThresholdUnitLabel.Text = "%";
		trigterShallowThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 2, 9, 1);
		trigterShallowThresholdTitleLabel.Text = "トリガー閾値 (浅)";
		trigterShallowThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 9.5f, 2, 4, 1);
		trigterShallowThresholdNumericUpDown.Maximum = 80;
		trigterShallowThresholdNumericUpDown.Minimum = 0;
		trigterShallowThresholdNumericUpDown.Value = 30;
		trigterShallowThresholdNumericUpDown.Increment = 1;
		trigterShallowThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 13.5f, 2, 1, 1);
		trigterShallowThresholdUnitLabel.Text = "%";
		trigterDeepThresholdTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 3, 9, 1);
		trigterDeepThresholdTitleLabel.Text = "トリガー閾値 (深)";
		trigterDeepThresholdNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 9.5f, 3, 4, 1);
		trigterDeepThresholdNumericUpDown.Maximum = 100;
		trigterDeepThresholdNumericUpDown.Minimum = 30;
		trigterDeepThresholdNumericUpDown.Value = 80;
		trigterDeepThresholdNumericUpDown.Increment = 1;
		trigterDeepThresholdUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 13.5f, 3, 1, 1);
		trigterDeepThresholdUnitLabel.Text = "%";
		notchHysteresisTitleLabel = CreateControl<Label>(inputConfigurationGroupBox, 0.5f, 4, 9, 1);
		notchHysteresisTitleLabel.Text = "ノッチヒステリシス";
		notchHysteresisNumericUpDown = CreateControl<NumericUpDown>(inputConfigurationGroupBox, 9.5f, 4, 4, 1);
		notchHysteresisNumericUpDown.Maximum = 100;
		notchHysteresisNumericUpDown.Minimum = 0;
		notchHysteresisNumericUpDown.Value = 20;
		notchHysteresisNumericUpDown.Increment = 1;
		notchHysteresisUnitLabel = CreateControl<Label>(inputConfigurationGroupBox, 13.5f, 4, 1, 1);
		notchHysteresisUnitLabel.Text = "%";
		enableDeadmanCheckBox = CreateControl<CheckBox>(inputConfigurationGroupBox, 0.5f, 5, 14, 1);
		enableDeadmanCheckBox.Text = "デッドマン連携";
		inputConfigurationGroupBox.ResumeLayout();

		mainPanel.ResumeLayout();
		ResumeLayout();
	}
}
