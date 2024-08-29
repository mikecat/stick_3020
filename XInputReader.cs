using System;
using System.Runtime.InteropServices;

class XInputReader
{
	public const ushort XINPUT_GAMEPAD_LEFT_SHOULDER = 0x0100;
	public const ushort XINPUT_GAMEPAD_RIGHT_SHOULDER = 0x0200;

	private const uint ERROR_SUCCESS = 0;

	[StructLayout(LayoutKind.Sequential)]
	public struct XInputGamepad
	{
		public ushort wButtons;
		public byte bLeftTrigger;
		public byte bRightTrigger;
		public short sThumbLX;
		public short sThumbLY;
		public short sThumbRX;
		public short sThumbRY;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct XInputState
	{
		public uint dwPacketNumber;
		public XInputGamepad Gamepad;
	}

	[DllImport("Xinput1_4.dll")]
	private static extern uint XInputGetState(uint dwUserIndex, ref XInputState pState);

	public static XInputGamepad? Read(int controllerId)
	{
		if (controllerId < 0 || 3 < controllerId)
		{
			throw new Exception("invalid controller ID");
		}
		XInputState state = new XInputState();
		uint ret = XInputGetState((uint)controllerId, ref state);
		if (ret != ERROR_SUCCESS) return null;
		return state.Gamepad;
	}
}
