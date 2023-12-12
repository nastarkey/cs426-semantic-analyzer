.assembly extern mscorlib {}
.assembly funprogram
{
	.ver 1:0:1:0
}
.method static void Test1() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "Variable Declaration Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable i 
	.locals init(int32 i)

	ldc.i4 1
	stloc i

	// Declaring Variable f1 
	.locals init(float32 f1)

	ldc.r8 2.3
	neg
	stloc f1

	// Declaring Variable f2 
	.locals init(float32 f2)

	ldc.r8 3.4e5
	stloc f2

	// Declaring Variable s 
	.locals init(string s)

	ldstr "hello world"
	stloc s

	ldstr "  i = "
	call void [mscorlib]System.Console::Write(string)
	ldloc i
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "  f1 = "
	call void [mscorlib]System.Console::Write(string)
	ldloc f1
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "  f2 = "
	call void [mscorlib]System.Console::Write(string)
	ldloc f2
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "  s = "
	call void [mscorlib]System.Console::Write(string)
	ldloc s
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.method static void Test2() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "Variable Assignment Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable x 
	.locals init(int32 x)

	ldc.i4 0
	stloc x

	// Declaring Variable y 
	.locals init(float32 y)

	ldc.r8 1.2
	stloc y

	// Declaring Variable s 
	.locals init(string s)

	ldstr "original string"
	stloc s

	ldstr "  x (before): "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "  y (before): "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "  s (before): "
	call void [mscorlib]System.Console::Write(string)
	ldloc s
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 12345
	stloc x

	ldc.r8 6.789
	neg
	stloc y

	ldstr "dog goes \"woof\", cat goes \"meow\", but what does the fox say \\sarcasm"
	stloc s

	ldstr "  x (after): "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "  y (after): "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "  s (after): "
	call void [mscorlib]System.Console::Write(string)
	ldloc s
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.method static void Test3() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "Math Operation Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable x 
	.locals init(int32 x)

	ldc.i4 5
	ldc.i4 4
	sub
	ldc.i4 4
	add
	stloc x

	ldloc x
	ldc.i4 2
	mul
	ldc.i4 2
	div
	stloc x

	ldloc x
	ldloc x
	mul
	ldc.i4 5
	ldc.i4 5
	sub
	mul
	stloc x

	ldstr "  x = "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable y 
	.locals init(float32 y)

	ldc.r8 10.44
	ldc.r8 9.79
	add
	ldc.r8 10.115
	sub
	stloc y

	ldloc y
	ldc.r8 2.0
	ldc.r8 1.0
	div
	mul
	neg
	stloc y

	ldstr "  y = "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.method static void Test4() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "Relational Operator Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 5
	ldc.i4 2
	bgt label2
	ldc.i4 0
	br label3
label2:
	ldc.i4 1
label3:
	brtrue label0
	br label1
label0:
	ldstr "  Greater than works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label1:
	ldc.i4 2
	ldc.i4 5
	blt label6
	ldc.i4 0
	br label7
label6:
	ldc.i4 1
label7:
	brtrue label4
	br label5
label4:
	ldstr "  Less than works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label5:
	ldc.i4 4
	ldc.i4 4
	bge label10
	ldc.i4 0
	br label11
label10:
	ldc.i4 1
label11:
	brtrue label8
	br label9
label8:
	ldstr "  Greater than or equal works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label9:
	ldc.i4 4
	ldc.i4 4
	ble label14
	ldc.i4 0
	br label15
label14:
	ldc.i4 1
label15:
	brtrue label12
	br label13
label12:
	ldstr "  Less than or equal works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label13:
	ldc.r8 5.0
	ldc.r8 5.0
	beq label18
	ldc.i4 0
	br label19
label18:
	ldc.i4 1
label19:
	brtrue label16
	br label17
label16:
	ldstr "  Equals works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label17:
	ldc.r8 2.5
	ldc.r8 3.0
	bne.un label22
	ldc.i4 0
	br label23
label22:
	ldc.i4 1
label23:
	brtrue label20
	br label21
label20:
	ldstr "  Not Equals works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label21:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.method static void Test5() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "Boolean Operator Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 1
	ldc.i4 2
	blt label26
	ldc.i4 0
	br label27
label26:
	ldc.i4 1
label27:
	ldc.i4 2
	ldc.i4 3
	blt label28
	ldc.i4 0
	br label29
label28:
	ldc.i4 1
label29:
	and
	brtrue label24
	br label25
label24:
	ldstr "  and works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label25:
	ldc.i4 2
	ldc.i4 1
	blt label32
	ldc.i4 0
	br label33
label32:
	ldc.i4 1
label33:
	ldc.i4 2
	ldc.i4 2
	blt label34
	ldc.i4 0
	br label35
label34:
	ldc.i4 1
label35:
	or
	ldc.i4 3
	neg
	ldc.i4 10
	neg
	bgt label36
	ldc.i4 0
	br label37
label36:
	ldc.i4 1
label37:
	or
	brtrue label30
	br label31
label30:
	ldstr "  Or works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label31:
	ldc.i4 2
	ldc.i4 1
	blt label40
	ldc.i4 0
	br label41
label40:
	ldc.i4 1
label41:
	ldc.i4 0
	ceq
	brtrue label38
	br label39
label38:
	ldstr "  Not works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label39:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.method static void Test6() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "If Statement Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.r8 1.0
	ldc.r8 1.0
	add
	ldc.r8 3.0
	blt label45
	ldc.i4 0
	br label46
label45:
	ldc.i4 1
label46:
	brtrue label42
	br label43
label42:
	ldstr "  If (true) Statement Passes"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br label44
label43:
	ldstr "  If (true) Statement Fails"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label44:
	ldc.r8 5.0
	ldc.r8 3.0
	mul
	ldc.r8 1.0
	blt label50
	ldc.i4 0
	br label51
label50:
	ldc.i4 1
label51:
	brtrue label47
	br label48
label47:
	ldstr "  If (false) Statement Fails"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br label49
label48:
	ldstr "  If (false) Statement Passes"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label49:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	call void Test7()
	ret
}

.method static void Test7() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "Nested If Statement Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 3
	ldc.i4 2
	bgt label54
	ldc.i4 0
	br label55
label54:
	ldc.i4 1
label55:
	brtrue label52
	br label53
label52:
	ldc.i4 5
	ldc.i4 4
	blt label59
	ldc.i4 0
	br label60
label59:
	ldc.i4 1
label60:
	brtrue label56
	br label57
label56:
	ldstr "  Nested If Statement Fails"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br label58
label57:
	ldstr "  Nested If Statement Passes"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
label58:
label53:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.method static void Test8() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "While Loop Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable i 
	.locals init(int32 i)

	ldc.i4 0
	stloc i

label61:
	ldloc i
	ldc.i4 10
	blt label63
	ldc.i4 0
	br label64
label63:
	ldc.i4 1
label64:
	brfalse label62
	ldstr "  "
	call void [mscorlib]System.Console::Write(string)
	ldloc i
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldloc i
	ldc.i4 1
	add
	stloc i

br label61
label62:
	ldstr "  Loop Complete"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	call void Test9()
	ret
}

.method static void Test9() cil managed
{
	.maxstack 128
	// Function Code Goes Here
	ldstr "Nested While Loop Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable x 
	.locals init(int32 x)

	ldc.i4 0
	stloc x

	// Declaring Variable y 
	.locals init(int32 y)

	ldc.i4 0
	stloc y

label65:
	ldloc y
	ldc.i4 3
	blt label67
	ldc.i4 0
	br label68
label67:
	ldc.i4 1
label68:
	brfalse label66
label69:
	ldloc x
	ldc.i4 3
	blt label71
	ldc.i4 0
	br label72
label71:
	ldc.i4 1
label72:
	brfalse label70
	ldstr "  "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr ", "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	ldc.i4 1
	add
	stloc x

br label69
label70:
	ldc.i4 0
	stloc x

	ldloc y
	ldc.i4 1
	add
	stloc y

br label65
label66:
	ldstr "  Loop Complete"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.method static void main() cil managed
{
	.maxstack 128
	.entrypoint

	// Main Code Goes Here
	call void Test1()
	call void Test2()
	call void Test3()
	call void Test4()
	call void Test5()
	call void Test6()
	call void Test8()
	ret
}

