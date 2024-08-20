abstract class UIText
{
	public abstract string ControllerSelect { get; }
	public abstract string ControllerDisconnected { get; }

	public abstract string BrakeSelect { get; }
	public abstract string BrakeAuto { get; }
	public abstract string Brake6Step { get; }
	public abstract string Brake7Step { get; }
	public abstract string Brake8Step { get; }
	public abstract string BrakeAnalog { get; }
	public abstract string Brake6StepForAuto { get; }
	public abstract string Brake7StepForAuto { get; }
	public abstract string Brake8StepForAuto { get; }
	public abstract string BrakeAnalogForAuto { get; }

	public abstract string OperationStatus { get; }
	public abstract string OperationPower { get; }
	public abstract string OperationPowerHoldingWithoutNumber { get; }
	public abstract string OperationPowerHolding { get; }
	public abstract string OperationBrake { get; }
	public abstract string OperationBrakeRelease { get; }
	public abstract string OperationLeftTrigger { get; }
	public abstract string OperationRightTrigger { get; }

	public abstract string InputConfiguration { get; }
	public abstract string InputStickThreshold { get; }
	public abstract string InputTriggerThresholdShallow { get; }
	public abstract string InputTriggerThresholdDeep { get; }
	public abstract string InputNotchHysteresis { get; }
	public abstract string InputDeadman { get; }
}
