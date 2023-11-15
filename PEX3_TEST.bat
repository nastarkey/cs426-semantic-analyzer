:: Creates a Variable for the Output File
@SET file="pex_test_results.txt"
@ECHO OFF
:: Erases Everything Currently In the Output File
type NUL>%file%

:: ----------------------------------------
:: TITLE
:: ----------------------------------------
echo PEX TEST CASES (C1C Starkey) >> %file%

:: ----------------------------------------
:: GOOD EXAMPLES
:: ----------------------------------------
::echo Testing Identifiers >> %file%
::bin\Debug\ConsoleApplication.exe testcases\pex1\test1.txt >> %file%
::echo. >> %file%

echo Testing Actual Parameters >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\actual_params_correct.txt >> %file%
echo. >> %file%

echo Testing Arithmetic >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\arithmetic_correct.txt >> %file%
echo. >> %file%

echo Testing Assignment >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\assign_correct.txt >> %file%
echo. >> %file%

echo Testing Boolean >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\boolean_correct.txt >> %file%
echo. >> %file%

echo Testing Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\declaration_correct.txt >> %file%
echo. >> %file%

echo Testing Formal Parameters >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\formal_params_correct.txt >> %file%
echo. >> %file%

echo Testing Functions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\functions_correct.txt >> %file%
echo. >> %file%

echo Testing If Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\if_correct.txt >> %file%
echo. >> %file%

echo Testing Main Function >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\main_correct.txt >> %file%
echo. >> %file%

echo Testing While Loops >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\while_correct.txt >> %file%
echo. >> %file%
:: ----------------------------------------
:: BAD EXAMPLES
:: ----------------------------------------
::echo Running Incorrect Test Cases >> %file%
::echo. >> %file%

echo Running Inccorect Test Cases >> %file%
echo. >> %file%
echo Testing Actual Parameters >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\actual_params_incorrect.txt >> %file%
echo. >> %file%

echo Testing Arithmetic >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\arithmetic_incorrect.txt >> %file%
echo. >> %file%

echo Testing Assignment >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\assign_incorrect.txt >> %file%
echo. >> %file%

echo Testing Boolean >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\boolean_incorrect.txt >> %file%
echo. >> %file%

echo Testing Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\declaration_incorrect.txt >> %file%
echo. >> %file%

echo Testing Formal Parameters >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\formal_params_incorrect.txt >> %file%
echo. >> %file%

echo Testing Functions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\functions_incorrect.txt >> %file%
echo. >> %file%

echo Testing If Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\if_incorrect.txt >> %file%
echo. >> %file%

echo Testing Main Function >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\main_incorrect.txt >> %file%
echo. >> %file%

echo Testing While Loops >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\while_incorrect.txt >> %file%
echo. >> %file%

pause