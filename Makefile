TARGET=Stick3020.exe
OPTIONS= \
	/target:winexe \
	/optimize+ \
	/warn:4 \
	/codepage:65001 \
	/win32icon:s3.ico \
	/reference:TrainCrewInput.dll

SOURCES= \
	Stick3020.cs \
	XInputReader.cs \
	RegistryIO.cs \
	UItext.cs \
	JapaneseUIText.cs \
	EnglishUIText.cs

$(TARGET): $(SOURCES)
	csc /out:$@ $(OPTIONS) $^
