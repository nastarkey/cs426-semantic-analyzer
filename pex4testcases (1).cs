using System;

class Main {

  // ------------------------------------------------------------
  // Variable Declaration Test
  // ------------------------------------------------------------
  public static void Test1() {
    Console.WriteLine("Variable Declaration Test");

    // Declares a bunch of variables
    int    i  = 1;
    double f1 = -2.3;
    double f2 = 3.4e5;
    string s  = "hello world";

    // Print out the variables
    Console.WriteLine("  i = " + i);
    Console.WriteLine("  f1 = " + f1);
    Console.WriteLine("  f2 = " + f2);
    Console.WriteLine("  s = " + s);
    Console.WriteLine();
  }

  // ------------------------------------------------------------
  // Variable Assignment Test
  // ------------------------------------------------------------
  public static void Test2() {
    Console.WriteLine("Variable Assignment Test");

    int x = 0;
    double y = 1.2;
    string s = "original string";
    
    Console.WriteLine("  x (before): " + x);
    Console.WriteLine("  y (before): " + y);
    Console.WriteLine("  s (before): " + s);

    x = 12345;
    y = -6.789;
    s = "dog goes \"woof\", cat goes \"meow\", but what does the fox say \\sarcasm";

    Console.WriteLine("  x (after): " + x);
    Console.WriteLine("  y (after): " + y);
    Console.WriteLine("  s (after): " + s);
    Console.WriteLine();
  }

  // ------------------------------------------------------------
  // Math Operation Test
  // ------------------------------------------------------------ 
  public static void Test3() {
    Console.WriteLine("Math Operation Test");

    // Testing Integers
    int x = 5 - 4 + 4;
    x = x * 2 / 2;
    x = (x * x) * (5 - 5);
    Console.WriteLine("  x = " + x);

    // Testing Floats (don't worry if the rounding is a little off)
    double y = 10.44 + 9.79 - 10.115;
    y = -(y * (2.0 / 1.0));
    Console.WriteLine("  y = " + y);

    Console.WriteLine();
  }

  // ------------------------------------------------------------
  // Relational Operators
  // ------------------------------------------------------------ 
  public static void Test4() {
    Console.WriteLine("Relational Operator Test");

    if (5 > 2) {
      Console.WriteLine("  Greater than works");
    }

    if (2 < 5) {
      Console.WriteLine("  Less than works");
    }

    if (4 >= 4) {
      Console.WriteLine("  Greater than or equal works");
    }

    if (4 <= 4) {
      Console.WriteLine("  Less than or equal works");
    }

    if (5.0 == 5.0) {
      Console.WriteLine("  Equals works");
    }

    if (2.5 != 3.0) {
      Console.WriteLine("  Not Equals works");
    }

    Console.WriteLine();
  }
  
  // ------------------------------------------------------------
  // Boolean Operators
  // ------------------------------------------------------------ 
  public static void Test5() {
    Console.WriteLine("Boolean Operator Test");

    if (1 < 2 && 2 < 3) {
      Console.WriteLine("  And works");
    }

    if (2 < 1 || 2 < 2 || -3 > -10) {
      Console.WriteLine("  Or works");
    }

    if (!(2 < 1)) {
      Console.WriteLine("  Not works");
    }

    Console.WriteLine();
  }

  // ------------------------------------------------------------
  // Control Structures:  If Statements
  // ------------------------------------------------------------ 
  public static void Test6() {
    Console.WriteLine("If Statement Test");

    if (1.0 + 1.0 < 3.0) {
      Console.WriteLine("  If (true) Statement Passes");
    }
    else {
      Console.WriteLine("  If (true) Statement Fails");
    }

    if (5.0 * 3.0 < 1) {
      Console.WriteLine("  If (false) Statement Fails");
    }
    else {
      Console.WriteLine("  If (false) Statement Passes");
    }

    Console.WriteLine();

    // Testing to See if Function Calls within a Function Work
    Test7();
  }

  // ------------------------------------------------------------
  // Control Structures:  Nested If Statements
  // ------------------------------------------------------------
  public static void Test7() {
    Console.WriteLine("Nested If Statement Test");
    
    if (3 > 2) {
      if (5 < 4) {
        Console.WriteLine("  Nested If Statement Fails");
      }
      else {
        Console.WriteLine("  Nested If Statement Passes");
      }
    }

    Console.WriteLine();
  }

  // ------------------------------------------------------------
  // Control Structures:  While Statements
  // ------------------------------------------------------------ 
  public static void Test8() {
    Console.WriteLine("While Loop Test");
    
    int i = 0;

    while (i < 10) {
      Console.WriteLine("  " + i);
      i = i + 1;
    }

    Console.WriteLine("  Loop Complete");
    Console.WriteLine();

    // Testing to See if Function Calls within a Function Work
    Test9();
  }

  // ------------------------------------------------------------
  // Control Structures:  Nested While Statements
  // ------------------------------------------------------------ 
  public static void Test9() {
    Console.WriteLine("Nested While Loop Test");
    
    int x = 0;
    int y = 0;

    while (y < 3) {
      while (x < 3) {
        Console.WriteLine("  " + x + ", " + y);
        x = x + 1;
      }
      x = 0;
      y = y + 1;
    }

    Console.WriteLine("  Loop Complete");
    Console.WriteLine();
  }

  // ------------------------------------------------------------
  // Main Program
  // ------------------------------------------------------------
  public static void Main (string[] args) {
    Test1();
    Test2();
    Test3();
    Test4();
    Test5();
    Test6();
    // Test 7 Is Called in Test6()
    Test8();
    // Test 9 is Called in Test8()
  }
}