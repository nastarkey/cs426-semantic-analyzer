.assembly extern mscorlib {}
.assembly funprogram
{
	.ver 1:0:1:0
}
.method static void main() cil managed
{
	.maxstack 128
	.entrypoint

	// Main Code Goes Here
	// Declaring Variable x 
	.locals init(int32 x)

	ldc.i4 5
	stloc x

	ldloc x
	ldc.i4 7
	add
	call void [mscorlib]System.Console::Write(int32)
	ret
}

