class JapaneseUIText: UIText
{
	public override string ControllerSelect { get { return "コントローラー選択"; }}
	public override string ControllerDisconnected { get { return "切断"; }}

	public override string BrakeSelect { get { return "ブレーキ設定"; }}
	public override string BrakeAuto { get { return "自動"; }}
	public override string Brake6Step { get { return "6段 (一般)"; }}
	public override string Brake7Step { get { return "7段 (4000/4000R形)"; }}
	public override string Brake8Step { get { return "8段 (3020形)"; }}
	public override string BrakeAnalog { get { return "アナログ (3020形)"; }}
	public override string Brake6StepForAuto { get { return "6段"; }}
	public override string Brake7StepForAuto { get { return "7段"; }}
	public override string Brake8StepForAuto { get { return "8段"; }}
	public override string BrakeAnalogForAuto { get { return "アナログ"; }}

	public override string OperationStatus { get { return "操作状態"; }}
	public override string OperationPower { get { return "力行"; }}
	public override string OperationPowerHoldingWithoutNumber { get { return "抑速"; }}
	public override string OperationPowerHolding { get { return "抑速"; }}
	public override string OperationBrake { get { return "ブレーキ"; }}
	public override string OperationBrakeRelease { get { return "ユルメ"; }}
	public override string OperationLeftTrigger { get { return "左トリガー"; }}
	public override string OperationRightTrigger { get { return "右トリガー"; }}

	public override string InputConfiguration { get { return "操作設定"; }}
	public override string InputStickThreshold { get { return "スティック閾値"; }}
	public override string InputTriggerThresholdShallow { get { return "トリガー閾値 (浅)"; }}
	public override string InputTriggerThresholdDeep { get { return "トリガー閾値 (深)"; }}
	public override string InputNotchHysteresis { get { return "ノッチヒステリシス"; }}
	public override string InputDeadman { get { return "デッドマン連携"; }}
}
