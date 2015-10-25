namespace csharp Limbo.Net

enum VariableType{
	BOOL = 1,
	BYTE = 2,
	INT = 3,
	DOUBLE = 4,
	STR = 5,
	BYTEARR = 6,
	MAP = 7,
	LIST = 8,
	SET = 9,
}

struct BaseMsg{
	1:required VariableType msgType,
	2:optional bool boolVal,
	3:optional byte byteVal,
	4:optional i32 intVal,
	5:optional double doubleVal,
	6:optional string strVal,
	7:optional binary byteArrVal,
	8:optional map<BaseMsg,BaseMsg> mapVal,
	9:optional list<BaseMsg> listVal,
	10:optional set<BaseMsg> setVal,
}

struct NetMsg{
	1:required string MessageName,
	2:optional map<string,BaseMsg> MessageBody,
}
