class EnglishUIText: UIText
{
	public override string ControllerSelect { get { return "Controller Select"; }}
	public override string ControllerDisconnected { get { return "disconnected"; }}

	public override string BrakeSelect { get { return "Brake Type Select"; }}
	public override string BrakeAuto { get { return "Auto"; }}
	public override string Brake6Step { get { return "6-Step (general)"; }}
	public override string Brake7Step { get { return "7-Step (model 4000/4000R)"; }}
	public override string Brake8Step { get { return "8-Step (model 3020)"; }}
	public override string BrakeAnalog { get { return "Analog (model 3020)"; }}
	public override string Brake6StepForAuto { get { return "6-Step"; }}
	public override string Brake7StepForAuto { get { return "7-Step"; }}
	public override string Brake8StepForAuto { get { return "8-Step"; }}
	public override string BrakeAnalogForAuto { get { return "Analog"; }}

	public override string OperationStatus { get { return "Operation Status"; }}
	public override string OperationPower { get { return "Power"; }}
	public override string OperationPowerHoldingWithoutNumber { get { return "Holding"; }}
	public override string OperationPowerHolding { get { return "HB"; }}
	public override string OperationBrake { get { return "Brake"; }}
	public override string OperationBrakeRelease { get { return "Release"; }}
	public override string OperationLeftTrigger { get { return "Left Trigger"; }}
	public override string OperationRightTrigger { get { return "Right Trigger"; }}

	public override string InputConfiguration { get { return "Input Configuration"; }}
	public override string InputStickThreshold { get { return "Stick Threshold"; }}
	public override string InputTriggerThresholdShallow { get { return "Trigger Threshold 1"; }}
	public override string InputTriggerThresholdDeep { get { return "Trigger Threshold 2"; }}
	public override string InputNotchHysteresis { get { return "Notch Hysteresis"; }}
	public override string InputDeadman { get { return "Use Deadman"; }}
}
