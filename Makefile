TARGET=Stick3020.exe
OPTIONS= \
	/target:winexe \
	/optimize+ \
	/warn:4 \
	/codepage:65001 \
	/reference:TrainCrewInput.dll

SOURCES= \
	Stick3020.cs

$(TARGET): $(SOURCES)
	csc /out:$@ $(OPTIONS) $^
